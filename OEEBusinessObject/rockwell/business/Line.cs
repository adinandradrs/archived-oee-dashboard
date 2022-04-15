using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class Line : Kpi
    {
        public int lineId { set; get; }
        public string description { set; get; }
        public int isActive { set; get; }
        public DateTime createdAt { set; get; }
        public User createdBy { set; get; }
        public DateTime modifiedAt { set; get; }
        public User modifiedBy { set; get; }

        public static string LINE_I = @"INSERT INTO [FDB].[dbo].[tm_line]
                                               ([description]
                                               ,[isactive]
                                               ,[createdby]
                                               ,[createdAt]
                                               ,[cavailable]
                                               ,[cperformance]
                                               ,[cquality],[wavailable]
                                               ,[wperformance]
                                               ,[wquality])
                                         VALUES
                                               (@description
                                               ,@isActive
                                               ,@createdBy
                                               ,SYSDATETIME(),@cAvailable,@cPerformance,@cQuality,@wAvailable,@wPerformance,@wQuality);";
        public static string LINE_D = @"DELETE FROM [FDB].[dbo].[tm_line] WHERE lineid = @lineId;";
        public static string LINE_U = @"UPDATE [FDB].[dbo].[tm_line] 
                                         SET description=@description, isactive=@isActive, modifiedby=@modifiedBy, modifiedAt=SYSDATETIME(), cavailable=@cAvailable, cperformance=@cPerformance, cquality=@cQuality,
                                         wavailable=@wAvailable, wperformance=@wPerformance, wquality=@wQuality
                                         WHERE lineid=@lineId;";
        public static string LINE_S = @"SELECT lineid, description, isactive,createdby,createdat, modifiedby, modifiedat, cavailable, cperformance, cquality,wavailable, wperformance, wquality 
                                         FROM [FDB].[dbo].[tm_line];";
    }
}
