using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using DataModel.GenericRepository;

namespace DataModel.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private DataStorageContext _context;
        private GenericRepository<KeyValue> _keyValueRepository;

        public UnitOfWork()
        {
            _context = new DataStorageContext();
        }

        public GenericRepository<KeyValue> KeyValueRepository
        {
            get
            {
                if (this._keyValueRepository==null)
                    this._keyValueRepository=new GenericRepository<KeyValue>(_context);
                return _keyValueRepository;
            }
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation error:",
                        DateTime.Now,
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName,
                            ve.ErrorMessage));
                    }
                }
                // Write error to file
                System.IO.File.AppendAllLines(@"C:\errors.txt",outputLines);

                throw e;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed.");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
