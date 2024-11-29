using Microsoft.ML;
using Microsoft.ML.Data;
using Lb3.Models;
using System.IO;

namespace Lb3.Services
{
    public class CarEvaluationService
    {
        private readonly MLContext _mlContext;
        private readonly string _modelPath;
        private readonly string _dataPath;
        private ITransformer? _model;

        public CarEvaluationService(string modelPath, string dataPath)
        {
            _mlContext = new MLContext();
            _modelPath = modelPath;
            _dataPath = dataPath;

            if (File.Exists(_modelPath))
            {
                _model = _mlContext.Model.Load(_modelPath, out _);
            }
            else
            {
                _model = null;
            }
        }

        public bool IsModelAvailable()
        {
            return _model != null;
        }

        public string Predict(Car input)
        {
            if (_model == null)
            {
                return "Модель не знайдена. Спочатку натренуйте модель.";
            }

            // Створюємо PredictionEngine
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<Car, CarEvaluationPrediction>(_model);

            // Виконуємо передбачення
            var prediction = predictionEngine.Predict(input);

            return prediction.PredictedEvaluation;
        }

        public void TrainModel()
        {
            // Завантаження даних
            var data = _mlContext.Data.LoadFromTextFile<Car>(
                _dataPath,
                hasHeader: false,
                separatorChar: ',');

            // Розділення на тренувальний і тестовий набори
            var split = _mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
            var trainingData = split.TrainSet;
            var testData = split.TestSet;

            // Побудова конвеєра для тренування
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(Car.Evaluation)) // Перетворення цільової змінної
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(nameof(Car.Buying), outputKind: Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Indicator))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(nameof(Car.Maint), outputKind: Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Indicator))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(nameof(Car.Doors), outputKind: Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Indicator))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(nameof(Car.Persons), outputKind: Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Indicator))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(nameof(Car.LugBoot), outputKind: Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Indicator))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(nameof(Car.Safety), outputKind: Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Indicator))
                .Append(_mlContext.Transforms.Concatenate("Features", nameof(Car.Buying), nameof(Car.Maint), nameof(Car.Doors),
                    nameof(Car.Persons), nameof(Car.LugBoot), nameof(Car.Safety))) // Об'єднання всіх колонок у Features
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features")) // Тренування моделі
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel")); // Перетворення ключа назад у текст

            // Тренування моделі
            var model = pipeline.Fit(trainingData);

            // Оцінка моделі
            var predictions = model.Transform(testData);
            var metrics = _mlContext.MulticlassClassification.Evaluate(predictions, "Label", "Score");

            // Вивід метрик у консоль
            Console.WriteLine($"Log-loss: {metrics.LogLoss}");
            Console.WriteLine($"Macro Accuracy: {metrics.MacroAccuracy}");
            Console.WriteLine($"Micro Accuracy: {metrics.MicroAccuracy}");

            // Збереження моделі
            Directory.CreateDirectory(Path.GetDirectoryName(_modelPath)!);
            _mlContext.Model.Save(model, trainingData.Schema, _modelPath);

            Console.WriteLine("Модель успішно натренована та збережена!");

            // Завантаження моделі для використання
            _model = _mlContext.Model.Load(_modelPath, out _);
            Console.WriteLine("Модель успішно завантажена після тренування.");
        }
    }
}
