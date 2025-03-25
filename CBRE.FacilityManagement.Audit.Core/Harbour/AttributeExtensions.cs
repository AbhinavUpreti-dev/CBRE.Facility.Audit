namespace CBRE.FacilityManagement.Audit.Core.Harbour
{
    public static class AttributeExtensions
    {
        /// <summary>
        /// Get Attribute Value
        /// </summary>
        /// <typeparam name="TAttribute">
        /// Attribute
        /// </typeparam>
        /// <typeparam name="TValue">
        /// Value
        /// </typeparam>
        /// <param name="type">
        /// Type
        /// </param>
        /// <param name="valueSelector">
        /// Value
        /// </param>
        /// <returns>
        /// The generic
        /// </returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }

            return default(TValue);
        }
    }
}
