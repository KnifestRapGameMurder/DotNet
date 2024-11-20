using System.Linq;
using System.Windows;
using GameStore.Entities;

namespace Lb2
{
    public partial class ProductWindow : Window
    {
        private readonly GameStoreContext _context;

        public ProductWindow()
        {
            InitializeComponent();
            _context = new GameStoreContext();
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _context.Products
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.Price,
                    Category = p.Category.CategoryName,
                    Currency = p.Currency.Symbol
                }).ToList();
            ProductsDataGrid.ItemsSource = products;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product
            {
                Name = "New Product",
                Price = 10,
                CategoryId = 1, // Замініть на вибір категорії
                CurrencyId = 1  // Замініть на вибір валюти
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            LoadProducts();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            // Реалізуйте редагування продукту
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct)
            {
                _context.Products.Remove(selectedProduct);
                _context.SaveChanges();
                LoadProducts();
            }
        }
    }
}