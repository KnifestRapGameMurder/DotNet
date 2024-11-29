using Microsoft.ML.Data;

public class Car
{
    [LoadColumn(0)] public string Buying { get; set; }
    [LoadColumn(1)] public string Maint { get; set; }
    [LoadColumn(2)] public string Doors { get; set; }
    [LoadColumn(3)] public string Persons { get; set; }
    [LoadColumn(4)] public string LugBoot { get; set; }
    [LoadColumn(5)] public string Safety { get; set; }
    [LoadColumn(6)] public string Evaluation { get; set; } // Цільова змінна для навчання
}
