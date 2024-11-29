using System.Collections.Generic;

namespace Lb3.Helpers
{
    public static class ValueMapper
    {
        public static readonly Dictionary<string, string> BuyingDescriptions = new()
        {
            { "vhigh", "Дуже висока" },
            { "high", "Висока" },
            { "med", "Середня" },
            { "low", "Низька" }
        };

        public static readonly Dictionary<string, string> BuyingColors = new()
        {
            { "vhigh", "red" },
            { "high", "orange" },
            { "med", "yellow" },
            { "low", "green" }
        };

        public static readonly Dictionary<string, string> MaintDescriptions = new()
        {
            { "vhigh", "Дуже висока" },
            { "high", "Висока" },
            { "med", "Середня" },
            { "low", "Низька" }
        };

        public static readonly Dictionary<string, string> MaintColors = new()
        {
            { "vhigh", "red" },
            { "high", "orange" },
            { "med", "yellow" },
            { "low", "green" }
        };

        public static readonly Dictionary<string, string> DoorsDescriptions = new()
        {
            { "2", "2 двері" },
            { "3", "3 двері" },
            { "4", "4 двері" },
            { "5more", "5 і більше дверей" }
        };

        public static readonly Dictionary<string, string> PersonsDescriptions = new()
        {
            { "2", "2 людини" },
            { "4", "4 людини" },
            { "more", "Більше 4 осіб" }
        };

        public static readonly Dictionary<string, string> PersonsColors = new()
        {
            { "2", "red" },
            { "4", "orange" },
            { "more", "green" }
        };

        public static readonly Dictionary<string, string> LugBootDescriptions = new()
        {
            { "small", "Маленький" },
            { "med", "Середній" },
            { "big", "Великий" }
        };

        public static readonly Dictionary<string, string> LugBootColors = new()
        {
            { "small", "red" },
            { "med", "orange" },
            { "big", "green" }
        };

        public static readonly Dictionary<string, string> SafetyDescriptions = new()
        {
            { "low", "Низька" },
            { "med", "Середня" },
            { "high", "Висока" }
        };

        public static readonly Dictionary<string, string> SafetyColors = new()
        {
            { "low", "red" },
            { "med", "orange" },
            { "high", "green" }
        };

        public static readonly Dictionary<string, string> EvaluationDescriptions = new()
        {
            { "unacc", "Неприйнятний" },
            { "acc", "Прийнятний" },
            { "good", "Хороший" },
            { "vgood", "Дуже хороший" }
        };

        public static readonly Dictionary<string, string> EvaluationColors = new()
        {
            { "unacc", "red" },
            { "acc", "orange" },
            { "good", "yellow" },
            { "vgood", "green" }
        };
    }
}
