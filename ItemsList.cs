namespace l1
{
	internal class ItemsList
	{
		private List<ItemStudent> _itemsStudent;
		private List<ItemTeacher> _itemsTtudent;

		public ItemsList()
		{
			_itemsStudent = [];
			_itemsTtudent = [];
		}

		public ItemsList(List<ItemStudent> items)
		{
			_itemsStudent = new List<ItemStudent>(items);
		}

		public void Add(ItemStudent item)
		{
			_itemsStudent.Add(item);
		}

		public void Clear()
		{
			_itemsStudent.Clear();
		}

		public ItemStudent GetItem(int index)
		{
			return _itemsStudent[index];
		}

		public List<ItemStudent> GetListStudent(string stLastName)
		{
			return _itemsStudent.Where(item => item.StLastName.Equals(stLastName, StringComparison.CurrentCultureIgnoreCase)).Select(item => item).ToList();
		}

		public List<ItemStudent> GetListTeacher(string tLastName)
		{
			return _itemsStudent.Where(item => item.TLastName.Equals(tLastName, StringComparison.CurrentCultureIgnoreCase)).Select(item => item).ToList();
		}

		public List<ItemStudent> GetListClassroom(int classroom)
		{
			return _itemsStudent.Where(item => item.Classroom == classroom).Select(item => item).ToList();
		}

		public List<ItemStudent> GetListBus(int bus)
		{
			return _itemsStudent.Where(item => item.Bus == bus).Select(item => item).ToList();
		}
	}
}
