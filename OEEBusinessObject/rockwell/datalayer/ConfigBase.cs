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
    public class ConfigBase : AccessLayer, IAccessLayer
    {
        private Config config;
        public List<Config> configList = new List<Config>();

        public ConfigBase(Config config)
        {
            this.config = config;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        public void doAdd()
        {
            throw new NotImplementedException();
        }

        public void doUpdate()
        {
            dbManager = this.getDbManager();
            query = Config.CONFIG_U;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@configId", config.configId));
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@value", config.value));
            executed = dbManager.getExecutedSQL(query, parameters);
        }

        public void doDelete()
        {
            throw new NotImplementedException();
        }

        public void doSelect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ini menggantikan scan rate, tinggal diisi id dari scan rate kalo mau ambil scan rate
        /// </summary>
        public void doSelectById()
        {
            dbManager = this.getDbManager();
            query = Config.CONFIG_S_ID;
            this.parameters = new List<System.Data.SqlClient.SqlParameter>();
            this.parameters.Add(new System.Data.SqlClient.SqlParameter("@configId", config.configId));
            hashData = dbManager.getSourceList(query, null);
            foreach (String[] values in hashData.Values)
            {
                config = new Config();
                config.configId = int.Parse(values[0]);
                config.parameter = values[1];
                config.value = values[2];
                configList.Add(config);
            }
        }
    }
}
