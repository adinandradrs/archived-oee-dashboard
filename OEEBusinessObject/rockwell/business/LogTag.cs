using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class LogTag
    {
        public int logtagid { set; get; }
        public string plctag { set; get; }
        public string value { set; get; }
        public DateTime createdAt { set; get; }
        public User createdBy { set; get; }

        public static string LOGTAG_I = @"INSERT INTO [FDB].[dbo].[tm_logtag]
                                               ([plctag]
                                               ,[value]
                                               ,[createdat]
                                               ,[createdby]])
                                         VALUES
                                               (@plctag
                                               ,@value
                                               ,@createdat
                                               ,@createdBy)";
        public static string LOGTAG_D = @"DELETE FROM [FDB].[dbo].[tm_logtag] 
                                         WHERE tagmapid = @tagmapid";
        public static string LOGTAG_U = @"UPDATE [FDB].[dbo].[tm_logtag]
                                         SET [plctag] = @plctag
                                              ,[value] = @value
                                              ,[createdat] = SYSDATETIME()
                                              ,[createdby] = @modifiedBy
                                         WHERE logtagid = @logtagid";
        public static string LOGTAG_S = @"SELECT [logtagid]
                                              ,[plctag]
                                              ,[value]
                                              ,[createdat]
                                              ,[createdby]
                                         FROM [FDB].[dbo].[tm_logtag]";
        public static string LOGTAG_S_ID = @"SELECT [logtagid]
                                              ,[plctag]
                                              ,[value]
                                              ,[createdat]
                                              ,[createdby]
                                            FROM [FDB].[dbo].[tm_logtag]
                                            WHERE logtagid = @logtagpid";
    }
}
