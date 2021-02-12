using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Application.Exceptions
{
    public class AppValidationException : AppException
    {
        public AppValidationException() : base("Validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public AppValidationException(List<ValidationFailure> failures) : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName ?? new Random().Next().ToString(), propertyFailures);
            }
        }


        public IDictionary<string, string[]> Failures { get; }


        private string getPropertyName(string propertyName)
        {
            string result = getCamelCase(propertyName);
            if (propertyName.Contains("."))
            {
                var propertyArray = result.Split('.');
                var nestedPropertyName = propertyArray[1];
                if (nestedPropertyName != null)
                    result = $"{propertyArray[0]}.{getCamelCase(nestedPropertyName)}";
            }

            return result;
        }

        private string getCamelCase(string str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                    return null;

                return str.Substring(0, 1).ToLower() + str.Substring(1, str.Length - 1);
            }
            catch (Exception)
            {
                return str;
            }
        }
    }
}
