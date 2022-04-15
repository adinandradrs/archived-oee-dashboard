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

namespace OEEClient.Controllers
{
    public class WorkcellController : Controller
    {

        private Line line;
        private Workcell workcell;
        private LineBase lineBase;
        private WorkcellBase workcellBase;
        private List<TagMap> tagMapList = new List<TagMap>();


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

        public List<Line> getLines()
        {
            line = new Line();
            lineBase = new LineBase(line);
            lineBase.doSelect();
            return lineBase.lineList;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult doAddRequest()
        {
            workcell = new Workcell();
            workcell.machineId = Request["machineId"];
            workcell.description = Request["description"];
            workcell.cAvailable = int.Parse(Request["cAvailable"]);
            workcell.cPerformance = int.Parse(Request["cPerformance"]);
            workcell.cQuality = int.Parse(Request["cQuality"]);
            if (Request["isActive"].Equals("true"))
            {
                workcell.isActive = 1;
            }
            else
            {
                workcell.isActive = 0;
            }
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                workcell.createdBy = new User();
                workcell.createdBy.userId = userGroupLogin.userId.userId;
            }
            else
            {
                workcell.createdBy = new User();
                workcell.createdBy.userId = "SYS";
            }
            workcell.lineId = new Line();
            workcell.lineId.lineId = int.Parse(Request["lineId"]);
            workcellBase = new WorkcellBase(workcell);
            try
            {
                workcellBase.doAdd();
                if (workcellBase.executed != 0)
                {
                    return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "failed", msg = "server cannot process the data" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult doUpdateRequest()
        {
            workcell = new Workcell();
            workcell.machineUid = int.Parse(Request["machineUid"]);
            workcell.description = Request["description"];
            workcell.cAvailable = int.Parse(Request["cAvailable"]);
            workcell.cPerformance = int.Parse(Request["cPerformance"]);
            workcell.cQuality = int.Parse(Request["cQuality"]);
            workcell.wAvailable = int.Parse(Request["wAvailable"]);
            workcell.wPerformance = int.Parse(Request["wPerformance"]);
            workcell.wQuality = int.Parse(Request["wQuality"]);
            if (Request["isActive"].Equals("true"))
            {
                workcell.isActive = 1;
            }
            else
            {
                workcell.isActive = 0;
            }
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                workcell.modifiedBy = new User();
                workcell.modifiedBy.userId = userGroupLogin.userId.userId;
            }
            else
            {
                workcell.modifiedBy = new User();
                workcell.modifiedBy.userId = "SYS";
            }
            workcell.lineId = new Line();
            workcell.lineId.lineId = int.Parse(Request["lineId"]);
            workcellBase = new WorkcellBase(workcell);
            try
            {
                workcellBase.doUpdate();
                if (workcellBase.executed != 0)
                {
                    return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "failed", msg = "server cannot process the data" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult doDeleteRequest()
        {
            workcell = new Workcell();
            workcell.machineUid = int.Parse(Request["machineUid"]);
            try
            {
                workcellBase = new WorkcellBase(workcell);
                workcellBase.doDelete();
                if (workcellBase.executed != 0)
                {
                    return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "failed", msg = "server cannot process the data" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult doFetchRequest()
        {
            workcell = new Workcell();
            workcellBase = new WorkcellBase(workcell);
            workcellBase.doSelect();
            int pageIndex = Convert.ToInt32(int.Parse(Request["page"])) - 1;
            int pageSize = int.Parse(Request["rows"]);
            int totalRecords = workcellBase.workcellList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var data = workcellBase.workcellList
                            .Skip(pageIndex * pageSize)
                            .Take(pageSize);
            var result = new { total = totalPages, page = int.Parse(Request["page"]), records = totalRecords, rows = data };
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public List<Workcell> getWorkcellMenu(int lineId)
        {
            List<Workcell> workcellMenu = new List<Workcell>();
            DBManager dbManager = new DBManager();
            List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@lineId",lineId));
            Hashtable hashData = dbManager.getSourceList(SqlHandler.USER_MENU_WORKCELL_ISACTIVE, parameters);
            foreach (String[] values in hashData.Values)
            {
                workcell = new Workcell();
                workcell.machineUid = int.Parse(values[0]);
                workcell.machineId = values[1];
                workcell.lineId = new Line();
                workcell.lineId.lineId = int.Parse(values[2]);
                workcellMenu.Add(workcell);
            }
            return workcellMenu;
        }

    }
}
