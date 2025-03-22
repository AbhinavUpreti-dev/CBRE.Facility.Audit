namespace CBRE.FacilityManagement.Audit.Core.Harbour
{
    /// <summary>
    /// The collection attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionAttribute : Attribute
    {
        public string Name { get; set; }

        public CollectionAttribute(string name)
        {
            this.Name = name;
        }
    }
}
