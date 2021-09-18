using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Interfaces
{
    public interface IEmployeeDAL
    {
        void Save(Employee item);

        Employee GetById(int id);

        List<Employee> GetAll(int skip, int take);

        void Update(Employee item);

    }
}
