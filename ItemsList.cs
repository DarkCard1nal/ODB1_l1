namespace l1
{
	internal class ItemsList
	{
		private List<Item> _items;

		public ItemsList()
		{
			_items = [];
		}

		public ItemsList(List<Item> items)
		{
			_items = new List<Item>(items);
		}

		public void Add(Item item)
		{
			_items.Add(item);
		}

		public void Clear()
		{
			_items.Clear();
		}

		public Item GetItem(int index)
		{
			return _items[index];
		}

		public List<Item> GetListStudent(string stLastName)
		{
			return _items.Where(item => item.StLastName.Equals(stLastName, StringComparison.CurrentCultureIgnoreCase)).Select(item => item).ToList();
		}

		public List<Item> GetListTeacher(string tLastName)
		{
			return _items.Where(item => item.TLastName.Equals(tLastName, StringComparison.CurrentCultureIgnoreCase)).Select(item => item).ToList();
		}

		public List<Item> GetListClassroom(int classroom)
		{
			return _items.Where(item => item.Classroom == classroom).Select(item => item).ToList();
		}

		public List<Item> GetListBus(int bus)
		{
			return _items.Where(item => item.Bus == bus).Select(item => item).ToList();
		}
	}
}
