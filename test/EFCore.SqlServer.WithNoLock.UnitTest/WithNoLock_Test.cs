using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer.WithNoLock;
using Xunit;
using Xunit.Abstractions;

namespace EFCore.SqlServer.WithNoLock.UnitTest
{
    public class WithNoLock_Test
    {
 

        [Fact]
        public void Query_WithNoLock()
        {
            using (var dataContext = new SampleDbContext())
            {
                var id = 2;
                Category entity = dataContext.Categories.WithNoLock().Single(item => item.CategoryID == id);
            }

            var sql = DumpSql();


            Assert.Contains("FROM [Category] AS [item] With(nolock)",sql);

        }

        [Fact]
        public void Query_WithoutNoLock()
        {
            using (var dataContext = new SampleDbContext())
            {
                var id = 2;
                Category entity = dataContext.Categories.Single(item => item.CategoryID == id);
            }

            var sql = DumpSql();


            Assert.DoesNotContain("With(nolock)", sql);

        }

        private static string DumpSql()
        {
            StringBuilder stringBuilder=new StringBuilder();
            foreach (var logMessage in SampleDbContext.LogMessages)
            {
                stringBuilder.AppendLine(logMessage);
            }

            return stringBuilder.ToString();
        }
    }
}