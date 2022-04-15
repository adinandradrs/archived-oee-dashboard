using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OEEComponent.iface;
using OEEComponent.baselayer;
using OEEBusinessObject.rockwell.business;
using System.Collections;
using OEEComponent.utilities;

namespace OEEBusinessObject.rockwell.datalayer
{
    public class GaugeValueBase: AccessLayer, IAccessLayer
    {
        private GaugeValue gaugeValue = null;
        public List<GaugeValue> gaugeValueList = new List<GaugeValue>();
        public int lowVal, kpiVal, tagVal;

        public GaugeValueBase(GaugeValue gaugeValue)
        {
            this.gaugeValue = gaugeValue;
        }

        public DBManager getDbManager()
        {
            return new DBManager();
        }

        public void selectWorkcellNow(string typeTag)
        {
            dbManager = new DBManager();
            parameters = new List<System.Data.SqlClient.SqlParameter>();
            parameters.Add(new System.Data.SqlClient.SqlParameter("@machineUid", gaugeValue.machineUid));
            
            if (typeTag.Equals("Available")) query = GaugeValue.WC_AV_NOW;
            else if (typeTag.Equals("Performance")) query = GaugeValue.WC_PE_NOW;
            else if (typeTag.Equals("Quality")) query = GaugeValue.WC_QY_NOW;
            else if (typeTag.Equals("Test")) query = GaugeValue.WC_KPI;
            hashData = dbManager.getSourceList(query, null);
            foreach (String[] values in hashData.Values)
            {
                if (values[0].Length > 0) tagVal = int.Parse(values[0]);
                break;
            }

            if (typeTag.Equals("Available")) query = GaugeValue.WC_AV_KPI;
            else if (typeTag.Equals("Performance")) query = GaugeValue.WC_PE_KPI;
            else if (typeTag.Equals("Quality")) query = GaugeValue.WC_QY_KPI;
            else if (typeTag.Equals("Test")) query = GaugeValue.WC_KPI;
            hashData = dbManager.getSourceList(query, null);
            foreach (String[] values in hashData.Values)
            {
                if (values[0].Length > 0) kpiVal = int.Parse(values[0]);
                if (values[1].Length > 0) lowVal = int.Parse(values[1]);
                break;
            }
        }

        public void doAdd()
        {
            throw new NotImplementedException();
        }

        public void doUpdate()
        {
            throw new NotImplementedException();
        }

        public void doDelete()
        {
            throw new NotImplementedException();
        }

        public void doSelect()
        {
            throw new NotImplementedException();
        }
    }
}
