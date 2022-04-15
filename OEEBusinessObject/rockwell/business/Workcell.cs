using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class Workcell : Kpi
    {
        public int machineUid { set; get; }
        public string machineId { set; get; }
        public string description { set; get; }
        public Line lineId { set; get; }
        public int isActive { set; get; }
        public DateTime createdAt { set; get; }
        public User createdBy { set; get; }
        public DateTime modifiedAt { set; get; }
        public User modifiedBy { set; get; }

        public static string WORKCELL_I = @"INSERT INTO [FDB].[dbo].[tm_workcell]
                                                       ([machineid]
                                                       ,[description]
                                                       ,[lineid]
                                                       ,[isactive]
                                                       ,[createdat]
                                                       ,[createdby],cAvailable,cPerformance,cQuality,wAvailable,wPerformance,wQuality)
                                                 VALUES
                                                       (@machineId
                                                       ,@description
                                                       ,@lineId
                                                       ,@isActive
                                                       ,SYSDATETIME()
                                                       ,@createdBy,@cAvailable,@cPerformance,@cQuality,@wAvailable,@wPerformance,@wQuality)
                                            ";
        public static string WORKCELL_D = @"DELETE FROM [FDB].[dbo].[tm_workcell] 
                                            WHERE machineuid = @machineUid";
        public static string WORKCELL_U = @"UPDATE [FDB].[dbo].[tm_workcell]
                                               SET [description] = @description
                                                  ,[lineid] = @lineId
                                                  ,[isactive] = @isActive
                                                  ,[modifiedat] = SYSDATETIME()
                                                  ,[modifiedby] = @modifiedBy,cAvailable=@cAvailable,cPerformance=@cPerformance,cQuality=@cQuality,wAvailable=@wAvailable,wPerformance=@wPerformance,wQuality=@wQuality
                                             WHERE machineuid = @machineUid
                                            ";
        public static string WORKCELL_S = @"select 
	                                            wc.machineuid, wc.machineid, wc.description,
	                                            wc.isactive, wc.createdby, wc.createdat, 
	                                            wc.modifiedat, wc.modifiedby, l.lineid, l.description, wc.cAvailable, wc.cPerformance, wc.cQuality,wc.wAvailable, wc.wPerformance, wc.wQuality  
                                            from tm_workcell wc
                                            join tm_line l on wc.lineid = l.lineid;";
    }
}
