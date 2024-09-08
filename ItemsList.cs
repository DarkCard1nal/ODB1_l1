namespace l1
{
	internal class ItemsList
	{
		private List<Item> _items;

		public ItemsList()
		{
			_items = new List<Item>();
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
			return _items.Where(item => item.StLastName.ToUpper() == stLastName.ToUpper()).Select(item => item).ToList();
		}
	}
}
