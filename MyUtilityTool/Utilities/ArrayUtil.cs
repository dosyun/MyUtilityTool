using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtilityTool.Utilities
{
    public static class ArrayUtil
    {
        public static T[] Uniq<T>(T[] array)
        {
            List<T> uniqList = new List<T>();
            foreach (T value in array)
            {
                if (!uniqList.Contains(value))
                {
                    uniqList.Add(value);
                }
            }
            return uniqList.ToArray();
        }

        public static T Shift<T>(ref T[] array)
        {
            if (array == null || array.Length == 0)
                return default(T);
            T value = array[0];
            T[] newArray = new T[array.Length - 1];
            Array.Copy(array, 1, newArray, 0, array.Length - 1);
            array = newArray;
            return value;
        }

        public static void Shuffle<T>(List<T> data)
        {
            for (int i = data.Count; i > 1; i--)
            {
                Swap<T>(data, RandomUtil.UniqueRandom.Next(i), i - 1);
            }
        }

        private static void Swap<T>(List<T> list, int index1, int index2)
        {
            T a = list[index1];
            list[index1] = list[index2];
            list[index2] = a;
        }
    }
}
