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
    public class LineController : Controller
    {
        private User user;
        private Line line;
        private LineBase lineBase;

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
            return View();
        }

        public ActionResult doAddRequest()
        {
            line = new Line();
            line.description = Request["description"];
            line.cAvailable = int.Parse(Request["cAvailable"]);
            line.cPerformance = int.Parse(Request["cPerformance"]);
            line.cQuality = int.Parse(Request["cQuality"]);
            line.wAvailable = int.Parse(Request["wAvailable"]);
            line.wPerformance = int.Parse(Request["wPerformance"]);
            line.wQuality = int.Parse(Request["wQuality"]);
            if (Request["isActive"].Equals("true"))
            {
                line.isActive = 1;
            }
            else
            {
                line.isActive = 0;
            }
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                line.createdBy = new User();
                line.createdBy.userId = userGroupLogin.userId.userId;
            }
            else
            {
                line.createdBy = new User();
                line.createdBy.userId = "SYS";
            }
            try
            {
                lineBase = new LineBase(line);
                lineBase.doAdd();
                if (lineBase.executed != 0)
                {
                    return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "failed", msg = "server cannot process the data : " }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult doUpdateRequest()
        {
            line = new Line();
            line.lineId = int.Parse(Request["lineId"]);
            line.description = Request["description"];
            line.cAvailable = int.Parse(Request["cAvailable"]);
            line.cPerformance = int.Parse(Request["cPerformance"]);
            line.cQuality = int.Parse(Request["cQuality"]);
            line.wAvailable = int.Parse(Request["wAvailable"]);
            line.wPerformance = int.Parse(Request["wPerformance"]);
            line.wQuality = int.Parse(Request["wQuality"]);
            if (Request["isActive"].Equals("true"))
            {
                line.isActive = 1;
            }
            else
            {
                line.isActive = 0;
            }
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                line.modifiedBy = new User();
                line.modifiedBy.userId = userGroupLogin.userId.userId;
            }
            else
            {
                line.modifiedBy = new User();
                line.modifiedBy.userId = "SYS";
            }
            try{
                lineBase = new LineBase(line);
                lineBase.doUpdate();
                if(lineBase.executed != 0)
                {
                    return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "failed", msg = "server cannot process the data : " }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e){
                return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult doDeleteRequest()
        {
            line = new Line();
            line.lineId = int.Parse(Request["lineId"]);
            try{
                lineBase = new LineBase(line);
                lineBase.doDelete();
                if(lineBase.executed != 0)
                {
                    return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "failed", msg = "server cannot process the data : " }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e){
                return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult doFetchRequest()
        {
            line = new Line();
            lineBase = new LineBase(line);
            lineBase.doSelect();
            //
            int pageIndex = Convert.ToInt32(int.Parse(Request["page"])) - 1;
            int pageSize = int.Parse(Request["rows"]);
            int totalRecords = lineBase.lineList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var data = lineBase.lineList
                            .Skip(pageIndex * pageSize)
                            .Take(pageSize);

            var result = new { total = totalPages, page = int.Parse(Request["page"]), records = totalRecords, rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public List<Line> getLineMenu()
        {
            List<Line> lineMenu = new List<Line>();
            DBManager dbManager = new DBManager();
            Hashtable hashData = dbManager.getSourceList(SqlHandler.USER_MENU_LINE_ISACTIVE, null);
            foreach (String[] values in hashData.Values)
            {
                line = new Line();
                line.lineId = int.Parse(values[0]);
                line.description = values[1];
                lineMenu.Add(line);
            }
            return lineMenu;
        }

    }
}
