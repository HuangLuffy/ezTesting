using System.Collections.Generic;

namespace CommonLib.Util
{
    /// <summary>
    /// IEnumerable接口的扩展方法，支持它的实现类是List的情况
    /// </summary>
    public static class UtilIEnumerable
    {
        /// <summary>
        /// 向集合中添加元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        public static void Add<T>(this IEnumerable<T> collection, T value)
        {
            (collection as List<T>)?.Add(value);
        }
        /// <summary>
        /// 从集合中删除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        public static void Remove<T>(this IEnumerable<T> collection, T value)
        {
            //collection.ToList<T>().Remove(value);
            ((List<T>) collection).Remove(value);
        }
        /// <summary>
        /// 检索集合中是否包含某个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains<T>(this IEnumerable<T> collection, T value)
        {
            return ((List<T>) collection).Contains(value);
        }

    }
}
