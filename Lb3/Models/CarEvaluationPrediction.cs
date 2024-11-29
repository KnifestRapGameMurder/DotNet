using Microsoft.ML.Data;

namespace Lb3.Models
{
    public class CarEvaluationPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedEvaluation { get; set; }
    }
}
