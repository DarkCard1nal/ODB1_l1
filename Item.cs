namespace l1
{
	internal class Item
	{
		public const int Count = 7;
		public string StLastName { get; private set; }
		public string StFirstName { get; private set; }
		public int Grade { get; private set; }
		public int Classroom { get; private set; }
		public int Bus { get; private set; }
		public string TLastName { get; private set; }
		public string TFirstName { get; private set; }

		public Item()
		{
			StLastName = string.Empty;
			StFirstName = string.Empty;
			Grade = 0;
			Classroom = 0;
			Bus = 0;
			TLastName = string.Empty;
			TFirstName = string.Empty;
		}

		public Item(string stLastName, string stFirstName, int grade, int classroom, int bus, string tLastName, string tFirstName)
		{
			StLastName = stLastName;
			StFirstName = stFirstName;
			Grade = grade;
			Classroom = classroom;
			Bus = bus;
			TLastName = tLastName;
			TFirstName = tFirstName;
		}

		public string ToConsolePrint()
		{
			return $"{StLastName} {StFirstName} | {Grade} | {Classroom} | {Bus} | {TLastName} {TFirstName}";
		}
	}
}
