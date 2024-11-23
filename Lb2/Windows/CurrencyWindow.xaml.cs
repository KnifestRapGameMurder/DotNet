using System.Linq;
using System.Windows;
using GameStore.Entities;

namespace Lb2
{
    public partial class CurrencyWindow : Window
    {
        private GameStoreContext _context;

        public CurrencyWindow()
        {
            InitializeComponent();
            _context = new GameStoreContext();
            LoadCurrencies();
        }

        // Завантаження списку валют
        private void LoadCurrencies()
        {
            var currencies = _context.Currencies.ToList();
            CurrenciesDataGrid.ItemsSource = currencies;
        }

        // Додавання нової валюти
        private void AddCurrency_Click(object sender, RoutedEventArgs e)
        {
            var newCurrencyName = NewCurrencyNameTextBox.Text.Trim();
            var newCurrencySymbol = NewCurrencySymbolTextBox.Text.Trim();

            if (string.IsNullOrEmpty(newCurrencyName) || string.IsNullOrEmpty(newCurrencySymbol))
            {
                MessageBox.Show("Both Currency Name and Symbol are required.");
                return;
            }

            var newCurrency = new Currency
            {
                CurrencyName = newCurrencyName,
                Symbol = newCurrencySymbol
            };

            _context.Currencies.Add(newCurrency);
            _context.SaveChanges();
            LoadCurrencies();

            NewCurrencyNameTextBox.Clear();
            NewCurrencySymbolTextBox.Clear();
            MessageBox.Show("Currency added.");
        }

        // Редагування валюти
        private void ToggleEditCurrency_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            var currency = button?.Tag as Currency;

            if (currency == null)
                return;

            if (currency.IsEditing)
            {
                // Зберігаємо зміни
                _context.SaveChanges();
                currency.IsEditing = false;
                LoadCurrencies();
                MessageBox.Show("Currency updated.");
            }
            else
            {
                // Увімкнути режим редагування
                currency.IsEditing = true;
            }
        }

        // Видалення валюти
        private void DeleteCurrency_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            var currency = button?.Tag as Currency;

            if (currency == null)
                return;

            _context.Currencies.Remove(currency);
            _context.SaveChanges();
            LoadCurrencies();
            MessageBox.Show("Currency deleted.");
        }
    }
}
