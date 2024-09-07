using System.Text;

namespace l1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			Item item = new Item("1", "2", 3, 4, 5, "6", "7");


			Console.WriteLine(item.ToConsolePrint());
		}
	}
}

