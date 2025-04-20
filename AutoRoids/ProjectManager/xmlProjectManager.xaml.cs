using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoRoids
{
    /// <summary>
    /// Interaction logic for xmlProjectManager.xaml
    /// </summary>
    public partial class xmlProjectManager : UserControl
    {
        public xmlProjectManager()
        {
            InitializeComponent(); 
            
            LoadTestData();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
        }
     
        private void LoadTestData()
        {
            var categories = new List<Category>
            {
                new Category { Name = "Category 1", Items = { new Item { Name = "Item 1", Description = "Description 1" }, new Item { Name = "Item 2", Description = "Description 2" } } },
                new Category { Name = "Category 2", Items = { new Item { Name = "Item 3", Description = "Description 3" }, new Item { Name = "Item 4", Description = "Description 4" } } }
            };

            treeView.ItemsSource = categories;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedCategory = treeView.SelectedItem as Category;
            if (selectedCategory != null)
            {
                listView.ItemsSource = selectedCategory.Items;
            }
        }

    }
}
