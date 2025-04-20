using System.Collections.Generic;

namespace AutoRoids
{
    public class CategoryTemplate
    {
        public string Name { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public override string ToString()
        {
            return Name;
        }
    }

    public class Category
    {
        public string Name { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}