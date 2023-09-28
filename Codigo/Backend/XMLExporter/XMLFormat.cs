using ExportationModel.Exceptions;
using ExportationModel.ExportDomain;
using ExportationModel.Interfaces;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Xml.Serialization;

namespace XMLExporter
{
    public class XMLFormat : IFormat
    {
        public string GetFormatName()
        {
            return "XML exporter";
        }

        IEnumerable<ExportationModel.ExportDomain.Parameter> IFormat.GetParameters()
        {
            ExportationModel.ExportDomain.Parameter parameter = new ExportationModel.ExportDomain.Parameter()
            {
                InputName = "path",
                InputValue = ""
            };
            return new List<ExportationModel.ExportDomain.Parameter> { parameter };
        }

        public void Export(IEnumerable<DrugExportationModel> drugDtos, IEnumerable<ExportationModel.ExportDomain.Parameter> parameters)
        {
            string path = null;
            foreach (var parameter in parameters)
            {
                if (parameter.InputName == "path")
                    path = parameter.InputValue;
            }
            if (string.IsNullOrEmpty(path))
                throw new InvalidParameterException("The path to export XML file is incorrect.");

            // Serialize the drugDtos to XML
            var serializer = new XmlSerializer(typeof(List<DrugExportationModel>));
            using (var stream = new StreamWriter(path))
            {
                serializer.Serialize(stream, drugDtos.ToList());
            }
        }
    }
}
