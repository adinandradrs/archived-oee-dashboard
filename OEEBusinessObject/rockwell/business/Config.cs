using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class Config
    {
        public int configId { set; get; }
        public string parameter { set; get; }
        public string value { set; get; }

        //scanrate
        public static string CONFIG_S_ID = @"SELECT [configid]
                                            ,[parameter]
                                            ,[value]
                                            FROM [FDB].[dbo].[sys_config]
                                            WHERE configid = @configId";
        public static string CONFIG_U = @"UPDATE [FDB].[dbo].[sys_config]
                                            SET [value] = @scanRate
                                            WHERE configid = @configId";
    }

   
}
