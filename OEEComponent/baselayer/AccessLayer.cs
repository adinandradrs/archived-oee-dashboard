using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OEEComponent.iface;
using OEEComponent.utilities;
using System.Data.SqlClient;

namespace OEEComponent.baselayer
{
    public class AccessLayer
    {
        //base
        public DBManager dbManager;
        public string query;
        public Hashtable hashData;
        public List<SqlParameter> parameters;
        public int executed;
    }
}
