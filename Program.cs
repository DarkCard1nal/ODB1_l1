using System.Diagnostics;
using System.Text;

namespace l1
{
	public class Program
	{
		private static string _filePath = "students.txt";
		private static string _resFile = "result.csv";
		private static bool _autoclosing = true;
		private static bool _saveResult = false;
		private static int _skipLine = 0;

		public static void Main(string[] args)
		{
			string? input;
			int i;

			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			if (args.Length == 0)
			{
				PrintMessageColor(ComandAuthor(), ConsoleColor.Green);
				Console.WriteLine(ComandHelp());

				do
				{
					input = Console.ReadLine();
				} while (ComandSwitch(input));
			}
			else
			{
				for (i = 0; i < args.Length; i++)
				{
					input = args[i];
					if (i + 1 < args.Length && !args[i + 1].Trim().StartsWith('-'))
					{
						input += " " + args[++i];
					}

					if (!ComandSwitch(input))
					{
						break;
					}
				}
			}

			if (!_autoclosing)
			{
				Console.WriteLine("Press the button to exit:");
				Console.ReadKey();
			}
		}

		private static bool ComandSwitch(string? input)
		{
			if (input == null) return true;

			const string wrongCommand = "Error wrong command. Use -H[elp].";
			int i, number, count;
			long time;
			bool b = true;
			string result;
			string[] values = input.Split([':'], 2);
			for (i = 0; i < values.Length; i++)
			{
				values[i] = values[i].Trim();
			}

			switch (values[0])
			{
				case null:
					break;
				case "-Q":
				case "-Quit":
					return false;
				case "-H":
				case "-Help":
					Console.WriteLine(ComandHelp());
					break;
				case "-A":
				case "-Author":
					PrintMessageColor(ComandAuthor(), ConsoleColor.Green);
					break;
				case "-Autoclosing":
					if (values.Length < 2 || !bool.TryParse(values[1], out b))
					{
						Console.WriteLine($"Autoclosing: {_autoclosing}");
						break;
					}
					else
					{
						_autoclosing = b;
						Console.WriteLine("Сommand is complete.");
					}
					break;
				case "-Saveresult":
					if (values.Length < 2 || !bool.TryParse(values[1], out b))
					{
						Console.WriteLine($"Saveresult: {_saveResult}");
						break;
					}
					else
					{
						_saveResult = b;
						Console.WriteLine("Сommand is complete.");
					}
					break;
				case "-Skipline":
					if (values.Length < 2 || !int.TryParse(values[1], out i) || i < 0)
					{
						Console.WriteLine($"Skipline: {_skipLine}");
						break;
					}
					else
					{
						_skipLine = i;
						Console.WriteLine("Сommand is complete.");
					}
					break;
				case "-S":
				case "-Student":
					if (values.Length < 2)
					{
						PrintMessageColor(wrongCommand, ConsoleColor.Red);
						break;
					}

					result = ComandFindStudent(values[1], out count, out time);

					if (_saveResult)
					{
						result = "StLastName, StFirstName, Grade, Classroom, TLastName, TFirstName\n" + result;
						FileStorage.UploadDataTXT(_resFile, result);
					}
					else
					{
						Console.WriteLine("StLastName StFirstName | Grade | Classroom | TLastName TFirstName");
						Console.WriteLine(result);
					}
					
					Console.WriteLine($"Operation complete, found {count} match. Time spent {time} ms.\n");
					break;
				case "-SB":
				case "-SBus":
				case "-StudentB":
				case "-StudentBus":
					if (values.Length < 2)
					{
						PrintMessageColor(wrongCommand, ConsoleColor.Red);
						break;
					}

					result = ComandFindStudent(values[1], out count, out time, true);

					if (_saveResult)
					{
						result = "StLastName, StFirstName, Bus\n" + result;
						FileStorage.UploadDataTXT(_resFile, result);
					}
					else
					{
						Console.WriteLine("StLastName StFirstName | Bus");
						Console.WriteLine(result);
					}

					Console.WriteLine($"Operation complete, found {count} match. Time spent {time} ms.\n");
					break;
				case "-F":
				case "-File":
					if (values.Length < 2)
					{
						Console.WriteLine($"File: {_filePath}");
						break;
					}
					input = FileStorage.FileExistsWrite(values[1]);

					if (input != null)
					{
						PrintMessageColor("The new file was not accepted.\n" + input, ConsoleColor.Red);
					}
					else
					{
						_filePath = values[1];
						Console.WriteLine("New file accepted.");
					}
					break;
				case "-T":
				case "-Teacher":
					if (values.Length < 2)
					{
						PrintMessageColor(wrongCommand, ConsoleColor.Red);
						break;
					}

					result = ComandFindTeacher(values[1], out count, out time);

					if (_saveResult)
					{
						result = "StLastName, StFirstName, TLastName, TFirstName\n" + result;
						FileStorage.UploadDataTXT(_resFile, result);
					}
					else
					{
						Console.WriteLine("StLastName StFirstName | TLastName TFirstName");
						Console.WriteLine(result);
					}

					Console.WriteLine($"Operation complete, found {count} match. Time spent {time} ms.\n");
					break;
				case "-C":
				case "-Classroom":
					if (values.Length < 1 || !int.TryParse(values[1], out number))
					{
						PrintMessageColor(wrongCommand, ConsoleColor.Red);
						break;
					}

					result = ComandFindClassroom(number, out count, out time);

					if (_saveResult)
					{
						result = "StLastName, StFirstName\n" + result;
						FileStorage.UploadDataTXT(_resFile, result);
					}
					else
					{
						Console.WriteLine("StLastName StFirstName");
						Console.WriteLine(result);
					}

					Console.WriteLine($"Operation complete, found {count} match. Time spent {time} ms.\n");
					break;
				case "-B":
				case "-Bus":
					if (values.Length < 1 || !int.TryParse(values[1], out number))
					{
						PrintMessageColor(wrongCommand, ConsoleColor.Red);
						break;
					}

					result = ComandFindBus(number, out count, out time);

					if (_saveResult)
					{
						result = "StLastName, StFirstName, Grade, Classroom\"\n" + result;
						FileStorage.UploadDataTXT(_resFile, result);
					}
					else
					{
						Console.WriteLine("StLastName StFirstName | Grade | Classroom");
						Console.WriteLine(result);
					}

					Console.WriteLine($"Operation complete, found {count} match. Time spent {time} ms.\n");
					break;
				default:
					PrintMessageColor(wrongCommand, ConsoleColor.Red);
					break;
			}
			return true;
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
					"-Autoclosing: <true/false>\n" +
					"-B[us]: <number>\n" +
					"-C[lassroom]: <number>\n" +
					"-F[ile]: <filePath>\n" +
					"-Saveresult: <true/false>\n" +
					"-Skipline: <number>\n" +
					"-S[tudent]: <lastName>\n" +
					"-S[tudent]B[us]: <lastName>\n" +
					"-T[eacher]: <lastname>\n" +
					"-Q[uit]\n" +
					"Enter comand:\n";
		}

