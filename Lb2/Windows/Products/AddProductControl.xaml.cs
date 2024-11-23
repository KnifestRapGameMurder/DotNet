using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GameStore.Entities;

namespace Lb2;

public partial class AddProductControl : UserControl
{
    public event Action<Product> ProductAdded;

    public ObservableCollection<Category> Categories
    {
        get => (ObservableCollection<Category>)CategoryComboBox.ItemsSource;
        set => CategoryComboBox.ItemsSource = value;
    }

    public ObservableCollection<Currency> Currencies
    {
        get => (ObservableCollection<Currency>)CurrencyComboBox.ItemsSource;
        set => CurrencyComboBox.ItemsSource = value;
    }

    public AddProductControl()
    {
        InitializeComponent();
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        var name = ProductNameTextBox.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Product name is required.");
            return;
        }

        if (!decimal.TryParse(ProductPriceTextBox.Text.Trim(), out var price))
        {
            MessageBox.Show("Invalid price format.");
            return;
        }

        if (CategoryComboBox.SelectedValue == null || CurrencyComboBox.SelectedValue == null)
        {
            MessageBox.Show("Please select a category and currency.");
            return;
        }

        var product = new Product
        {
            Name = name,
            Price = price,
            CategoryId = (int)CategoryComboBox.SelectedValue,
            CurrencyId = (int)CurrencyComboBox.SelectedValue
        };

        // Виклик події, щоб передати новий продукт батьківському компоненту
        ProductAdded?.Invoke(product);

        // Очищення полів після додавання
        ProductNameTextBox.Clear();
        ProductPriceTextBox.Clear();
        CategoryComboBox.SelectedIndex = -1;
        CurrencyComboBox.SelectedIndex = -1;
    }
}