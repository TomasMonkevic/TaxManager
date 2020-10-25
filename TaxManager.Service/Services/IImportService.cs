using System.IO;

namespace TaxManager.Service
{
    public interface IImportService
    {
        bool ImportMunicipalities(StreamReader stream);
    }
}
