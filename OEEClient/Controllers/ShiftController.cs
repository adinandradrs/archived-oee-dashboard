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
    public class ShiftController : Controller
    {
        private Shift shift;
        private ShiftBase shiftBase;

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
            shift = new Shift();
            shift.description = Request["description"];
            if (Request["isActive"].Equals("true"))
            {
                shift.isActive = 1;
            }
            else
            {
                shift.isActive = 0;
            }
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                shift.createdBy = new User();
                shift.createdBy.userId = userGroupLogin.userId.userId;
            }
            else
            {
                shift.createdBy = new User();
                shift.createdBy.userId = "SYS";
            }
            shift.started = int.Parse(Request["started"].Replace(":",""));
            shift.ended = int.Parse(Request["ended"].Replace(":", ""));
            try
            {
                shiftBase = new ShiftBase(shift);
                shiftBase.doAdd();
                if (shiftBase.executed != 0)
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
            shift = new Shift();
            shift.shiftId = int.Parse(Request["shiftId"]);
            shift.description = Request["description"];
            if (Request["isActive"].Equals("true"))
            {
                shift.isActive = 1;
            }
            else
            {
                shift.isActive = 0;
            }
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                shift.modifiedBy = new User();
                shift.modifiedBy.userId = userGroupLogin.userId.userId;
            }
            else
            {
                shift.modifiedBy = new User();
                shift.modifiedBy.userId = "SYS";
            }
            shift.started = int.Parse(Request["started"].Replace(":", ""));
            shift.ended = int.Parse(Request["ended"].Replace(":", ""));
            try{
                shiftBase = new ShiftBase(shift);
                shiftBase.doUpdate();
                if(shiftBase.executed != 0)
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
            shift = new Shift();
            shift.shiftId = int.Parse(Request["shiftId"]);
            try{
                shiftBase = new ShiftBase(shift);
                shiftBase.doDelete();
                if(shiftBase.executed != 0)
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
            shift = new Shift();
            shiftBase = new ShiftBase(shift);
            shiftBase.doSelect();
            int pageIndex = Convert.ToInt32(int.Parse(Request["page"])) - 1;
            int pageSize = int.Parse(Request["rows"]);
            int totalRecords = shiftBase.shiftList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var data = shiftBase.shiftList
                            .Skip(pageIndex * pageSize)
                            .Take(pageSize);

            var result = new { total = totalPages, page = int.Parse(Request["page"]), records = totalRecords, rows = data };
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public List<Shift> getShiftMenu()
        {
            List<Shift> shiftMenu = new List<Shift>();
            DBManager dbManager = new DBManager();
            Hashtable hashData = dbManager.getSourceList(SqlHandler.USER_MENU_SHIFT_ISACTIVE, null);
            foreach (String[] values in hashData.Values)
            {
                shift = new Shift();
                shift.shiftId = int.Parse(values[0]);
                shift.description = values[1];
                shift.started = int.Parse(values[2]);
                shift.ended = int.Parse(values[3]);
                shiftMenu.Add(shift);
            }
            return shiftMenu;
        }

    }
}
