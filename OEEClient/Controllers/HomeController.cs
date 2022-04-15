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

namespace OEEClient.Controllers
{
    public class HomeController : Controller
    {
        private Group group;
        private User user;
        private UserGroup userGroup;
        private bool canLogin;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult doLoginRequest()
        {
            user = new User();
            user.userId = Request["userId"];
            user.password = Request["password"];
            this.doLogin(user);
            if (canLogin)
            {
                return RedirectToAction("Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult doLogoutRequest()
        {
            if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        private void doLogin(User user)
        {
            Session.Clear();
            DBManager dbManager = new DBManager();
            List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@userId",user.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@password", user.password));
            Hashtable hashData = dbManager.getSourceList(SqlHandler.USER_LOGIN, parameters);
            if (hashData.Values.Count > 0)
            {
                foreach (String[] values in hashData.Values)
                {
                    userGroup = new UserGroup();
                    userGroup.userId = new User();
                    userGroup.groupId = new Group();
                    userGroup.userGroupId = int.Parse(values[0]);
                    userGroup.groupId.groupId = int.Parse(values[1]);
                    userGroup.groupId.description = values[2];
                    userGroup.userId.userUid = int.Parse(values[3]);
                    userGroup.userId.userId = values[4];
                    if (values[5].Equals("True"))
                    {
                        userGroup.userId.isActive = 1;
                    }
                    else
                    {
                        userGroup.userId.isActive = 0;
                    }
                    userGroup.userId.firstName = values[6];
                    userGroup.userId.lastName = values[7];
                    userGroup.userId.password = values[8];
                    if (values[9].Equals("True"))
                    {
                        userGroup.groupId.isActive = 1;
                    }
                    else
                    {
                        userGroup.groupId.isActive = 0;
                    }
                    if (values[10].Equals("True"))
                    {
                        userGroup.isActive = 1;
                    }
                    else
                    {
                        userGroup.isActive = 0;
                    }
                }
                Session[AppConstants.SESSION_LOGIN_NOTE] = "";
                if (userGroup.groupId.isActive == 0)
                {
                    canLogin = false;
                    Session[AppConstants.SESSION_LOGIN_NOTE] = AppConstants.WEB_INACTIVE_GROUP;
                }
                else
                {
                    canLogin = true;
                }
                if (canLogin)
                {
                    if (userGroup.userId.isActive == 0)
                    {
                        canLogin = false;
                        Session[AppConstants.SESSION_LOGIN_NOTE] = AppConstants.WEB_INACTIVE_USER;
                    }
                    else
                    {
                        canLogin = true;
                        Session[AppConstants.SESSION_LOGIN_USERGROUP] = userGroup;
                    }
                    if (canLogin)
                    {
                        if (userGroup.isActive == 0)
                        {
                            canLogin = false;
                            Session[AppConstants.SESSION_LOGIN_NOTE] = AppConstants.WEB_INACTIVE_USERGROUP;
                        }
                        else
                        {
                            canLogin = true;
                            if (canLogin)
                            {
                                this.updateLoginTime(user);
                            }
                        }
                    }
                }
            }
            else
            {
                canLogin = false;
                Session[AppConstants.SESSION_LOGIN_NOTE] = AppConstants.WEB_INVALID_LOGIN;
            }
            
        }

        private void updateLoginTime(User user)
        {
            DBManager dbManager = new DBManager();
            List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@userId", user.userId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@password", user.password));
            dbManager.getExecutedSQL(SqlHandler.USER_LOGIN_TIME_UPDATE, parameters);
        }
    }
}
