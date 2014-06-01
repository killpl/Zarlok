using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZarlokMvc.Models
{
    public class BaseModel : IDisposable
    {
        protected ZarlokEntities db;

        public BaseModel()
        {
            db = new ZarlokEntities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}