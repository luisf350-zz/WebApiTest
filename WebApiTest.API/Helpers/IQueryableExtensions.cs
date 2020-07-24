using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using WebApiTest.API.Services;

namespace WebApiTest.API.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrEmpty(orderBy))
            {
                return source;
            }

            var orderByString = string.Empty;

            // The orderBy string is separated by "," so we split it
            var orderByAfterSplit = orderBy.Split(',');

            // Apply each orderby clause in reverse order - otherwise, the
            // IQueryable will be ordered in the wrong order
            foreach (var item in orderByAfterSplit.Reverse())
            {
                // Trim the orderBy clause, as it might contain leading
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var
                var trimmendOrderByClause = item.Trim();

                // If sort option ends with " desc", we order
                // descending, ortherwise ascending
                var orderDescending = trimmendOrderByClause.EndsWith(" desc");

                // Remove " asc" or " desc" from the orderByClause, so we
                // get the property name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmendOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmendOrderByClause :
                    trimmendOrderByClause.Remove(indexOfFirstSpace);

                // Find the matching property
                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                // Get the PropertyMappingValue
                var propertyMappingValue = mappingDictionary[propertyName];
                if (propertyMappingValue == null)
                {
                    throw new ArgumentNullException("propertyMappingValue");
                }

                // Run through the property names
                // so the orderby clauses are applied in the correct order
                foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
                {
                    // Revert sort order if necessary
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    orderByString = orderByString + (
                        string.IsNullOrEmpty(orderByString) ? string.Empty : ", ")
                        + destinationProperty
                        + (orderDescending ? " descending" : " ascending");
                }
            }

            return source.OrderBy(orderByString);
        }
    }
}
