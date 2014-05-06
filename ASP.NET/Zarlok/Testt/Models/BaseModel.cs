using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testt.Models
{
    public class BaseModel : IDisposable
    {
        protected ZarlokEntities1 db;

        public BaseModel()
        {
            db = new ZarlokEntities1();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}