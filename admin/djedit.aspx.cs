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

public partial class admin_djedit : System.Web.UI.Page
{
    public string Novali = "";
    public int NointVali = 0;
    public DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["no"] != null)
        {
            Novali = MyFunction.Filter(Server.UrlDecode(Request.QueryString["no"]).ToString().Trim()).Trim();
            try { NointVali = Convert.ToInt32(Novali); }
            catch {  }
        }
        if (!IsPostBack)
        {
           
            string ssq = "select * from dengji where did=" + NointVali + "";
            try
            {
                ds = Logic.ExecuteDataSet(ssq);
            }
            catch { }
            if (checkDataSetNull(ds))
            {
                TextBox1.Text = ds.Tables[0].Rows[0]["vdj"].ToString();
                TextBox2.Text = ds.Tables[0].Rows[0]["qsjf"].ToString();
                TextBox3.Text = ds.Tables[0].Rows[0]["jsjf"].ToString();
                
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
       
        string nr3 = MyFunction.Filter(TextBox3.Text).Trim().Replace(" ", "");



        try
        {
            Logic.ExecuteNonQuery("update dengji set jsjf="+ nr3 +" where did=" + NointVali + "");
        }
        catch
        {
            Class1.AlertShow("信息传输有误！", "djedit.aspx?id=" + NointVali + "");
            return;
        }
        Class1.AlertShow("修改成功！", "jfdj.aspx");
    }
    public bool checkDataSetNull(DataSet ds)
    {
        bool rtnval = false;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rtnval = true;
                }
            }
        }
        return rtnval;
    }
}
