using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using DataModel.Models;
using DataModel.UnitOfWork;
using Entities;

namespace Services
{
    public class KeyValueServices : IKeyValueServices
    {
        private UnitOfWork _unitOfWork;

        public KeyValueServices()
        {
            _unitOfWork = new UnitOfWork();
            Mapper.CreateMap<KeyValue, KeyValueEntity>();
        }

        public KeyValueEntity GetKeyValueDataById(long kvId)
        {
            var kvData = _unitOfWork.KeyValueRepository.GetById(kvId);
            if (kvData != null)
            {
                var kvDataEntity = Mapper.Map<KeyValueEntity>(kvData);
                Mapper.AssertConfigurationIsValid();
                return kvDataEntity;
            }
            return null;
        }

        public IEnumerable<KeyValueEntity> GetAllKeyValueData()
        {
            var allKvData = _unitOfWork.KeyValueRepository.GetAll().ToList();
            if (allKvData.Any())
            {
                var allKvDataEntities = Mapper.Map<List<KeyValue>,List<KeyValueEntity>>(allKvData);
                Mapper.AssertConfigurationIsValid();
                return allKvDataEntities;
            }
            return null;
        }

        public long CreateKeyValueData(KeyValueEntity kvEntity)
        {
            using (var scope = new TransactionScope())
            {
                // New Data
                var kvData = new KeyValue
                {
                    CreatedOn = DateTime.Now,
                    Key = kvEntity.Key,
                    Value = kvEntity.Value
                };
                _unitOfWork.KeyValueRepository.Insert(kvData);
                _unitOfWork.Save();
                scope.Complete();
                return kvData.Id;
            }
        }

        public bool UpdateKeyValueData(long kvId, KeyValueEntity kvEntity)
        {
            var success = false;
            if (kvEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var kvData = _unitOfWork.KeyValueRepository.GetById(kvId);
                    _unitOfWork.KeyValueRepository.Update(kvData);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }
            return success;
        }

        public bool DeleteKeyValueData(long kvId)
        {
            var success = false;

            if (kvId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var kvData = _unitOfWork.KeyValueRepository.GetById(kvId);
                    if (kvData != null)
                    {
                        _unitOfWork.KeyValueRepository.Delete(kvData);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
