using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OEEComponent.iface;
using OEEComponent.baselayer;
using OEEComponent.utilities;
using OEEBusinessObject.rockwell.business;
using System.Collections;

namespace OEEBusinessObject.rockwell.datalayer
{
    public class LogTagBase : AccessLayer, IAccessLayer
    {
        private LogTag logTag;
        public List<LogTag> tagLogList = new List<LogTag>();

        public LogTagBase(LogTag logTag)
        {
            this.logTag = logTag;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        public void doAdd()
        {
            dbManager = this.getDbManager();
            query = LogTag.LOGTAG_I;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@plcTag", logTag.plctag));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@value", logTag.value));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@createdat", logTag.createdAt));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@createdby", logTag.createdBy.userId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doUpdate()
        {
            throw new NotImplementedException();
        }

        public void doDelete()
        {
            throw new NotImplementedException();
        }

        public void doSelect()
        {
            throw new NotImplementedException();
        }
    }
}
