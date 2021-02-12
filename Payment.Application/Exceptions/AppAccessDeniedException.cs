using System;

namespace Payment.Application.Exceptions
{
    public class AppAccessDeniedException : AppException
    {
        public AppAccessDeniedException() : base("User not authorized to access the resource.")
        {
        }

        public AppAccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
