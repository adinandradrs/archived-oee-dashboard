using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OEEComponent.iface;
using OEEComponent.baselayer;
using OEEBusinessObject.rockwell.business;
using System.Collections;
using OEEComponent.utilities;

namespace OEEBusinessObject.rockwell.datalayer
{
    public class ShiftBase : AccessLayer, IAccessLayer
    {
        private Shift shift = null;
        public List<Shift> shiftList = new List<Shift>();

        public ShiftBase(Shift shift)
        {
            this.shift = shift;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        public void doAdd()
        {
            dbManager = new DBManager();
            query = Shift.SHIFT_I;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@description", shift.description));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", shift.isActive));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@createdBy", shift.createdBy.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@started", shift.started));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@ended", shift.ended));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doUpdate()
        {
            dbManager = new DBManager();
            query = Shift.SHIFT_U;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@shiftId", shift.shiftId)) ;
            parameters.Add(new System.Data.SqlClient.SqlParameter("@description", shift.description));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", shift.isActive));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@modifiedBy", shift.modifiedBy.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@started", shift.started));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@ended", shift.ended));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doDelete()
        {
            dbManager = new DBManager();
            query = Shift.SHIFT_D;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@shiftId", shift.shiftId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doSelect()
        {
            dbManager = new DBManager();
            query = Shift.SHIFT_S;
            hashData = dbManager.getSourceList(query, null);
            foreach (String[] values in hashData.Values)
            {
                Shift shift = new Shift();
                shift.shiftId = int.Parse(values[0]);
                shift.description = values[1];
                if (values[2].Equals("True"))
                {
                    shift.isActive = 1;
                }
                else
                {
                    shift.isActive = 0;
                }
                if (values[3].Length > 0)
                {
                    shift.createdBy = new User();
                    shift.createdBy.userId = values[3];
                    shift.createdAt = DateTime.Parse(values[4]);
                }
                if (values[5].Length > 0)
                {
                    shift.modifiedBy = new User();
                    shift.modifiedBy.userId = values[5];
                    shift.modifiedAt = DateTime.Parse(values[6]);
                }
                shift.started = int.Parse(values[7]);
                if (values[7].Length < 4)
                {
                    shift.startedString = 0 + values[7].Substring(0, 1) + ":" + values[7].Substring(1, values[7].Length-1);
                }
                else
                {
                    shift.startedString = values[7].Substring(0, 2) + ":" + values[7].Substring(2, values[7].Length-2);
                }
                shift.ended = int.Parse(values[8]);
                if (values[8].Length < 4)
                {
                    shift.endedString = 0 + values[8].Substring(0, 1) + ":" + values[8].Substring(1, values[8].Length-1);
                }
                else
                {
                    shift.endedString = values[8].Substring(0, 2) + ":" + values[8].Substring(2, values[8].Length-2);
                }
                shiftList.Add(shift);
            }
        }
    }
}
