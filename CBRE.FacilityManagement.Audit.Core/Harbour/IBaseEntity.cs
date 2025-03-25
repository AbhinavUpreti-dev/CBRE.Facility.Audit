using Newtonsoft.Json;

namespace CBRE.FacilityManagement.Audit.Core.Harbour
{
    public interface IBaseEntity
    {
        /// <summary>
        /// Gets or sets the entity which describes the type of document.
        /// </summary>
        /// <value>The bucket.</value>
        string Entity { get; set; }

        /// <summary>
        /// Gets or sets a partition key value.
        /// </summary>
        /// <value>The shard.</value>
        string Shard { get; set; }

        /// <summary>
        /// Gets or sets which CosmosDB container this entity should be projected to and read from
        /// </summary>
        /// <value>The shard.</value>
        string Container { get; set; }

        /// <summary>
        /// Gets or sets the created by user id.
        /// </summary>
        string CreatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the last updated by user.
        /// </summary>
        string LastUpdatedByUser { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        string LastUpdatedDate { get; set; }
    }

    /// <summary>
    /// The base entity.
    /// </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// The bucket
        /// </summary>
        private string entity;

        /// <summary>
        /// The shard
        /// </summary>
        private string shard;

        /// <summary>
        /// The container.
        /// </summary>
        private string container;

        /// <summary>
        /// Gets or sets the entity which describes the type of document.
        /// </summary>
        /// <value>The bucket.</value>
        public virtual string Entity
        {
            get => this.entity ?? this.GetType().Name;

            set => this.entity = value;
        }

        /// <summary>
        /// Gets or sets a partition key value.
        /// </summary>
        /// <value>The shard.</value>
        public virtual string Shard
        {
            get => this.shard ?? this.GetType().Name;

            set => this.shard = value;
        }

        /// <summary>
        /// Gets or sets the record identifier.
        /// </summary>
        /// <value>
        /// The record identifier.
        /// </value>
        public string RecordId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets which CosmosDB container this entity should be projected to and read from
        /// </summary>
        /// <value>The shard.</value>
        public virtual string Container
        {
            get => this.container ?? this.GetType().Name;

            set => this.container = value;
        }

        /// <summary>
        /// Gets or sets the created by user id.
        /// </summary>
        public string CreatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public virtual string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the last updated by user.
        /// </summary>
        public string LastUpdatedByUser { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public virtual string LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the local timezone of the user. To be passed from client side.
        /// </summary>
        public string Timezone { get; set; }
    }
}
