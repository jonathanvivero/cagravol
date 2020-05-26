using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Enums.Common
{
    public static class EnumErrorCode
    {
        public const string USER_NOT_VALID = "USER_NOT_VALID";
        public const string VALIDATION_ERROR = "VALIDATION_ERROR";
        public const string INSUFFICIENT_PRIVILEGES = "INSUFFICIENT_PRIVILEGES";
        public const string ITEM_DOES_NOT_EXIST = "ITEM_DOES_NOT_EXIST";
        public const string CUSTOMER_USER_NOT_FOUND_FOR_ANY_PROJECT = "CUSTOMER_USER_NOT_FOUND_FOR_ANY_PROJECT";
        public const string FILE_NOT_FOUND = "FILE_NOT_FOUND";
        public const string CUSTOMER_NOT_FOUND = "CUSTOMER_NOT_FOUND";
        public const string PROJECT_NOT_FOUND = "PROJECT_NOT_FOUND";
        public const string WORKFLOW_NOT_FOUND = "WORKFLOW_NOT_FOUND";
        public const string STATE_NOT_FOUND = "STATE_NOT_FOUND";
        public const string TRANSITION_NOT_FOUND = "TRANSITION_NOT_FOUND";
        public const string FILE_WF_STATE_DOES_NOT_MATCH = "FILE_WF_STATE_DOES_NOT_MATCH";
        public const string ZERO_PROJECTS = "ZERO_PROJECTS";
        public const string SPACE_IS_NOT_FREE = "SPACE_IS_NOT_FREE";
        public const string SPACE_RESERVATION_NOT_FOUND = "SPACE_RESERVATION_NOT_FOUND";
    }
}
