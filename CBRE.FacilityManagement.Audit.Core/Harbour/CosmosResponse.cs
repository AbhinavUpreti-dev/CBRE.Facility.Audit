namespace CBRE.FacilityManagement.Audit.Core.Harbour
{
    public class CosmosResponse<T> where T : IBaseEntity
    {

        public CosmosResponse(IEnumerable<T> results, string continuationToken)
        {
            this.Results = results;
            this.ContinuationToken = continuationToken;
        }

        public IEnumerable<T> Results { get; set; }
        public string ContinuationToken { get; set; }
    }
}
