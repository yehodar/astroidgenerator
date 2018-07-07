using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;



public partial class itemseditor : System.Web.UI.Page
{
    string imagesLibPath = "uploadedFiles/";
    //משתנים גלובליים המגדירים את התנאים לכמויות הפריטים במשחק, מוגדרים גלובלית כדי שיהיה קל לשנות אם יעלה צורך
    int maxTotalItemsAllowed = 30;
    int currentTrueItems = 0;
    int maxTrueItemsAllowed = 15;
    int minTrueItemsAllowed = 10;
    int currentFalseItems = 0;
    int maxFalseItemsAllowed = 15;
    int minFalseItemsAllowed = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            loadHeaderInfo();
            //יצירת הגבלה לשדות טקסט של הוספת פריט ועריכת פריט
            edit_item_popup_astroid_txtb.Attributes.Add("maxlength", "30");
            add_item_popup_astroid_txtb.Attributes.Add("maxlength", "30");

           
           
          
        }
        else
        {
            
        }

        loadTrueItems();
        

        //הפניה לפונקציה המפעילה את כפתור השמירה והיציאה אם המשתמש מקליד מידע בשדות שם וקטגוריה
        //item_name_tb.Attributes.Add("onkeyup", "SetButtonsActive()");
        //item_category_tb.Attributes.Add("onkeyup", "SetButtonsActive()");

        item_name_tb.Attributes.Add("onkeyup", "updateCharCount('item_name_tb', 'item_name_tb_Counter', 40, 31.5, 'item_name_save_btn')");
        item_name_tb.Attributes.Add("onload", "updateCharCount('item_name_tb', 'item_name_tb_Counter', 40, 31.5, 'item_name_save_btn')");
        item_category_tb.Attributes.Add("onkeyup", "updateCharCount('item_category_tb', 'item_category_tb_Counter', 82, 74, 'item_name_save_btn')");
        item_category_tb.Attributes.Add("onload", "updateCharCount('item_category_tb', 'item_category_tb_Counter', 82, 74, 'item_name_save_btn')");




        //בחלון הנפתח של הוספת פריט או עריכת פריט - קישור לפונקציית גאווה סקריפט שמונה דינאמית את כמות התווים תוך כדי הקלדה
        edit_item_popup_astroid_txtb.Attributes.Add("onkeyup", "updateCharCount('edit_item_popup_astroid_txtb' , 'editPopopTextCounter', 68, 30,'edit_item_popup_save_btn')");
        add_item_popup_astroid_txtb.Attributes.Add("onkeyup", "updateCharCount('add_item_popup_astroid_txtb' , 'newItemPopopTextCounter',68,30,'add_item_popup_save_btn')");
       

    }
    void Page_LoadComplete(object sender, EventArgs e)
    {
        updateSurveyor();
        //בדיקת עמידה בתנאי פרסום
        checkPublishOption();
        itemeditorMainHeadline.InnerText = "עריכת משחק - " + item_name_tb.Text;
    }

    //פונקציה לטעינת כל מוני הפריטים
    protected void updateSurveyor()
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        //עדכון מונים בסוגריים
        //פריטים נכונים
        XmlNodeList selectedItemsTrue = myDoc.SelectNodes("//game[@id='" + currentgame + "']/item[@correct='true']");
        currentTrueItems = selectedItemsTrue.Count;
        item_editor_h2_true.InnerText = "פריטים נכונים (" + currentTrueItems.ToString() + ")";
        //פריטים שגויים
        XmlNodeList selectedItemsFalse = myDoc.SelectNodes("//game[@id='" + currentgame + "']/item[@correct='false']");
        currentFalseItems = selectedItemsFalse.Count;
        item_editor_h2_false.InnerText = "פריטים שגויים (" + currentFalseItems.ToString() + ")";
        //סהכ פריטים
        int currentTotalItems = currentTrueItems + currentFalseItems;
        item_editor_h1_total.InnerText = "עורך פריטים (" + currentTotalItems.ToString() + ")";


        //עדכון הערות מונה
        //פריטים נכונים
        if (currentTrueItems < minTrueItemsAllowed-1)
        {
            surveyorNoteTrue.InnerText = "*לפחות עוד " + (minTrueItemsAllowed - currentTrueItems).ToString() + " פריטים.";
            surveyorNoteTrue.Style.Add("color", "red");

            add_true_item.Enabled = true;
            add_true_item.ImageUrl = "~/images/txt.png";
            add_true_item_pic.Enabled = true;
            add_true_item_pic.ImageUrl = "~/images/pic.png";
        }
        else if(currentTrueItems == minTrueItemsAllowed - 1)
        {
            surveyorNoteTrue.InnerText = "לפחות עוד פריט אחד.";
            surveyorNoteTrue.Style.Add("color", "red");

            add_true_item.Enabled = true;
            add_true_item.ImageUrl = "~/images/txt.png";
            add_true_item_pic.Enabled = true;
            add_true_item_pic.ImageUrl = "~/images/pic.png";
        }
        else if (currentTrueItems < maxTrueItemsAllowed)
        {
            surveyorNoteTrue.InnerText="*הוזן המינימום הנדרש, אפשר עוד " +(maxTrueItemsAllowed- currentTrueItems).ToString() +" פריטים.";
            surveyorNoteTrue.Style.Add("color", "green");

            add_true_item.Enabled = true;
            add_true_item.ImageUrl = "~/images/txt.png";
            add_true_item_pic.Enabled = true;
            add_true_item_pic.ImageUrl = "~/images/pic.png";
        }
        else
        {
            surveyorNoteTrue.InnerText = "הוזן מספר הפריטים המקסימלי";
            surveyorNoteTrue.Style.Add("color", "black");

            add_true_item.Enabled = false;
            add_true_item.ImageUrl = "~/images/dis_txt.png";
            add_true_item_pic.Enabled = false;
            add_true_item_pic.ImageUrl = "~/images/dis_pic.png";
        }

        //פריטים שגויים 
        if (currentFalseItems < minFalseItemsAllowed - 1)
        {
            surveyorNoteFalse.InnerText = "*לפחות עוד " + (minFalseItemsAllowed - currentFalseItems).ToString() + " פריטים.";
            surveyorNoteFalse.Style.Add("color", "red");

            add_false_item.Enabled = true;
            add_false_item.ImageUrl = "~/images/txt.png";
            add_false_item_pic.Enabled = true;
            add_false_item_pic.ImageUrl = "~/images/pic.png";
        }
        else if (currentFalseItems == minFalseItemsAllowed - 1)
        {
            surveyorNoteFalse.InnerText = "לפחות עוד פריט אחד.";
            surveyorNoteFalse.Style.Add("color", "red");

            add_false_item.Enabled = true;
            add_false_item.ImageUrl = "~/images/txt.png";
            add_false_item_pic.Enabled = true;
            add_false_item_pic.ImageUrl = "~/images/pic.png";
        }
        else if (currentFalseItems < maxFalseItemsAllowed)
        {
            surveyorNoteFalse.InnerText = "*הוזן המינימום הנדרש, אפשר עוד " + (maxFalseItemsAllowed - currentFalseItems).ToString() + " פריטים.";
            surveyorNoteFalse.Style.Add("color", "green");

            add_false_item.Enabled = true;
            add_false_item.ImageUrl = "~/images/txt.png";
            add_false_item_pic.Enabled = true;
            add_false_item_pic.ImageUrl = "~/images/pic.png";
        }
        else
        {
            surveyorNoteFalse.InnerText = "הוזן מספר הפריטים המקסימלי";
            surveyorNoteFalse.Style.Add("color", "black");

            add_false_item.Enabled = false;
            add_false_item.ImageUrl = "~/images/dis_txt.png";
            add_false_item_pic.Enabled = false;
            add_false_item_pic.ImageUrl = "~/images/dis_pic.png";
        }



    }  
       

    //פונקציה ליצירה דינאמית של תצוגת הפריטים
    protected void loadTrueItems()
    {
        true_panel.Controls.Clear();
        false_panel.Controls.Clear();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));
        string currentgame = Session["theItemIdSessions"].ToString();
        string currentid;
        XmlNodeList selecteditems = myDoc.SelectNodes("//game[@id='" + currentgame + "']/item");

        //יצירה דינאמית בלולאה של תצוגת כל הפריטים
        for (int i = 0; i < selecteditems.Count; i++)
        {
            //משתנה השומר את המזהה של הפריט אותו אנחנו יוצרים עכשיו
            currentid = selecteditems[i].Attributes["id"].Value;

            //יצירת לייבל לתוכן הפריט
            Label itemContent = new Label();
            itemContent.ID = "item" + currentid;
            itemContent.Text = selecteditems[i].InnerXml + "  ";

            //הוספת פריט מסוג תמונה
            Image itemimage = new Image();
            itemimage.ID = "item" + currentid;
            itemimage.ImageUrl = "~/uploadedFiles/" + selecteditems[i].FirstChild.InnerXml;
            //itemimage.Attributes["class"] = "edit_item_pic";
            itemimage.Style.Add("width", "70px");
            itemimage.Style.Add("transition", "width linear 0.3s");
            //הוספת הגדלה במעבר עכבר
            itemimage.Attributes.Add("onmouseover", "MagImgOver('item" + currentid+"')");
            itemimage.Attributes.Add("onmouseout", "MagImgOver('item" + currentid + "')");
           
            //הוספת כפתור מסוג תמונה לעריכת פריט
            ImageButton editBtn = new ImageButton();
            editBtn.ID = "editBtn" + currentid;
            editBtn.ImageUrl = "~/images/edit.png";
            editBtn.Style.Add("margin-right", "10px");
            editBtn.Click += new ImageClickEventHandler(editBtn_Click);
            editBtn.Style.Add("width", "15px");
            editBtn.CssClass = "btnHover";



            //הוספת כפתור מסוג תמונה למחיקת פריט
            ImageButton deleteBtn = new ImageButton();
            deleteBtn.ID = "deleteBtn" + currentid;
            deleteBtn.ImageUrl = "~/images/garb.png";
            deleteBtn.Style.Add("margin-right", "4px");
            deleteBtn.Click += new ImageClickEventHandler(deleteBtn_Click);
            deleteBtn.Style.Add("width", "15px");
            deleteBtn.CssClass = "btnHover";

            LiteralControl mybr = new LiteralControl("<br/><br/>");

            //תנאי שמגדיר לאיזה פאנל להוסיף, לזה של הנכונים או לזה של השגויים

            if (selecteditems[i].Attributes["correct"].Value == "true")
            {
                if (selecteditems[i].Attributes["type"].Value == "text")
                {
                    true_panel.Controls.Add(itemContent);
                }
                else
                {
                    true_panel.Controls.Add(itemimage);
                }

                true_panel.Controls.Add(editBtn);
                true_panel.Controls.Add(deleteBtn);
                true_panel.Controls.Add(mybr);


            }

            else
            {
                if (selecteditems[i].Attributes["type"].Value == "text")
                {
                    false_panel.Controls.Add(itemContent);
                }
                else
                {
                    false_panel.Controls.Add(itemimage);
                }
                false_panel.Controls.Add(editBtn);
                false_panel.Controls.Add(deleteBtn);
                false_panel.Controls.Add(mybr);


            }


        }

        true_panel.DataBind();
        false_panel.DataBind();
        /*
        //קוד שרושם את כמות התווים ההתחלתית
        int startCharCount = item_name_tb.Text.Length;
        
        if (startCharCount < 3)
        {
            item_name_tb_Counter.InnerText = "יש להזין לפחות 3 תווים";
            item_name_tb_Counter.Style.Add("color", "red");
        }
        else
        {
            item_name_tb_Counter.InnerText = startCharCount.ToString() + "/30";
            item_name_tb_Counter.Style.Add("color", "black");
        }
        startCharCount = item_category_tb.Text.Length;
        
        if (startCharCount < 3)
        {
            item_category_tb_Counter.InnerText = "יש להזין לפחות 3 תווים";
            item_category_tb_Counter.Style.Add("color", "red");
            item_category_tb_Counter.Style.Add("right", "77%");
        }
        else
        {
            item_category_tb_Counter.InnerText = startCharCount.ToString() + "/30";
            item_category_tb_Counter.Style.Add("color", "black");
        }*/
    }

    //האדר האדר האדר
    //פונקציה לטעינת שם המשחק והקטגוריה לאיסוף למעלה
    protected void loadHeaderInfo()
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedGameNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']");
        item_name_tb.Text = selectedGameNode.Attributes["name"].Value;
        item_category_tb.Text = selectedGameNode.Attributes["category"].Value;
    }
    //האדר - כפתור חזור
    protected void backToMyGamesBTN_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("mygames.aspx");
    }
    //האדר- שם וקטגוריה - כפתור ביטול
    protected void item_name_back_btn_Click(object sender, ImageClickEventArgs e)
    {
        loadHeaderInfo();
        loadTrueItems();
      //  item_category_tb_Counter.Style.Add("right", "71.7%");


    }
    //האדר - שם וקטגוריה - כפתור שמירה
    protected void item_name_save_btn_Click(object sender, ImageClickEventArgs e)
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedGameNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']");
        selectedGameNode.Attributes["name"].Value = item_name_tb.Text;
        selectedGameNode.Attributes["category"].Value = item_category_tb.Text;
        myDoc.Save(MapPath("trees/XMLFile.xml"));
    }

    //פריטים פריטים פריטים

    //כפתור הוספת פריט נכון מסוג טקסט
    protected void add_true_item_Click(object sender, ImageClickEventArgs e)
    {
        add_item_popup.Attributes["class"] = "show_popup";
        Session["newItemCorrect"] = "true";
        Session["newItemType"] = "text";
    }
    //כפתור הוספת פריט שגוי מסוג טקסט
    protected void add_false_item_Click(object sender, ImageClickEventArgs e)
    {
        add_item_popup.Attributes["class"] = "show_popup";
        Session["newItemCorrect"] = "false";
        Session["newItemType"] = "text";
    }

    protected void edit_item_popup_save_btn_Click(object sender, ImageClickEventArgs e)
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']/item[@id='" + Session["currentItemSession"] + "']/content");

        selectedItemNode.InnerXml = edit_item_popup_astroid_txtb.Text;
        myDoc.Save(MapPath("trees/XMLFile.xml"));
        loadTrueItems();
        edit_item_popup_astroid_txtb.Text = "";
        edit_item_popup.Attributes["class"] = "";

       

    }
    
    //כפתור עריכה ליד פריט
    protected void editBtn_Click(object sender, ImageClickEventArgs e)
    {
        string currentgame = Session["theItemIdSessions"].ToString();

        string selectedItem = ((ImageButton)sender).ClientID;
        string curItemId = selectedItem.Substring(7);
        
        Session["currentItemSession"] = curItemId;

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']/item[@id='" + curItemId + "']");

        if (selectedItemNode.Attributes["type"].Value == "text")
        {
            edit_item_popup.Attributes["class"] = "show_popup";
            edit_item_popup_astroid_txtb.Text = selectedItemNode.FirstChild.InnerXml;
            //קוד שרושם את כמות התווים ההתחלתית
            int startCharCount = edit_item_popup_astroid_txtb.Text.Length;
            editPopopTextCounter.InnerText = startCharCount.ToString() + "/30";
        }
        else
        {
            add_pic_item_popup.Attributes["class"] = "show_popup";
            Session["isNewPic"] = "false";
            upload_pic_item_preview.ImageUrl = imagesLibPath + selectedItemNode.FirstChild.InnerXml;

        }
        
    }
    //כפתור מחיקה ליד פריט
    protected void deleteBtn_Click(object sender, ImageClickEventArgs e)
    {
        string selectedItem = ((ImageButton)sender).ClientID;
        string curItemId = selectedItem.Substring(9);
        
        string currentgame = Session["theItemIdSessions"].ToString();

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']/item[@id='" + curItemId + "']");
        selectedItemNode.ParentNode.RemoveChild(selectedItemNode);
        myDoc.Save(MapPath("trees/XMLFile.xml"));//שמירה לעץ

        if (selectedItemNode.Attributes["type"].Value == "pic")
        {
            
            System.IO.File.Delete(Server.MapPath("uploadedFiles/"+selectedItemNode.FirstChild.InnerXml));
        }

        loadTrueItems();
        

    }


    

    //חלון הוספת פריט חדש מסוג טקסט - כפתור שמירת פריט
    protected void add_item_popup_save_btn_Click(object sender, ImageClickEventArgs e)
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']");
        string lastid;
        //if (selectedItemNode.LastChild.Attributes["id"].Value !=null)
        if (selectedItemNode.ChildNodes.Count > 0)
        {
            lastid = selectedItemNode.LastChild.Attributes["id"].Value;
        }
        else
        {
            lastid = "0";
        }

        
        int newid = Convert.ToInt16(lastid) + 1;

        string newItemType = Session["newItemType"].ToString();
        string newItemCorrect = Session["newItemCorrect"].ToString();

        //הוספת פריט חדש לאקסמל
        XmlElement newItemNode = myDoc.CreateElement("item");
        newItemNode.SetAttribute("id", newid.ToString());
        newItemNode.SetAttribute("type", newItemType);
        newItemNode.SetAttribute("correct", newItemCorrect);
        XmlElement newContentNode = myDoc.CreateElement("content");
        XmlText newContentText = myDoc.CreateTextNode(add_item_popup_astroid_txtb.Text);
        newContentNode.AppendChild(newContentText);
        newItemNode.AppendChild(newContentNode);
        selectedItemNode.AppendChild(newItemNode);
        myDoc.Save(MapPath("trees/XMLFile.xml"));

        add_item_popup_astroid_txtb.Text = "";
        add_item_popup.Attributes["class"] = "";
        loadTrueItems();
        
    }

    //כפתור יציאה מחלון עריכה או הוספה של פריט
    protected void item_popup_exit_Click(object sender, ImageClickEventArgs e)
    {
        edit_item_popup_astroid_txtb.Text = "";
        edit_item_popup.Attributes["class"] = "";

        add_item_popup_astroid_txtb.Text = "";
        add_item_popup.Attributes["class"] = "";

        add_pic_item_popup.Attributes["class"] = "";
        upload_pic_item_preview.ImageUrl = "";
    }

    //כפתור הוספת פריט נכון מסוג תמונה -האדר
    protected void add_true_item_pic_Click(object sender, ImageClickEventArgs e)
    {
        add_pic_item_popup.Attributes["class"] = "show_popup";
        Session["isPicCorrect"] = "true";
        Session["isNewPic"] = "true";

    }
    //כפתור הוספת פריט שגוי מסוג תמונה - האדר
    protected void add_false_item_pic_Click(object sender, ImageClickEventArgs e)
    {
        add_pic_item_popup.Attributes["class"] = "show_popup";
        Session["isPicCorrect"] = "false";
        Session["isNewPic"] = "true";
    }

    //חלון הוספת פריט מסוג תמונה  - פונקציה לכפתור שמירה
    protected void add_pic_item_popup_save_btn_Click(object sender, ImageClickEventArgs e)
    {
        string currentgame = Session["theItemIdSessions"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));

        //האם זו תמונה חדשה או עורכים תמונה
        if (Session["isNewPic"].ToString() == "false")
        {
            XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']/item[@id='" + Session["currentItemSession"] + "']/content");
            string imageNewName = Session["newPicItemPath"].ToString();
            selectedItemNode.InnerXml = imageNewName;
            myDoc.Save(MapPath("trees/XMLFile.xml"));
        }
        else
        {
   
        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']");
            string lastid;
       

            
            
            //if (selectedItemNode.LastChild.Attributes["id"].Value !=null)
            if (selectedItemNode.ChildNodes.Count > 0)
            {
                lastid = selectedItemNode.LastChild.Attributes["id"].Value;
            }
            else
            {
                lastid = "0";
            }


            int newid = Convert.ToInt16(lastid) + 1;


            //פריקת שם התמונה החדשה בבסיס נתונים
            string imageNewName = Session["newPicItemPath"].ToString();

        string newItemCorrect = Session["isPicCorrect"].ToString();

        //הוספת פריט חדש לאקסמל
        XmlElement newItemNode = myDoc.CreateElement("item");
        newItemNode.SetAttribute("id", newid.ToString());
        newItemNode.SetAttribute("type", "pic");
        newItemNode.SetAttribute("correct", newItemCorrect);
        XmlElement newContentNode = myDoc.CreateElement("content");
        XmlText newContentText = myDoc.CreateTextNode(imageNewName);
        newContentNode.AppendChild(newContentText);
        newItemNode.AppendChild(newContentNode);
        selectedItemNode.AppendChild(newItemNode);
        myDoc.Save(MapPath("trees/XMLFile.xml"));
        }
        add_item_popup_astroid_txtb.Text = "";
        add_item_popup.Attributes["class"] = "";
        loadTrueItems();
        add_pic_item_popup_save_btn.Style.Add("opacity", "0");
        add_pic_item_popup.Attributes["class"] = "";
        upload_pic_item_preview.ImageUrl = "";
    }

    //כפתור העלאת תמונה 
    protected void upload_pic_item_btn_Click(object sender, ImageClickEventArgs e)
    {
        string fileType = pic_Item_FileUpload.PostedFile.ContentType;
        if (fileType.Contains("image"))
        {      

            //שמירת הנתיב המלא של הקובץ
            string fileName = pic_Item_FileUpload.PostedFile.FileName;
            // הסיומת של הקובץ
            string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
            // חיבור השם החדש עם הסיומת של הקובץ
            string imageNewName = "imageNewName" + endOfFileName;

            String myTime = DateTime.Now.ToString("dd_MM_yy - HH_mm_ss");
            imageNewName = "imageNewName" + myTime + endOfFileName;
            myTime = DateTime.Now.ToString("dd_MM_yy - HH_mm_ss");
            imageNewName = "imageNewName" + myTime + endOfFileName;

            //שמירה של הקובץ לספרייה בשם החדש שלו
            pic_Item_FileUpload.PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewName);

            //חיתוך ושמירה מחדש

            System.Drawing.Image bitmap;

            using (var bmpTemp = new System.Drawing.Bitmap(Server.MapPath(imagesLibPath) + imageNewName))

            {

                bitmap = new System.Drawing.Bitmap(bmpTemp);

            }

            bitmap = FixedSize(bitmap, 239, 154);

            bitmap.Save(Server.MapPath(imagesLibPath) + imageNewName);

            //שמירת כתובת התמונה לסשן
            Session["newPicItemPath"] = imageNewName;
            //יצירת תצוגה מקדימה

            add_pic_item_popup_save_btn.Style.Add("opacity", "1");
            upload_pic_item_preview.ImageUrl = imagesLibPath + imageNewName;


        }
    }

    //פונקציה שמשנה גודל לתמונות

    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)

    {

        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;
        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
        }

        else
        {
            nPercent = nPercentW;

            destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
        }


        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height, PixelFormat.Format24bppRgb);

        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);

        grPhoto.Clear(System.Drawing.Color.White);

        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;


        grPhoto.DrawImage(imgPhoto,

          new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),

          new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),

          System.Drawing.GraphicsUnit.Pixel);


        grPhoto.Dispose();

        return bmPhoto;

    }



    //פונקציה לבדיקת עמידה בתנאי פרסום
    protected void checkPublishOption()
    {
        bool canBePublish = true;
       

        string currentgame = Session["theItemIdSessions"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));
        //בדיקת שם וקטגוריה
        XmlNode selectedItemNode = myDoc.SelectSingleNode("//game[@id='" + currentgame + "']");
        if (selectedItemNode.Attributes["name"].Value.Length <3)
        {
            canBePublish = false;
        }
        if (selectedItemNode.Attributes["category"].Value.Length <3)
        {
            canBePublish = false;
        }
        //בדיקת כמות פריטים נכונים ושגויים
        XmlNodeList gameItems = myDoc.SelectNodes("//game[@id='" + currentgame + "']/item");
        int correctItems = 0;
        int wrongItems = 0;
        foreach (XmlNode x in gameItems)
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
        if (correctItems < minTrueItemsAllowed)
        {
            canBePublish = false;
        }
        if (wrongItems < minFalseItemsAllowed)
        {
            canBePublish = false;
        }

        //עדכון הסטטוס בבסיס הנתונים

        if (canBePublish == true)
        {
            selectedItemNode.Attributes["publishoption"].Value = "true";
        }
        else
        {
            selectedItemNode.Attributes["publishoption"].Value = "false";
            selectedItemNode.Attributes["published"].Value = "false";
        }
        myDoc.Save(MapPath("trees/XMLFile.xml"));

    }









   
}