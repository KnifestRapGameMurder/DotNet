using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Entities;

public class Currency : INotifyPropertyChanged
{
    public int CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public string Symbol { get; set; }

    private bool _isEditing;

    [NotMapped] // Ця властивість не буде включена до бази даних
    public bool IsEditing
    {
        get => _isEditing;
        set
        {
            _isEditing = value;
            OnPropertyChanged(nameof(IsEditing));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}