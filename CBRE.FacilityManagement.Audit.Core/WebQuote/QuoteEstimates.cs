using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.WebQuote
{
    public class QuoteEstimate
    {
        public int Id { get; set; }
        public int QuoteTypeId { get; set; }
        public QuoteQuoteType QuoteType { get; set; }
        public int GroupId { get; set; }
        public CoreGroup Group { get; set; }
        public int SubGroupId { get; set; }
        public CoreSubGroup SubGroup { get; set; }
        public int StatusCodeId { get; set; }
        public QuoteEstimateStatus Status { get; set; }
        public int ClientId { get; set; }
        public CoreClient Client { get; set; }
        public int ContractId { get; set; }
        public CoreContract Contract { get; set; }
        public int ContractLocationId { get; set; }
        public CoreContractLocation ContractLocation { get; set; }
        public int RequestId { get; set; }
        public QuoteRequest Request { get; set; }
        public int CurrencyId { get; set; }
        public CoreCurrency Currency { get; set; }
        public int WorksCategoryId { get; set; }
        public QuoteWorksCategory WorksCategory { get; set; }
        public int InputByStaffId { get; set; }
        public CoreStaff InputByStaff { get; set; }
        public string Description { get; set; }
        public bool Closed { get; set; }
    }

    public class QuoteQuoteType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CoreGroup
    {
        public int Id { get; set; }
        public string GroupDescription { get; set; }
    }

    public class CoreSubGroup
    {
        public int Id { get; set; }
        public string SubGroupDescription { get; set; }
    }

    public class QuoteEstimateStatus
    {
        public int Id { get; set; }
        public bool ShowDescription { get; set; }
    }

    public class CoreClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CoreContract
    {
        public int Id { get; set; }
        public string Reference { get; set; }
    }

    public class CoreContractLocation
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public CoreLocation Location { get; set; }
    }

    public class CoreLocation
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class QuoteRequest
    {
        public int Id { get; set; }
        // Add other properties as needed
    }

    public class CoreCurrency
    {
        public int Id { get; set; }
        // Add other properties as needed
    }

    public class QuoteWorksCategory
    {
        public int Id { get; set; }
        // Add other properties as needed
    }

    public class CoreStaff
    {
        public int Id { get; set; }
        // Add other properties as needed
    }
}
