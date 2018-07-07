<%@ Page Language="C#" AutoEventWireup="true" CodeFile="itemseditor.aspx.cs" Inherits="itemseditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ארוחת אסטרואידים - עורך פריטים</title>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script src="script/JavaScript.js" type="text/javascript"></script>

</head>
<body>
   
    <form id="form1" runat="server">
           <div id="topToolbar">
              
                  <a href="#" id="helpLink">עזרה</a>                   
            <a href="#" onclick="changeInfoPopup('block')">אודות</a>
            <a href="gameHome.html">למשחק</a> 
            </div>
        <a href="gameHome.html" id="gameLogoLink"></a>
        
        <div id="itemeditor_container">
            <asp:XmlDataSource ID="correct_XmlDataSource" runat="server" DataFile="~/trees/XMLFile.xml" XPath="//games//game[@id='1']/item[@correct='true']"></asp:XmlDataSource>
          <p id="itemeditorMainHeadline" runat="server">עורך משחק</p>
          
              <div id="item_name_div">
                <asp:ImageButton ID="backToMyGamesBTN" runat="server" ImageUrl="~/images/back.png" OnClick="backToMyGamesBTN_Click" CssClass="btnHover"/>
                            <p class="item_editor_h2_TOP">שם המשחק:</p>
                        <asp:TextBox ID="item_name_tb" runat="server" MaxLength="30"></asp:TextBox>
                        <p id="item_name_tb_Counter" runat="server">0/30</p>
                            <p class="item_editor_h2_TOP">קטגוריה לאיסוף:</p>
                       <asp:TextBox ID="item_category_tb" runat="server" MaxLength="30"></asp:TextBox>
                <p id="item_category_tb_Counter" runat="server">0/30</p>

                            <asp:ImageButton ID="item_name_back_btn" runat="server" ImageUrl="~/images/canc.png" Enabled="false" OnClick="item_name_back_btn_Click" AlternateText="בטל שינויים"  CssClass="btnHover"/>
                            <asp:ImageButton ID="item_name_save_btn" runat="server" ImageUrl="~/images/save.png" Enabled="false" OnClick="item_name_save_btn_Click" AlternateText="שמור שינויים"  CssClass="btnHover"/>
               

            </div>

            <div id="items_gridview_header_div">
                    <p class="item_editor_h1" runat="server" id="item_editor_h1_total">עורך פריטים</p>
                </div>
            <div id="items_gridview_div">
                
                <%--חלק של פריטים נכונים--%>
                <div id="true_header_div">
                    <p class="item_editor_h2" runat="server" id="item_editor_h2_true" style="display: inline-block; margin-left: 60px;">פריטים נכונים</p>                   
                    <p class="headerText_or">הוספת פריט חדש: </p>
                    <asp:ImageButton ID="add_true_item" runat="server" ImageUrl="~/images/txt.png" OnClick="add_true_item_Click" Style="display: inline-block;"  CssClass="btnHover"/>
                    <p class="headerText_or">או </p>
                    <asp:ImageButton ID="add_true_item_pic" runat="server" ImageUrl="~/images/pic.png" OnClick="add_true_item_pic_Click" Style="display: inline-block;"  CssClass="btnHover"/>                      
                     <p class="surveyorNote" id="surveyorNoteTrue" runat="server">ניסיון</p>
                    <br /><br />
                     <asp:Panel ID="true_panel" runat="server">
                       
                    </asp:Panel>
                </div>
                <%--חלק של פריטים שגויים--%>
                <div id="false_header_div">
                    <p class="item_editor_h2" runat="server" id="item_editor_h2_false" style="display: inline-block; margin-left: 60px;">פריטים שגויים</p>
                    <p class="headerText_or">הוספת פריט חדש: </p>
                    <asp:ImageButton ID="add_false_item" runat="server" ImageUrl="~/images/txt.png" OnClick="add_false_item_Click" Style="display: inline-block;"  CssClass="btnHover"/>
                    <p class="headerText_or">או </p>
                     <asp:ImageButton ID="add_false_item_pic" runat="server" ImageUrl="~/images/pic.png" OnClick="add_false_item_pic_Click" Style="display: inline-block;" CssClass="btnHover" />
                    <p class="surveyorNote" id="surveyorNoteFalse" runat="server">ניסיון</p>
                    <br /><br />
                     <asp:Panel ID="false_panel" runat="server">
                    </asp:Panel>
              
                </div>

                

                   
           
               
               
            </div>

            <%--חלונית עריכת פריט--%>
            <div id="edit_item_popup" runat="server">
                <div id="edit_item_popup_inside">
                    <div id="edit_item_popup_astroid">
                        <asp:TextBox ID="edit_item_popup_astroid_txtb" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <p id="editPopopTextCounter" class="textCounterPopup" runat="server">0/30</p>
                    </div>   
                    <asp:ImageButton class="item_popup_exit btnHover" runat="server" ImageUrl="~/images/canc.png" OnClick="item_popup_exit_Click"/> 

                    <asp:ImageButton ID="edit_item_popup_save_btn" runat="server" ImageUrl="~/images/save.png" OnClick="edit_item_popup_save_btn_Click" Enabled="false"  CssClass="btnHover"/> 
                </div>
                </div>
             <%--חלונית הוספת פריט--%>
            <div id="add_item_popup" runat="server">
                <div id="add_item_popup_inside">
                    <div id="add_item_popup_astroid">
                        <asp:TextBox ID="add_item_popup_astroid_txtb" runat="server"  TextMode="MultiLine"></asp:TextBox>
                        <p id="newItemPopopTextCounter" class="textCounterPopup" runat="server">0/30</p>
                    </div>   
                     <asp:ImageButton class="item_popup_exit btnHover" runat="server" ImageUrl="~/images/canc.png" OnClick="item_popup_exit_Click"/> 

                    <asp:ImageButton ID="add_item_popup_save_btn" runat="server" ImageUrl="~/images/save.png" OnClick="add_item_popup_save_btn_Click"  CssClass="btnHover"/> 
                </div>
        </div>

            <%--חלונית הוספת פריט מסוג תמונה--%>
            <div id="add_pic_item_popup" runat="server">
                <div id="add_pic_item_popup_inside">
                    <div id="add_pic_item_popup_astroid">
                         <asp:Image ID="upload_pic_item_preview" runat="server" />
                        <asp:FileUpload ID="pic_Item_FileUpload" runat="server" />
                        <asp:ImageButton ID="upload_pic_item_btn" runat="server" ImageUrl="~/images/uplo.png" OnClick="upload_pic_item_btn_Click" />
                       
                    </div>   
                     <asp:ImageButton ID="item_pic_popup_exit" runat="server" ImageUrl="~/images/canc.png" OnClick="item_popup_exit_Click"  CssClass="btnHover" /> 
                    <asp:ImageButton ID="add_pic_item_popup_save_btn" runat="server" ImageUrl="~/images/save.png" OnClick="add_pic_item_popup_save_btn_Click"  CssClass="btnHover"/> 
                </div>
        </div>

        <%--חלונית אודות--%>
            <div id="infoPopupDarkScreen">
          <div id="infoPopup">
              <a href="http://www.hit.ac.il/telem/overview" target="_blank"><img src="images/logoHIT.png" /></a>
              <a href="http://www.hit.ac.il/telem/overview" target="_blank">פותח בפקולטה לטכנולוגיות למידה, מכון טכנולוגי חולון</a>
              <p>המחולל נוצר ע"י מורן נהור ויהודר שוץ.</p>
              <p>במסגרת קורס סביבות לימוד אינטראקטיביות 2, תכנות 2 ו-תכנות אינטראקטיבי 2.</p>
              <p>יוני 2018</p>
              <p>
                  מורן - moran@nahor.org‏  ||
                  יהודר - yehodar.s@gmail.com

              </p>
          </div>
                <img src="images/ex.png" id="infoPopupCloseBTN"  onclick="changeInfoPopup('none')" />
        </div>
               <!--חלונית הוראות-->
        <div id="instructionPopupDarkScreen">
            <div id="instructionPopup">
                <div id="instructionPopupimg">
                    <img src="images/instruction_1.jpg" id="instructionPopupImage"/>
                </div>
                <br />
                <div id="instructionPopupNav">
                    <p onclick="changeInstruction('prev')">הקודם</p>
                    <span>   ||   </span>
                    <p onclick="changeInstruction('next')">הבא</p>
                </div>
            </div>
            <img src="images/ex.png" id="instructionPopupCloseBTN" onclick="changeinstructionPopup('none')" />
        </div>
            
             </div>          

    </form>

</body>
</html>
