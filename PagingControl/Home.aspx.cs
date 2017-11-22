using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.IO.Compression;

using PagingControl.DataAccess;

namespace PagingControl
{
    public partial class Home : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
       
                ArrayList CheckBoxArray;
                if (ViewState["CheckBoxArray"] != null)
                {
                    CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                }
                else
                {
                    CheckBoxArray = new ArrayList();
                }
                if (!IsPostBack)
                {
                    this.BindData();
                    int CheckBoxIndex;
                    bool CheckAllWasChecked = false;
                    CheckBox chkAll = (CheckBox)gvuser.HeaderRow.Cells[0].FindControl("chkSelectAll");
                    string checkAllIndex = "chkSelectAll-" + gvuser.PageIndex;
                    if (chkAll.Checked)
                    {
                        if (CheckBoxArray.IndexOf(checkAllIndex) == -1)
                        {
                            CheckBoxArray.Add(checkAllIndex);
                        }
                    }
                    else
                    {
                        if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                        {
                            CheckBoxArray.Remove(checkAllIndex);
                            CheckAllWasChecked = true;
                        }
                    }
                    for (int i = 0; i < gvuser.Rows.Count; i++)
                    {
                        if (gvuser.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chk = (CheckBox)gvuser.Rows[i].Cells[0].FindControl("chkSelect");
                            CheckBoxIndex = gvuser.PageSize * gvuser.PageIndex + (i + 1);
                            if (chk.Checked)
                            {
                                if (CheckBoxArray.IndexOf(CheckBoxIndex) == -1 && !CheckAllWasChecked)
                                {
                                    CheckBoxArray.Add(CheckBoxIndex);
                                }
                            }
                            else
                            {
                                if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1 || CheckAllWasChecked)
                                {
                                    CheckBoxArray.Remove(CheckBoxIndex);
                                }
                            }
                        }
                    }

                    ViewState["CheckBoxArray"] = CheckBoxArray;
                }
            
        }

       private void BindData()
        {
            DataSet ds = new MemberDAO().GetUse();
            gvuser.DataSource = ds;
            gvuser.DataBind();


        }
       private void BindDataByOrder(string AgreementNo)
        {
            DataTable dt = new MemberDAO().GetUseByOrder(AgreementNo);
            gvuser.DataSource = dt;
            gvuser.DataBind();

        }
        private void BindDataDetialByOrder(string AgreementNo)
        {
            DataTable dt = new MemberDAO().GetUseDetialByOrder(AgreementNo);
            gvdata.DataSource = dt;
            gvdata.DataBind();
        }

        protected void gvuser_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvuser, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }

        }

        protected void gvuser_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

      

        protected void bth_Click(object sender, EventArgs e)
        {
            BindDataByOrder(txtOrder.Text.Trim());
            txtOrder.Text = "";
        }

        protected void gvuser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvuser.PageIndex = e.NewPageIndex;
            BindDataByOrder(txtOrder.Text.Trim());
            if (ViewState["CheckBoxArray"] != null)
            {
                ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                string checkAllIndex = "chkAll-" + gvuser.PageIndex;

                if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                {
                    CheckBox chkAll = (CheckBox)gvuser.HeaderRow.Cells[0].FindControl("chkSelectAll");
                    chkAll.Checked = true;
                }
                for (int i = 0; i < gvuser.Rows.Count; i++)
                {

                    if (gvuser.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                        {
                            CheckBox chk = (CheckBox)gvuser.Rows[i].Cells[0].FindControl("chkSelect");
                            chk.Checked = true;
                            gvuser.Rows[i].Attributes.Add("style", "background-color:aqua");
                        }
                        else
                        {
                            int CheckBoxIndex = gvuser.PageSize * (gvuser.PageIndex) + (i + 1);
                            if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                            {
                                CheckBox chk = (CheckBox)gvuser.Rows[i].Cells[0].FindControl("chkSelect");
                                chk.Checked = true;
                                gvuser.Rows[i].Attributes.Add("style", "background-color:#C2D69B");
                            }
                        }
                    }
                }
            }

        }

        protected void gvuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gvuser.SelectedRow.RowIndex;
            string AgreementNo = gvuser.SelectedRow.Cells[1].Text;
            this.BindDataDetialByOrder(AgreementNo);


        }

        protected void gvdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvdata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvdata_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}