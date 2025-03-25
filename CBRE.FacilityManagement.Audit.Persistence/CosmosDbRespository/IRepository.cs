using CBRE.FacilityManagement.Audit.Core.Harbour;
using CBRE.FacilityManagement.Audit.Core.Common;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace CBRE.FacilityManagement.Audit.Persistence.CosmosDbRespository
{
    public interface IRepository
    {
        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="partitionKey">
        /// The partition key.
        /// </param>
        /// <param name="isUnpartitioned">
        /// The is Unpartitioned.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<T> GetByIdAsync<T>(string id, string partitionKey, bool isUnpartitioned = false)
            where T : IBaseEntity;


        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <param name="partitionKey">
        /// The partition Key.
        /// </param>
        /// <param name="isUnpartitioned">
        /// The isUnpartitioned.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task DeleteAsync<T>(string id, string partitionKey, bool isUnpartitioned = false)
            where T : IBaseEntity;

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="enableCrossPartitionQuery">
        /// The enable cross partition query.
        /// </param>
        /// <param name="maxItemCount">
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> GetCount<T>(
            [Optional] Expression<Func<T, bool>> predicate,
            bool enableCrossPartitionQuery = false,
            string partitionKey = null)
            where T : IBaseEntity;

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="enableCrossPartitionQuery">
        /// The enable cross partition query.
        /// </param>
        /// <param name="maxItemCount">
        /// The max item count.
        /// </param>
        /// <param name="partitionKey">
        /// The partition Key.
        /// </param>
        /// <param name="continuationToken">
        /// The continuation Token.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<CosmosResponse<T>> GetTokenizedAsync<T>(
            [Optional] Expression<Func<T, bool>> predicate,
            bool enableCrossPartitionQuery = false,
            int maxItemCount = -1,
            string partitionKey = null,
            string continuationToken = null,
            bool isUnpartitioned = false,
            Expression<Func<T, object>> orderByPredicated = null,
            bool orderByAsc = true)

            where T : IBaseEntity;

        /// <summary>
        /// The insert async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task InsertAsync<T>(T entity)
            where T : IBaseEntity;

        /// <summary>
        /// The update async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="partitionKey">
        /// The partition Key.
        /// </param>
        /// <typeparam name="T">
        /// The generic IBaseEntity parameter.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task UpsertAsync<T>(T entity)
            where T : IBaseEntity;


        /// <summary>The get async. based on provided query</summary>
		/// <param name="query">The query based on which data to fetch.</param>
		/// <param name="enableCrossPartitionQuery">The enable cross partition query.</param>
		/// <param name="maxItemCount">The max item count.</param>
		/// <param name="partitionKey">The partition Key.</param>
		/// <param name="continuationToken">The continuation Token.</param>
		/// <typeparam name="T"></typeparam><returns>
		/// The <see cref="Task"/>.</returns>
		Task<CustomQueryResponse<T2>> GetTokenizedAsync<T, T2>(
            string query,
            bool enableCrossPartitionQuery = false,
            int maxItemCount = -1,
            string partitionKey = null,
            string continuationToken = null,
            bool isUnpartitioned = false,
            bool orderByAsc = true)
            where T : IBaseEntity
            where T2 : class;
    }
}
