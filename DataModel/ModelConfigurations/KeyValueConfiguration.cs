using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;

namespace DataModel.ModelConfigurations
{
    public class KeyValueConfiguration:EntityTypeConfiguration<KeyValue>
    {
        public KeyValueConfiguration()
        {
            Property(kv => kv.Key)
                .HasMaxLength(40)
                .IsRequired();

            Property(kv => kv.Value)
                .IsRequired();

            Property(kv => kv.CreatedOn)
                .IsRequired();
        }
    }
}
