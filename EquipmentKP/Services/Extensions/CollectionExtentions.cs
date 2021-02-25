using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.ComponentModel
{
    static class CollectionExtentions
    {
        public static int Count(this ICollectionView collection)
        {
            int count = 0;
            foreach (var item in collection)
                count++;
            return count;
        }
    }
}
