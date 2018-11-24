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

public partial class admin_djadd : System.Web.UI.Page
{
    public string Noval = "";
    public int NointVal = 0, xs = 0, zhi = 0, zhix=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["no"] != null)
        {
            Noval = MyFunction.Filter(Server.UrlDecode(Request.QueryString["no"]).ToString().Trim()).Trim();
            try { NointVal = Convert.ToInt32(Noval); }
            catch { }
        }

        if (!IsPostBack) {

            
        }

        xs = Convert.ToInt32(Logic.ExecuteScalar("select count(did) from dengji"));
        DataSet ds = Logic.ExecuteDataSet("select * from dengji");
         zhi = Convert.ToInt32(ds.Tables[0].Rows[xs - 1]["jsjf"].ToString());
         zhix = Convert.ToInt32(ds.Tables[0].Rows[xs - 1]["vdj"].ToString());
        zhi = zhi + 1;
        zhix = zhix + 1;
        TextBox1.Text = "v" + zhix;
        TextBox2.Text = "" + zhi;
       

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        string nr3 = MyFunction.Filter(TextBox3.Text).Trim().Replace(" ", "");


        try
        {
            Logic.ExecuteNonQuery("insert into dengji(vdj,qsjf,jsjf)values('" + zhix + "','" + zhi + "','" + nr3 + "')");
           
        }
        catch
        {
            Class1.AlertShow("信息传输有误！", "djadd.aspx"); return;
        }
        Class1.AlertShow("添加成功！", "jfdj.aspx");
    }
}
