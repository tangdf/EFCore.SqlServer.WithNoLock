using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.SqlServer.WithNoLock.UnitTest
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }



        public override string ToString()
        {
            return $"{nameof(CategoryID)}: {CategoryID}, {nameof(CategoryName)}: {CategoryName}";
        }
    }
}
