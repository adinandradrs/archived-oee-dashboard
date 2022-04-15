using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class Group
    {
        public int groupId { set; get; }
        public string description { set; get; }
        public int isActive { set; get; }
        public int isSys { set; get; }
        public DateTime createdAt { set; get; }
        public User createdBy { set; get; }
        public DateTime modifiedAt { set; get; }
        public User modifiedBy { set; get; }

        public static string GROUP_I = @"INSERT INTO [FDB].[dbo].[sys_group]
                                               ([description]
                                               ,[isactive]
                                               ,[issys]
                                               ,[createdby]])
                                         VALUES
                                               (@description
                                               ,@isActive
                                               ,@isSys
                                               ,@createdBy)";
        public static string GROUP_D = @"DELETE FROM [FDB].[dbo].[sys_group] 
                                         WHERE groupid = @groupId";
        public static string GROUP_U = @"UPDATE [FDB].[dbo].[sys_group]
                                         SET [description] = @description
                                              ,[isactive] = @isActive
                                              ,[issys] = @isSys
                                              ,[modifiedat] = SYSDATETIME()
                                              ,[modifiedby] = @modifiedBy
                                         WHERE groupid = @groupId";
        public static string GROUP_S = @"SELECT [groupid]
                                              ,[description]
                                              ,[isactive]
                                              ,[issys]
                                              ,[createdat]
                                              ,[createdby]
                                              ,[modifiedat]
                                              ,[modifiedby]
                                         FROM [FDB].[dbo].[sys_group]";
        public static string GROUP_S_ID = @"SELECT [groupid]
                                              ,[description]
                                              ,[isactive]
                                              ,[issys]
                                              ,[createdat]
                                              ,[createdby]
                                              ,[modifiedat]
                                              ,[modifiedby]
                                            FROM [FDB].[dbo].[sys_group]
                                            WHERE groupid = @groupId";
    }
}
