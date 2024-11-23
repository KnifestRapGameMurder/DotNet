using System.Collections.ObjectModel;
using System.Windows;
using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lb2
{
    public partial class ProductsWindow : Window
    {
        private readonly GameStoreContext _context;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Currency> Currencies { get; set; }

        public ProductsWindow()
        {
            InitializeComponent();
            _context = new GameStoreContext();

            // Завантаження категорій і валют
            Categories = new ObservableCollection<Category>(_context.Categories.ToList());
            Currencies = new ObservableCollection<Currency>(_context.Currencies.ToList());

            // Передача категорій і валют у AddProductControl
            AddProductSection.Categories = Categories;
            AddProductSection.Currencies = Currencies;

            // Обробка події ProductAdded
            AddProductSection.ProductAdded += product =>
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                LoadProducts();
                MessageBox.Show("Product added.");
            };

            LoadProducts();
            InitializeFilterSection();
        }


        private void LoadData()
        {
            Categories = new ObservableCollection<Category>(_context.Categories.ToList());
            Currencies = new ObservableCollection<Currency>(_context.Currencies.ToList());

            // Завантаження продуктів у DataGrid
            LoadProducts();
        }


        private void LoadProducts()
        {
            ProductsDataGrid.ItemsSource = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Currency)
                .ToList();
        }

        private void InitializeFilterSection()
        {
            var filterSection = new FilterSection
            {
                Categories = Categories,  // Передача категорій
                Currencies = Currencies   // Передача валют
            };

            // Обробка подій
            filterSection.ApplyFilter += () =>
            {
                var query = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Currency)
                    .AsQueryable();

                if (filterSection.SelectedCategoryId.HasValue)
                    query = query.Where(p => p.CategoryId == filterSection.SelectedCategoryId.Value);

                if (filterSection.SelectedCurrencyId.HasValue)
                    query = query.Where(p => p.CurrencyId == filterSection.SelectedCurrencyId.Value);

                if (filterSection.MinPrice.HasValue)
                    query = query.Where(p => p.Price >= filterSection.MinPrice.Value);

                if (filterSection.MaxPrice.HasValue)
                    query = query.Where(p => p.Price <= filterSection.MaxPrice.Value);

                ProductsDataGrid.ItemsSource = query.ToList();
            };

            filterSection.ResetFilter += () =>
            {
                filterSection.ClearFilters();
                LoadProducts();
            };

            FilterContainer.Content = filterSection; // Додаємо `FilterSection` до відповідного контейнера
        }



        private void ProductsDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product editedProduct)
            {
                try
                {
                    _context.Products.Update(editedProduct);
                    _context.SaveChanges();

                    MessageBox.Show("Product updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating product: {ex.Message}");
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement button && button.Tag is Product product)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                LoadProducts();
                MessageBox.Show("Product deleted.");
            }
        }
    }
}