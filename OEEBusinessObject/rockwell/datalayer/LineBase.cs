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
    public class LineBase : AccessLayer, IAccessLayer
    {
        private Line line = null;
        public List<Line> lineList = new List<Line>();

        public LineBase(Line line)
        {
            this.line = line;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        public void doAdd()
        {
            dbManager = new DBManager();
            query = Line.LINE_I;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@description", line.description));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", line.isActive));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@createdBy", line.createdBy.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cAvailable", line.cAvailable));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cPerformance", line.cPerformance));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cQuality", line.cQuality));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wAvailable", line.wAvailable));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wPerformance", line.wPerformance));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wQuality", line.wQuality));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doUpdate()
        {
            dbManager = new DBManager();
            query = Line.LINE_U;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@lineId", line.lineId)) ;
            parameters.Add(new System.Data.SqlClient.SqlParameter("@description", line.description));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", line.isActive));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@modifiedBy", line.modifiedBy.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cAvailable", line.cAvailable));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cPerformance", line.cPerformance));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@cQuality", line.cQuality));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wAvailable", line.wAvailable));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wPerformance", line.wPerformance));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@wQuality", line.wQuality));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doDelete()
        {
            dbManager = new DBManager();
            query = Line.LINE_D;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@lineId", line.lineId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doSelect()
        {
            dbManager = new DBManager();
            query = Line.LINE_S;
            hashData = dbManager.getSourceList(query, null);
            foreach (String[] values in hashData.Values)
            {
                Line line = new Line();
                line.lineId = int.Parse(values[0]);
                line.description = values[1];
                if (values[2].Equals("True"))
                {
                    line.isActive = 1;
                }
                else
                {
                    line.isActive = 0;
                }
                if (values[3].Length > 0)
                {
                    line.createdBy = new User();
                    line.createdBy.userId = values[3];
                    line.createdAt = DateTime.Parse(values[4]);
                }
                if (values[5].Length > 0)
                {
                    line.modifiedBy = new User();
                    line.modifiedBy.userId = values[5];
                    line.modifiedAt = DateTime.Parse(values[6]);
                }
                if (values[7].Length > 0)
                {
                    line.cAvailable = int.Parse(values[7]);
                }
                if (values[8].Length > 0)
                {
                    line.cPerformance = int.Parse(values[8]);
                }
                if (values[9].Length > 0)
                {
                    line.cQuality = int.Parse(values[9]);
                }

                if (values[10].Length > 0)
                {
                    line.wAvailable = int.Parse(values[10]);
                }
                if (values[11].Length > 0)
                {
                    line.wPerformance = int.Parse(values[11]);
                }
                if (values[12].Length > 0)
                {
                    line.wQuality = int.Parse(values[12]);
                }
                lineList.Add(line);
            }
        }
    }
}
