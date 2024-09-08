using System.Text;

namespace l1
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			FileStorage.PrintMessageColor("Schoolsearch l1 by Shkilnyi V. CS31", ConsoleColor.Green);


			Item item = new Item("1", "2", 3, 4, 5, "6", "7");


			Console.WriteLine(item.ToConsolePrint());
		}
	}
}

