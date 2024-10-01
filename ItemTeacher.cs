namespace l1
{
	internal class ItemTeacher
	{
		public const int Count = 3;
		public string TLastName { get; private set; }
		public string TFirstName { get; private set; }
		public int Classroom { get; private set; }

		public ItemTeacher()
		{
			TLastName = string.Empty;
			TFirstName = string.Empty;
			Classroom = 0;
		}

		public ItemTeacher(string tLastName, string tFirstName, int classroom)
		{
			TLastName = tLastName;
			TFirstName = tFirstName;
			Classroom = classroom;
		}

		public string ToConsolePrint()
		{
			return $"{TLastName} {TFirstName} | {Classroom}";
		}

		public static ItemTeacher ParseItem(string[] sringsLine)
		{
			try
			{
				return new ItemTeacher(sringsLine[0].Trim(), sringsLine[1].Trim(), int.Parse(sringsLine[2].Trim()));
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
