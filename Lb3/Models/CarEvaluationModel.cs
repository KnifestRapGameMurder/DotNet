using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Lb3.Models
{
    public class CarEvaluationModel
    {
        public string Buying { get; set; }
        public string Maint { get; set; }
        public string Doors { get; set; }
        public string Persons { get; set; }
        public string LugBoot { get; set; }
        public string Safety { get; set; }
        
        [ValidateNever]
        public string PredictedEvaluation { get; set; }
    }
}
