namespace Shared.Models.Deliverables.Responses
{
    public class DeliverableResponseStyles
    {
        public double IconWidth { get; set; } = 35; // Ancho mínimo para ID
        public double IdWidth { get; set; } = 50; // Ancho mínimo para ID
        public double WbsWidth { get; set; } = 100; // Ancho mínimo para WBS
        public double NameWidth { get; set; } = 250; // Ancho mínimo para Name
        public double StartDateWidth { get; set; } = 150; // Ancho mínimo para StartDate
        public double EndDateWidth { get; set; } = 150; // Ancho mínimo para EndDate
        public double DurationWidth { get; set; } = 100; // Ancho mínimo para Duration
        public double DependencesWidth { get; set; } = 100; // Ancho mínimo para sDependences
        public double DependencyTypeWidth { get; set; } = 150; // Ancho mínimo para DependencyType
        public double LagWidth { get; set; } = 50; // Ancho mínimo para Lag
        public double ActionsWidth { get; set; } = 130; // Ancho mínimo para Lag
        public double BudgetWidth { get; set; } = 100; // Ancho mínimo para Budget
        public double SumofWidth => ActionsWidth + LagWidth + DependencyTypeWidth + DependencesWidth + DurationWidth +
            EndDateWidth + StartDateWidth + NameWidth + WbsWidth + IdWidth + IconWidth+ BudgetWidth;
        public string GridStyle => $"grid-template-columns: {IconWidth / SumofWidth * 100}% " +
            $"{IdWidth / SumofWidth * 100}% {WbsWidth / SumofWidth * 100}% {NameWidth / SumofWidth * 100}% {StartDateWidth / SumofWidth * 100}% " +
            $"{EndDateWidth / SumofWidth * 100}% {DurationWidth / SumofWidth * 100}% {DependencesWidth / SumofWidth * 100}% " +
            $"{DependencyTypeWidth / SumofWidth * 100}% {LagWidth / SumofWidth * 100}% {BudgetWidth / SumofWidth * 100}% {ActionsWidth / SumofWidth * 100}%;  padding-left: 2px;";
        public string GridStyle2 => $"grid-template-columns: {IconWidth}px {IdWidth}px {WbsWidth}px {NameWidth}px {StartDateWidth}px {EndDateWidth}px {DurationWidth}px {DependencesWidth}px {DependencyTypeWidth}px {LagWidth}px {ActionsWidth}px; padding-left: 2px;";
    }
    public class DeliverableResponseStyles2
    {
        private readonly Dictionary<string, double> _columnWidths = new()
    {
        { "Icon", 35 },
        { "Id", 50 },
        { "Wbs", 100 },
        { "Name", 300 },
        { "StartDate", 120 },
        { "EndDate", 120 },
        { "Duration", 80 },
        { "Dependences", 80 },
        { "DependencyType", 100 },
        { "Lag", 50 },
        { "Budget", 100 },
        { "Actions", 140 }
    };

        public double SumOfWidth => _columnWidths.Values.Sum();

        public string GridStyle => $"grid-template-columns: " +
            string.Join(" ", _columnWidths.Values.Select(width => $"{width / SumOfWidth * 100}%")) + "; padding-left: 2px;";

      
    }
}
