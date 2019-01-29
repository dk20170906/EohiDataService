using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace EohiDataServerApi
{
   internal  class EFDbContextFactory
    {
            public DbContext GetDbContext()
            {
                string key = "kailifonEntities";
                DbContext dbContext = CallContext.GetData(key) as DbContext; if (dbContext == null)
                {
                    //dbContext = new kailifonEntities();
                    dbContext = new Models.kailifonEntities();
                    CallContext.SetData(key, dbContext);
                }
                return dbContext;
            }
       

    }
}
