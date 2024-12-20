﻿using System.Windows;

namespace Lb2;

public interface IMainWindow
{
    void ManageProducts_Click(object sender, RoutedEventArgs e);
    void ManageCategories_Click(object sender, RoutedEventArgs e);
    void ManageCurrencies_Click(object sender, RoutedEventArgs e);
}
    
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ManageProducts_Click(object sender, RoutedEventArgs e)
    {
        var productWindow = new ProductsWindow();
        productWindow.Show();
    }

    private void ManageCategories_Click(object sender, RoutedEventArgs e)
    {
        var categoryWindow = new CategoryWindow();
        categoryWindow.Show();
    }

    private void ManageCurrencies_Click(object sender, RoutedEventArgs e)
    {
        var currencyWindow = new CurrencyWindow();
        currencyWindow.Show();
    }
}