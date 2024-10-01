namespace l1
{
	internal abstract class FileStorage
	{
		public static ItemsList? DownloadDataListTXT(string fileStudentPath, string fileTeacherPath, int skipLine)
		{
			ItemsList items = new();

			DownloadDataTXT(fileStudentPath, skipLine, 
				ItemStudent.Count, (values) => items.AddStudent(ItemStudent.ParseItem(values)));
			DownloadDataTXT(fileTeacherPath, skipLine, 
				ItemTeacher.Count, (values) => items.AddTeacher(ItemTeacher.ParseItem(values)));

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
				Program.PrintMessageColor($"Error: insufficient permissions to write to the file.\n{ex.Message}",
					ConsoleColor.Red);
			}
			catch (DirectoryNotFoundException ex)
			{
				Program.PrintMessageColor($"Error: directory not found.\n{ex.Message}", ConsoleColor.Red);
			}
			catch (Exception ex)
			{
				Program.PrintMessageColor($"Error.\n{ex.Message}", ConsoleColor.Red);
			}
			return false;
		}
		
		public static string? FileExistsWrite(string filePath)
		{
			if (File.Exists(filePath))
			{
				try
				{
					using FileStream fs = File.OpenRead(filePath);
					// The file exists and is open for reading
					return null;
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

		private static bool DownloadDataTXT(string filePath, int skipLine, int count, Action<string[]> methodAdd)
		{
			string? fleExistsWrite = FileExistsWrite(filePath);
			if (fleExistsWrite != null)
			{
				Program.PrintMessageColor(fleExistsWrite, ConsoleColor.Red);
				return false;
			}

			using var reader = new StreamReader(filePath);
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

				if (values.Length < count)
				{
					tmp = values;
					values = new string[count];
					Array.Fill(values, "0");
					Array.Copy(tmp, values, tmp.Length);

					tmp = null;
				}

				try
				{
					// Add new Item to ItemsList
					methodAdd(values);
				}
				catch (FormatException ex)
				{
					Program.PrintMessageColor(
						$"Error {filePath} during string line processing: {line}. Error: {ex.Message}.",
						ConsoleColor.Red);
				}
			}

			return true;
		}
	}
}
