using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Middlewares.Models
{
    /// <summary>
    /// The ignore property class.
    /// </summary>
    public static class IgnorePropertyClass
    {
        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="propertyLambda">The property lambda.</param>
        public static void IgnoreProperty<T, TR>(this T parameter, Expression<Func<T, TR>> propertyLambda)
        {
            var parameterType = parameter.GetType();
            var propertyName = propertyLambda.GetReturnedPropertyName();
            if (propertyName == null)
            {
                return;
            }

            var jsonPropertyAttribute = parameterType.GetProperty(propertyName).GetCustomAttribute<JsonPropertyAttribute>();
            jsonPropertyAttribute.DefaultValueHandling = DefaultValueHandling.Ignore;
        }

        /// <summary>
        /// Gets the returned property name.
        /// </summary>
        /// <param name="propertyLambda">The property lambda.</param>
        /// <returns>A string.</returns>
        public static string GetReturnedPropertyName<T, TR>(this Expression<Func<T, TR>> propertyLambda)
        {
            var member = propertyLambda.Body as MemberExpression;
            var memberPropertyInfo = member?.Member as PropertyInfo;
            return memberPropertyInfo?.Name;
        }
    }
}