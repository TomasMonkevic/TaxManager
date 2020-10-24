using System.IO;

namespace TaxManager.Service
{
    public interface IImportService
    {
        void ImportMunicipalities(StreamReader stream);
    }
}
