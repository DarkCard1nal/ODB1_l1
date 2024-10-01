namespace l1
{
	internal class ItemsList
	{
		private List<ItemStudent> _itemsStudent;
		private List<ItemTeacher> _itemsTeacher;

		public ItemsList()
		{
			_itemsStudent = [];
			_itemsTeacher = [];
		}

		public ItemsList(List<ItemStudent> itemsStudent, List<ItemTeacher> itemsTeacher)
		{
			_itemsStudent = new List<ItemStudent>(itemsStudent);
			_itemsTeacher = new List<ItemTeacher>(itemsTeacher);
		}

		public void AddStudent(ItemStudent item)
		{
			_itemsStudent.Add(item);
		}

		public void AddTeacher(ItemTeacher item)
		{
			_itemsTeacher.Add(item);
		}

		public void ClearStudent()
		{
			_itemsStudent.Clear();
		}

		public void ClearTeacher()
		{
			_itemsTeacher.Clear();
		}

		public void Clear()
		{
			ClearStudent();
			ClearTeacher();
		}

		public ItemStudent GetItemStudent(int index)
		{
			return _itemsStudent[index];
		}

		public ItemTeacher GetItemTeacher(int index)
		{
			return _itemsTeacher[index];
		}

		public List<ItemStudent> GetListStudent(string stLastName)
		{
			return _itemsStudent.Where(item => item.StLastName.Equals(stLastName, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		public List<ItemTeacher> GetListTeacher(string tLastName)
		{
			return _itemsTeacher.Where(item => item.TLastName.Equals(tLastName, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		public ItemTeacher GetFirstTeacherClassroom(int classroom)
		{
			return _itemsTeacher.First(item => item.Classroom == classroom);
		}

		public List<ItemStudent> GetListClassroom(int classroom)
		{
			return _itemsStudent.Where(item => item.Classroom == classroom).ToList();
		}

		public List<ItemStudent> GetListBus(int bus)
		{
			return _itemsStudent.Where(item => item.Bus == bus).ToList();
		}
	}
}
