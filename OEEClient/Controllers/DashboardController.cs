using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OEEBusinessObject.rockwell.datalayer;
using OEEComponent.iface;
using OEEComponent.baselayer;
using OEEComponent.utilities;
using OEEComponent.maintenance;
using OEEBusinessObject.rockwell.business;
using OEEClient.clientmaintenance;
using System.Collections;
using System.Web.Routing;
using System.Data.SqlClient;

namespace OEEClient.Controllers
{
    public class DashboardController : Controller
    {
        struct style
        {
            public string fill;
            public string stroke;
        }

        struct range
        {
            public int startValue;
            public int endValue;
            public style style;
            public int endWidth;
            public int startWidth;
        }

        struct tick
        {
            public int interval;
            public string size;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }

        public ActionResult NotAllowed()
        {
            return RedirectToRoute("Home/Index");
        }
   
        public ActionResult Index()
        {
            if (Request["type"].Equals(AppConstants.IS_LINE))
            {
                ViewData["param1"] = AppConstants.IS_LINE_LBL;
            }
            else
            {
                ViewData["param1"] = AppConstants.IS_WORKCELL_LBL;    
            }
            ViewData["param2"] = Request["key"];
            return View();
        }

        public ActionResult WidgetAvailable()
        {
            return View();
        }

        public ActionResult WidgetPerformance()
        {
            return View();
        }

        public ActionResult WidgetQuality()
        {
            return View();
        }

        public ActionResult WidgetOee()
        {
            return View();
        }

