using System;

namespace Rolo.Auth.Core.Validations
{
    public static class Validations
    {
        public static Boolean NotNullOrEmpty(string property)
        {
            return !String.IsNullOrEmpty(property);
        }
    }
}
