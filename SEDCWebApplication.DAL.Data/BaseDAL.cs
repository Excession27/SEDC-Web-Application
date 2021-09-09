using Microsoft.Extensions.Configuration;
using SEDCWebApplication.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SEDCWebApplication.DAL.Data
{
    public abstract class BaseDAL
    {

        private const string DB_CONNECTION_STRING_KEY = "SEDC";

        protected abstract string COLUMN_PREFIX { get; }

        protected SqlConnection ConnectionGet()
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = ReadConnectionString();
            }
            catch (Exception ex)
            {
                string msg = String.Format(
                    "ConnectionPSGet failed. Check {0} in config file.",
                    DB_CONNECTION_STRING_KEY);
                throw new Exception(msg, ex);
            }
            return cn;
        }

        private string ReadConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(GetAppSettingsFileName(), optional: true, reloadOnChange: true)
                .Build();

            return configuration.GetConnectionString(DB_CONNECTION_STRING_KEY);
        }

        private string GetAppSettingsFileName()
        {
            var appsettings = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
            return appsettings;
        }

        protected SqlCommand CommandGet(SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        protected string ColumnNameWithPrefixGet(
            string prefix,
            string columnName)
        {
            if (!String.IsNullOrEmpty(prefix))
            {
                return prefix + "_" + columnName;
            }
            else
            {
                return columnName;
            }
        }

        protected T ReaderColumnReadObject<T>(
            IDataReader reader,
            string columnName,
            string columnPrefix = null) where T : class
        {
            if (reader[ColumnNameWithPrefixGet(columnPrefix, columnName)] == DBNull.Value)
            {
                return null;
            }

            object columnValue = reader[ColumnNameWithPrefixGet(columnPrefix, columnName)];

            return (T)Convert.ChangeType(columnValue, typeof(T));
        }

        protected T ReaderColumnReadValueType<T>(
            IDataReader reader,
            string columnName,
            string columnPrefix = null) where T : struct
        {
            if (reader[ColumnNameWithPrefixGet(columnPrefix, columnName)] == DBNull.Value)
            {
                throw new Exception(String.Format("columnName {0} is null", columnName));
            }

            object columnValue = reader[ColumnNameWithPrefixGet(columnPrefix, columnName)];

            return (T)Convert.ChangeType(columnValue, typeof(T));
        }


        protected Nullable<T> ReaderColumnReadNullableValueType<T>(
            IDataReader reader,
            string columnName,
            string columnPrefix = null) where T : struct
        {
            if (reader[ColumnNameWithPrefixGet(columnPrefix, columnName)] == DBNull.Value)
            {
                return null;
            }

            object columnValue = reader[ColumnNameWithPrefixGet(columnPrefix, columnName)];

            return (T)Convert.ChangeType(columnValue, typeof(T));
        }

        protected T ReaderColumnReadEnum<T>(
            IDataReader reader,
            string columnName,
            string columnPrefix = null) where T : struct
        {
            int columnValue = ReaderColumnReadValueType<int>(
                reader,
                columnName,
                columnPrefix);
            return (T)Enum.ToObject(typeof(T), columnValue);
        }

        protected Nullable<T> ReaderColumnReadNullableEnum<T>(
            IDataReader reader,
            string columnName,
            string columnPrefix = null) where T : struct
        {
            int? columnValue = ReaderColumnReadNullableValueType<int>(
                reader,
                columnName,
                columnPrefix);

            if (columnValue == null)
            {
                return null;
            }

            return (T)Enum.ToObject(typeof(T), columnValue);
        }

        /// <summary>
        /// Gets FK property from DAL method.
        /// </summary>
        /// <typeparam name="T">Type of property to get</typeparam>
        protected T ReaderColumnReadFK<T>(
            IDataReader reader,
            string columnName,
            Func<int, T> fkDALGetByIdDelegate,
            string columnPrefix = null) where T : class
        {
            int? fkId = ReaderColumnReadNullableValueType<int>(
                reader,
                columnName,
                columnPrefix);

            if (fkId == null)
            {
                return null;
            }

            return fkDALGetByIdDelegate(
                fkId.Value);
        }


        protected void ParamComplexPropertyNullableValueSet<T>(
            SqlCommand cmd,
            SqlParameter prm,
            T complexObject)
            where T : BaseEntity
        {
            if (complexObject == null)
            {
                prm.Value = DBNull.Value;
                cmd.Parameters.Add(prm);
                return;
            }

            if (complexObject.Id.HasValue)
            {
                prm.Value = complexObject.Id.Value;
                cmd.Parameters.Add(prm);
            }
            else
            {
                throw new ArgumentNullException(
                    String.Format(
                        "Null value of property of complex object {0} is unexpected",
                        complexObject.GetType()));
            }
        }

        protected void ParamComplexPropertyNonNullableValueSet<T>(
            SqlCommand cmd,
            SqlParameter prm,
            T complexObject)
            where T : BaseEntity
        {
            if (complexObject == null)
            {
                throw new ArgumentNullException(
                    String.Format(
                        "Null value of property of type {0} is unexpected. paramName: {1}",
                        typeof(T),
                        prm.ParameterName));
            }

            if (complexObject.Id.HasValue)
            {
                prm.Value = complexObject.Id.Value;
                cmd.Parameters.Add(prm);
            }
            else
            {
                throw new ArgumentNullException(
                    String.Format(
                        "Null value of property of complex object {0} is unexpected. paramName: {1}",
                        complexObject.GetType(),
                        prm.ParameterName));
            }
        }

        protected void ParamStringNullableValueSet(
            SqlCommand cmd,
            SqlParameter prm,
            string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                prm.Value = DBNull.Value;
            }
            else
            {
                prm.Value = propertyValue;
            }
            cmd.Parameters.Add(prm);
        }

        protected void ParamStringNonNullableValueSet(
            SqlCommand cmd,
            SqlParameter prm,
            string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                throw new ArgumentNullException(
                        prm.ParameterName,
                        String.Format(
                            "Null or Empty value string is unexpected"));
            }
            else
            {
                prm.Value = propertyValue;
                cmd.Parameters.Add(prm);
            }
        }

        protected void ParamValueTypeNonNullableValueSet<T>(
            SqlCommand cmd,
            SqlParameter prm,
            T paramValue) where T : struct
        {
            prm.Value = paramValue;
            cmd.Parameters.Add(prm);
        }

        protected void ParamNullableValueTypeNullableValueSet<T>(
            SqlCommand cmd,
            SqlParameter prm,
            Nullable<T> paramValue) where T : struct
        {
            if (paramValue.HasValue)
            {
                prm.Value = paramValue;
            }
            else
            {
                prm.Value = DBNull.Value;
            }
            cmd.Parameters.Add(prm);
        }

        #region Params

        protected void ParamComplexPropertyNullableValueSet<T>(
           SqlCommand cmd,
           T complexObject,
            string paramName,
            SqlDbType paramType)
           where T : BaseEntity
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            if (complexObject == null)
            {
                prm.Value = DBNull.Value;
                cmd.Parameters.Add(prm);
                return;
            }

            if (complexObject.Id.HasValue)
            {
                prm.Value = complexObject.Id.Value;
                cmd.Parameters.Add(prm);
            }
            else
            {
                throw new ArgumentNullException(
                    prm.ParameterName,
                    String.Format(
                        "Null value of property of complex object {0} is unexpected",
                        complexObject.GetType()));
            }
        }

        protected void ParamComplexPropertyNullableValueSet<T>(
           SqlCommand cmd,
           T complexObject,
            string paramName,
            SqlDbType paramType,
            int paramSize)
           where T : BaseEntity
        {
            SqlParameter prm = new SqlParameter(paramName, paramType, paramSize);
            if (complexObject == null)
            {
                prm.Value = DBNull.Value;
                cmd.Parameters.Add(prm);
                return;
            }

            if (complexObject.Id.HasValue)
            {
                prm.Value = complexObject.Id.Value;
                cmd.Parameters.Add(prm);
            }
            else
            {
                throw new ArgumentNullException(
                    prm.ParameterName,
                    String.Format(
                        "Null value of property of complex object {0} is unexpected",
                        complexObject.GetType()));
            }
        }

        protected void ParamComplexPropertyNonNullableValueSet<T>(
            SqlCommand cmd,
            T complexObject,
            string paramName,
            SqlDbType paramType)
            where T : BaseEntity
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            this.ParamComplexPropertyNonNullableValueSet<T>(
                cmd,
                prm,
                complexObject);
        }

        protected void ParamComplexPropertyNonNullableValueSet<T>(
            SqlCommand cmd,
            T complexObject,
            string paramName,
            SqlDbType paramType,
            int paramSize)
            where T : BaseEntity
        {
            SqlParameter prm = new SqlParameter(paramName, paramType, paramSize);
            this.ParamComplexPropertyNonNullableValueSet<T>(
                cmd,
                prm,
                complexObject);
        }

        protected void ParamStringNullableValueSet(
            SqlCommand cmd,
            string propertyValue,
            string paramName,
            SqlDbType paramType)
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                prm.Value = DBNull.Value;
            }
            else
            {
                if (propertyValue.Length > prm.Size)
                {
                    throw new Exception(String.Format("Parameter: '{0}' size is '{1}' but property length is '{2}'", paramName, prm.Size, propertyValue.Length));
                }

                prm.Value = propertyValue;
            }
            cmd.Parameters.Add(prm);
        }

        protected void ParamStringNullableValueSet(
            SqlCommand cmd,
            string propertyValue,
            string paramName,
            SqlDbType paramType,
            int paramSize)
        {
            SqlParameter prm = new SqlParameter(paramName, paramType, paramSize);
            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                prm.Value = DBNull.Value;
            }
            else
            {
                if (propertyValue.Length > prm.Size)
                {
                    throw new Exception(String.Format("Parameter: '{0}' size is '{1}' but property length is '{2}'", paramName, prm.Size, propertyValue.Length));
                }

                prm.Value = propertyValue;
            }
            cmd.Parameters.Add(prm);
        }

        protected void ParamStringNonNullableValueSet(
            SqlCommand cmd,
            string propertyValue,
            string paramName,
            SqlDbType paramType)
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                throw new ArgumentNullException(
                        prm.ParameterName,
                        String.Format(
                            "Null or Empty value string is unexpected"));
            }
            else
            {
                prm.Value = propertyValue;
                cmd.Parameters.Add(prm);
            }
        }

        protected void ParamStringNonNullableValueSet(
            SqlCommand cmd,
            string propertyValue,
            string paramName,
            SqlDbType paramType,
            int paramSize)
        {
            SqlParameter prm = new SqlParameter(paramName, paramType, paramSize);
            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                throw new ArgumentNullException(
                        prm.ParameterName,
                        String.Format(
                            "Null or Empty value string is unexpected"));
            }
            else
            {
                prm.Value = propertyValue;
                cmd.Parameters.Add(prm);
            }
        }

        protected void ParamNullableValueTypeNullableValueSet<T>(
            SqlCommand cmd,
            T paramValue,
            string paramName,
            SqlDbType paramType) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            prm.Value = paramValue;
            cmd.Parameters.Add(prm);
        }

        protected void ParamValueTypeNonNullableValueSet<T>(
            SqlCommand cmd,
            T paramValue,
            string paramName,
            SqlDbType paramType,
            int paramSize) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType, paramSize);
            prm.Value = paramValue;
            cmd.Parameters.Add(prm);
        }

        protected void ParamNullableValueTypeNullableValueSet<T>(
            SqlCommand cmd,
            Nullable<T> paramValue,
            string paramName,
            SqlDbType paramType) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            if (paramValue.HasValue)
            {
                prm.Value = paramValue;
            }
            else
            {
                prm.Value = DBNull.Value;
            }
            cmd.Parameters.Add(prm);
        }

        protected void ParamNullableValueTypeNullableValueSet<T>(
           SqlCommand cmd,
           Nullable<T> paramValue,
           string paramName,
           SqlDbType paramType,
            int paramSize) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType, paramSize);
            if (paramValue.HasValue)
            {
                prm.Value = paramValue;
            }
            else
            {
                prm.Value = DBNull.Value;
            }
            cmd.Parameters.Add(prm);
        }

        protected void ParamArrayNullableValueSet<T>(
            SqlCommand cmd,
            T[] paramValue,
            string paramName,
            SqlDbType paramType) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            if (paramValue != null)
            {
                prm.Value = paramValue;
            }
            else
            {
                prm.Value = DBNull.Value;
            }
            cmd.Parameters.Add(prm);
        }

        protected void ParamArrayNullableValueSet<T>(
           SqlCommand cmd,
           T[] paramValue,
           string paramName,
           SqlDbType paramType,
            int paramSize) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType, paramSize);
            if (paramValue != null)
            {
                prm.Value = paramValue;
            }
            else
            {
                prm.Value = DBNull.Value;
            }
            cmd.Parameters.Add(prm);
        }


        protected void ParamArrayNonNullableValueSet<T>(
               SqlCommand cmd,
               T[] paramValue,
               string paramName,
               SqlDbType paramType,
               int paramSize) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType, paramSize);
            prm.Value = paramValue;
            cmd.Parameters.Add(prm);
        }

        protected void ParamNullableEnumNullableValueSet<T>(
            SqlCommand cmd,
            Nullable<T> propertyValue,
            string paramName,
            SqlDbType paramType) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            if (propertyValue == null)
            {
                prm.Value = DBNull.Value;
            }
            else
            {
                prm.Value = (int)Convert.ChangeType(propertyValue, typeof(int));
            }
            cmd.Parameters.Add(prm);
        }

        protected void ParamEnumNonNullableValueSet<T>(
            SqlCommand cmd,
            T propertyValue,
            string paramName,
            SqlDbType paramType) where T : struct
        {
            SqlParameter prm = new SqlParameter(paramName, paramType);
            prm.Value = (int)Convert.ChangeType(propertyValue, typeof(int));
            cmd.Parameters.Add(prm);
        }

        #endregion

        protected TRetValue GetByIdAndThrow<TRetValue>(
            Dictionary<int, TRetValue> dict,
            int key)
        {
            try
            {
                return dict[key];
            }
            catch (KeyNotFoundException)
            {
                string msg = String.Format("dict key not found. key: {0}, dict type: {1}", key, dict.GetType().FullName);
                throw new KeyNotFoundException(msg);
            }
        }

        protected TRetValue GetByIdAndReturnNull<TRetValue>(
            Dictionary<int, TRetValue> dict,
            int key)
            where TRetValue : class
        {
            try
            {
                return dict[key];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

    }
}
