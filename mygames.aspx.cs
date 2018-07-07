using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;




public partial class mygames : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            mycheckboxload();
            
        }
        else
        {
            

        }

        newGameTxb.Attributes.Add("onkeyup", "updateCharCount('newGameTxb' , 'newGameTxbCounter',55,50,'addGameBtn')");
        newGameTxb.Attributes.Add("onload", "updateCharCount('newGameTxb' , 'newGameTxbCounter',55,50,'addGameBtn')");


    }

   
    //כפתור הוספת משחק חדש
    protected void addGameBtn_Click(object sender, ImageClickEventArgs e)
    {
        
        //טעינת העץ
        XmlDocument xmlDoc = mygamesDataSource.GetXmlDocument();
        // הקפצה של מונה האי די בתוך קובץ האקס אם אל באחד
        int myId = Convert.ToInt16(xmlDoc.SelectSingleNode("//idCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString();
        xmlDoc.SelectSingleNode("//idCounter").InnerXml = myNewId;

        // יצירת ענף משחק     
        XmlElement myNewGameNode = xmlDoc.CreateElement("game");
        myNewGameNode.SetAttribute("id", myNewId);
        myNewGameNode.SetAttribute("code", (myId + 1000).ToString());
        myNewGameNode.SetAttribute("name", newGameTxb.Text);
        myNewGameNode.SetAttribute("published", "false");
        myNewGameNode.SetAttribute("publishoption", "false");
        myNewGameNode.SetAttribute("category", "");
        

        // הוספת ענף התלמיד לעץ
        xmlDoc.SelectSingleNode("/games").AppendChild(myNewGameNode);
        mygamesDataSource.Save();
        GridView1.DataBind();
        mycheckboxload();
        newGameTxb.Text = "";
      



    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e )
    {
        
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];
        Session["theItemIdSession"] = i.Attributes["theItemId"];

        
        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה
            case "deleteRow":
                //לקיחת ID לפי הלחיצה של המשתמש 
                Session["theItemIdSessions"] = i.Attributes["theItemId"];
                //   deleteRow((string)Session["theItemIdSessions"]);

                int index = Convert.ToInt16(e.CommandArgument);
                //Label deleteGameLabel = (Label)GridView1.SelectedRow.FindControl("nameLabel");
                Label deleteGameLabel = (Label)GridView1.Rows[index].FindControl("nameLabel");
                string deleteGameName = deleteGameLabel.Text.ToString();
                deleteValidtionNameP.InnerText = deleteGameName;
                deleteValidtionDarkScreen.Style.Add("display", "block");
                newGameTxb.Text = "";

                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה
            case "edit":
                Session["theItemIdSessions"] = i.Attributes["theItemId"];
                
                Response.Redirect("itemseditor.aspx");
                break;

            
        }
    }

    void deleteRow(string theItemId)
    {    
        XmlDocument Document = mygamesDataSource.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("games/game[@id='" + theItemId + "']");
        node.ParentNode.RemoveChild(node);
        mygamesDataSource.Save();
        GridView1.DataBind();
        mycheckboxload();
    }
    
   
   
  
    void mycheckboxload()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("trees/XMLFile.xml"));
        XmlNodeList myGames = xmlDoc.SelectNodes("//game");

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox pubChk = (CheckBox)GridView1.Rows[i].FindControl("publishCbx");
            
            //במידה והמשחק מפורסם
            if (myGames.Item(i).Attributes["published"].Value == "true" && myGames.Item(i).Attributes["publishoption"].Value == "true")
            {
                pubChk.Enabled = true;
                pubChk.Checked = true;
                pubChk.ToolTip = "המשחק מפורסם";
            }
            //במידה והמשחק עומד בתנאים לפרסום ולא מפורסם כרגע
            else if (myGames.Item(i).Attributes["published"].Value == "false" && myGames.Item(i).Attributes["publishoption"].Value == "true")
            {
                pubChk.Enabled = true;
                pubChk.Checked = false;
                pubChk.ToolTip = "המשחק ניתן לפרסום";
            }
            //במידה והמשחק לא עומד בתנאים לפרסום
            else if (myGames.Item(i).Attributes["publishoption"].Value == "false")
            {
                pubChk.Enabled = false;
                pubChk.Checked = false;
                pubChk.ToolTip = "המשחק לא ניתן לפרסום, יש לגשת לדף העריכה ולהוסיף: ";
                if (myGames.Item(i).Attributes["name"].Value == "")
                {
                    pubChk.ToolTip += " *שם משחק";
                }
                if (myGames.Item(i).Attributes["category"].Value == "")
                {
                    pubChk.ToolTip += " * קטגוריה לאיסוף";
                }
                //בדיקה כמה פריטים שגויים ונכונים חסרים לפרסום
                XmlNodeList myGameItemsCorrect = myGames.Item(i).ChildNodes;
                int correctItems = 0;
                int wrongItems = 0;
                foreach (XmlNode x in myGameItemsCorrect)
                {
                    if (x.Attributes["correct"].Value == "true")
                    {
                        correctItems++;
                    }
                    else
                    {
                        wrongItems++;
                    }
                }
                if (correctItems < 10)
                {
                    pubChk.ToolTip += " * " + (10 - correctItems).ToString() + " פריטים נכונים";
                }
                if (wrongItems < 10)
                {
                    pubChk.ToolTip += " * " + (10 - wrongItems).ToString() + " פריטים שגויים";
                }

            }
            else
            {
                pubChk.Enabled = false;
                pubChk.Checked = false;
                pubChk.ToolTip = "תקלה";
            }
        }
    }



    protected void publishCbx_CheckedChanged(object sender, EventArgs e)
    {
        
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));
        XmlNodeList myGames = myDoc.SelectNodes("//game");

        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        int index = row.RowIndex;
        newGameTxb.Text = "";
        CheckBox pubChk = (CheckBox)GridView1.Rows[index].FindControl("publishCbx");

        if (myGames.Item(index).Attributes["published"].Value == "true")
        {
            myGames.Item(index).Attributes["published"].Value = "false";
            pubChk.Checked = false;
            pubChk.ToolTip = "המשחק ניתן לפרסום";

        }
        else
        {
            myGames.Item(index).Attributes["published"].Value = "true";
            pubChk.Checked = true;
            pubChk.ToolTip = "המשחק מפורסם";
        }
        myDoc.Save(MapPath("trees/XMLFile.xml"));
    }

    protected void deleteValidtionBtn_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton i = (ImageButton)sender;  
        string t = i.ID.ToString();
        
        if(t == "deleteValidtionYesBtn")
        {
            deleteRow((string)Session["theItemIdSessions"]);
            
        }
      
        deleteValidtionDarkScreen.Style.Add("display", "none");
    }

    
}