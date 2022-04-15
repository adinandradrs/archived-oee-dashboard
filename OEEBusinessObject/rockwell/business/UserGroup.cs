using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class UserGroup
    {
        public int userGroupId { set; get; }
        public User userId { set; get; }
        public Group groupId { set; get; }
        public int isActive { set; get; }
        public DateTime createdAt { set; get; }
        public User createdBy { set; get; }
        public DateTime modifiedAt { set; get; }
        public User modifiedBy { set; get; }

        public static string USERGROUP_I = @"INSERT INTO [FDB].[dbo].[sys_usergroup]
                                               ([userid]
                                               ,[groupid]
                                               ,[isactive]
                                               ,[createdby])
                                             VALUES
                                                (@userId
                                                ,@groupId
                                                ,@isActive
                                                ,@createdBy)";
        public static string USERGROUP_D = @"DELETE FROM [FDB].[dbo].[sys_usergroup]
                                             WHERE usergroupid = @userGroupId";
        public static string USERGROUP_U = @"UPDATE [FDB].[dbo].[sys_usergroup]
                                               SET [userid] = @userId
                                                  ,[groupid] = @groupId
                                                  ,[isactive] = @isActive
                                                  ,[modifiedat] = SYSDATETIME()
                                                  ,[modifiedby] = @modifiedBy
                                             WHERE usergroupid = @userGroupId";
        public static string USERGROUP_S = @"select ug.usergroupid, 
	                                            g.groupid,g.description, 
	                                            u.useruid, u.userId, u.isActive, u.firstname, u.lastname, u.password, g.isActive,
                                                ug.isActive 
                                             from sys_usergroup ug
                                             join sys_user u on u.userid = ug.userid
                                             join sys_group g on g.groupid = ug.groupid";

    }
}
