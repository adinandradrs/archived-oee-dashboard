using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OEEComponent.iface;
using OEEComponent.baselayer;
using OEEComponent.utilities;
using OEEBusinessObject.rockwell.business;
using System.Collections;

namespace OEEBusinessObject.rockwell.datalayer
{
    public class TagMapBase : AccessLayer, IAccessLayer
    {
        private TagMap tagMap;
        public List<TagMap> tagMapList = new List<TagMap>();

        public TagMapBase(TagMap tagMap)
        {
            this.tagMap=tagMap;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        public void doAdd()
        {
            dbManager = this.getDbManager();
            query = TagMap.TAGMAP_I;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@plcTag", tagMap.plcTag));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", tagMap.isActive));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@createdBy", tagMap.createdBy.userId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@machineId", tagMap.machineId.machineId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@tagType", tagMap.tagType));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doUpdate()
        {
            dbManager = this.getDbManager();
            query = TagMap.TAGMAP_U;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@plcTag", tagMap.plcTag));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@isActive", tagMap.isActive));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@modifiedBy", tagMap.modifiedBy.userId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@tagMapId", tagMap.tagMapId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@tagType", tagMap.tagType));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doDelete()
        {
            dbManager = this.getDbManager();
            query = TagMap.TAGMAP_D;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@tagMapId", tagMap.tagMapId));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doSelect()
        {
            dbManager = this.getDbManager();
            query = TagMap.TAGMAP_S;
            hashData = dbManager.getSourceList(query, null);
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
    }
}
