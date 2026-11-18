using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Common.Validation
{
    public static class Guard
    {
        public static void NotNull<T>(T value, string name = null) where T : class
        {

            if (value == null)
                throw new ValidationException( new[] {new ValidationFailure("Object", "Object Is Null")});
        }

        public static void GreaterThanZero(int value)
        {
            if(value<=0)
                throw new ValidationException(new[] { new ValidationFailure("Integer", "Object Is Null") });
        }
       
    }
}