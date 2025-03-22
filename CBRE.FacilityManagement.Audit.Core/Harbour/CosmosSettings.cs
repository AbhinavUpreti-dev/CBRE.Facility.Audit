namespace CBRE.FacilityManagement.Audit.Core.Harbour
{
    public class CosmosSettings
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the service endpoint.
        /// </summary>
        public string ServiceEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the preferred CosmosDB geo-replicated region for write operations
        /// </summary>
        public string PreferredRegion { get; set; }

        /// <summary>
        /// Gets or sets the CosmosDbKey for the cosmos db
        /// </summary>
        public string CosmosDbKey { get; set; }
    }
}
