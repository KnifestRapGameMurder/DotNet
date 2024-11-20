using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using GameStore.Entities;

namespace Lb2
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            return string.IsNullOrEmpty(text) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public partial class CategoryWindow : Window
    {
        private readonly GameStoreContext _context;

        public CategoryWindow()
        {
            InitializeComponent();
            _context = new GameStoreContext();
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = _context.Categories.ToList();
            CategoriesDataGrid.ItemsSource = categories;
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var newCategoryName = NewCategoryNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(newCategoryName))
            {
                MessageBox.Show("Category name cannot be empty.");
                return;
            }

            var newCategory = new Category
            {
                CategoryName = newCategoryName
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            LoadCategories();

            NewCategoryNameTextBox.Clear();
            MessageBox.Show("Category added.");
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            var category = button?.Tag as Category;

            if (category == null)
                return;

            CategoriesDataGrid.BeginEdit(); // Вмикає режим редагування
        }

        private void SaveCategory_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            var category = button?.Tag as Category;

            if (category == null)
                return;

            _context.SaveChanges(); // Зберігає зміни в базі даних
            CategoriesDataGrid.CommitEdit(); // Завершує редагування
            LoadCategories();
            MessageBox.Show("Category updated.");
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            var category = button?.Tag as Category;

            if (category == null)
                return;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            LoadCategories();
            MessageBox.Show("Category deleted.");
        }
    }
}
