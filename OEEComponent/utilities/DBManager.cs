using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using OEEComponent.maintenance;

namespace OEEComponent.utilities
{
    public class DBManager
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlDataReader;
        private SqlTransaction sqlTransaction;
        private int executed = 0;
        private Hashtable hashData = new Hashtable();

        public string connectionStatus { set; get; }
        public string commandStatus { set; get; }
        public string dataReaderStatus { set; get; }
        public string executedQueryStatus { set; get; }
        public string transactionStatus { set; get; }
        public string executedQueryMode { set; get; }
        
        //only for DML
        public int getExecutedSQL(String query, List<SqlParameter> parameters)
        {
            this.doOpenConnection();
            try
            {
                this.sqlCommand = new SqlCommand(query, this.sqlConnection);
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        this.sqlCommand.Parameters.Add(param);
                    }
                }
                    this.sqlTransaction = this.sqlConnection.BeginTransaction();
                    this.sqlCommand.Transaction = this.sqlTransaction;
                    this.executedQueryMode = AppConstants.DB_CMD_CRUD;
                    int executed = this.sqlCommand.ExecuteNonQuery();
                    this.sqlTransaction.Commit();
                    this.transactionStatus = AppConstants.DB_CMD_TR_COMMIT;
                    this.executed = executed;

                if (this.executed != 0)
                {
                    this.executedQueryStatus = AppConstants.DB_CMD_EXECUTED;
                }
                else
                {
                    this.executedQueryStatus = AppConstants.DB_CMD_NOT_EXECUTED;
                }
            }
            catch (Exception e)
            {
                this.executedQueryStatus = AppConstants.DB_CMD_ERROR;
                if (this.sqlTransaction != null)
                {
                    try
                    {
                        this.sqlTransaction.Rollback();
                        this.transactionStatus = AppConstants.DB_CMD_TR_ROLLBACK;
                    }
                    catch (Exception ex)
                    {
                        this.transactionStatus = AppConstants.DB_CMD_TR_ROLLBACK_ERROR;
                    }
                }
                else
                {
                    this.transactionStatus = AppConstants.DB_CMD_TR_NOT_INITIALIZED;
                }
            }
            finally
            {
                this.doCloseConnection();
            }
            return this.executed;
        }

        public Hashtable getSourceList(String query, List<SqlParameter> parameters)
        {
            Hashtable hashData = new Hashtable();
            this.doOpenConnection();
            try
            {
                this.sqlCommand = new SqlCommand(query, this.sqlConnection);
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        this.sqlCommand.Parameters.Add(param);
                    }
                }
                    
                this.executedQueryMode = AppConstants.DB_CMD_DATASET;
                this.sqlDataReader = this.sqlCommand.ExecuteReader();
                int fieldCount = this.sqlDataReader.VisibleFieldCount;
                String[] column; 
                while (this.sqlDataReader.Read())
                {
                    column = new String[fieldCount];
                    for (int i = 0; i < fieldCount; i++)
                    {
                        column[i] = (this.sqlDataReader[i].ToString());
                    }
                    hashData[this.sqlDataReader[0]] = column;
                }

                
                if (hashData.Keys != null)
                {
                    this.executedQueryStatus = AppConstants.DB_CMD_EXECUTED;
                }
                else
                {
                    this.executedQueryStatus = AppConstants.DB_CMD_NOT_EXECUTED;
                }
            }
            catch (Exception e)
            {
                this.executedQueryStatus = AppConstants.DB_CMD_ERROR;
            }
            finally
            {
                this.doCloseConnection();
            }
            return hashData;
        }

        private void doOpenConnection() 
        {
            try
            {
                this.sqlConnection = new SqlConnection(AppConstants.DB_URL);
                this.sqlConnection.Open();
                this.connectionStatus = AppConstants.DB_CONN_OPEN;
            }
            catch (Exception e)
            {
                this.connectionStatus = AppConstants.DB_CONN_ERROR;
            }
        }

        private void doCloseConnection()
        {
            if (this.sqlConnection != null)
            {
                try
                {
                    this.doDisposeTransaction();
                    this.doDisposeCommand();
                    this.doCloseDataReader();
                    this.sqlConnection.Close();
                    this.connectionStatus = AppConstants.DB_CONN_CLOSE;
                }
                catch (Exception e)
                {
                    this.connectionStatus = AppConstants.DB_CONN_ERROR;
                }
            }
            else
            {
                this.connectionStatus = AppConstants.DB_CONN_OPEN;
            }
        }

        private void doCloseDataReader()
        {
            if (sqlDataReader != null)
            {
                try
                {
                    sqlDataReader.Close();
                    this.dataReaderStatus = AppConstants.DB_DREADER_CLOSE;
                }
                catch (Exception e)
                {
                    this.dataReaderStatus = AppConstants.DB_DREADER_NOT_CLOSE;
                }
            }
            else
            {
                this.dataReaderStatus = AppConstants.DB_DREADER_NOT_INITIALIZED;
            }
        }

        private void doDisposeCommand()
        {
            if (sqlCommand != null)
            {
                try{
                    sqlCommand.Dispose();
                    this.commandStatus = AppConstants.DB_CMD_DISPOSED;
                }
                catch(Exception e){
                    this.commandStatus = AppConstants.DB_CMD_NOT_DISPOSED;
                }
                
            }
            else
            {
                this.commandStatus = AppConstants.DB_CMD_NOT_INITIALIZED;
            }
        }

        private void doDisposeTransaction()
        {
            if (sqlTransaction != null)
            {
                try
                {
                    sqlTransaction.Dispose();
                    this.transactionStatus = AppConstants.DB_CMD_TR_DISPOSED;
                }
                catch (Exception e)
                {
                    this.transactionStatus = AppConstants.DB_CMD_TR_NOT_DISPOSED;
                }
            }
            else
            {
                this.transactionStatus = AppConstants.DB_CMD_TR_NOT_INITIALIZED;
            }
        }

    }
}
