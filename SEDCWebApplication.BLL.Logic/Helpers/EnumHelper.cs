using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.BLL.Logic.Helpers
{
    public class EnumHelper
    {
        public static string GetString<T>(object value) 
        {
            return Enum.GetName(typeof(T), value);
        }
    }
}
