using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OEEComponent.utilities;
using System.Web;

namespace OEEComponent.iface
{
    public interface IAccessLayer
    {

        DBManager getDbManager();
        void doAdd();
        void doUpdate();
        void doDelete();
        void doSelect();
    }
}
