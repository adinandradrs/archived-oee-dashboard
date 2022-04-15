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
    public class UserBase : AccessLayer, IAccessLayer
    {
        private User user;
        public List<User> userList = new List<User>();

        public UserBase(User user)
        {
            this.user = user;
        }

        public void doAdd()
        {
            dbManager = this.getDbManager();
            query = User.USER_I;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userId", user.userId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@password", user.password));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@firstName", user.firstName));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@lastName", user.lastName));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", user.isActive));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@createdBy", user.createdBy.userId));
            executed = dbManager.getExecutedSQL(query,parameters);
        }

        public void doUpdate()
        {
            dbManager = this.getDbManager();
            query = User.USER_U;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@password", user.password));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@firstName", user.firstName));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@lastName", user.lastName));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", user.isActive));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@modifiedBy", user.modifiedBy.userId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userId", user.userId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userUid", user.userUid));
            executed = dbManager.getExecutedSQL(query, parameters); 
        }

        public void doDelete()
        {
            dbManager = this.getDbManager();
            query = User.USER_D;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userUid",user.userUid));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@userId", user.userId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doSelect()
        {
            dbManager = this.getDbManager();
            query = User.USER_S;
            hashData = dbManager.getSourceList(query, null);
            foreach (String[] values in hashData.Values)
            {
                User user = new User();
                user.userUid = int.Parse(values[0]);
                user.userId = values[1];
                user.password = values[2];
                user.firstName = values[3];
                user.lastName = values[4];
                if (values[5].Length > 0)
                {
                    user.loginAt = DateTime.Parse(values[5]);
                }
                if (values[6].Equals("True"))
                {
                    user.isActive = 1;
                }
                else
                {
                    user.isActive = 0;
                }
                user.createdBy = new User();
                user.createdBy.userId = values[7];
                user.createdAt = DateTime.Parse(values[8]);
                if (values[9].Length > 0)
                {
                    user.modifiedBy = new User();
                    user.modifiedBy.userId = values[9];
                    user.modifiedAt = DateTime.Parse(values[10]);
                }
                userList.Add(user);
            }
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }
    }
}