        public ActionResult Gauge()
        {
            DBManager dbManager = new DBManager();
            string time = "";
            string query = "";
            string gaugeType = Request["gaugeType"];
            int btmValue = 0;
            int topValue = 0;
            int cursor = 0;
            Workcell workcell = new Workcell();
            string mode = "";
            if (Request["machineUid"] != "")
            {
                workcell.machineUid = int.Parse(Request["machineUid"]);
                mode = AppConstants.IS_WORKCELL;
            }
            else
            {
                workcell.lineId = new Line();
                workcell.lineId.lineId = int.Parse(Request["lineId"]);
                mode = AppConstants.IS_LINE;
            }
            //by button
            if (Request["time"] != "")
            {
                time = Request["time"];
                if (time.Equals("latest"))
                {
                    if (gaugeType.Equals("available"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_AV_LTS;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_AV_LTS;
                        }
                    }
                    else if (gaugeType.Equals("performance"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_PE_LTS;
                        }
                        else if(mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_PE_LTS;
                        }
                    }
                    else if (gaugeType.Equals("quality"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_QY_LTS;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_QY_LTS;
                        }
                    }
                    else if (gaugeType.Equals("oee"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_OEE_LST;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_OEE_LTS;
                        }
                    }
                }
                else if (time.Equals("yesterday"))
                {
                    if (gaugeType.Equals("available"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_AV_YST;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_AV_YST;
                        }
                    }
                    else if (gaugeType.Equals("performance"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_PE_YST;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_PE_YST;
                        }
                    }
                    else if (gaugeType.Equals("quality"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_QY_YST;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_QY_YST;
                        }
                    }
                    else if (gaugeType.Equals("oee"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_OEE_YST;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_OEE_YST;
                        }
                    }
                }
                else if (time.Equals("lastweek"))
                {
                    if (gaugeType.Equals("available"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_AV_WEE;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_AV_WEE;
                        }
                    }
                    else if (gaugeType.Equals("performance"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_PE_WEE;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_PE_WEE;
                        }
                    }
                    else if (gaugeType.Equals("quality"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_QY_WEE;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_QY_WEE;
                        }
                    }
                    else if (gaugeType.Equals("oee"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_OEE_WEE;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_OEE_WEE;
                        }
                    }
                }
                else if (time.Equals("lastmonth"))
                {
                    if (gaugeType.Equals("available"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_AV_MON;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_AV_MON;
                        }
                    }
                    else if (gaugeType.Equals("performance"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_PE_MON;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_PE_MON;
                        }
                    }
                    else if (gaugeType.Equals("quality"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_QY_MON;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_QY_MON;
                        }
                    }
                    else if (gaugeType.Equals("oee"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_OEE_MON;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_OEE_MON;
                        }
                    }
                }
                else if (time.Equals("lastyear"))
                {
                    if (gaugeType.Equals("available"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_AV_YEA;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_AV_YEA;
                        }
                    }
                    else if (gaugeType.Equals("performance"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_PE_YEA;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_PE_YEA;
                        }
                    }
                    else if (gaugeType.Equals("quality"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_QY_YEA;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_QY_YEA;
                        }
                    }
                    else if (gaugeType.Equals("oee"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_OEE_YEA;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_OEE_YEA;
                        }
                    }
                }
                else if (time.Equals("shift"))
                {
                    if (gaugeType.Equals("available"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_AV_SHI;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            //query = SqlHandler.LN_AV_SHI;
                        }
                    }
                    else if (gaugeType.Equals("performance"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_PE_SHI;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            //query = SqlHandler.LN_PE_YEA;
                        }
                    }
                    else if (gaugeType.Equals("quality"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_QY_SHI;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            //query = SqlHandler.LN_QY_YEA;
                        }
                    }
                    else if (gaugeType.Equals("oee"))
                    {
                        if (mode.Equals(AppConstants.IS_WORKCELL))
                        {
                            query = SqlHandler.WC_OEE_SHI;
                        }
                        else if (mode.Equals(AppConstants.IS_LINE))
                        {
                            query = SqlHandler.LN_OEE_YEA;
                        }
                    }
                }
            }
            //default
            else
            {
                if (gaugeType.Equals("available"))
                {
                    if (mode.Equals(AppConstants.IS_WORKCELL))
                    {
                        query = SqlHandler.WC_AV_LTS;
                    }
                    else if (mode.Equals(AppConstants.IS_LINE))
                    {
                        query = SqlHandler.LN_AV_LTS;
                    }
                }
                else if(gaugeType.Equals("performance"))
                {
                    if (mode.Equals(AppConstants.IS_WORKCELL))
                    {
                        query = SqlHandler.WC_PE_LTS;
                    }
                    else if (mode.Equals(AppConstants.IS_LINE))
                    {
                        query = SqlHandler.LN_PE_LTS;
                    }
                }
                else if (gaugeType.Equals("quality"))
                {
                    if(mode.Equals(AppConstants.IS_WORKCELL))
                    {
                        query = SqlHandler.WC_QY_LTS;
                    }
                    else if (mode.Equals(AppConstants.IS_LINE))
                    {
                        query = SqlHandler.LN_QY_LTS;
                    }
                }
                else if (gaugeType.Equals("oee"))
                {
                    if (mode.Equals(AppConstants.IS_WORKCELL))
                    {
                        query = SqlHandler.WC_OEE_LST;
                    }
                    else if (mode.Equals(AppConstants.IS_LINE))
                    {
                        query = SqlHandler.LN_OEE_LTS;
                    }
                }
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (mode.Equals(AppConstants.IS_WORKCELL))
            {
                parameters.Add(new SqlParameter("@machineUid", workcell.machineUid));
            }
            else if (mode.Equals(AppConstants.IS_LINE))
            {
                parameters.Add(new SqlParameter("@lineId", workcell.lineId.lineId));
            }
            Hashtable hash = dbManager.getSourceList(query, parameters);

            range[] ranges = new range[3];
            if (hash.Values != null)
            {
                if (!gaugeType.Equals("oee"))
                {
                    foreach (String[] values in hash.Values)
                    {
                        cursor = int.Parse(values[0]);
                        topValue = int.Parse(values[1]);
                        btmValue = int.Parse(values[2]);
                    }
                }
                else
                {
                    int[] cursors = new int[3];
                    int[] topValues = new int[3];
                    int[] btmValues = new int[3];
                    int index = 0;
                    foreach (String[] values in hash.Values)
                    {
                        if (values[1] != null)
                        {
                            cursors[index] = int.Parse(values[1]);
                        }
                        else
                        {
                            cursors[index] = 0;
                        }
                        if (values[2] != null)
                        {
                            topValues[index] = int.Parse(values[2]);
                        }
                        else
                        {
                            topValues[index] = 0;
                        }
                        if (values[3] != null)
                        {
                            btmValues[index] = int.Parse(values[3]);
                        }
                        else
                        {
                            btmValues[index] = 0;
                        }
                        index++;
                    }
                    cursor = 1;
                    foreach(int val in cursors)
                    {
                        cursor *= val;
                    }
                    cursor = cursor / 10000;
                    topValue = 1;
                    foreach (int val in topValues)
                    {
                        topValue *= val;
                    }
                    topValue = topValue / 10000;
                    btmValue = 1;
                    foreach (int val in btmValues)
                    {
                        btmValue *= val;
                    }
                    btmValue = btmValue / 10000;
                }

                ranges[0].endValue = btmValue;
                ranges[0].startValue = 0;

                ranges[0].endWidth = 5;
                ranges[0].startWidth = 1;

                ranges[1].endValue = topValue;
                ranges[1].startValue = btmValue;

                ranges[1].endWidth = 10;
                ranges[1].startWidth = 5;

                ranges[2].endValue = 100;
                ranges[2].startValue = topValue;
            }
            ranges[0].style.fill = "#e02629";
            ranges[0].style.stroke = "#e02629";

            ranges[1].style.fill = "#fbd109";
            ranges[1].style.stroke = "#fbd109";

            ranges[2].style.fill = "#4bb648";
            ranges[2].style.stroke = "#4bb648";
            
            ranges[2].endWidth = 16;
            ranges[2].startWidth = 13;
            int valueFromDb = cursor;
 
            tick tickMinor;
            tickMinor.interval = 5;
            tickMinor.size = "5%";
            
            tick tickMajor;
            tickMajor.interval = 5;
            tickMajor.size = "5%";
            var result = new {
                    max = 100,
                    width = 140,
                    height = 140,
                    value = valueFromDb,
                    ranges = ranges,
                    ticksMinor = tickMinor,
                    ticksMajor = tickMajor,
                    colorScheme = "scheme05"

            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
