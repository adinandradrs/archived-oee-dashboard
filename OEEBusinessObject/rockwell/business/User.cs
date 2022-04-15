using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class User
    {
        public int userUid { set; get; }
        public string userId { set; get; }
        public string password { set; get; }
        public int isActive { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public DateTime createdAt { set; get; }
        public User createdBy { set; get; }
        public DateTime modifiedAt { set; get; }
        public User modifiedBy { set; get; }
        public DateTime loginAt { set; get; }

        public static string USER_I = @"INSERT INTO [FDB].[dbo].[sys_user]
                                       ([userid]
                                       ,[password]
                                       ,[firstname]
                                       ,[lastname]
                                       ,[isactive]
                                       ,[createdby])
                                 VALUES
                                       (@userId,
		                               @password,
		                               @firstName,
		                               @lastName,
		                               @isActive,
		                               @createdBy)";
        public static string USER_D = @"DELETE FROM [FDB].[dbo].[sys_user]
                                WHERE useruid = @userUid and userid=@userId";
        public static string USER_U = @"UPDATE [FDB].[dbo].[sys_user]
                               SET [password] = @password
                                  ,[firstname] = @firstName
                                  ,[lastname] = @lastName
                                  ,[isactive] = @isActive
                                  ,[modifiedby] = @modifiedBy
                                  ,[modifiedat] = SYSDATETIME()
                             WHERE userid = @userId and userUid = @userUid";
        public static string USER_S = @"SELECT [useruid]
                              ,[userid]
                              ,[password]
                              ,[firstname]
                              ,[lastname]
                              ,[loginat]
                              ,[isactive]
                              ,[createdby]
                              ,[createdat]
                              ,[modifiedby]
                              ,[modifiedat]
                          FROM [FDB].[dbo].[sys_user]";
        
    }
}
