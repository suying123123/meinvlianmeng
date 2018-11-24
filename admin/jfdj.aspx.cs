using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

public partial class admin_assets_jfdj : System.Web.UI.Page
{
    public int Pagee = 1;
    public int CountPage = 1, Page = 1;
    public string PageHtml = "";
    public int pageNoVal = 0;
    public string pageNo = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){

           

            DataSet ds1 = new DataSet();
            #region Access分页
            if (Request.QueryString["page"] != null)
            {
                pageNo = Server.UrlDecode(Request.QueryString["page"].ToString().Trim());
                if (MyFunction.IsInt(pageNo))
                {
                    try
                    {
                        pageNoVal = Convert.ToInt32(pageNo);
                    }
                    catch
                    {
                        return;
                    }
                    Page = pageNoVal;
                }
                else
                {
                    Page = 1;
                }
            }
            else
            {
                Page = 1;
            }
            int PageSize = 10;
            string sqlvalue = "";
            sqlvalue = "select count(did) from dengji  where did>0";
            int Count = Convert.ToInt32(Logic.ExecuteScalar(sqlvalue));
            if (Count <= PageSize) { fenye.Style.Add("display", "none"); }
            CountPage = Convert.ToInt32(Math.Ceiling((decimal)Count / (decimal)PageSize));
            int Lpage = Page > 4 ? Page - 3 : 1;
            for (int p = Lpage; p < Lpage + 7; p++)
            {
                if (p > CountPage) break;
                if (p == Page)
                {
                    PageHtml += "<li class=\"am-active\"><a href=\"javascript:;\">" + p + "</a></li>";
                }
                else
                {
                    PageHtml += "<li><a href=\"jfdj.aspx?page=" + p + "\">" + p + "</a></li>";
                }
            }
            int zonghe = PageSize * (Page - 1);
            string OrderValue = " ";//排序条件
            if (zonghe <= 0)
            {
                string sqlvalueone = "";
                sqlvalueone = "select top " + PageSize + " * from dengji where  did>0";
                string sqles = sqlvalueone;
                try
                {
                    ds1 = Logic.ExecuteDataSet(sqles);
                }
                catch { }
                Repeater1.DataSource = ds1;
                Repeater1.DataBind();
            }
            else
            {
                StringBuilder aa = new StringBuilder();
                aa.Append("select top " + PageSize + " * from dengji where  did>0 and did not in(");
                aa.Append("select top " + PageSize * (Page - 1) + " did from dengji where  did>0)");
                string sqles = aa.ToString();
                try
                {
                    ds1 = Logic.ExecuteDataSet(sqles);
                }
                catch { }
                Repeater1.DataSource = ds1;
                Repeater1.DataBind();
            }
            #endregion

        
        }
    }
    public string syy(string s)
    {
        string mc;
        int sx = Convert.ToInt32(s);
        if (s != "1")
        {
            sx = sx - 1;
            mc = "<li><a href=\"jfdj.aspx?page=1\">««</a></li>" +
                "<li><a href=\"jfdj.aspx?page=" + sx + "\">«</a></li>";
        }
        else
        {
            mc = "<li class=\"am-disabled\"><a style=\"background:#f5f5f5;\">««</a></li>" +
                "<li class=\"am-disabled\"><a style=\"background:#f5f5f5;\">«</a></li>";
        }
        return mc;
    }
    public string xyy(string x)
    {
        string mc;
        int sx = Convert.ToInt32(x);
        if (sx == CountPage)
        {
            mc = "<li class=\"am-disabled\"><a style=\"background:#f5f5f5;\">»</a></li>" +
                "<li class=\"am-disabled\"><a style=\"background:#f5f5f5;\">»»</a></li>";
        }
        else
        {
            sx = sx + 1;
            mc = "<li><a href=\"jfdj.aspx?page=" + sx + "\">»</a></li>" +
                "<li><a href=\"jfdj.aspx?page=" + CountPage + "\">»»</a></li>";
        }
        return mc;
    }

}
