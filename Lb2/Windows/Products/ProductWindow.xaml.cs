using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lb2;

public interface IProductsWindow
{
    ObservableCollection<Category> Categories { get; set; }
    ObservableCollection<Currency> Currencies { get; set; }

    void LoadCategoriesAndCurrencies();
    void LoadProducts();
    void InitializeFilterSection();
    void UpdatePageInfo();
    void PreviousPage_Click(object sender, EventArgs e);
    void NextPage_Click(object sender, EventArgs e);
    void AddProduct_Click(object sender, EventArgs e);
    void ProductsDataGrid_CurrentCellChanged(object sender, EventArgs e);
    void DeleteProduct_Click(object sender, EventArgs e);
}
    
public partial class ProductsWindow : Window
{
    private readonly GameStoreContext _context;
    public ObservableCollection<Category> Categories { get; set; }
    public ObservableCollection<Currency> Currencies { get; set; }

    private int _currentPage = 1;
    private const int PageSize = 10;
    private int _totalPages;

    public ProductsWindow()
    {
        InitializeComponent();
        _context = new GameStoreContext();

        // Завантаження даних
        LoadCategoriesAndCurrencies();
        InitializeFilterSection();
        LoadProducts();
    }

    private void LoadCategoriesAndCurrencies()
    {
        Categories = new ObservableCollection<Category>(_context.Categories.ToList());
        Currencies = new ObservableCollection<Currency>(_context.Currencies.ToList());

        // Прив'язка до ComboBox
        NewProductCategoryComboBox.ItemsSource = Categories;
        NewProductCurrencyComboBox.ItemsSource = Currencies;
    }

    private void LoadProducts()
    {
        var totalProducts = _context.Products.Count();
        _totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);

        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Skip((_currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        ProductsDataGrid.ItemsSource = products;
        UpdatePageInfo();
    }

    private void InitializeFilterSection()
    {
        var filterSection = new FilterSection
        {
            Categories = Categories,
            Currencies = Currencies
        };

        filterSection.ApplyFilter += () =>
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Currency)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterSection.ProductName))
            {
                query = query.Where(p => p.Name.Contains(filterSection.ProductName));
            }

            if (filterSection.SelectedCategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filterSection.SelectedCategoryId.Value);
            }

            if (filterSection.SelectedCurrencyId.HasValue)
            {
                query = query.Where(p => p.CurrencyId == filterSection.SelectedCurrencyId.Value);
            }

            if (filterSection.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filterSection.MinPrice.Value);
            }

            if (filterSection.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filterSection.MaxPrice.Value);
            }

            ProductsDataGrid.ItemsSource = query
                .Skip((_currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            UpdatePageInfo();
        };

        filterSection.ResetFilter += () =>
        {
            filterSection.ClearFilters();
            LoadProducts();
        };

        FilterContainer.Content = filterSection;
    }

    private void UpdatePageInfo()
    {
        PageInfoTextBlock.Text = $"Page {_currentPage} of {_totalPages}";
    }

    private void PreviousPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentPage > 1)
        {
            _currentPage--;
            LoadProducts();
        }
    }

    private void NextPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentPage < _totalPages)
        {
            _currentPage++;
            LoadProducts();
        }
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        var name = NewProductNameTextBox.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Product name is required.");
            return;
        }

        if (!decimal.TryParse(NewProductPriceTextBox.Text.Trim(), out var price))
        {
            MessageBox.Show("Invalid price format.");
            return;
        }

        if (NewProductCategoryComboBox.SelectedValue == null || NewProductCurrencyComboBox.SelectedValue == null)
        {
            MessageBox.Show("Please select a category and currency.");
            return;
        }

        var product = new Product
        {
            Name = name,
            Price = price,
            CategoryId = (int)NewProductCategoryComboBox.SelectedValue,
            CurrencyId = (int)NewProductCurrencyComboBox.SelectedValue
        };

        _context.Products.Add(product);
        _context.SaveChanges();
        LoadProducts();
        MessageBox.Show("Product added.");
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