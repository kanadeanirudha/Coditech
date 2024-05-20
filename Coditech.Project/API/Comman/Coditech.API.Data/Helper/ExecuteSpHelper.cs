using Microsoft.EntityFrameworkCore;

using System.Data;
using System.Data.SqlClient;

namespace Coditech.API.Data
{

    public class ExecuteSpHelper
    {
        private readonly List<SqlParameter> parameterList = new List<SqlParameter>();
        private readonly CoditechDbContext _context;
        public string ReturnParameter { get; set; } = string.Empty;
        public ExecuteSpHelper()
        {
        }
        public ExecuteSpHelper(CoditechDbContext context)
        {
            _context = context;
        }

        public void ClearParameters()
        {
            parameterList.Clear();
        }

        public void GetParameter(string ParameterName, object ParameterValue, ParameterDirection Direction, SqlDbType dbType)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = ParameterName;
            sqlParameter.Value = ParameterValue;
            sqlParameter.SqlDbType = dbType;
            if (Direction != ParameterDirection.Output)
            {
                sqlParameter.Direction = ParameterDirection.Input;
            }
            else
            {
                sqlParameter.Direction = ParameterDirection.Output;
                ReturnParameter = ParameterName;
            }

            parameterList.Add(sqlParameter);
        }

        public void SetTableValueParameter(string ParameterName, object ParameterValue, ParameterDirection Direction, SqlDbType dbType, string tableValueTypeName)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = ParameterName;
            sqlParameter.Value = ParameterValue;
            sqlParameter.SqlDbType = dbType;
            sqlParameter.TypeName = tableValueTypeName;
            if (!object.Equals(Direction, ParameterDirection.Output))
            {
                sqlParameter.Direction = ParameterDirection.Input;
            }
            else
            {
                sqlParameter.Direction = ParameterDirection.Output;
                ReturnParameter = ParameterName;
            }

            parameterList.Add(sqlParameter);
        }

        public DataSet GetSPResultInDataSet(string storedProcedureName)
        {
            string connectionString = this._context.Database.GetConnectionString();
            DataSet dataSet = new DataSet();
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            using SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
            sqlCommand.CommandTimeout = 400;
            sqlCommand.Parameters.AddRange(parameterList.ToArray());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            try
            {
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public DataSet GetSPResultInDataSet(string storedProcedureName, int indexOutParamater, out int status)
        {
            status = 0;
            string connectionString = this._context.Database.GetConnectionString();

            DataSet dataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
                sqlCommand.CommandTimeout = 400;
                sqlCommand.Parameters.AddRange(parameterList.ToArray());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                try
                {
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDataAdapter.Fill(dataSet);
                    if (dataSet.Tables.Count == 0)
                    {
                        status = Convert.ToInt32(parameterList[Convert.ToInt32(indexOutParamater)].Value);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return dataSet;
        }

        public DataSet GetQueryResultInDataSet(string connectionString, string query)
        {
            DataSet dataSet = new DataSet();
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand selectCommand = new SqlCommand(query, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                try
                {
                    sqlDataAdapter.Fill(dataSet);
                    return dataSet;
                }
                finally
                {
                    ((IDisposable)(object)sqlDataAdapter)?.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetSPResultInObject(string storedProcedureName)
        {
            string connectionString = this._context.Database.GetConnectionString();
            object obj = null;
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            using SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
            sqlCommand.Parameters.AddRange(parameterList.ToArray());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            try
            {
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection.Open();
                obj = sqlCommand.ExecuteScalar();
                sqlCommand.Connection.Close();
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}