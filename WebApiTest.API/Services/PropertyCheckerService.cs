using System.Reflection;

namespace WebApiTest.API.Services
{
    public class PropertyCheckerService : IPropertyCheckerService
    {
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrEmpty(fields))
            {
                return true;
            }

            // The field are separated by ",", so we split it
            var fieldsAfterSplit = fields.Split(',');

            // Check if the requested fields exists on source
            foreach (var field in fieldsAfterSplit)
            {
                // trim each field, as it might contain leading 
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var.
                var propertyName = field.Trim();

                // use reflection to get the property on the source object
                // we need to include public and instance, b/c specifying a 
                // binding flag overwrites the already-existing binding flags.
                var propertyInfo = typeof(T)
                    .GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // It can't  be found, return false
                if (propertyInfo == null)
                {
                    return false;
                }
            }

            // All checks out, return true
            return true;
        }
    }
}
