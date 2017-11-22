using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagingControl
{
    public class PageBase :System.Web.UI.Page
    {

        public string SortColumn
        {
            get { if (ViewState["SortColumn"] == null) ViewState["SortColumn"] = ""; return ViewState["SortColumn"].ToString(); }
            set { ViewState["SortColumn"] = value; }
        }

        public string SortOrder
        {
            get { if (ViewState["SortOrder"] == null) ViewState["SortOrder"] = ""; return ViewState["SortOrder"].ToString(); }
            set { ViewState["SortOrder"] = value; }
        }



    }
}