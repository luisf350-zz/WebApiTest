using CourseLibrary.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiTest.API.Models;

namespace WebApiTest.API.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _authorPropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
            { "Id", new PropertyMappingValue(new List<string>() {"Id"}) },
            { "MainCategory", new PropertyMappingValue(new List<string>() {"MainCategory"}) },
            { "Age", new PropertyMappingValue(new List<string>() {"DateOfBirth"}, true) },
            { "Name", new PropertyMappingValue(new List<string>() {"FirstName" , "LastName"}) }
        };

        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<AuthorDto, Author>(_authorPropertyMapping));
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrEmpty(fields))
            {
                return true;
            }

            // The string is separated by ",", so we split it
            var fieldsAfterSplit = fields.Split(',');

            // Run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // Trim
                var trimmedField = field.Trim();

                // Remove everything after the first " " - if the fields
                // are coming from an orderBy string, this part must be
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField :
                    trimmedField.Remove(indexOfFirstSpace);

                // Find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            // Get matching mapping
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.FirstOrDefault()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}>");
        }
    }
}