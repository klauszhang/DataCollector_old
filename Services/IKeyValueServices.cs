using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services
{
    public interface IKeyValueServices
    {
        KeyValueEntity GetKeyValueDataById(long kvId);
        IEnumerable<KeyValueEntity> GetAllKeyValueData();
        long CreateKeyValueData(KeyValueEntity kvEntity);
        bool UpdateKeyValueData(long kvId, KeyValueEntity kvEntity);
        bool DeleteKeyValueData(long kvId);
    }
}
