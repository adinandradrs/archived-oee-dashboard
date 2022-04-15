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
    public class UserController : Controller
    {
        private Group group;
        private User user;
        private UserGroup userGroup;
        private GroupBase groupBase;
        private UserBase userBase;
        private UserGroupBase userGroupBase;

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

        public List<Group> getGroups()
        {
            group = new Group();
            groupBase = new GroupBase(group);
            groupBase.doSelect();
            return groupBase.groupList;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult doAddRequest()
        {
            user = new User();
            user.userId = Request["userId"];
            user.firstName = Request["firstName"];
            user.lastName = Request["lastName"];
            user.password = Request["password"];
            if (Request["isActive"].Equals("true"))
            {
                user.isActive = 1;
            }
            else
            {
                user.isActive = 0;
            }
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                user.createdBy = new User();
                user.createdBy.userId = userGroupLogin.userId.userId;
            }
            else
            {
                user.createdBy = new User();
                user.createdBy.userId = "SYS";
            }
            group = new Group();
            group.groupId = int.Parse(Request["groupId"]);
            try
            {
                this.doSaveNewUser(user, group);
                if(userGroupBase.executed != 0 )
                {
                    return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                }
                else{
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
            user = new User();
            user.userUid = int.Parse(Request["userUid"]);
            user.userId = Request["userId"];
            userGroup = new UserGroup();
            userGroup.userGroupId = int.Parse(Request["userGroupId"]);
            try
            {
                this.doDeleteUser(user, userGroup);
                if (userGroupBase.executed != 0)
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
            user = new User();
            user.userUid = int.Parse(Request["userUid"]);
            user.userId = Request["userId"];
            user.firstName = Request["firstName"];
            user.lastName = Request["lastName"];
            user.password = Request["password"];
            if (Request["isActive"].Equals("true"))
            {
                user.isActive = 1;
            }
            else
            {
                user.isActive = 0;
            }
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                user.modifiedBy = new User();
                user.modifiedBy.userId = userGroupLogin.userId.userId;
            }
            else
            {
                user.modifiedBy = new User();
                user.modifiedBy.userId = "SYS";
            }
            userGroup = new UserGroup();
            userGroup.userGroupId = int.Parse(Request["userGroupId"]);
            userGroup.userId = user;
            userGroup.isActive = user.isActive;
            userGroup.groupId = new Group();
            userGroup.groupId.groupId = int.Parse(Request["groupId"]);
            userGroup.modifiedBy = user.modifiedBy;
            try
            {
                this.doUpdateUser(user, userGroup);
                if (userGroupBase.executed != 0)
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

        public ActionResult doFetchRequest()
        {
            userGroup = new UserGroup();
            userGroupBase = new UserGroupBase(userGroup);
            userGroupBase.doSelect();
            int pageIndex = Convert.ToInt32(int.Parse(Request["page"])) - 1;
            int pageSize = int.Parse(Request["rows"]);
            int totalRecords = userGroupBase.userGroupList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var data = userGroupBase.userGroupList
                            .Skip(pageIndex * pageSize)
                            .Take(pageSize);

            var result = new { total = totalPages, page = int.Parse(Request["page"]), records = totalRecords, rows = data };
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private bool isUserExistInGroup(UserGroup userGroup)
        {
            DBManager dbManager = new DBManager();
            List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@userId", userGroup.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@groupId", userGroup.groupId));
            Hashtable hashData = dbManager.getSourceList(SqlHandler.USERGROUP_COUNT_USERID_GROUPID, parameters);
            int counter = 0;
            foreach (String[] values in hashData.Values)
            {
                counter = int.Parse(values[0]);
            }
            if (counter > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void doSaveNewUser(User user, Group group)
        {
            userGroup = new UserGroup();
            userGroup.userId = user;
            userGroup.groupId = group;
            userGroup.createdBy = user.createdBy;
            userGroup.isActive = user.isActive;
            if (!isUserExistInGroup(userGroup))
            {
                userBase = new UserBase(user);
                userBase.doAdd();
                if (userBase.executed != 0)
                {
                    userGroupBase = new UserGroupBase(userGroup);
                    userGroupBase.doAdd();
                }
            }
        }

        private void doDeleteUser(User user, UserGroup userGroup)
        {
            userBase = new UserBase(user);
            userGroupBase = new UserGroupBase(userGroup);
            userBase.doDelete();
            if (userBase.executed != 0)
            {
                userGroupBase.doDelete();
            }
        }

        private void doUpdateUser(User user, UserGroup userGroup)
        {
            userBase = new UserBase(user);
            userGroupBase = new UserGroupBase(userGroup);
            userBase.doUpdate();
            if (userBase.executed != 0)
            {
                userGroupBase.doUpdate();
            }
        }

    }
}
