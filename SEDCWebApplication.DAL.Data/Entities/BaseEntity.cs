using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.Data.Entities
{
    public class BaseEntity
    {
        protected BaseEntity(int? id)
        {
            this.EntityState = id.HasValue ? EntityStateEnum.Loaded : EntityStateEnum.New;
            this.Id = id;
        }

        public int? Id { get; set; }

        public EntityStateEnum EntityState { get; set; }
    }
}
