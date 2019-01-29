using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EohiDataServerApi
{
    public abstract class EFDBHelperBase<T> where T : class ,new()
    {
        protected DbContext Db = new EFDbContextFactory().GetDbContext();

        public DbContext GetDbContext()
        {
            return Db;
        }
    }
}
