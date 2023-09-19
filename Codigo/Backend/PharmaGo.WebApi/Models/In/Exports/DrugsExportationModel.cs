using ExportationModel.ExportDomain;

namespace PharmaGo.WebApi.Models.In.Exports
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class DrugsExportationModel
    {
        public string FormatName { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}
