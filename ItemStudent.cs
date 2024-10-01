namespace l1
{
	internal class ItemStudent
	{
		public const int Count = 5;
		public string StLastName { get; private set; }
		public string StFirstName { get; private set; }
		public int Grade { get; private set; }
		public int Classroom { get; private set; }
		public int Bus { get; private set; }

		public ItemStudent()
		{
			StLastName = string.Empty;
			StFirstName = string.Empty;
			Grade = 0;
			Classroom = 0;
			Bus = 0;
		}

		public ItemStudent(string stLastName, string stFirstName, int grade, int classroom, int bus)
		{
			StLastName = stLastName;
			StFirstName = stFirstName;
			Grade = grade;
			Classroom = classroom;
			Bus = bus;
		}

		public string ToConsolePrint()
		{
			return $"{StLastName} {StFirstName} | {Grade} | {Classroom} | {Bus}";
		}

		public static ItemStudent ParseItem(string[] sringsLine)
		{
			try
			{
				return new ItemStudent(sringsLine[0].Trim(), sringsLine[1].Trim(), int.Parse(sringsLine[2].Trim()), int.Parse(sringsLine[3].Trim()), int.Parse(sringsLine[4].Trim()));
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
