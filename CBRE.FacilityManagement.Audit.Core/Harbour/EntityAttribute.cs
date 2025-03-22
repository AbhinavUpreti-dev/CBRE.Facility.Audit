namespace CBRE.FacilityManagement.Audit.Core.Harbour
{
    /// <summary>
    /// The collection attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityAttribute : Attribute
    {
        public string Name { get; set; }

        public EntityAttribute(string name)
        {
            this.Name = name;
        }
    }
}
