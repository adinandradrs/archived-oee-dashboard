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
    public class WorkcellBase : AccessLayer, IAccessLayer
    {

        private Workcell workcell;
        public List<Workcell> workcellList = new List<Workcell>();

        public WorkcellBase(Workcell workcell)
        {
            this.workcell = workcell;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        public void doAdd()
        {
            dbManager = this.getDbManager();
            query = Workcell.WORKCELL_I;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@machineId",workcell.machineId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@description", workcell.description));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@lineId", workcell.lineId.lineId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", workcell.isActive));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@createdBy", workcell.createdBy.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cAvailable", workcell.cAvailable));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cPerformance", workcell.cPerformance));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cQuality", workcell.cQuality));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wAvailable", workcell.wAvailable));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wPerformance", workcell.wPerformance));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wQuality", workcell.wQuality));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doUpdate()
        {
            dbManager = this.getDbManager();
            query = Workcell.WORKCELL_U;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@machineUid", workcell.machineUid));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@description", workcell.description));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@lineId", workcell.lineId.lineId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", workcell.isActive));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@modifiedBy", workcell.modifiedBy.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cAvailable", workcell.cAvailable));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cPerformance", workcell.cPerformance));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cQuality", workcell.cQuality));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wAvailable", workcell.wAvailable));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wPerformance", workcell.wPerformance));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wQuality", workcell.wQuality));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doDelete()
        {
            dbManager = this.getDbManager();
            query = Workcell.WORKCELL_D;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@machineUid", workcell.machineUid));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doSelect()
        {
            dbManager = this.getDbManager();
            query = Workcell.WORKCELL_S;
            hashData = dbManager.getSourceList(query, null);
            foreach (String[] values in hashData.Values)
            {
                Workcell workcell = new Workcell();
                workcell.machineUid = int.Parse(values[0]);
                workcell.machineId = values[1];
                workcell.description = values[2];
                if (values[3].Equals("True"))
                {
                    workcell.isActive = 1;
                }
                else
                {
                    workcell.isActive = 0;
                }
                workcell.createdBy = new User();
                workcell.createdBy.userId = values[4];
                workcell.createdAt = DateTime.Parse(values[5]);
                if (values[6].Length > 0)
                {
                    workcell.modifiedAt = DateTime.Parse(values[6]);
                    workcell.modifiedBy = new User();
                    workcell.modifiedBy.userId = values[7];
                }
                workcell.lineId = new Line();
                workcell.lineId.lineId = int.Parse(values[8]);
                workcell.lineId.description = values[9];
                if (values[10].Length > 0)
                {
                    workcell.cAvailable = int.Parse(values[10]);
                }
                if (values[11].Length > 0)
                {
                    workcell.cPerformance = int.Parse(values[11]);
                }
                if (values[12].Length > 0)
                {
                    workcell.cQuality = int.Parse(values[12]);
                }
                if (values[13].Length > 0)
                {
                    workcell.wAvailable = int.Parse(values[13]);
                }
                if (values[14].Length > 0)
                {
                    workcell.wPerformance = int.Parse(values[14]);
                }
                if (values[15].Length > 0)
                {
                    workcell.wQuality = int.Parse(values[15]);
                }
                workcellList.Add(workcell);
            }
        }
    }
}
