namespace l1
{
	internal abstract class FileStorage
	{
		public static ItemsList? DownloadDataTXT(string filePath, int skipLine)
		{
			string? fleExistsWrite = FileExistsWrite(filePath);
			if (fleExistsWrite != null)
			{
				PrintMessageColor(fleExistsWrite, ConsoleColor.Red);
				return null;
			}
			
			ItemsList items = new();

			using (var reader = new StreamReader(filePath))
			{
				string? line;
				string[] values;
				string[]? tmp;
				while (!reader.EndOfStream)
				{
					line = reader.ReadLine();
					if (line == null || skipLine > 0)
					{
						skipLine--;
						continue;
					}

					values = line.Split(',');

					if (values.Length < Item.Count)
					{
						tmp = values;
						values = new string[Item.Count];
						Array.Fill(values, "0");
						Array.Copy(tmp, values, tmp.Length);

						tmp = null;
					}

					try
					{
						// Add new Item to ItemsList
						items.Add(new Item(values[0].Trim(), values[1].Trim(), int.Parse(values[2].Trim()), int.Parse(values[3].Trim()), int.Parse(values[4].Trim()), values[5].Trim(), values[6].Trim()));
					}
					catch (FormatException ex)
					{
						PrintMessageColor($"Error during string line processing: {line}. Error: {ex.Message}.", ConsoleColor.Red);

					}
				}
			}

			return items;
		}

		public static bool UploadDataTXT(string filePath, string text)
		{
			try
			{
				File.WriteAllText(filePath, text);
				return true;
			}
			catch (UnauthorizedAccessException ex)
			{
				PrintMessageColor($"Error: insufficient permissions to write to the file.\n{ex.Message}", ConsoleColor.Red);
			}
			catch (DirectoryNotFoundException ex)
			{
				PrintMessageColor($"Error: directory not found.\n{ex.Message}", ConsoleColor.Red);
			}
			catch (Exception ex)
			{
				PrintMessageColor($"Error.\n{ex.Message}", ConsoleColor.Red);
			}
			return false;
		}

		public static void PrintMessageColor(string message, ConsoleColor color)
		{
			ConsoleColor oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ForegroundColor = oldColor;
		}

		public static string? FileExistsWrite(string filePath)
		{
			if (File.Exists(filePath))
			{
				try
				{
					using (FileStream fs = File.OpenRead(filePath))
					{
						// The file exists and is open for reading
						return null;
					}
				}
				catch (UnauthorizedAccessException ex)
				{
					return $"Error The file {filePath} exists, but you do not have permissions to read it. Error: {ex.Message}.";
				}
				catch (IOException ex)
				{
					return $"Error The file {filePath} exists, but there was an error trying to open it. Error: {ex.Message}.";
				}
			}
			else
			{
				return $"The file {filePath} doesn't exist. Use -F[ile] to select an existing file.";
			}
		}
	}
}
