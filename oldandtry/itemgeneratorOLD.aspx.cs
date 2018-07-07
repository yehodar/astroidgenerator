using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class generator : System.Web.UI.Page
{
    
    

    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (Page.IsPostBack == false)
        {
           
            loadMyItems();
        }
        else
        {
          
        }

    }

   

    protected void myitems_rbl_SelectedIndexChanged(object sender, EventArgs e)
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        int chosen = Convert.ToInt16(myitems_rbl.SelectedValue);
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']/item[@id='" + chosen + "']");

        //בודקים האם זה טקסט או תמונה ומעדכנים רדיו
        if (selectedItemNode.Attributes["type"].Value == "text")
        {
            type_rbl.SelectedValue = "text";
        }
        else
        {
            type_rbl.SelectedValue = "pic";
        }

        //בודקים האם זה נכון או לא ומעדכנים רדיו
        if (selectedItemNode.Attributes["correct"].Value == "true")
        {
            correct_rbl.SelectedValue = "true";
        }
        else
        {
            correct_rbl.SelectedValue = "false";
        }

        //ממלאים את שדה תוכן הפריט
        content_txtb.Text = selectedItemNode.InnerXml;
    }

    //כפתור עדכון
    protected void update_btn_Click(object sender, EventArgs e)
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        int chosen = Convert.ToInt16(myitems_rbl.SelectedValue);
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']/item[@id='" + chosen + "']");

        selectedItemNode.Attributes["type"].Value = type_rbl.SelectedValue;
        selectedItemNode.Attributes["correct"].Value = correct_rbl.SelectedValue;
        selectedItemNode.InnerXml = content_txtb.Text;

        myDoc.Save(MapPath("trees/XMLFile.xml"));//שמירה לעץ
        clearselected();
        loadMyItems();




    }

    //כפתור מחיקה
    protected void delete_btn_Click(object sender, EventArgs e)
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        int chosen = Convert.ToInt16(myitems_rbl.SelectedValue);
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']/item[@id='" + chosen + "']");
        selectedItemNode.ParentNode.RemoveChild(selectedItemNode);
        myDoc.Save(MapPath("trees/XMLFile.xml"));//שמירה לעץ

        clearselected();
        loadMyItems();
    }
    //מנקה את הבחירות של המשתמש
    protected void clearselected()
    {
        myitems_rbl.ClearSelection();
        type_rbl.ClearSelection();
        correct_rbl.ClearSelection();
        content_txtb.Text = "";
    }

    //פונקציה שטוענת את הפריטים הקיימים במשחק
    protected void loadMyItems()
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        myitems_rbl.Items.Clear();

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNodeList myItemsNode = myDoc.SelectNodes("//game[@id='"+ currentgame + "']/item");
        for (int i = 0; i < myItemsNode.Count; i++)
        {
            ListItem li = new ListItem();
            li.Text = myItemsNode.Item(i).InnerXml;
            li.Value = myItemsNode.Item(i).Attributes["id"].Value;
            myitems_rbl.Items.Add(li);
        }
    }
}