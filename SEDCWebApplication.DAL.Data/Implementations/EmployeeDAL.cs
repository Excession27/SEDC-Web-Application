using SEDCWebApplication.DAL.Data.Entities;
using SEDCWebApplication.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SEDCWebApplication.DAL.Data.Implementations
{
    public class EmployeeDAL : BaseDAL, IEmployeeDAL
    {

        protected override string COLUMN_PREFIX
        {
            get { return ColumnPrefixConstants.EMP; }
        }


        public void Save(Employee item)
        {
            switch (item.EntityState)
            {
                case EntityStateEnum.New:
                    this.Add(item);
                    break;
                case EntityStateEnum.Updated:
                    this.Update(item);
                    break;
                case EntityStateEnum.Loaded:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(String.Format("EntityState is invalid: Value: {0}", item.EntityState));
            }
            item.EntityState = EntityStateEnum.Loaded;
        }

        public Employee GetById(int id)
        {
            SqlConnection cn = ConnectionGet();

            Employee result = null;

            SqlCommand cmd = CommandGet(cn);
            cmd.CommandText = "Employee_GetById";

            this.ParamNullableValueTypeNullableValueSet(cmd, id, "@EmployeeId", SqlDbType.Int);

            try
            {
                cn.Open();

                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = Create(reader);
                }
            }
            catch (Exception ex)
            {
                //DMLogger.Singleton.LogError(LogCategories.SECURITY, ex);
                throw;
            }
            finally
            {
                cn.Close();
            }
            return result;
        }


        private void Add(Employee item)
        {
            SqlConnection cn = ConnectionGet();

            SqlCommand cmd = CommandGet(cn);
            cmd.CommandText = "Employee_Ins";

            CommonParametersAdd(item, cmd);

            SqlParameter prm = new SqlParameter();
            prm.ParameterName = "@ID";
            prm.SqlDbType = SqlDbType.Int;
            prm.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(prm);

            try
            {
                cn.Open();

                cmd.ExecuteNonQuery();

                item.Id = Convert.ToInt32(cmd.Parameters["@ID"].Value);

            }
            catch (Exception ex)
            {
                //DMLogger.Singleton.LogError(ex, item);
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void Update(Employee item)
        {
            SqlConnection cn = ConnectionGet();

            SqlCommand cmd = CommandGet(cn);
            cmd.CommandText = "Employee_Upd";

            CommonParametersAdd(item, cmd);

            SqlParameter prm = new SqlParameter();
            prm.ParameterName = "@ID";
            prm.SqlDbType = SqlDbType.Int;
            prm.Value = item.Id;
            cmd.Parameters.Add(prm);

            try
            {
                cn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //DMLogger.Singleton.LogError(ex, item);
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public List<Employee> SearchEmployees(string searchParam)
        {
            SqlConnection cn = ConnectionGet();

            Employee result = null;
            List<Employee> results = new List<Employee>();

            SqlCommand cmd = CommandGet(cn);
            cmd.CommandText = "Employee_Search";

            this.ParamStringNullableValueSet(cmd, searchParam, "@SearchParam", SqlDbType.NVarChar, 50);

            try
            {
                cn.Open();

                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = Create(reader);
                    results.Add(result);
                }
            }
            catch (Exception ex)
            {
                //DMLogger.Singleton.LogError(LogCategories.SECURITY, ex);
                throw;
            }
            finally
            {
                cn.Close();
            }
            return results;
        }

        public List<Employee> GetAll(int skip, int take)
        {
            SqlConnection cn = ConnectionGet();

            Employee result = null;
            List<Employee> results = new List<Employee>();

            SqlCommand cmd = CommandGet(cn);
            cmd.CommandText = "Employee_GetAll";

            this.ParamNullableValueTypeNullableValueSet(cmd, skip, "@RowsToSkip", SqlDbType.Int);
            this.ParamNullableValueTypeNullableValueSet(cmd, take, "@RowsToTake", SqlDbType.Int);

            try
            {
                cn.Open();

                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = Create(reader);
                    results.Add(result);
                }
            }
            catch (Exception ex)
            {
                //DMLogger.Singleton.LogError(LogCategories.SECURITY, ex);
                throw;
            }
            finally
            {
                cn.Close();
            }
            return results;
        }


        private void CommonParametersAdd(Employee item, SqlCommand cmd)
        {
            this.ParamStringNullableValueSet(cmd, item.UserName, "@UserName", SqlDbType.NVarChar, 50);
            this.ParamStringNullableValueSet(cmd, item.Password, "@Password", SqlDbType.NVarChar, 50);
            this.ParamStringNonNullableValueSet(cmd, item.Name, "@EmployeeName", SqlDbType.NVarChar, 50);
            this.ParamStringNullableValueSet(cmd, item.Gender, "@Gender", SqlDbType.NVarChar, 50);
            this.ParamNullableValueTypeNullableValueSet(cmd, item.RoleId, "@RoleId", SqlDbType.Int);
            this.ParamNullableValueTypeNullableValueSet(cmd, item.DateOfBirth, "@DateOfBirth", SqlDbType.Date);
            this.ParamStringNullableValueSet(cmd, item.ImagePath, "@ImagePath", SqlDbType.NVarChar, 255);
        }

        private Employee Create(IDataReader reader)
        {
            Employee item = new Employee(ReaderColumnReadNullableValueType<Int32>(reader, "ID", COLUMN_PREFIX));

            item.Name = ReaderColumnReadObject<string>(reader, "EmployeeName", COLUMN_PREFIX);
            item.Gender = ReaderColumnReadObject<string>(reader, "Gender", COLUMN_PREFIX);
            item.RoleId = ReaderColumnReadValueType<int>(reader, "RoleId", COLUMN_PREFIX);
            item.DateOfBirth = ReaderColumnReadValueType<DateTime>(reader, "DateOfBirth", COLUMN_PREFIX);
            item.ImagePath = ReaderColumnReadObject<string>(reader, "ImagePath", COLUMN_PREFIX);

            return item;
        }
    }
}
