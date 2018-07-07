using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class generatorLogin : System.Web.UI.Page
{
    //בדיקה האם המשתמש התחבר כבר ב20 דקות האחרונו
    protected void Page_Init(object sender, EventArgs e)
    {
        string signInYet = (string)Session["signInSES"];
        if(signInYet == "yes")
        {
            Response.Redirect("mygames.aspx");
        }
        else
        {
            signInYet = "no";
            Session["signInSES"] = signInYet;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        string curPassword = passwordTBX.Text;
        passwordTBX.Attributes.Add("value", curPassword);
       
        if (Page.IsPostBack == false)
        {
            

        }
        else
        {


        }
    }
    
    string correctUserName = "שבתאי";
    string correctPassword = "12345";
    
    protected void loginBtn_Click(object sender, EventArgs e)
    {
        string myUserName = userNameTBX.Text;
        string myPassword = passwordTBX.Text;

        if (myUserName == "" && myPassword == "")
        {
            feedbackLBL.Text = "יש להזין שם משתמש וסיסמא";
        }
        else if (myUserName == "" && myPassword != "")
        {
            feedbackLBL.Text = "יש להזין שם משתמש";
        }
        else if (myUserName != "" && myPassword == "")
        {
            feedbackLBL.Text = "יש להזין סיסמא";
        }
        else
        {
            if (myUserName == correctUserName && myPassword == correctPassword)
            {
                Session["signInSES"] = "yes";
                Response.Redirect("mygames.aspx");

                
            }
            else if (myUserName != correctUserName && myPassword == correctPassword)
            {
                feedbackLBL.Text = "שם משתמש לא נכון";
            }
            else if (myUserName == correctUserName && myPassword != correctPassword)
            {
                feedbackLBL.Text = "סיסמא לא נכונה";
            }
            else if (myUserName != correctUserName && myPassword != correctPassword)
            {
                feedbackLBL.Text = "שם משתמש וסיסמא שגויים";
            }

            else
            {
                feedbackLBL.Text = "בעיה בהתחברות";
            }

        }
       




    }
}