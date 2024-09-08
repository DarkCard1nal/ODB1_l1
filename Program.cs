using System.Text;

namespace l1
{
	public class Program
	{
		private static string _filePath = "students.txt";

		public static void Main(string[] args)
		{
			const string wrongCommand = "Error wrong command. Use -H[elp].";
			string? input;
			string[] values;
			int i;

			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			FileStorage.PrintMessageColor("Schoolsearch l1 by Shkilnyi V. CS31", ConsoleColor.Green);

			Console.WriteLine(ComandHelp());

			while (true)
			{
				input = Console.ReadLine();
				if (input == null) continue;

				values = input.Split([':'], 2);
				for (i = 0; i < values.Length; i++)
				{
					values[i] = values[i].Trim();
				}

				switch (values[0])
				{
					case null:
						continue;
					case "-Q":
					case "-Quit":
						return;
					case "-H":
					case "-Help":
						Console.WriteLine(ComandHelp());
						break;
					case "-S":
					case "-Student":
						if (values.Length < 1)
						{
							FileStorage.PrintMessageColor(wrongCommand, ConsoleColor.Red);
							break;
						}

						Console.WriteLine("StLastName StFirstName | Grade | Classroom | TLastName TFirstName");

						Console.WriteLine(ComandFindStudent(values[1]));
						break;
					case "-S B":
					case "-S Bus":
					case "-Student B":
					case "-Student Bus":
						if (values.Length < 1)
						{
							FileStorage.PrintMessageColor(wrongCommand, ConsoleColor.Red);
							break;
						}

						Console.WriteLine("StLastName StFirstName | Bus");

						Console.WriteLine(ComandFindStudent(values[1], true));
						break;
					case "-F":
					case "-File":
						if (values.Length < 1)
						{
							FileStorage.PrintMessageColor(wrongCommand, ConsoleColor.Red);
							break;
						}
						input = FileStorage.FileExistsWrite(values[1]);

						if (input != null)
						{
							FileStorage.PrintMessageColor("The new file was not accepted.\n" + input, ConsoleColor.Red);
						}
						else
						{
							_filePath = values[1];
							Console.WriteLine("New file accepted.");
						}
						break;
					default: 
						FileStorage.PrintMessageColor(wrongCommand, ConsoleColor.Red);
						break;
				}
			}
		}

		private static string ComandHelp()
		{
			return "Comand list:\n" +
					"-H[elp]\n" +
					"-S[tudent]: <lastName>\n" +
					"-S[tudent] B[us]: <lastName>\n" +
					"-F[ile]: <filePath>\n" +
					"-Q[uit]\n" +
					"Enter comand:\n";
		}

		private static string ComandFindStudent(string lastName, bool findBus = false)
		{
			ItemsList? items = FileStorage.DownloadDataTXT(_filePath);

			if (items == null)
			{
				return "Use -H[elp] or F[ile]: <filePath>";
			}

			List<Item> resultItems = items.GetListStudent(lastName);
			string result = "";

			if (resultItems.Count == 0)
			{
				result += "None\n";
			}
			else if (findBus)
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.Bus });

				foreach (var item in res)
				{
					result += $"{item.StLastName} {item.StFirstName} | {item.Bus}\n";
				}
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.Grade, item.Classroom, item.TLastName, item.TFirstName });

				foreach (var item in res)
				{
					result += $"{item.StLastName} {item.StFirstName} | {item.Grade} | {item.Classroom} | {item.TLastName} {item.TFirstName}\n";
				}
			}

			return result;
		}

	}
}

