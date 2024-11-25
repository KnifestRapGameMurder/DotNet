using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GameStore.Entities;

namespace Lb2;

public interface IFilterSection
{
    event Action ApplyFilter;
    event Action ResetFilter;

    string ProductName { get; }
    int? SelectedCategoryId { get; }
    int? SelectedCurrencyId { get; }
    decimal? MinPrice { get; }
    decimal? MaxPrice { get; }

    ObservableCollection<Category> Categories { get; set; }
    ObservableCollection<Currency> Currencies { get; set; }

    void ClearFilters();
    void ApplyFilter_Click(object sender, EventArgs e);
    void ResetFilter_Click(object sender, EventArgs e);
}
    
public partial class FilterSection : UserControl
{
    // Події для передачі фільтрів до батьківського вікна
    public event Action ApplyFilter;
    public event Action ResetFilter;

    public FilterSection()
    {
        InitializeComponent();
    }

    // Метод для виклику події ApplyFilter
    private void ApplyFilter_Click(object sender, RoutedEventArgs e)
    {
        ApplyFilter?.Invoke();
    }

    // Метод для виклику події ResetFilter
    private void ResetFilter_Click(object sender, RoutedEventArgs e)
    {
        ResetFilter?.Invoke();
    }

    // Властивості для фільтрації
    public string ProductName => FilterNameTextBox.Text.Trim();

    public int? SelectedCategoryId
    {
        get => FilterCategoryComboBox.SelectedValue as int?;
    }

    public int? SelectedCurrencyId
    {
        get => FilterCurrencyComboBox.SelectedValue as int?;
    }

    public decimal? MinPrice
    {
        get
        {
            if (decimal.TryParse(FilterPriceMinTextBox.Text, out var minPrice))
                return minPrice;
            return null;
        }
    }

    public decimal? MaxPrice
    {
        get
        {
            if (decimal.TryParse(FilterPriceMaxTextBox.Text, out var maxPrice))
                return maxPrice;
            return null;
        }
    }

    public ObservableCollection<Category> Categories
    {
        get => (ObservableCollection<Category>)FilterCategoryComboBox.ItemsSource;
        set => FilterCategoryComboBox.ItemsSource = value;
    }

    public ObservableCollection<Currency> Currencies
    {
        get => (ObservableCollection<Currency>)FilterCurrencyComboBox.ItemsSource;
        set => FilterCurrencyComboBox.ItemsSource = value;
    }

    // Метод для скидання фільтрів
    public void ClearFilters()
    {
        FilterNameTextBox.Clear();
        FilterCategoryComboBox.SelectedIndex = -1;
        FilterCurrencyComboBox.SelectedIndex = -1;
        FilterPriceMinTextBox.Clear();
        FilterPriceMaxTextBox.Clear();
    }
}