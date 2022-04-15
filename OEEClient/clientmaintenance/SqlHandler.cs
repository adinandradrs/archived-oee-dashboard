using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OEEClient.clientmaintenance
{
    public class SqlHandler
    {
        #region "User Maintenance"
        public static string USERGROUP_COUNT_USERID_GROUPID     = @"select count(*) as total 
                                                                    from usergroup 
                                                                    where userid=@userId and groupid=@groupId;";
        public static string USER_LOGIN                         = @"select ug.usergroupid,
	                                                                    g.groupid,g.description, 
	                                                                    u.useruid, u.userId, u.isActive, u.firstname, u.lastname, u.password, g.isActive,
                                                                        ug.isActive 
                                                                    from sys_usergroup ug
                                                                    join sys_user u on u.userid = ug.userid
                                                                    join sys_group g on g.groupid = ug.groupid
                                                                    where u.password = @password and u.userid = @userId; ";
        public static string USER_LOGIN_TIME_UPDATE             = @"update sys_user set loginat = sysdatetime() where userid=@userId and password=@password;";
        public static string USER_MENU_SHIFT_ISACTIVE           = @"select shiftid, description, started, ended from tm_shift where isactive=1;";

        public static string USER_MENU_LINE_ISACTIVE            = @"select lineId, description from tm_line where isactive = 1;";
        public static string USER_MENU_WORKCELL_ISACTIVE = @"select machineuid, machineId,lineId from tm_workcell where isactive = 1 and lineid = @lineId;";
        #endregion

        #region "Workcell Maintenance"
        public static string TAGMAP_BY_TAGTYPE_MACHINEID = @"SELECT count(*)
                                                             from [FDB].[dbo].[tm_tagmap] WHERE machineId=@machineId and tagType=@tagType;
                                                            ";

        public static string TAGMAP_BY_MACHINEID = @"SELECT [tagmapid]
                                              ,[plctag]
                                              ,[isactive]
                                              ,[createdat]
                                              ,[createdby]
                                              ,[modifiedat]
                                              ,[modifiedby]
                                              ,[machineid],[tagType]
                                            FROM [FDB].[dbo].[tm_tagmap]
                                            WHERE machineid = @machineId";

        #endregion

        #region "Dashboard"
        
        public static string WC_AV_LTS = @" select tm_logtag.value, tm_workcell.cavailable, tm_workcell.wavailable from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_logtag.logtagid = 
                                            (select MAX(tm_logtag.logtagid) from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_workcell.machineuid = @machineUid 
                                            and tm_tagmap.tagtype = 'Available');";

        public static string WC_PE_LTS = @"select tm_logtag.value, tm_workcell.cavailable, tm_workcell.wavailable from tm_logtag
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_logtag.logtagid = 
                                            (select MAX(tm_logtag.logtagid) from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_workcell.machineuid = @machineUid 
                                            and tm_tagmap.tagtype = 'Performance');";

        public static string WC_QY_LTS = @" select tm_logtag.value, tm_workcell.cavailable, tm_workcell.wavailable from tm_logtag
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_logtag.logtagid = 
                                            (select MAX(tm_logtag.logtagid) from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_workcell.machineuid = @machineUid 
                                            and tm_tagmap.tagtype = 'Quality');";


        public static string WC_AV_YST = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid
                                            AND tm_tagmap.tagtype = 'Available' AND
                                            cast(tm_logtag.createdat as date) = cast (dateadd(day,-1,getdate()) as date);";

        public static string WC_PE_YST = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit                                            
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Performance' AND
                                            cast(tm_logtag.createdat as date) = cast (dateadd(day,-1,getdate()) as date);";

        public static string WC_QY_YST = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Quality' AND
                                            cast(tm_logtag.createdat as date) = cast (dateadd(day,-1,getdate()) as date);";

        public static string WC_AV_WEE = @" SELECT
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Available' AND
                                            tm_logtag.createdat >= dateadd(week,-1,getdate());";

        public static string WC_PE_WEE = @" SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Performance' AND
                                            tm_logtag.createdat >= dateadd(week,-1,getdate());";

        public static string WC_QY_WEE = @" SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Quality' AND
                                            tm_logtag.createdat >= dateadd(week,-1,getdate());";

        public static string WC_AV_MON = @" SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Available' AND
                                            tm_logtag.createdat >= dateadd(month,-1,getdate());";

        public static string WC_PE_MON = @" SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Performance' AND
                                            tm_logtag.createdat >= dateadd(month,-1,getdate());";

        public static string WC_QY_MON = @" SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Quality' AND
                                            tm_logtag.createdat >= dateadd(month,-1,getdate());";

        public static string WC_AV_YEA = @" SELECT
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Available' AND
                                            tm_logtag.createdat >= dateadd(year,-1,getdate());";

        public static string WC_PE_YEA = @" SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Performance' AND
                                            tm_logtag.createdat >= dateadd(year,-1,getdate());";

        public static string WC_QY_YEA = @" SELECT
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            WHERE tm_workcell.machineuid = @machineUid 
                                            AND tm_tagmap.tagtype = 'Quality' AND 
                                            tm_logtag.createdat >= dateadd(year,-1,getdate());";

//        public static string WC_AV_SHI = @"SELECT 
//                                            AVG(CAST(tm_logtag.value AS int)) as value,  
//                                            max(tm_workcell.cavailable) as topLimit, 
//                                            min(tm_workcell.wavailable) as btmLimit 
//
//                                            FROM tm_shift, tm_logtag 
//                                            INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
//                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
//                                            WHERE tm_workcell.machineuid = @machineUid
//                                            AND tm_tagmap.tagtype = 'Available' AND
//                                            tm_logtag.createdat > DATEADD (HOUR, 
//                                                                (select (tm_shift.started - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100
//                                                                from tm_shift
//                                                                where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                                                and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
//                                            and tm_logtag.createdat < DATEADD (HOUR, 
//                                                                (select (tm_shift.ended - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
//                                                                where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                                                and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE());";

        public static string WC_AV_SHI = @"SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
                                                max(tm_workcell.cavailable) as topLimit, 
                                                min(tm_workcell.wavailable) as btmLimit
                                                from tm_logtag
                                                INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag
                                                INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                                INNER JOIN 
                                                (select 
	                                                top 1 
		                                                cast(DATEPART(YEAR,GETDATE()) as varchar) + '-' + cast(DATEPART(M,GETDATE()) as varchar) + '-' + cast(DATEPART(DAY,GETDATE()) as varchar) +
		                                                ' ' +  
		                                                case 
			                                                when LEN(cast(started as varchar)) = 3 then
				                                                '0' + substring(cast(started as varchar),1,1) + ':00'
			                                                when LEN(cast(started as varchar)) = 4 then
				                                                substring(cast(started as varchar),1,2) + ':' + substring(cast(started as varchar),3,LEN(cast(started as varchar)))
		                                                end as started,
		                                                cast(DATEPART(YEAR,GETDATE()) as varchar) + '-' + cast(DATEPART(M,GETDATE()) as varchar) + '-' + cast(DATEPART(DAY,GETDATE()) as varchar) +
		                                                ' ' +  
		                                                case 
			                                                when LEN(cast(ended as varchar)) = 3 then
				                                                '0' + substring(cast(ended as varchar),1,1) + ':00'
			                                                when LEN(cast(ended as varchar)) = 4 then
				                                                substring(cast(ended as varchar),1,2) + ':' + substring(cast(ended as varchar),3,LEN(cast(ended as varchar)))
		                                                end as ended
	                                                from tm_shift
	                                                where 
		                                                ended >= (
			                                                select cast(cast(DATEPART(hour, sysdatetime()) as varchar) + 
			                                                case 
				                                                when LEN(cast(DATEPART(MI,sysdatetime()) as varchar)) > 1 then 
					                                                (cast(DATEPART(MI,sysdatetime()) as varchar))
				                                                else
					                                                '0' + (cast(DATEPART(MI,sysdatetime()) as varchar))	
				                                                end
			                                                as int))
	                                                order by shiftid) as shift
                                                on tm_logtag.createdat>= shift.started and tm_logtag.createdat<=shift.ended
                                                where tm_workcell.machineuid = @machineUid
                                                AND tm_tagmap.tagtype = 'Available';";

//        public static string WC_PE_SHI = @"SELECT 
//                                            AVG(CAST(tm_logtag.value AS int)) as value,  
//                                            max(tm_workcell.cavailable) as topLimit, 
//                                            min(tm_workcell.wavailable) as btmLimit 
//
//                                            FROM tm_shift, tm_logtag 
//                                            INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
//                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
//                                            WHERE tm_workcell.machineuid = @machineUid
//                                            AND tm_tagmap.tagtype = 'Performance' AND
//                                            tm_logtag.createdat > DATEADD (HOUR, 
//                                                                (select (tm_shift.started - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100
//                                                                from tm_shift
//                                                                where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                                                and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
//                                            and tm_logtag.createdat < DATEADD (HOUR, 
//                                                                (select (tm_shift.ended - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
//                                                                where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                                                and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE());";
        public static string WC_PE_SHI = @"SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
                                                max(tm_workcell.cavailable) as topLimit, 
                                                min(tm_workcell.wavailable) as btmLimit
                                                from tm_logtag
                                                INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag
                                                INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                                INNER JOIN 
                                                (select 
	                                                top 1 
		                                                cast(DATEPART(YEAR,GETDATE()) as varchar) + '-' + cast(DATEPART(M,GETDATE()) as varchar) + '-' + cast(DATEPART(DAY,GETDATE()) as varchar) +
		                                                ' ' +  
		                                                case 
			                                                when LEN(cast(started as varchar)) = 3 then
				                                                '0' + substring(cast(started as varchar),1,1) + ':00'
			                                                when LEN(cast(started as varchar)) = 4 then
				                                                substring(cast(started as varchar),1,2) + ':' + substring(cast(started as varchar),3,LEN(cast(started as varchar)))
		                                                end as started,
		                                                cast(DATEPART(YEAR,GETDATE()) as varchar) + '-' + cast(DATEPART(M,GETDATE()) as varchar) + '-' + cast(DATEPART(DAY,GETDATE()) as varchar) +
		                                                ' ' +  
		                                                case 
			                                                when LEN(cast(ended as varchar)) = 3 then
				                                                '0' + substring(cast(ended as varchar),1,1) + ':00'
			                                                when LEN(cast(ended as varchar)) = 4 then
				                                                substring(cast(ended as varchar),1,2) + ':' + substring(cast(ended as varchar),3,LEN(cast(ended as varchar)))
		                                                end as ended
	                                                from tm_shift
	                                                where 
		                                                ended >= (
			                                                select cast(cast(DATEPART(hour, sysdatetime()) as varchar) + 
			                                                case 
				                                                when LEN(cast(DATEPART(MI,sysdatetime()) as varchar)) > 1 then 
					                                                (cast(DATEPART(MI,sysdatetime()) as varchar))
				                                                else
					                                                '0' + (cast(DATEPART(MI,sysdatetime()) as varchar))	
				                                                end
			                                                as int))
	                                                order by shiftid) as shift
                                                on tm_logtag.createdat>= shift.started and tm_logtag.createdat<=shift.ended
                                                where tm_workcell.machineuid = @machineUid
                                                AND tm_tagmap.tagtype = 'Performance';";

//        public static string WC_QY_SHI = @"SELECT 
//                                            AVG(CAST(tm_logtag.value AS int)) as value,  
//                                            max(tm_workcell.cavailable) as topLimit, 
//                                            min(tm_workcell.wavailable) as btmLimit 
//
//                                            FROM tm_shift, tm_logtag 
//                                            INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
//                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
//                                            WHERE tm_workcell.machineuid = @machineUid
//                                            AND tm_tagmap.tagtype = 'Quality' AND
//                                            tm_logtag.createdat > DATEADD (HOUR, 
//                                                                (select (tm_shift.started - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100
//                                                                from tm_shift
//                                                                where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                                                and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
//                                            and tm_logtag.createdat < DATEADD (HOUR, 
//                                                                (select (tm_shift.ended - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
//                                                                where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                                                and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE());";

        public static string WC_QY_SHI = @"SELECT 
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
                                                max(tm_workcell.cavailable) as topLimit, 
                                                min(tm_workcell.wavailable) as btmLimit
                                                from tm_logtag
                                                INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag
                                                INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                                INNER JOIN 
                                                (select 
	                                                top 1 
		                                                cast(DATEPART(YEAR,GETDATE()) as varchar) + '-' + cast(DATEPART(M,GETDATE()) as varchar) + '-' + cast(DATEPART(DAY,GETDATE()) as varchar) +
		                                                ' ' +  
		                                                case 
			                                                when LEN(cast(started as varchar)) = 3 then
				                                                '0' + substring(cast(started as varchar),1,1) + ':00'
			                                                when LEN(cast(started as varchar)) = 4 then
				                                                substring(cast(started as varchar),1,2) + ':' + substring(cast(started as varchar),3,LEN(cast(started as varchar)))
		                                                end as started,
		                                                cast(DATEPART(YEAR,GETDATE()) as varchar) + '-' + cast(DATEPART(M,GETDATE()) as varchar) + '-' + cast(DATEPART(DAY,GETDATE()) as varchar) +
		                                                ' ' +  
		                                                case 
			                                                when LEN(cast(ended as varchar)) = 3 then
				                                                '0' + substring(cast(ended as varchar),1,1) + ':00'
			                                                when LEN(cast(ended as varchar)) = 4 then
				                                                substring(cast(ended as varchar),1,2) + ':' + substring(cast(ended as varchar),3,LEN(cast(ended as varchar)))
		                                                end as ended
	                                                from tm_shift
	                                                where 
		                                                ended >= (
			                                                select cast(cast(DATEPART(hour, sysdatetime()) as varchar) + 
			                                                case 
				                                                when LEN(cast(DATEPART(MI,sysdatetime()) as varchar)) > 1 then 
					                                                (cast(DATEPART(MI,sysdatetime()) as varchar))
				                                                else
					                                                '0' + (cast(DATEPART(MI,sysdatetime()) as varchar))	
				                                                end
			                                                as int))
	                                                order by shiftid) as shift
                                                on tm_logtag.createdat>= shift.started and tm_logtag.createdat<=shift.ended
                                                where tm_workcell.machineuid = @machineUid
                                                AND tm_tagmap.tagtype = 'Quality';";

        //line
        public static string LN_AV_LTS = @" select tm_logtag.value, tm_line.cavailable, tm_line.wavailable 
                                            from tm_logtag inner join tm_tagmap on tm_logtag.plctag = tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid = tm_tagmap.machineid
                                            inner join tm_line on tm_workcell.lineid = tm_line.lineid
                                            where tm_logtag.logtagid = 
                                                (select MAX(tm_logtag.logtagid)
                                                from tm_logtag inner join tm_tagmap on tm_logtag.plctag = tm_tagmap.plctag 
                                                inner join tm_workcell on tm_workcell.machineid = tm_tagmap.machineid
                                                inner join tm_line on tm_workcell.lineid = tm_line.lineid
                                                where tm_line.lineid = @lineId
                                                and tm_tagmap.tagtype = 'Available');";

        public static string LN_PE_LTS = @"select tm_logtag.value, tm_line.cperformance, tm_line.wperformance 
                                            from tm_logtag inner join tm_tagmap on tm_logtag.plctag = tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid = tm_tagmap.machineid
                                            inner join tm_line on tm_workcell.lineid = tm_line.lineid
                                            where tm_logtag.logtagid = 
                                                (select MAX(tm_logtag.logtagid)
                                                from tm_logtag inner join tm_tagmap on tm_logtag.plctag = tm_tagmap.plctag 
                                                inner join tm_workcell on tm_workcell.machineid = tm_tagmap.machineid
                                                inner join tm_line on tm_workcell.lineid = tm_line.lineid
                                                where tm_line.lineid = @lineId
                                                and tm_tagmap.tagtype = 'Performance');";

        public static string LN_QY_LTS = @"select tm_logtag.value, tm_line.cquality, tm_line.wquality 
                                            from tm_logtag inner join tm_tagmap on tm_logtag.plctag = tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid = tm_tagmap.machineid
                                            inner join tm_line on tm_workcell.lineid = tm_line.lineid
                                            where tm_logtag.logtagid = 
                                                (select MAX(tm_logtag.logtagid)
                                                from tm_logtag inner join tm_tagmap on tm_logtag.plctag = tm_tagmap.plctag 
                                                inner join tm_workcell on tm_workcell.machineid = tm_tagmap.machineid
                                                inner join tm_line on tm_workcell.lineid = tm_line.lineid
                                                where tm_line.lineid = @lineId
                                                and tm_tagmap.tagtype = 'Quality');";

        public static string LN_AV_YST = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cavailable) as topLimit, 
	                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Available' AND
                                            cast(tm_logtag.createdat as date) = cast(dateadd(day,-1,getdate()) as date);";

        public static string LN_PE_YST = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cperformance) as topLimit, 
	                                            min(tm_line.wperformance) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Performance' AND
                                            cast(tm_logtag.createdat as date) = cast(dateadd(day,-1,getdate()) as date);";

        public static string LN_QY_YST = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cquality) as topLimit, 
	                                            min(tm_line.wquality) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Quality' AND
                                            cast(tm_logtag.createdat as date) = cast(dateadd(day,-1,getdate()) as date);";

        public static string LN_AV_WEE = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cavailable) as topLimit, 
	                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Available' AND
                                            tm_logtag.createdat >= dateadd(week,-1,getdate());";

        public static string LN_PE_WEE = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cperformance) as topLimit, 
	                                            min(tm_line.wperformance) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Performance' AND
                                            tm_logtag.createdat >= dateadd(week,-1,getdate());";

        public static string LN_QY_WEE = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cquality) as topLimit, 
	                                            min(tm_line.wquality) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Quality' AND
                                            tm_logtag.createdat >= dateadd(week,-1,getdate());";

        public static string LN_AV_MON = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cavailable) as topLimit, 
	                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Available' AND
                                            tm_logtag.createdat >= dateadd(month,-1,getdate());";

        public static string LN_PE_MON = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cperformance) as topLimit, 
	                                            min(tm_line.wperformance) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Performance' AND
                                            tm_logtag.createdat >= dateadd(month,-1,getdate());";

        public static string LN_QY_MON = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cquality) as topLimit, 
	                                            min(tm_line.wquality) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Quality' AND
                                            tm_logtag.createdat >= dateadd(month,-1,getdate());";

        public static string LN_AV_YEA = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cavailable) as topLimit, 
	                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Available' AND
                                            tm_logtag.createdat >= dateadd(year,-1,getdate());";

        public static string LN_PE_YEA = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cperformance) as topLimit, 
	                                            min(tm_line.wperformance) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Performance' AND
                                            tm_logtag.createdat >= dateadd(year,-1,getdate());";

        public static string LN_QY_YEA = @" SELECT 
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_line.cquality) as topLimit, 
	                                            min(tm_line.wquality) as btmLimit 
                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Quality' AND
                                            tm_logtag.createdat >= dateadd(year,-1,getdate());";

        public static string LN_AV_SHI = @"SELECT 
                                            	AVG(CAST(tm_logtag.value AS int)) as value,  
                                            	max(tm_line.cavailable) as topLimit, 
                                            	min(tm_line.wavailable) as btmLimit 
                                            FROM tm_shift, tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
                                            AND tm_tagmap.tagtype = 'Available' 
                                            AND
                                            tm_logtag.createdat > DATEADD (HOUR, (select (tm_shift.started - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100
                                            from tm_shift
                                            where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
                                            and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
                                            and tm_logtag.createdat < DATEADD (HOUR, (select 
                                            (tm_shift.ended - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
                                            where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
                                            and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE());";

//        public static string LN_PE_SHI = @"SELECT 
//                                            	AVG(CAST(tm_logtag.value AS int)) as value,  
//                                            	max(tm_line.cavailable) as topLimit, 
//                                            	min(tm_line.wavailable) as btmLimit 
//                                            FROM tm_shift, tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
//                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
//                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
//                                            WHERE tm_line.lineid = @lineId
//                                            AND tm_tagmap.tagtype = 'Performance' 
//                                            AND
//                                            tm_logtag.createdat > DATEADD (HOUR, (select (tm_shift.started - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100
//                                            from tm_shift
//                                            where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                            and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
//                                            and tm_logtag.createdat < DATEADD (HOUR, (select 
//                                            (tm_shift.ended - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
//                                            where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                            and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE());";

//        public static string LN_QY_SHI = @"SELECT 
//                                            	AVG(CAST(tm_logtag.value AS int)) as value,  
//                                            	max(tm_line.cavailable) as topLimit, 
//                                            	min(tm_line.wavailable) as btmLimit 
//                                            FROM tm_shift, tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
//                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
//                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
//                                            WHERE tm_line.lineid = @lineId
//                                            AND tm_tagmap.tagtype = 'Quality' 
//                                            AND
//                                            tm_logtag.createdat > DATEADD (HOUR, (select (tm_shift.started - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100
//                                            from tm_shift
//                                            where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                            and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
//                                            and tm_logtag.createdat < DATEADD (HOUR, (select 
//                                            (tm_shift.ended - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
//                                            where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                            and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE());";


        //WC OEE
        public static string WC_OEE_LST = @" SELECT tm_logtag.logtagid,
                                                        tm_logtag.value, tm_workcell.cavailable, tm_workcell.wavailable from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_logtag.logtagid in  
                                            (select MAX(tm_logtag.logtagid) as logtagid from tm_logtag 
                                            inner join tm_tagmap on tm_logtag.plctag=tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid=tm_tagmap.machineid
                                            where tm_workcell.machineuid = @machineUid 
                                            and tm_tagmap.tagtype in ('Available','Performance','Quality')
                                            group by tm_tagmap.tagtype);";

        public static string WC_OEE_YST = @" SELECT MAX(tm_logtag.logtagid),
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
                                                max(tm_workcell.cavailable) as topLimit, 
                                                min(tm_workcell.wavailable) as btmLimit 
                                                FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                                INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                                WHERE tm_workcell.machineuid = @machineUid
                                                AND cast(tm_logtag.createdat as date) = cast(dateadd(day,-1,getdate()) as date)
                                                AND tm_tagmap.tagtype in ('Available','Performance','Quality')
                                                group by tm_tagmap.tagtype;";

        public static string WC_OEE_WEE = @" SELECT MAX(tm_logtag.logtagid),
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
                                                max(tm_workcell.cavailable) as topLimit, 
                                                min(tm_workcell.wavailable) as btmLimit 
                                                FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                                INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                                WHERE tm_workcell.machineuid = @machineUid
                                                AND tm_logtag.createdat >= dateadd(week,-1,getdate())
                                                AND tm_tagmap.tagtype in ('Available','Performance','Quality')
                                                group by tm_tagmap.tagtype;";

        public static string WC_OEE_MON = @" SELECT MAX(tm_logtag.logtagid),
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
                                                max(tm_workcell.cavailable) as topLimit, 
                                                min(tm_workcell.wavailable) as btmLimit 
                                                FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                                INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                                WHERE tm_workcell.machineuid = @machineUid
                                                AND tm_logtag.createdat >= dateadd(month,-1,getdate())
                                                AND tm_tagmap.tagtype in ('Available','Performance','Quality')
                                                group by tm_tagmap.tagtype;";

        public static string WC_OEE_YEA = @" SELECT MAX(tm_logtag.logtagid),
                                                AVG(CAST(tm_logtag.value AS int)) as value,  
                                                max(tm_workcell.cavailable) as topLimit, 
                                                min(tm_workcell.wavailable) as btmLimit 
                                                FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                                INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                                WHERE tm_workcell.machineuid = @machineUid
                                                AND tm_logtag.createdat >= dateadd(year,-1,getdate())
                                                AND tm_tagmap.tagtype in ('Available','Performance','Quality')
                                                group by tm_tagmap.tagtype;";

//        public static string WC_OEE_SHI = @" SELECT MAX(tm_logtag.logtagid),
//                                                AVG(CAST(tm_logtag.value AS int)) as value,  
//                                                max(tm_workcell.cavailable) as topLimit, 
//                                                min(tm_workcell.wavailable) as btmLimit 
//                                                FROM tm_shift, tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
//                                                INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
//                                                WHERE tm_workcell.machineuid = @machineUid
//                                                AND 
//                                                    tm_logtag.createdat > DATEADD (HOUR, (select (tm_shift.started - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100
//                                                                                    from tm_shift
//                                                                                    where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                                                                    and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
//                                                    and tm_logtag.createdat < DATEADD (HOUR, (select 
//                                                                                    (tm_shift.ended - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
//                                                                                    where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
//                                                                                    and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
//                                                AND tm_tagmap.tagtype in ('Available','Performance','Quality')
//                                                group by tm_tagmap.tagtype;";

        public static string WC_OEE_SHI = @" SELECT MAX(tm_logtag.logtagid),
	                                            AVG(CAST(tm_logtag.value AS int)) as value,  
	                                            max(tm_workcell.cavailable) as topLimit, 
	                                            min(tm_workcell.wavailable) as btmLimit 
	                                            FROM tm_logtag INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
	                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
	                                            INNER JOIN 
	                                            (select 
		                                            top 1 
		                                                cast(DATEPART(YEAR,GETDATE()) as varchar) + '-' + cast(DATEPART(M,GETDATE()) as varchar) + '-' + cast(DATEPART(DAY,GETDATE()) as varchar) +
		                                                ' ' +  
		                                                case 
			                                                when LEN(cast(started as varchar)) = 3 then
				                                                '0' + substring(cast(started as varchar),1,1) + ':00'
			                                                when LEN(cast(started as varchar)) = 4 then
				                                                substring(cast(started as varchar),1,2) + ':' + substring(cast(started as varchar),3,LEN(cast(started as varchar)))
		                                                end as started,
		                                                cast(DATEPART(YEAR,GETDATE()) as varchar) + '-' + cast(DATEPART(M,GETDATE()) as varchar) + '-' + cast(DATEPART(DAY,GETDATE()) as varchar) +
		                                                ' ' +  
		                                                case 
			                                                when LEN(cast(ended as varchar)) = 3 then
				                                                '0' + substring(cast(ended as varchar),1,1) + ':00'
			                                                when LEN(cast(ended as varchar)) = 4 then
				                                                substring(cast(ended as varchar),1,2) + ':' + substring(cast(ended as varchar),3,LEN(cast(ended as varchar)))
		                                                end as ended
		                                            from tm_shift
		                                            where 
			                                            ended >= (
				                                            select cast(cast(DATEPART(hour, sysdatetime()) as varchar) + 
				                                            case 
					                                            when LEN(cast(DATEPART(MI,sysdatetime()) as varchar)) > 1 then 
						                                            (cast(DATEPART(MI,sysdatetime()) as varchar))
					                                            else
						                                            '0' + (cast(DATEPART(MI,sysdatetime()) as varchar))	
					                                            end
				                                            as int))
		                                            order by shiftid) as shift
	                                            on tm_logtag.createdat>= shift.started and tm_logtag.createdat<=shift.ended
	                                            WHERE tm_workcell.machineuid = @machineUid
	                                            AND tm_tagmap.tagtype in ('Available','Performance','Quality')
	                                            group by tm_tagmap.tagtype;";

        public static string LN_OEE_LTS = @"select tm_logtag.logtagid,
                                            tm_logtag.value, tm_line.cavailable, tm_line.wavailable 
	                                        from tm_logtag 
	                                        inner join tm_tagmap on tm_logtag.plctag = tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid = tm_tagmap.machineid
                                            inner join tm_line on tm_workcell.lineid = tm_line.lineid
                                            where tm_logtag.logtagid in 
		                                    (select MAX(tm_logtag.logtagid) as logtagid
		                                    from tm_logtag inner join tm_tagmap on tm_logtag.plctag = tm_tagmap.plctag 
                                            inner join tm_workcell on tm_workcell.machineid = tm_tagmap.machineid
                                            inner join tm_line on tm_workcell.lineid = tm_line.lineid
			                                    where tm_line.lineid = @lineId
                                                and tm_tagmap.tagtype in ('Available','Performance','Quality')
			                                    group by tm_tagmap.tagtype);";



        public static string LN_OEE_YST = @"SELECT MAX(tm_logtag.logtagid),
                                            AVG(CAST(tm_logtag.value AS int)) as value,  
                                            max(tm_line.cavailable) as topLimit, 
                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_logtag 
                                            INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
	                                            AND cast(tm_logtag.createdat as date) = cast(dateadd(day,-1,getdate()) as date)
	                                            and tm_tagmap.tagtype in ('Available','Performance','Quality')
	                                            group by tm_tagmap.tagtype;";

        public static string LN_OEE_WEE = @"SELECT MAX(tm_logtag.logtagid),
                                            AVG(CAST(tm_logtag.value AS int)) as value,  
                                            max(tm_line.cavailable) as topLimit, 
                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_logtag 
                                            INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
	                                            AND tm_logtag.createdat >= dateadd(week,-1,getdate())
	                                            and tm_tagmap.tagtype in ('Available','Performance','Quality')
	                                            group by tm_tagmap.tagtype;";

        public static string LN_OEE_MON = @"SELECT MAX(tm_logtag.logtagid),
                                            AVG(CAST(tm_logtag.value AS int)) as value,  
                                            max(tm_line.cavailable) as topLimit, 
                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_logtag 
                                            INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
	                                            AND tm_logtag.createdat >= dateadd(month,-1,getdate())
	                                            and tm_tagmap.tagtype in ('Available','Performance','Quality')
	                                            group by tm_tagmap.tagtype;";

        public static string LN_OEE_YEA = @"SELECT MAX(tm_logtag.logtagid),
                                            AVG(CAST(tm_logtag.value AS int)) as value,  
                                            max(tm_line.cavailable) as topLimit, 
                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_logtag 
                                            INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
	                                            AND tm_logtag.createdat >= dateadd(year,-1,getdate())
	                                            and tm_tagmap.tagtype in ('Available','Performance','Quality')
	                                            group by tm_tagmap.tagtype;";

        public static string LN_OEE_SHI = @"SELECT MAX(tm_logtag.logtagid),
                                            AVG(CAST(tm_logtag.value AS int)) as value,  
                                            max(tm_line.cavailable) as topLimit, 
                                            min(tm_line.wavailable) as btmLimit 
                                            FROM tm_shift, tm_logtag 
                                            INNER JOIN tm_tagmap ON tm_logtag.plctag = tm_tagmap.plctag 
                                            INNER JOIN tm_workcell ON tm_workcell.machineid = tm_tagmap.machineid 
                                            INNER JOIN tm_line on tm_workcell.lineid = tm_line.lineid
                                            WHERE tm_line.lineid = @lineId
	                                            AND tm_logtag.createdat > DATEADD (HOUR, (select (tm_shift.started - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
                                                where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
                                                and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
                                                and tm_logtag.createdat < DATEADD (HOUR, (select 
                                                (tm_shift.ended - (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())))/100 from tm_shift
                                                where (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) > tm_shift.started
                                                and (datepart(hour,GETDATE())*100 + DATEPART(minute,getdate())) < tm_shift.ended), GETDATE())
	                                            AND tm_tagmap.tagtype in ('Available','Performance','Quality')
	                                            group by tm_tagmap.tagtype;";


        #endregion

    }
}