using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class GaugeValue : Kpi
    {
        public string machineUid { set; get; }
        public int tagValue { set; get; }
        public int targetValue { set; get; }
        public int warningValue { set; get; }

        public static string WC_AV_KPI = @"select tm_workcell.cavailable, tm_workcell.wavailable
                                            from tm_workcell where tm_workcell.machineuid = '15';";

        public static string WC_PE_KPI = @"select tm_workcell.cperformance, tm_workcell.wperformance
                                            from tm_workcell where tm_workcell.machineuid = '15';";

        public static string WC_QY_KPI = @"select tm_workcell.cquality, tm_workcell.wquality
                                            from tm_workcell where tm_workcell.machineuid = '15';";

        public static string WC_AV_NOW = @"select tm_logtag.value from tm_logtag 
                                            where tm_logtag.logtagid = 
                                            (select MAX(tm_logtag.logtagid) from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_workcell.machineuid = '15' 
                                            and tm_tagmap.tagtype = 'Available');";

        public static string WC_PE_NOW = @"select tm_logtag.value from tm_logtag 
                                            where tm_logtag.logtagid = 
                                            (select MAX(tm_logtag.logtagid) from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_workcell.machineuid = '15' 
                                            and tm_tagmap.tagtype = 'Performance');";

        public static string WC_QY_NOW = @"select tm_logtag.value from tm_logtag 
                                            where tm_logtag.logtagid = 
                                            (select MAX(tm_logtag.logtagid) from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_workcell.machineuid = '15' 
                                            and tm_tagmap.tagtype = 'Quality');";

        public static string WC_KPI = @"SELECT tm_workcell.cavailable, tm_workcell.cperformance, tm_workcell.cquality
                                        FROM tm_workcell WHERE tm_workcell.machineuid='15';";

        public static string WC_AV_YST = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Available'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 2;";

        public static string WC_PE_YST = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Performance'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 2;";

        public static string WC_QY_YST = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Quality'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 2;";

        public static string WC_AV_WEE = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Available'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 8;";

        public static string WC_PE_WEE = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Performance'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 8;";

        public static string WC_QY_WEE = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Quality'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 8;";

        public static string WC_AV_MON = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Available'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 31;";

        public static string WC_PE_MON = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Performance'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 31;";

        public static string WC_QY_MON = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Quality'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 31;";

        public static string WC_AV_YEA = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Available'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 366;";

        public static string WC_PE_YEA = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Performance'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 366;";

        public static string WC_QY_YEA = @"SELECT AVG(CAST(tm_logtag.value AS int)), tm_workcell.cavailable, tm_workcell.wavailable
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = '15' 
                                            AND tm_tagmap.tagtype = 'Quality'
                                            AND DATEDIFF( d,tm_logtag.createdat,getdate()) <= 366;";

    }
}
