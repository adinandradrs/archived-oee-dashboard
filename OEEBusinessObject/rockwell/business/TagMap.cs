using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class TagMap
    {
        public int tagMapId { set; get; }
        public string plcTag { set; get; }
        public int isActive { set; get; }
        public string tagType { set; get; }
        public int mttr { set; get; }
        public DateTime createdAt { set; get; }
        public User createdBy { set; get; }
        public DateTime modifiedAt { set; get; }
        public User modifiedBy { set; get; }
        public string description { set; get; }
        public Workcell machineId { set; get; }

        public static string TAGMAP_I = @"INSERT INTO [FDB].[dbo].[tm_tagmap]
                                               ([plctag]
                                               ,[isactive]
                                               ,[createdby]
                                               ,[machineid],[tagType])
                                         VALUES
                                               (@plcTag
                                               ,@isActive
                                               ,@createdBy
                                               ,@machineId, @tagType)";
        public static string TAGMAP_D = @"DELETE FROM [FDB].[dbo].[tm_tagmap] 
                                         WHERE tagmapid = @tagMapId";
        public static string TAGMAP_U = @"UPDATE [FDB].[dbo].[tm_tagmap]
                                         SET [plctag] = @plcTag
                                              ,[isactive] = @isActive
                                              ,[modifiedat] = SYSDATETIME()
                                              ,[modifiedby] = @modifiedBy, [tagType]=@tagType
                                         WHERE tagmapid = @tagMapId";
        public static string TAGMAP_S = @"SELECT [tagmapid]
                                              ,[plctag]
                                              ,[isactive]
                                              ,[createdat]
                                              ,[createdby]
                                              ,[modifiedat]
                                              ,[modifiedby]
                                              ,[machineid],[tagType]
                                         FROM [FDB].[dbo].[tm_tagmap]";
        public static string TAGMAP_S_ID = @"SELECT [tagmapid]
                                              ,[plctag]
                                              ,[description]
                                              ,[isactive]
                                              ,[createdat]
                                              ,[createdby]
                                              ,[modifiedat]
                                              ,[modifiedby],[tagType]
                                            FROM [FDB].[dbo].[tm_tagmap]
                                            WHERE tagmapid = @tagMapId";
    }
}
