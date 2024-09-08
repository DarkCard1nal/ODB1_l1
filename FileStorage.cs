namespace l1
{
	internal abstract class FileStorage
	{
		public static ItemsList DownloadDataTXT(string filePath)
		{
			ItemsList items = new ItemsList();

			using (var reader = new StreamReader(filePath))
			{
				string? line;
				string[] values;
				string[]? tmp;
				while (!reader.EndOfStream)
				{
					line = reader.ReadLine();
					if (line == null) continue;

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
						Console.WriteLine($"Error during string line processing: {line}. Error: {ex.Message}");
					}
				}
			}

			return items;
		}
	}
}