		private static string ComandFindStudent(string lastName, out int count, out long time, bool findBus = false)
		{
			ItemsList? items = FileStorage.DownloadDataTXT(_filePath, _skipLine);
			count = 0;
			time = 0;

			if (items == null)
			{
				return "Use -H[elp] or F[ile]: <filePath>";
			}

			List<Item> resultItems = items.GetListStudent(lastName);
			
			string result = "";
			Stopwatch stopwatch = new();

			stopwatch.Start();

			if (resultItems.Count == 0 && !_saveResult)
			{
				result += "None\n";
			}
			else if (findBus)
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.Bus });

				if (_saveResult)
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName}, {item.StFirstName}, {item.Bus}\n";
					}
				}
				else
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName}, {item.StFirstName}, {item.Bus}\n";
					}
				}

				count = res.Count();
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.Grade, item.Classroom, item.TLastName, item.TFirstName });

				if (_saveResult)
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName}, {item.StFirstName}, {item.Grade}, {item.Classroom}, {item.TLastName}, {item.TFirstName}\n";
					}
				}
				else
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName} {item.StFirstName} | {item.Grade} | {item.Classroom} | {item.TLastName} {item.TFirstName}\n";
					}
				}

				count = res.Count();
			}

			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			items.Clear();

			return result;
		}

		private static string ComandFindTeacher(string lastName, out int count, out long time)
		{
			ItemsList? items = FileStorage.DownloadDataTXT(_filePath, _skipLine);
			count = 0;
			time = 0;

			if (items == null)
			{
				return "Use -H[elp] or F[ile]: <filePath>";
			}

			List<Item> resultItems = items.GetListTeacher(lastName);
			string result = "";
			Stopwatch stopwatch = new();

			stopwatch.Start();

			if (resultItems.Count == 0 && !_saveResult)
			{
				result += "None\n";
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.TLastName, item.TFirstName });

				if (_saveResult)
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName}, {item.StFirstName}, {item.TLastName}, {item.TFirstName}\n";
					}
				}
				else
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName} {item.StFirstName} | {item.TLastName} {item.TFirstName}\n";
					}
				}

				count = res.Count();
			}

			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			items.Clear();

			return result;
		}

		private static string ComandFindClassroom(int number, out int count, out long time)
		{
			ItemsList? items = FileStorage.DownloadDataTXT(_filePath, _skipLine);
			count = 0;
			time = 0;

			if (items == null)
			{
				return "Use -H[elp] or F[ile]: <filePath>";
			}

			List<Item> resultItems = items.GetListClassroom(number);
			string result = "";
			Stopwatch stopwatch = new();

			stopwatch.Start();

			if (resultItems.Count == 0 && !_saveResult)
			{
				result += "None\n";
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName });

				if (_saveResult)
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName}, {item.StFirstName}\n";
					}
				}
				else
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName} {item.StFirstName}\n";
					}
				}

				count = res.Count();
			}

			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			items.Clear();

			return result;
		}

		private static string ComandFindBus(int number, out int count, out long time)
		{
			ItemsList? items = FileStorage.DownloadDataTXT(_filePath, _skipLine);
			count = 0;
			time = 0;

			if (items == null)
			{
				return "Use -H[elp] or F[ile]: <filePath>";
			}

			List<Item> resultItems = items.GetListBus(number);
			string result = "";
			Stopwatch stopwatch = new();

			stopwatch.Start();

			if (resultItems.Count == 0 && !_saveResult)
			{
				result += "None\n";
			}
			else
			{
				var res = resultItems.Select(item => new { item.StLastName, item.StFirstName, item.Grade, item.Classroom });

				if (_saveResult)
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName}, {item.StFirstName}, {item.Grade}, {item.Classroom}\n";
					}
				}
				else
				{
					foreach (var item in res)
					{
						result += $"{item.StLastName} {item.StFirstName} | {item.Grade} | {item.Classroom}\n";
					}
				}

				count = res.Count();
			}

			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			items.Clear();

			return result;
		}

		public static void PrintMessageColor(string message, ConsoleColor color)
		{
			ConsoleColor oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ForegroundColor = oldColor;
		}
	}
}

