using System.Collections.Generic;
using System.Reflection;

namespace Core.Common.Utility
{
    public class ObjectsCompareUtil
    {
        public static bool IsEqual(object firstObject, object secondObject, List<string> exclusion = null)
        {
            bool result = default(bool);
            foreach (PropertyInfo firstObjectPropertyInfo in firstObject.GetType().GetProperties())
            {
                if (exclusion != null && exclusion.Contains(firstObjectPropertyInfo.Name))
                    continue;


                foreach (PropertyInfo secondObjectPropertyInfo in secondObject.GetType().GetProperties())
                {

                    if (firstObjectPropertyInfo.Name == secondObjectPropertyInfo.Name)
                    {
                        result = firstObjectPropertyInfo.GetValue(firstObject, null).ToString() == secondObjectPropertyInfo.GetValue(secondObject, null).ToString();
                        if (!result) break;
                    }
                }
                if (!result) break;
            }
            return result;
        }
    }
}