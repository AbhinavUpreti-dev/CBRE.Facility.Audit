using System;
using System.Reflection.Metadata;

namespace CBRE.FacilityManagement.Audit.Core.ElogBook
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
        public List<Action> Actions { get; set; } = new List<Action>();
    }
    public class DocumentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; } // Nullable for groups
        public DocumentGroup Parent { get; set; }
        public ICollection<DocumentGroup> SubGroups { get; set; }
        public ICollection<Documents> Documents { get; set; }
        public ICollection<DocumentGroupProperty> DocumentGroupProperties { get; set; }
        public int? MasterDocumentGroupId { get; set; } // Foreign key to MasterDocumentGroup
        public MasterDocumentGroup MasterDocumentGroup { get; set; } // Navigation property to MasterDocumentGroup
        public List<Action> Actions { get; set; } = new List<Action>();

        // Computed property for EffectiveName
        public string EffectiveName => !string.IsNullOrEmpty(Name) ? Name : MasterDocumentGroup?.Name;
    }

    public class MasterDocumentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMandatory { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public bool IsClientResponsible { get; set; }
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
        public string UniqueId { get; set; }
        public ICollection<ContractBuilding> ContractBuildings { get; set; }
    }
    public class Action
    {
        public int DocumentGroupId { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? RequiredDate { get; set; }
        public DateTimeOffset? ClosedDate { get; set; }
        public DocumentGroup DocumentGroup { get; set; }
    }


}
