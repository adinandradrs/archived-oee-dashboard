using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEComponent.maintenance
{
    public class AppConstants
    {
        #region "DB Constants"
        public static String DB_APP_SQLSERVER = "SQL Server 2008 R2";
        public static String DB_APP_ORACLE = "Oracle 11G";
        public static String DB_URL = "Data Source=.;Initial Catalog=FDB;User ID=sa;pwd=heeroyuyISme;";
        public static String DB_CONN_OPEN = "Database connection state is OPEN";
        public static String DB_CONN_CLOSE = "Database connection state is CLOSE";
        public static String DB_CONN_ERROR = "ERROR : Database connection";
        public static String DB_CONN_NOT_INITIALIZE = "Database connection is not initialized yet";
        public static String DB_CMD_DISPOSED = "Sql Command state is DISPOSED";
        public static String DB_CMD_NOT_DISPOSED = "Sql Command state is NOT DISPOSED";
        public static String DB_CMD_NOT_INITIALIZED = "Sql command is not initialized yet";
        public static String DB_CMD_CRUD = "Sql Command is prepared for CRUD";
        public static String DB_CMD_DATASET = "Sql Command is prepared for data set";
        public static String DB_CMD_EXECUTED = "Sql Command state is EXECUTED";
        public static String DB_CMD_NOT_EXECUTED = "Sql Command state is NOT EXECUTED";
        public static String DB_CMD_ERROR = "ERROR : Sql Command";
        public static String DB_CMD_TR_NOT_INITIALIZED = "Sql Command transaction is not initialized yet";
        public static String DB_CMD_TR_ROLLBACK = "Sql Command transaction state is ROLLBACK";
        public static String DB_CMD_TR_COMMIT = "Sql Command transaction state is COMMITED";
        public static String DB_CMD_TR_COMMIT_ERROR = "Error : Sql Command transaction when try to COMMIT";
        public static String DB_CMD_TR_ROLLBACK_ERROR = "Error : Sql Command transaction when try to ROLLBACK";
        public static String DB_CMD_TR_DISPOSED = "Sql Command transaction state is DISPOSED";
        public static String DB_CMD_TR_NOT_DISPOSED = "Sql Command transaction state is NOT DISPOSED";
        public static String DB_DREADER_CLOSE = "Sql Data Reader state is CLOSE";
        public static String DB_DREADER_NOT_CLOSE = "Sql Data Reader state is NOT CLOSE";
        public static String DB_DREADER_NOT_INITIALIZED = "Sql Data Reader is not initialized yet";
        #endregion "DB Constants"

        #region "Web Constants"
        public static String WEB_INACTIVE_USER = "User ID is not activated yet";
        public static String WEB_INACTIVE_GROUP = "Group is not activated yet";
        public static String WEB_INACTIVE_USERGROUP = "User Group is not activated yet";
        public static String WEB_INVALID_LOGIN = "Invalid login";
        public static int IS_ADMIN = 2;
        public static int IS_USER = 3;
        public static String IS_WORKCELL = "workcell";
        public static String IS_LINE = "line";
        public static String IS_LINE_LBL = "Line";
        public static String IS_WORKCELL_LBL = "Workcell";
        #endregion

        #region "Session Constants"
        public static String SESSION_LOGIN_NOTE = "session_login_note";
        public static String SESSION_LOGIN_USERGROUP = "session_login_usergroup";
        #endregion

        #region "OPC Constants"
        public static String OPC_URL = "RSLinx OPC Server";
        public static String OPC_SERVER = "RSLinx";
        #endregion
    }
}
