using CBRE.FacilityManagement.Audit.Core.Common;
using CBRE.FacilityManagement.Audit.Core.Harbour;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq;

namespace CBRE.FacilityManagement.Audit.Persistence.CosmosDbRespository
{
    public class HarbourCosmosDbRepository : IRepository
    {
        /// <summary>
        /// The database name.
        /// </summary>
        private const string DatabaseName = "Aurora";

        /// <summary>
        /// CosmosClient.
        /// </summary>
        private readonly CosmosClient client;

        public HarbourCosmosDbRepository(CosmosClient client)
        {
            this.client = client;
        }

        public async Task<T> GetByIdAsync<T>(string id, string partitionKey, bool isUnpartitioned = false)
            where T : IBaseEntity
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(partitionKey))
            {
                throw new ArgumentNullException($"{nameof(id)} or {nameof(partitionKey)} are missing");
            }
            try
            {
                var collection = this.GetContainer<T>();
                PartitionKey partitionValue =
                                                 isUnpartitioned
                                                     ? PartitionKey.None
                                                     : new PartitionKey(partitionKey);

                T document = await collection.ReadItemAsync<T>(id, partitionValue);

                return (T)(dynamic)document;
            }
            catch (CosmosException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(T);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task UpsertAsync<T>(T entity)
            where T : IBaseEntity
        {
            var collection = this.GetContainer<T>();

            await collection.UpsertItemAsync(entity, new PartitionKey(entity.Shard));
        }

        public async Task DeleteAsync<T>(string id, string partitionKey, bool isUnpartitioned = false)
            where T : IBaseEntity
        {
            var collection = this.GetContainer<T>();
            PartitionKey partitionValue =
                                            isUnpartitioned
                                                ? new PartitionKey(null)
                                                : new PartitionKey(
            partitionKey);

            await collection.DeleteItemAsync<T>(id, partitionValue);
        }

        /// <inheritdoc />
        public async Task InsertAsync<T>(T entity)
            where T : IBaseEntity
        {

            var collection = this.GetContainer<T>();

            await collection.CreateItemAsync(entity, new PartitionKey(entity.Shard));

        }

        public async Task<CosmosResponse<T>> GetTokenizedAsync<T>(
            [Optional] Expression<Func<T, bool>> predicate,
            bool enableCrossPartitionQuery = false,
            int maxItemCount = -1,
            string partitionKey = null,
            string continuationToken = null,
            bool isUnpartitioned = false,
            Expression<Func<T, object>> orderByPredicated = null,
            bool orderByAsc = true)
            where T : IBaseEntity
        {
            if (predicate == null)
            {
                predicate = x => x.Entity == this.GetEntityType<T>();
            }

            QueryRequestOptions requestOptions = new QueryRequestOptions()
            {
                PartitionKey = !string.IsNullOrEmpty(partitionKey) ? new PartitionKey(partitionKey) : null,
                MaxItemCount = maxItemCount,
            };

            var collection = this.GetContainer<T>();

            var query = collection.GetItemLinqQueryable<T>(continuationToken: (string.IsNullOrEmpty(continuationToken) ? null : continuationToken), requestOptions: requestOptions).Where(predicate);

            if (orderByPredicated != null)
            {
                query = orderByAsc ?
                            query.OrderBy(orderByPredicated)
                            : query.OrderByDescending(orderByPredicated);
            }

            var builtQuery = query.ToFeedIterator();

            var results = new List<T>();
            double totalRus = 0;
            var result = await builtQuery.ReadNextAsync();
            results.AddRange(result);
            totalRus += result.RequestCharge;

            return new CosmosResponse<T>(result, result.ContinuationToken);
        }

        /// <inheritdoc />
        public async Task<int> GetCount<T>(
            [Optional] Expression<Func<T, bool>> predicate,
            bool enableCrossPartitionQuery = false,
            string partitionKey = null)
            where T : IBaseEntity
        {
            if (predicate == null)
                predicate = x => x.Entity == this.GetEntityType<T>();
            var collection = this.GetContainer<T>();

            QueryRequestOptions requestOptions = new QueryRequestOptions()
            {
                PartitionKey = !string.IsNullOrEmpty(partitionKey) ? new PartitionKey(partitionKey) : null,
            };


            var count = await collection.GetItemLinqQueryable<T>(requestOptions: requestOptions)
                .Where(predicate).CountAsync();

            return count;
        }

        public async Task<CustomQueryResponse<T2>> GetTokenizedAsync<T, T2>(
            string query,
            bool enableCrossPartitionQuery = false,
            int maxItemCount = -1,
            string partitionKey = null,
            string continuationToken = null,
            bool isUnpartitioned = false,
            bool orderByAsc = true)
            where T : IBaseEntity
            where T2 : class
        {
            QueryRequestOptions requestOptions = new QueryRequestOptions()
            {
                PartitionKey = !string.IsNullOrEmpty(partitionKey) ? new PartitionKey(partitionKey) : null,
                MaxItemCount = maxItemCount,
            };

            var collection = this.GetContainer<T>();

            var searchQuery = collection.GetItemQueryIterator<T2>(new QueryDefinition(query), (string.IsNullOrEmpty(continuationToken) ? null : continuationToken), requestOptions: requestOptions);

            double totalRus = 0;
            Microsoft.Azure.Cosmos.FeedResponse<T2> result = await searchQuery.ReadNextAsync();

            totalRus += result.RequestCharge;

            return new CustomQueryResponse<T2>(result, result.ContinuationToken);
        }

        private Container GetContainer<T>()
        {
            Database db = this.client.GetDatabase(DatabaseName);
            db.CreateContainerIfNotExistsAsync(new ContainerProperties() { PartitionKeyPath = "/Shard", Id = GetCollection<T>() }, throughput: 400).GetAwaiter().GetResult();
            return this.client.GetContainer(
                 DatabaseName,
                 GetCollection<T>()
                 );
        }

        /// <summary>
        /// The get collection.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetCollection<T>()
        {
            var retVal = typeof(T).GetAttributeValue((CollectionAttribute dna) => dna.Name);
            return retVal;
        }

        /// <summary>
        /// The get entity type.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetEntityType<T>()
        {
            return typeof(T).GetAttributeValue((EntityAttribute dna) => dna.Name);
        }

    }
}
