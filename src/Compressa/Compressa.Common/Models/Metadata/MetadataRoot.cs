using System.Text.Json.Serialization;

namespace Compressa.Models.Metadata
{
    public class MetadataRoot
    {
        public int Version { get; set; } = 1;
        public Audiobook Audiobook { get; set; }
    }
}