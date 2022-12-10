using System.Text.Json.Serialization;

namespace Compressa.API.Models.Metadata
{
    public class MetadataRoot
    {
        public int Version { get; set; } = 1;
        public Audiobook Audiobook { get; set; }
    }
}