using Compressa.Models.Metadata;

namespace Compressa.GUI.Services
{
    public interface ICompressaClientService
    {
        Task<MetadataRoot[]> GetAllMetadataAsync();
    }
}