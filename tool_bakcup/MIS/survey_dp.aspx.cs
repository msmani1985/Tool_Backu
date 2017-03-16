using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class survey_dp : System.Web.UI.Page
{
    static int gridrowcnt = 1;
    datasourceSQL sobj = new datasourceSQL();
    DataSet sds = new DataSet();
    protected void Page_Init(object sender, EventArgs e)
    {
        createquestion();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
        {
            
        }
        
    }
    private void createquestion()
    {
        if (Session["employeeid"] != null)
        {
            sds = sobj.ExcProcedure("spget_survey_details", new string[,] { { "@empid", Session["employeeid"].ToString() } }, CommandType.StoredProcedure);
            Session["survey_ds"] = sds;
        }
        else
            throw new ArgumentException("Your Session has been expired, Please re-login ");
        try
        {
            if (Session["survey_ds"] != null)
            {
                sds = new DataSet();
                sds.RejectChanges();
                sds = (DataSet)Session["survey_ds"];
                DataView section_dv = new DataView();
                section_dv =sds.Tables[0].DefaultView;
                DataTable displaytable = section_dv.ToTable(true, "display_section");
                for (int scnt = 0; displaytable.Rows.Count > scnt; scnt++)
                {
                    DataView step_view = sds.Tables[0].DefaultView;
                    step_view.RowFilter = "display_section='" + displaytable.Rows[scnt]["display_section"].ToString() + "'";
                    Table step1table = new Table();
                    step1table.HorizontalAlign = HorizontalAlign.Left;
                    step1table.Width = Unit.Pixel(600);
                    step1table.Height = Unit.Pixel(300);
                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();
                    tc.Text = "&nbsp";
                    tr.Cells.Add(tc);
                    step1table.Controls.Add(tr);
                    for (int qcnt = 0; step_view.ToTable().Rows.Count > qcnt; qcnt++)
                    {
                        TableRow str = new TableRow();
                        TableCell stc = new TableCell();
                        stc.Text = step_view.ToTable().Rows[qcnt]["question_name"].ToString();
                        stc.Font.Bold = true;
                        str.Cells.Add(stc);
                        step1table.Rows.Add(str);
                        TableRow row_value = new TableRow();
                        TableCell valuecell = new TableCell();
                        DataRow[] dr=null;
                        if(sds.Tables[5].Rows.Count>0)
                            dr = (DataRow[])sds.Tables[5].Select("question_no=" + step_view.ToTable().Rows[qcnt]["question_no"].ToString());
                        switch (step_view.ToTable().Rows[qcnt]["value_type"].ToString())
                        {
                            case "1": //For designation
                                valuecell.Controls.Add(createdropdowncontrol(sds.Tables[1], "designation_id", "designation_name", "dd_" + step_view.ToTable().Rows[qcnt]["question_no"].ToString(), (dr!=null)?dr[0]["answer_id"].ToString():"0"));
                                break;
                            case "2"://For Department
                                valuecell.Controls.Add(createdropdowncontrol(sds.Tables[2], "department_id", "department_name", "dd_" + step_view.ToTable().Rows[qcnt]["question_no"].ToString(), (dr != null) ?dr[0]["answer_id"].ToString() : "0"));
                                break;
                            case "3": //For How Long (Year Type)
                                DataView year_dv = sds.Tables[3].DefaultView;
                                year_dv.RowFilter = "value_type_id=3";
                                valuecell.Controls.Add(createdropdowncontrol(year_dv.ToTable(), "value_no", "value_name", "dd_" + step_view.ToTable().Rows[qcnt]["question_no"].ToString(), (dr != null) ? dr[0]["answer_id"].ToString() : "0"));
                                break;
                            case "4":
                            case "5": //For Satisfied type
                                DataView statis_dv = sds.Tables[3].DefaultView;
                                statis_dv.RowFilter = "value_type_id=" + step_view.ToTable().Rows[qcnt]["value_type"].ToString();
                                valuecell.Font.Bold = false;
                                valuecell.Controls.Add(createradiobuttoncontrol(statis_dv.ToTable(), "value_no", "value_name", "rb_" + step_view.ToTable().Rows[qcnt]["question_no"].ToString(), (dr != null) ? dr[0]["answer_id"].ToString() : ""));
                                break;
                            case "6":
                                DataView sub_qusdv = sds.Tables[4].DefaultView;
                                sub_qusdv.RowFilter = "question_no=" + step_view.ToTable().Rows[qcnt]["question_no"].ToString();
                                DataView value_dv = sds.Tables[3].DefaultView;
                                value_dv.RowFilter = "value_type_id=" + step_view.ToTable().Rows[qcnt]["value_type"].ToString();
                                gridrowcnt = 1;
                                valuecell.Controls.Add(creategridviewcontrol(sub_qusdv.ToTable(), value_dv.ToTable(), sub_qusdv.ToTable().Rows[0]["question_no"].ToString()));
                                break;
                            case "7":
                                DataView likeyly_dv = sds.Tables[3].DefaultView;
                                likeyly_dv.RowFilter = "value_type_id=" + step_view.ToTable().Rows[qcnt]["value_type"].ToString();
                                valuecell.Font.Bold = false;
                                valuecell.Controls.Add(createradiobuttoncontrol(likeyly_dv.ToTable(), "value_no", "value_name", "rb_" + step_view.ToTable().Rows[qcnt]["question_no"].ToString(), (dr != null) ? dr[0]["answer_id"].ToString() : ""));
                                break;
                            case "8" :
                                valuecell.Controls.Add(createtextcontrol(step_view.ToTable().Rows[qcnt]["question_no"].ToString(), (dr != null) ? dr[0]["answer_text"].ToString() : ""));
                                break;
                        }
                        row_value.Cells.Add(valuecell);
                        step1table.Rows.Add(row_value);
                        TableRow emptyrow = new TableRow();
                        TableCell emptycell = new TableCell();
                        emptycell.Text = "&nbsp";
                        emptyrow.Cells.Add(emptycell);
                        step1table.Rows.Add(emptyrow);
                    }
                    wd_survey_dp.FindControl("div_" + displaytable.Rows[scnt]["display_section"].ToString()).Controls.Add(step1table);
                }


            }

        }
        catch (Exception ex)
        { throw ex; }
        finally
        { //sobj = null; 
        }
    }
    private TextBox createtextcontrol(string txtid,string txt_val)
    {
        TextBox txtbx = new TextBox();
        txtbx.ID = "txt_" + txtid;
        txtbx.Text = txt_val;
        txtbx.TextMode = TextBoxMode.MultiLine;
        txtbx.Width = Unit.Pixel(500);
        txtbx.Height = Unit.Pixel(50);
        return txtbx;
    }
    private GridView creategridviewcontrol(DataTable row_dt, DataTable column_dt,string gridname)
    {
        GridView gv = new GridView();
        gv.Width = Unit.Pixel(550);
        gv.AlternatingRowStyle.BackColor = System.Drawing.Color.FromName("#efefef");
        gv.RowStyle.BackColor = System.Drawing.Color.FromName("#ffffff");
        gv.ID = "Gv_wizard_" + gridname;
        gv.Controls.Clear();
        gv.Attributes.Add("runat", "server");
        try
        {
            gv.AutoGenerateColumns = false;
            BoundField qu_bf = new BoundField();
            qu_bf.HeaderText = "";
            qu_bf.DataField = "subquestion_name";
            gv.Columns.Add(qu_bf);

            int hfid=0;
            foreach (DataRow col_dr in column_dt.Rows)
            {
                TemplateField tf = new TemplateField();
                tf.HeaderText = col_dr["value_name"].ToString();
                tf.ItemTemplate = new GridviewTemplate(DataControlRowType.DataRow, col_dr["value_no"].ToString(), "hf_" + ++hfid);
                gv.Columns.Add(tf);
                //BoundField value_bf = new BoundField();
                //value_bf.HeaderText = col_dr["value_name"].ToString();
                //gv.Columns.Add(value_bf);
                //gridrowcnt += 1;
            }
            BoundField qu_bf_no = new BoundField();
            qu_bf_no.HeaderText = "";
            qu_bf_no.DataField = "subquestion_no";
            gv.Columns.Add(qu_bf_no);
            //qu_bf_no.Visible = false;
            gv.RowDataBound +=new GridViewRowEventHandler(gv_RowDataBound);
            gv.DataSource = row_dt;
            gv.DataBind();
            return gv;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { //gv = null; 
        }

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButton rb = new RadioButton();
            rb = new RadioButton();
            rb.ID = "rb_" + e.Row.RowIndex + "_" + 1;
            rb.GroupName = "gn" + gridrowcnt.ToString();
            if(e.Row.Cells[1].FindControl("hf_1")!=null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "answer_id").ToString() == ((HiddenField)e.Row.Cells[1].FindControl("hf_1")).Value)
                    rb.Checked = true;
            }
            e.Row.Cells[1].Controls.Add(rb);
            rb = new RadioButton();
            rb.ID = "rb_" + e.Row.RowIndex + "_" + 2;
            rb.GroupName = "gn" + gridrowcnt.ToString();
            if (e.Row.Cells[2].FindControl("hf_2") != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "answer_id").ToString() == ((HiddenField)e.Row.Cells[2].FindControl("hf_2")).Value)
                    rb.Checked = true;
            }
            e.Row.Cells[2].Controls.Add(rb);
            rb = new RadioButton();
            rb.ID = "rb_" + e.Row.RowIndex + "_" + 3;
            rb.GroupName = "gn" + gridrowcnt.ToString();
            if (e.Row.Cells[3].FindControl("hf_3") != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "answer_id").ToString() == ((HiddenField)e.Row.Cells[3].FindControl("hf_3")).Value)
                    rb.Checked = true;
            }
            e.Row.Cells[3].Controls.Add(rb);
            rb = new RadioButton();
            rb.ID = "rb_" + e.Row.RowIndex + "_" + 4;
            rb.GroupName = "gn" + gridrowcnt.ToString();
            if (e.Row.Cells[4].FindControl("hf_4") != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "answer_id").ToString() == ((HiddenField)e.Row.Cells[4].FindControl("hf_4")).Value)
                    rb.Checked = true;
            }
            e.Row.Cells[4].Controls.Add(rb);
            rb = new RadioButton();
            rb.ID = "rb_" + e.Row.RowIndex + "_" + 5;
            rb.GroupName = "gn" + gridrowcnt.ToString();
            if (e.Row.Cells[5].FindControl("hf_5") != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "answer_id").ToString() == ((HiddenField)e.Row.Cells[5].FindControl("hf_5")).Value)
                    rb.Checked = true;
            }
            e.Row.Cells[5].Controls.Add(rb);
            e.Row.Cells[6].Visible = false;
            gridrowcnt += 1;
        }
    }
    private RadioButtonList createradiobuttoncontrol(DataTable dt, string datavaluefield, string datatextfield,string rb_id,string select_value)
    {
        RadioButtonList rl = new RadioButtonList();
        rl.ID = rb_id;
        rl.DataTextField = datatextfield;
        rl.DataValueField = datavaluefield;
        rl.DataSource = dt;
        rl.DataBind();
        rl.SelectedValue = select_value;
        return rl;
    }
    private DropDownList createdropdowncontrol(DataTable dt, string datavaluefield, string datatextfield,string dd_id,string select_value)
    {
        DropDownList dd_value = new DropDownList();
        dd_value.ID = dd_id;
        dd_value.DataValueField = datavaluefield;
        dd_value.DataTextField = datatextfield;
        dd_value.DataSource = dt;
        dd_value.DataBind();
        dd_value.Items.Insert(0,new ListItem("-- Select a value --","0"));
        dd_value.SelectedValue = select_value;
        return dd_value;
    }
    protected void wd_survey_dp_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        setwizardheader(e);
    }

    protected void wd_survey_dp_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {
        setwizardheader(e);
    }
    protected void wd_survey_dp_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
    {
        setwizardheader(e);
    }
    private void setwizardheader(WizardNavigationEventArgs e)
    {
        if (e.NextStepIndex == 0)
            ((Label)wd_survey_dp.FindControl("headerContainer$lbl_wizard_header")).Text = "Your Position in the Company";
        else if (e.NextStepIndex == 1)
            ((Label)wd_survey_dp.FindControl("headerContainer$lbl_wizard_header")).Text = "Your Job at DataPage";
        else if (e.NextStepIndex == 2)
            ((Label)wd_survey_dp.FindControl("headerContainer$lbl_wizard_header")).Text = "Company Ratings";
        else if (e.NextStepIndex == 3)
            ((Label)wd_survey_dp.FindControl("headerContainer$lbl_wizard_header")).Text = "Datapage Leadership";
        else if (e.NextStepIndex == 4)
            ((Label)wd_survey_dp.FindControl("headerContainer$lbl_wizard_header")).Text = "Comments";
    }
    protected void wd_survey_dp_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        string insertqry = "";
        datasourceSQL sobj = new datasourceSQL();
        try
        {
            string stp_insert = "";
            for (int wcnt = 0; div_step1.Controls.Count > wcnt; wcnt++)
            {
                if (div_step1.Controls[wcnt].GetType().ToString() == "System.Web.UI.WebControls.Table")
                    stp_insert = splittable((Table)div_step1.Controls[wcnt]);
                if (stp_insert == "")
                { alert("All fields are madatory. Please check missing fields"); return; }
                else
                    insertqry += stp_insert;
            }

            stp_insert = "";
            for (int wcnt = 0; div_step2.Controls.Count > wcnt; wcnt++)
            {
                if (div_step2.Controls[wcnt].GetType().ToString() == "System.Web.UI.WebControls.Table")
                    stp_insert = splittable((Table)div_step2.Controls[wcnt]);
                if (stp_insert == "")
                { alert("All fields are madatory. Please check missing fields"); return; }
                else
                    insertqry += stp_insert;
            }
            stp_insert = "";
            for (int wcnt = 0; div_step3.Controls.Count > wcnt; wcnt++)
            {
                if (div_step3.Controls[wcnt].GetType().ToString() == "System.Web.UI.WebControls.Table")
                    stp_insert = splittable((Table)div_step3.Controls[wcnt]);
                if (stp_insert == "")
                { alert("All fields are madatory. Please check missing fields"); return; }
                else
                    insertqry += stp_insert;
            }
            stp_insert = "";
            for (int wcnt = 0; div_step4.Controls.Count > wcnt; wcnt++)
            {
                if (div_step4.Controls[wcnt].GetType().ToString() == "System.Web.UI.WebControls.Table")
                    stp_insert = splittable((Table)div_step4.Controls[wcnt]);
                if (stp_insert == "")
                { alert("All fields are madatory. Please check missing fields"); return; }
                else
                    insertqry += stp_insert;
            }
            stp_insert = "";
            for (int wcnt = 0; div_step5.Controls.Count > wcnt; wcnt++)
            {
                if (div_step5.Controls[wcnt].GetType().ToString() == "System.Web.UI.WebControls.Table")
                    stp_insert = splittable((Table)div_step5.Controls[wcnt]);
                if (stp_insert == "")
                { alert("All fields are madatory. Please check missing fields"); return; }
                else
                    insertqry += stp_insert;
            }
            sobj.ExcSProcedure("spInsert_Survey_value", new string[,] { { "@insertval", insertqry } },CommandType.StoredProcedure);
            alert("Saved successfully");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { sobj = null; }

    }
    private string splittable(Table tt)
    {
        string inqry = "";
        try
        {
            if (sds != null)
            {
                DataTable ans_dt = sds.Tables[5];
                
                foreach (TableRow tr in tt.Rows)
                {
                    foreach (TableCell tc in tr.Cells)
                    {
                        if (tc.Controls.Count > 0)
                        {
                            switch (tc.Controls[0].GetType().ToString())
                            {
                                case "System.Web.UI.WebControls.DropDownList":
                                    if (((DropDownList)tc.Controls[0]).SelectedValue.ToString() == "0")
                                    { alert("All fields are madatory. Please check missing fields"); return ""; }
                                    if(ans_dt.Rows.Count>0)//Means the current user already has value
                                        inqry +="update survey_answer set answer_id=" + ((DropDownList)tc.Controls[0]).SelectedValue.ToString() + " where question_no=" + ((DropDownList)tc.Controls[0]).ID.Split('_')[1].ToString() + " and employee_id=" + Session["employeeid"].ToString() + ";";
                                    else
                                        inqry += "insert into survey_answer(question_no,answer_id,employee_id) values(" + ((DropDownList)tc.Controls[0]).ID.Split('_')[1].ToString() + "," + ((DropDownList)tc.Controls[0]).SelectedValue.ToString() + "," + Session["employeeid"].ToString() + ");";
                                    break;
                                case "System.Web.UI.WebControls.RadioButtonList":
                                    if (((RadioButtonList)tc.Controls[0]).SelectedValue != "")
                                    {
                                        if (ans_dt.Rows.Count > 0)//Means the current user already has value
                                            inqry += "update survey_answer set answer_id=" + ((RadioButtonList)tc.Controls[0]).SelectedValue.ToString() + " where question_no=" + ((RadioButtonList)tc.Controls[0]).ID.Split('_')[1].ToString() + " and employee_id=" + Session["employeeid"].ToString() + ";";
                                        else
                                            inqry += "insert into survey_answer(question_no,answer_id,employee_id) values(" + ((RadioButtonList)tc.Controls[0]).ID.Split('_')[1].ToString() + "," + ((RadioButtonList)tc.Controls[0]).SelectedValue.ToString() + "," + Session["employeeid"].ToString() + ");";
                                    }
                                    else
                                    { alert("All fields are madatory. Please check missing fields"); return ""; }
                                    break;
                                case "System.Web.UI.WebControls.GridView":
                                    GridView gvvalue = ((GridView)tc.Controls[0]);
                                    for (int gridcnt = 0; gvvalue.Rows.Count > gridcnt; gridcnt++)
                                    {
                                        string sub_val = "";

                                        for (int cellcnt = 0; gvvalue.Rows[gridcnt].Cells.Count-1 > cellcnt; cellcnt++)
                                        {
                                            if (gvvalue.Rows[gridcnt].Cells[cellcnt].HasControls() && (RadioButton)gvvalue.Rows[gridcnt].Cells[cellcnt].Controls[1] != null)
                                            {
                                                if (((RadioButton)gvvalue.Rows[gridcnt].Cells[cellcnt].Controls[1]).Checked)
                                                { sub_val = ((HiddenField)gvvalue.Rows[gridcnt].Cells[cellcnt].Controls[0]).Value; }
                                            }
                                        }
                                        if (sub_val != "")
                                        {
                                            if (ans_dt.Rows.Count > 0)//Means the current user already has value
                                                inqry += "update survey_answer set answer_id=" + sub_val + " where sub_question_id=" + gvvalue.Rows[gridcnt].Cells[6].Text.ToString() + " and employee_id=" + Session["employeeid"].ToString() + ";";
                                            else
                                                inqry += "insert into survey_answer(sub_question_id,answer_id,employee_id) values(" + gvvalue.Rows[gridcnt].Cells[6].Text.ToString() + "," + sub_val + "," + Session["employeeid"].ToString() + ");";
                                        }
                                        else
                                        { alert("All fields are madatory. Please check missing fields"); return ""; }
                                    }
                                    break;
                                case "System.Web.UI.WebControls.TextBox":
                                    if (((TextBox)tc.Controls[0]).Text == "")
                                    { alert("All fields are madatory. Please check missing fields"); return ""; }
                                    else
                                    {
                                        if (ans_dt.Rows.Count > 0)//Means the current user already has value
                                            inqry += "update survey_answer set answer_text='" + ((TextBox)tc.Controls[0]).Text.Trim().ToString() + "' where question_no=" + ((TextBox)tc.Controls[0]).ID.Split('_')[1].ToString() + " and employee_id=" + Session["employeeid"].ToString() + ";";
                                        else
                                            inqry += "insert into survey_answer(question_no,answer_text,employee_id) values(" + ((TextBox)tc.Controls[0]).ID.Split('_')[1].ToString() + ",'" + ((TextBox)tc.Controls[0]).Text.Trim().ToString() + "'," + Session["employeeid"].ToString() + ");";
                                    }
                                    break;
                            }

                        }
                    }
                }
                return inqry;
            }
            else
                throw new ArgumentException("Your Session has been expired, Please relogin");

        }
        catch (Exception ex)
        { throw ex; }
        finally
        { }
        
    }
    private void alert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script laguage='javascript'>alert('" + msg + "');</script>");
    }
}
public class GridviewTemplate : ITemplate
{
    private DataControlRowType templatetype;
    private string hiddenvalue;
    private string hiddenid;
    public GridviewTemplate(DataControlRowType datatype, string hf_val,string hf_id)
    {
        templatetype = datatype;
        hiddenvalue = hf_val;
        hiddenid = hf_id;
    }
    public void InstantiateIn(Control container)
    {
        switch (templatetype)
        {
            case DataControlRowType.DataRow :
                HiddenField hf = new HiddenField();
                hf.ID = hiddenid;
                hf.Value = hiddenvalue;
                container.Controls.Add(hf);
                break;
        }
    }
}
