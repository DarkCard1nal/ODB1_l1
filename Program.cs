using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			int i, number;

			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;
			
			if (args.Length == 0)
			{
				FileStorage.PrintMessageColor(ComandAuthor(), ConsoleColor.Green);
				Console.WriteLine(ComandHelp());
			}
			else
			{

			}

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
					case "-A":
					case "-Author":
						FileStorage.PrintMessageColor(ComandAuthor(), ConsoleColor.Green);
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
					case "-T":
					case "-Teacher":
						if (values.Length < 1)
						{
							FileStorage.PrintMessageColor(wrongCommand, ConsoleColor.Red);
							break;
						}

						Console.WriteLine("StLastName StFirstName | TLastName TFirstName");

						Console.WriteLine(ComandFindTeacher(values[1]));
						break;
					case "-C":
					case "-Classroom":
						if (values.Length < 1 || !int.TryParse(values[1], out number))
						{
							FileStorage.PrintMessageColor(wrongCommand, ConsoleColor.Red);
							break;
						}

						Console.WriteLine("StLastName StFirstName");

						Console.WriteLine(ComandFindClassroom(number));
						break;
					case "-B":
					case "-Bus":
						if (values.Length < 1 || !int.TryParse(values[1], out number))
						{
							FileStorage.PrintMessageColor(wrongCommand, ConsoleColor.Red);
							break;
						}

						Console.WriteLine("StLastName StFirstName | Grade | Classroom");

						Console.WriteLine(ComandFindBus(number));
						break;
					default:
						FileStorage.PrintMessageColor(wrongCommand, ConsoleColor.Red);
						break;
				}
			}
		}

		private static string ComandAuthor()
		{
			return "Schoolsearch l1 by Shkilnyi V. CS31";
		}

		private static string ComandHelp()
		{
			return "Comand list:\n" +
					"-H[elp]\n" +
					"-A[uthor]\n" +
					"-B[us]: <Number>\n" +
					"-C[lassroom]: <Number>\n" +
					"-F[ile]: <filePath>\n" +
					"-S[tudent]: <lastName>\n" +
					"-S[tudent] B[us]: <lastName>\n" +
					"-T[eacher]: <lastname>\n" +
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
			int count = 0;
			string result = "";
			Stopwatch stopwatch = new();
			
			stopwatch.Start();

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

				count = res.Count();
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.Grade, item.Classroom, item.TLastName, item.TFirstName });

				foreach (var item in res)
				{
					result += $"{item.StLastName} {item.StFirstName} | {item.Grade} | {item.Classroom} | {item.TLastName} {item.TFirstName}\n";
				}

				count = res.Count();
			}

			stopwatch.Stop();

			items.Clear();

			result += $"Operation complete, found {count} match. Time spent {stopwatch.ElapsedMilliseconds} ms.\n";

			return result;
		}

		private static string ComandFindTeacher(string lastName)
		{
			ItemsList? items = FileStorage.DownloadDataTXT(_filePath);

			if (items == null)
			{
				return "Use -H[elp] or F[ile]: <filePath>";
			}

			List<Item> resultItems = items.GetListTeacher(lastName);
			string result = "";
			int count = 0;
			Stopwatch stopwatch = new();
			
			stopwatch.Start();

			if (resultItems.Count == 0)
			{
				result += "None\n";
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.TLastName, item.TFirstName });

				foreach (var item in res)
				{
					result += $"{item.StLastName} {item.StFirstName} | {item.TLastName} {item.TFirstName}\n";
				}

				count = res.Count();
			}

			stopwatch.Stop();

			result += $"Operation complete, found {count} match. Time spent {stopwatch.ElapsedMilliseconds} ms.\n";

			items.Clear();

			return result;
		}

		private static string ComandFindClassroom(int number)
		{
			ItemsList? items = FileStorage.DownloadDataTXT(_filePath);

			if (items == null)
			{
				return "Use -H[elp] or F[ile]: <filePath>";
			}

			List<Item> resultItems = items.GetListClassroom(number);
			string result = "";
			int count = 0;
			Stopwatch stopwatch = new();

			stopwatch.Start();

			if (resultItems.Count == 0)
			{
				result += "None\n";
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName});

				foreach (var item in res)
				{
					result += $"{item.StLastName} {item.StFirstName}\n";
				}

				count = res.Count();
			}

			stopwatch.Stop();

			result += $"Operation complete, found {count} match. Time spent {stopwatch.ElapsedMilliseconds} ms.\n";

			items.Clear();

			return result;
		}

		private static string ComandFindBus(int number)
		{
			ItemsList? items = FileStorage.DownloadDataTXT(_filePath);

			if (items == null)
			{
				return "Use -H[elp] or F[ile]: <filePath>";
			}

			List<Item> resultItems = items.GetListBus(number);
			string result = "";
			int count = 0;
			Stopwatch stopwatch = new();

			stopwatch.Start();

			if (resultItems.Count == 0)
			{
				result += "None\n";
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.Grade, item.Classroom });

				foreach (var item in res)
				{
					result += $"{item.StLastName} {item.StFirstName} | {item.Grade} | {item.Classroom}\n";
				}

				count = res.Count();
			}

			stopwatch.Stop();

			result += $"Operation complete, found {count} match. Time spent {stopwatch.ElapsedMilliseconds} ms.\n";

			items.Clear();

			return result;
		}
	}
}

