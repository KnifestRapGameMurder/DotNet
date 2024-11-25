using System.Windows;
using GameStore.Entities;

namespace Lb2;

public interface ICurrencyWindow
{
    void LoadCurrencies();
    void AddCurrency_Click(object sender, RoutedEventArgs e);
    void ToggleEditCurrency_Click(object sender, RoutedEventArgs e);
    void DeleteCurrency_Click(object sender, RoutedEventArgs e);
}
    
public partial class CurrencyWindow : Window
{
    private readonly GameStoreContext _context;

    public CurrencyWindow()
    {
        InitializeComponent();
        _context = new GameStoreContext();
        LoadCurrencies();
    }

    private void LoadCurrencies()
    {
        CurrenciesDataGrid.ItemsSource = _context.Currencies.ToList();
    }

    private void AddCurrency_Click(object sender, RoutedEventArgs e)
    {
        var name = NewCurrencyNameTextBox.Text.Trim();
        var symbol = NewCurrencySymbolTextBox.Text.Trim();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(symbol))
        {
            MessageBox.Show("Both name and symbol are required.");
            return;
        }

        var currency = new Currency { CurrencyName = name, Symbol = symbol };
        _context.Currencies.Add(currency);
        _context.SaveChanges();
        LoadCurrencies();

        NewCurrencyNameTextBox.Clear();
        NewCurrencySymbolTextBox.Clear();
        MessageBox.Show("Currency added.");
    }

    private void ToggleEditCurrency_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as FrameworkElement;
        if (button?.Tag is not Currency currency) return;

        if (currency.IsEditing)
        {
            // Зберігаємо зміни
            _context.Currencies.Update(currency);
            _context.SaveChanges();
            currency.IsEditing = false;
            LoadCurrencies();
            MessageBox.Show("Currency updated.");
        }
        else
        {
            // Активуємо режим редагування
            currency.IsEditing = true;
        }
    }

    private void DeleteCurrency_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as FrameworkElement;
        if (button?.Tag is not Currency currency) return;

        var confirm = MessageBox.Show(
            $"Are you sure you want to delete the currency '{currency.CurrencyName}'? All associated products will also be deleted.",
            "Confirm Delete",
            MessageBoxButton.YesNo);

        if (confirm == MessageBoxResult.Yes)
        {
            _context.Currencies.Remove(currency);
            _context.SaveChanges();
            LoadCurrencies();
            MessageBox.Show("Currency and associated products deleted.");
        }
    }
}