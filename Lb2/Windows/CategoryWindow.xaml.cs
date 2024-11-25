using System.Windows;
using GameStore.Entities;

namespace Lb2;

public interface ICategoryWindow
{
    void LoadCategories();
    void AddCategory_Click(object sender, RoutedEventArgs e);
    void EditCategory_Click(object sender, RoutedEventArgs e);
    void SaveCategory_Click(object sender, RoutedEventArgs e);
    void DeleteCategory_Click(object sender, RoutedEventArgs e);
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
        CategoriesDataGrid.ItemsSource = _context.Categories.ToList();
    }

    private void AddCategory_Click(object sender, RoutedEventArgs e)
    {
        var newCategoryName = NewCategoryNameTextBox.Text.Trim();

        if (string.IsNullOrEmpty(newCategoryName))
        {
            MessageBox.Show("Category name cannot be empty.");
            return;
        }

        var newCategory = new Category { CategoryName = newCategoryName };
        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        LoadCategories();

        NewCategoryNameTextBox.Clear();
        MessageBox.Show("Category added.");
    }

    private void EditCategory_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as FrameworkElement;
        if (button?.Tag is not Category category) return;

        CategoriesDataGrid.BeginEdit(); // Активує режим редагування
    }

    private void SaveCategory_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as FrameworkElement;
        if (button?.Tag is not Category category) return;

        _context.Categories.Update(category);
        _context.SaveChanges();
        LoadCategories();
        MessageBox.Show("Category updated.");
    }

    private void DeleteCategory_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as FrameworkElement;
        if (button?.Tag is not Category category) return;

        var confirm = MessageBox.Show(
            $"Are you sure you want to delete the category '{category.CategoryName}'? All associated products will also be deleted.",
            "Confirm Delete",
            MessageBoxButton.YesNo);

        if (confirm == MessageBoxResult.Yes)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            LoadCategories();
            MessageBox.Show("Category and associated products deleted.");
        }
    }
}