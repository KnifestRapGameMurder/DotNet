using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameStore.Entities;

public class Product : INotifyPropertyChanged
{
    private int _productId;
    private string _name;
    private decimal _price;
    private int _categoryId;
    private int _currencyId;

    public int ProductId
    {
        get => _productId;
        set => SetProperty(ref _productId, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public decimal Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    public int CategoryId
    {
        get => _categoryId;
        set => SetProperty(ref _categoryId, value);
    }

    public int CurrencyId
    {
        get => _currencyId;
        set => SetProperty(ref _currencyId, value);
    }

    public Category Category { get; set; }
    public Currency Currency { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (!Equals(field, value))
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}