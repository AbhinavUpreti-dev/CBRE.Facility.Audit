using System;

namespace CBRE.FacilityManagement.Audit.Core
{
    public class Documents
    {
        public int Id { get; set; }
        public Guid? EntityId { get; set; }
        public int? DocumentGroupId { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string Extension { get; set; }
        public DateTimeOffset? Created { get; set; } // Nullable
        public int? CreatedById { get; set; }
        public DateTimeOffset? Modified { get; set; } // Nullable
        public int? ModifiedById { get; set; }
        public Guid? UniqueId { get; set; }
        public bool? IsActive { get; set; }
        public Guid? RowId { get; set; }
        public byte[] FileData { get; set; }
        public int? LevelTypeId { get; set; }
        public int? AssetId { get; set; }
        public string ScanResult { get; set; }
    }
}
