using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Validators
{
    public static class Extenstions
    {
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            return items.Contains(item);
        }
    }
}
