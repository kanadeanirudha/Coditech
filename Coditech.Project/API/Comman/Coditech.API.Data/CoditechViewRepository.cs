using Dapper;

using Microsoft.Data.SqlClient;

using System.Data;

namespace Coditech.API.Data
{
    public class CoditechViewRepository<T> : ICoditechViewRepository<T> where T : class
    {
        #region Declarations

        private readonly CoditechDbContext _context;
        DynamicParameters dynamicParameterList = new DynamicParameters();
        public string ReturnParameter = string.Empty;
        private string connectionString = "Server=45.35.13.71;Database=RARIndia;User Id=sa;Password=saralsoftservices0098#$; TrustServerCertificate=True";
        #endregion

        #region Constructor

        public CoditechViewRepository()
        {

        }

        public CoditechViewRepository(CoditechDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods
        //Executes the Stored Procedure using the Entity Framework.
        public IList<T> ExecuteStoredProcedureList(string commandText)
        {
            int totalRowCount = 0;
            var result = ExecuteStoredProcedureList(commandText, null, out totalRowCount);
            return result;
        }

        //Executes the Stored Procedure using the Entity Framework, return the total row count for the mentioned index location.
        public IList<T> ExecuteStoredProcedureList(string commandText, int? indexOutParamater, out int totalRowCount)
        {
            var result = ExecuteStoredProcedureList(commandText, 0, indexOutParamater, out totalRowCount);
            return result;
        }

        //Set the Stored Procedure Parameters.
        public void SetParameter(string ParameterName, object ParameterValue, ParameterDirection Direction, DbType dbType)
        {
            dynamicParameterList.Add(ParameterName, ParameterValue, dbType, Direction);

            if (Equals(Direction, ParameterDirection.Output))
                ReturnParameter = ParameterName;
        }

        //Set the Stored Procedure Parameters.
        public void SetTableValueParameter(string ParameterName, DataTable ParameterValue, ParameterDirection Direction, SqlDbType dbType, string tableValueTypeName)
        {
            dynamicParameterList.Add(ParameterName, ParameterValue.AsTableValuedParameter(tableValueTypeName), direction: Direction);

            if (Equals(Direction, ParameterDirection.Output))
                ReturnParameter = ParameterName;
        }

        //Set the Stored Procedure Parameters.
        public void SetParameter(string ParameterName, object ParameterValue, ParameterDirection Direction, DbType dbType, byte predicate, byte scale)
        {
            dynamicParameterList.Add(ParameterName, ParameterValue, dbType, direction: Direction);

            if (Equals(Direction, ParameterDirection.Output))
                ReturnParameter = ParameterName;
        }


        //Executes the Stored Procedure using the Entity Framework.
        public IList<T> ExecuteStoredProcedureList(string commandText, int storedProcedureTimeOut)
        {
            int totalRowCount = 0;
            var result = ExecuteStoredProcedureList(commandText, storedProcedureTimeOut, null, out totalRowCount);
            return result;
        }


        //Executes the Stored Procedure using the Entity Framework, return the total row count for the mentioned index location.
        public IList<T> ExecuteStoredProcedureList(string commandText, int storedProcedureTimeOut, int? indexOutParamater, out int totalRowCount)
        {
            totalRowCount = 0;
            try
            {
                //using IDbConnection db = new SqlConnection(this._context.Database.GetConnectionString());
                using IDbConnection db = new SqlConnection(connectionString);
                var result = db.Query<T>(sql: "exec " + commandText, param: dynamicParameterList, commandTimeout: storedProcedureTimeOut).ToList();

                if (!Equals(result, null) && indexOutParamater.HasValue)
                {
                    totalRowCount = Convert.ToInt32(dynamicParameterList.Get<object>(ReturnParameter));
                }

                return result;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(T), commandText, ex);
                throw;
            }
            finally
            {
                ClearParameters();
            }
        }
        #endregion

        #region Private Methods
        //Clears the defined parameter.
        private void ClearParameters()
        {
            dynamicParameterList = new DynamicParameters();
        }
        #endregion
    }
}
