using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class Shift
    {
        public int shiftId { set; get; }
        public string description { set; get; }
        public int isActive { set; get; }
        public DateTime createdAt { set; get; }
        public User createdBy { set; get; }
        public DateTime modifiedAt { set; get; }
        public User modifiedBy { set; get; }
        public int started { set; get; }
        public int ended { set; get; }
        public string startedString { set; get; }
        public string endedString { set; get; }

        public static string SHIFT_I = @"INSERT INTO [FDB].[dbo].[tm_shift]
                                               ([description]
                                               ,[isactive]
                                               ,[createdby]
                                               ,[createdAt]
                                               ,[started]
                                               ,[ended])
                                         VALUES
                                               (@description
                                               ,@isActive
                                               ,@createdBy
                                               ,SYSDATETIME(),@started,@ended);";
        public static string SHIFT_D = @"DELETE FROM [FDB].[dbo].[tm_shift] WHERE shiftid = @shiftId;";
        public static string SHIFT_U = @"UPDATE [FDB].[dbo].[tm_shift] 
                                         SET description=@description, isactive=@isActive, modifiedby=@modifiedBy, modifiedAt=SYSDATETIME(), started=@started, ended=@ended
                                         WHERE shiftid=@shiftId;";
        public static string SHIFT_S = @"SELECT shiftid, description, isactive,createdby,createdat, modifiedby, modifiedat,started,ended 
                                         FROM [FDB].[dbo].[tm_shift];";
    }
}
