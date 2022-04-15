using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OEEComponent.iface;
using OEEBusinessObject.rockwell.business;
using OEEComponent.baselayer;
using OEEComponent.utilities;

namespace OEEBusinessObject.rockwell.datalayer
{
    public class UserGroupBase : AccessLayer, IAccessLayer
    {
        private UserGroup userGroup;
        public List<UserGroup> userGroupList = new List<UserGroup>();

        public UserGroupBase(UserGroup userGroup)
        {
            this.userGroup = userGroup;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        public void doAdd()
        {
            dbManager = this.getDbManager();
            query = UserGroup.USERGROUP_I;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userId", userGroup.userId.userId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@groupId", userGroup.groupId.groupId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", userGroup.isActive));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@createdBy", userGroup.createdBy.userId));
            executed = dbManager.getExecutedSQL(query, parameters); 
        }

        public void doUpdate()
        {
            dbManager = this.getDbManager();
            query = UserGroup.USERGROUP_U;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userId", userGroup.userId.userId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@groupId", userGroup.groupId.groupId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", userGroup.isActive));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@modifiedBy", userGroup.modifiedBy.userId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userGroupId", userGroup.userGroupId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doDelete()
        {
            dbManager = this.getDbManager();
            query = UserGroup.USERGROUP_D;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userGroupId", userGroup.userGroupId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doSelect()
        {
            dbManager = this.getDbManager();
            query = UserGroup.USERGROUP_S;
            hashData = dbManager.getSourceList(query, null);
            foreach (String[] values in hashData.Values)
            {
                UserGroup userGroup = new UserGroup();
                userGroup.userId = new User();
                userGroup.groupId = new Group();
                userGroup.userGroupId = int.Parse(values[0]);
                userGroup.groupId.groupId = int.Parse(values[1]);
                userGroup.groupId.description = values[2];
                userGroup.userId.userUid = int.Parse(values[3]);
                userGroup.userId.userId = values[4];
                if (values[5].Equals("True"))
                {
                    userGroup.userId.isActive = 1;
                }
                else
                {
                    userGroup.userId.isActive = 0;
                }
                userGroup.userId.firstName = values[6];
                userGroup.userId.lastName = values[7];
                userGroup.userId.password = values[8];
                if (values[9].Equals("True"))
                {
                    userGroup.groupId.isActive = 1;
                }
                else
                {
                    userGroup.groupId.isActive = 0;
                }
                if (values[10].Equals("True"))
                {
                    userGroup.isActive = 1;
                }
                else
                {
                    userGroup.isActive = 0;
                }
                userGroupList.Add(userGroup);
            }
        }

    }
}
