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
    public class TagMapController : Controller
    {

        private Workcell workcell;
        private TagMap tagMap;
        private List<TagMap> tagMapList = new List<TagMap>();
        private TagMapBase tagMapBase = null;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult doFetchDetailRequest()
        {
            workcell = new Workcell();
            if (Request["machineId"] != null)
            {
                workcell.machineId = Request["machineId"];
                this.doFetchByMachine(workcell);
            }
            int pageIndex = Convert.ToInt32(int.Parse(Request["page"])) - 1;
            int pageSize = int.Parse(Request["rows"]);
            int totalRecords = tagMapList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var data = tagMapList
                            .Skip(pageIndex * pageSize)
                            .Take(pageSize);
            var result = new { total = totalPages, page = int.Parse(Request["page"]), records = totalRecords, rows = data };
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult doAddRequest()
        {
            tagMap = new TagMap();
            tagMap.machineId = new Workcell();
            tagMap.machineId.machineId = Request["machineId"];
            tagMap.plcTag = Request["plcTag"];
            tagMap.tagType = Request["tagType"];
            if (!this.forceConstraintTagType(tagMap))
            {
                if (Request["isActive"].Equals("true"))
                {
                    tagMap.isActive = 1;
                }
                else
                {
                    tagMap.isActive = 0;
                }
                if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
                {
                    UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                    tagMap.createdBy = new User();
                    tagMap.createdBy.userId = userGroupLogin.userId.userId;
                }
                else
                {
                    tagMap.createdBy = new User();
                    tagMap.createdBy.userId = "SYS";
                }
                try
                {
                    tagMapBase = new TagMapBase(tagMap);
                    tagMapBase.doAdd();
                    if (tagMapBase.executed != 0)
                    {
                        return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = "success", msg = "server cannot process the data" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { result = "failed", msg = "constraint tag type" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult doUpdateRequest()
        {
            tagMap = new TagMap();
            tagMap.machineId = new Workcell();
            tagMap.tagMapId = int.Parse(Request["tagMapId"]);
            tagMap.machineId.machineId = Request["machineId"];
            tagMap.plcTag = Request["plcTag"];
            tagMap.tagType = Request["tagType"];
            if (!this.forceConstraintTagType(tagMap))
            {
                if (Request["isActive"].Equals("true"))
                {
                    tagMap.isActive = 1;
                }
                else
                {
                    tagMap.isActive = 0;
                }
                if (Session[AppConstants.SESSION_LOGIN_USERGROUP] != null)
                {
                    UserGroup userGroupLogin = (UserGroup)Session[AppConstants.SESSION_LOGIN_USERGROUP];
                    tagMap.modifiedBy = new User();
                    tagMap.modifiedBy.userId = userGroupLogin.userId.userId;
                }
                else
                {
                    tagMap.modifiedBy = new User();
                    tagMap.modifiedBy.userId = "SYS";
                }
                try
                {
                    tagMapBase = new TagMapBase(tagMap);
                    tagMapBase.doUpdate();
                    if (tagMapBase.executed != 0)
                    {
                        return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = "success", msg = "server cannot process the data" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else{
                return Json(new { result = "failed", msg = "constraint tag type"}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult doDeleteRequest()
        {
            tagMap = new TagMap();
            tagMap.machineId = new Workcell();
            tagMap.tagMapId = int.Parse(Request["tagMapId"]);
            try
            {
                tagMapBase = new TagMapBase(tagMap);
                tagMapBase.doDelete();
                if (tagMapBase.executed != 0)
                {
                    return Json(new { result = "success", msg = "sent to server" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "success", msg = "server cannot process the data" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "failed", msg = "[HTTP Error] : " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void doFetchByMachine(Workcell workcell)
        {
            DBManager dbManager = new DBManager();
            List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@machineId", workcell.machineId));
            Hashtable hashData = dbManager.getSourceList(SqlHandler.TAGMAP_BY_MACHINEID, parameters);
            foreach (String[] values in hashData.Values)
            {
                TagMap tagMap = new TagMap();
                tagMap.tagMapId = int.Parse(values[0]);
                tagMap.plcTag = values[1];
                if (values[2].Equals("True"))
                {
                    tagMap.isActive = 1;
                }
                else
                {
                    tagMap.isActive = 0;
                }
                tagMap.createdAt = DateTime.Parse(values[3]);
                tagMap.createdBy = new User();
                tagMap.createdBy.userId = values[4];

                if (values[5].Length > 0)
                {
                    tagMap.modifiedAt = DateTime.Parse(values[5]);
                    tagMap.modifiedBy = new User();
                    tagMap.modifiedBy.userId = values[6];
                }
                tagMap.machineId = new Workcell();
                tagMap.machineId.machineId = values[7];
                tagMap.tagType = values[8];
                tagMapList.Add(tagMap);
            }
        }

        private bool forceConstraintTagType(TagMap tagMap)
        {
            DBManager dbManager = new DBManager();
            List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@machineId", tagMap.machineId.machineId));
            parameters.Add(new System.Data.SqlClient.SqlParameter("@tagType", tagMap.tagType));
            Hashtable hashData = dbManager.getSourceList(SqlHandler.TAGMAP_BY_TAGTYPE_MACHINEID, parameters);
            int counter = 0;
            foreach (String[] values in hashData.Values)
            {
                counter = int.Parse(values[0]);
            }
            if (counter > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
