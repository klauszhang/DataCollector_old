using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.ModelConfigurations;
using DataModel.Models;

namespace DataModel
{
    public class DataStorageContext:DbContext
    {
        public DataStorageContext():base("DataCollectorDb")
        {
        }

        public DbSet<KeyValue> KeyValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new KeyValueConfiguration());
;        }
    }
}
