using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OEEClient.Controllers;
using OEEBusinessObject.rockwell.business;
namespace OEEClient.Views.Shared
{
    public partial class master_form : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void repeaterLine_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Repeater innerRepeater = (Repeater)item.FindControl("repeaterWorkcell");
                innerRepeater.DataSource = new WorkcellController().getWorkcellMenu(((Line)e.Item.DataItem).lineId);
                innerRepeater.DataBind();
            }
        }
    }
}