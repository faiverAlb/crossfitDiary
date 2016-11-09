using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossfitDiary.Web.MvcHelpers
{
    public static class EnumHelper
    {
        public static List<KeyValuePair<string, int>> ToList(Type enumType) 
        {
            var list = new List<KeyValuePair<string, int>>();

            foreach (var e in Enum.GetValues(enumType))
            {
                list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
            }
            return list;
        }
    }
}