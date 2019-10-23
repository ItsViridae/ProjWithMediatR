using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectZ.Common.Responses
{
    public static class ErrorMessages
    {
        public static class User
        {
            public const string EmailOrPasswordIsIncorrect = "'Email' or 'Password' is incorrect.";
            public const string EmailAlreadyExists = "An 'Email' with this name already exists.";
            public const string IdDoesNotExist = "'Id' does not exist.";
        }

        public static class Association
        {
            public const string IdDoesNotExist = "'Id' does not exist.";
            public const string NameAlreadyExists = "'Name' with this association name already exists.";
        }
    }
}
