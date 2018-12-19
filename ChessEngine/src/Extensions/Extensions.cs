using System.Collections.Generic;

namespace ChessEngine.Extensions
{
    public static class Extensions
    {
        public static List<T> AddMultiple<T> (this List<T> list, int count, params T[] objectType)
        {
            var index = 0;
            for(int i = 0; i<count; i++)
            {
                list.Add(objectType[index]);
                if(index < objectType.Length-1)
                {
                    index++;
                }
            }
            return list;
        }
        public static Dictionary<string,T> AddMultiple<T>(this Dictionary<string,T> list, int count, T objectType)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(i.ToString(),objectType);
               
            }
            return list;
        }
    }
}