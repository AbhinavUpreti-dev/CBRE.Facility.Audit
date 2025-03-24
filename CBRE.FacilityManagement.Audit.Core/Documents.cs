using System;
using System.Reflection.Metadata;

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
        public DocumentGroup DocumentGroup { get; set; }
    }
    public class DocumentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Documents> Documents { get; set; }
        public ICollection<DocumentGroupProperty> DocumentGroupProperties { get; set; }
    }

    public class DocumentGroupProperty
    {
        public int Id { get; set; }
        public int DocumentGroupId { get; set; }
        public DocumentGroup DocumentGroup { get; set; }
        public int BuildingId { get; set; }
        public ContractBuilding ContractBuilding { get; set; }
    }

    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; } // Foreign key to Customer
        public Customer Customer { get; set; } // Navigation property to Customer
        public ICollection<ContractBuilding> ContractBuildings { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }

    public class ContractBuilding
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        public ICollection<DocumentGroupProperty> DocumentGroupProperties { get; set; }
    }

    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ContractBuilding> ContractBuildings { get; set; }
    }

}
