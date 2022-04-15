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
    public class GroupBase : AccessLayer, IAccessLayer
    {
        private Group group;
        public List<Group> groupList = new List<Group>();

        public GroupBase(Group group)
        {
            this.group = group;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        //obsolete
        /*
        public void doAdd()
        {
            dbManager = this.getDbManager();
            query = Group.GROUP_I;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@description", group.description));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", group.isActive));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isSys", group.isSys));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@createdBy", group.createdBy.userId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        //obsolete
        public void doUpdate()
        {
            dbManager = this.getDbManager();
            query = Group.GROUP_U;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@description", group.description));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", group.isActive));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@isSys", group.isSys));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@modifiedBy", group.modifiedBy.userId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        //obsolete
        public void doDelete()
        {
            dbManager = this.getDbManager();
            query = Group.GROUP_D;
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@groupId",group.groupId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }
        */

        public void doSelect()
        {
            dbManager = this.getDbManager();
            query = Group.GROUP_S;
            hashData = dbManager.getSourceList(query, null);
            foreach(String[] values in hashData.Values){
                Group group = new Group();
                group.groupId = int.Parse(values[0]);
                group.description = values[1];
                if (values[2].Equals("True"))
                    group.isActive = 1;
                else
                    group.isActive = 0;
                if (values[3].Equals("True"))
                    group.isSys = 1;
                else
                    group.isSys = 0;
                group.createdAt = DateTime.Parse(values[4]);
                group.createdBy = new User();
                group.createdBy.userId = values[5];
                int len = values[6].Length;
                if (values[6].Length > 1)
                {
                    group.modifiedAt = DateTime.Parse(values[6]);
                }
                if (values[7].Length > 1)
                {
                    group.modifiedBy = new User();
                    group.modifiedBy.userId = values[7];
                }
                groupList.Add(group);
            }
        }

        public void doAdd()
        {
            throw new NotImplementedException();
        }

        public void doUpdate()
        {
            throw new NotImplementedException();
        }

        public void doDelete()
        {
            throw new NotImplementedException();
        }
    }
}
