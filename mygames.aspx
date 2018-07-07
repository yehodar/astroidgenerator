<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mygames.aspx.cs" Inherits="mygames" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>המשחקים שלי - מחולל ארוחת אסטרואידים</title>
    
 <link rel="stylesheet" href="css/main.css" type="text/css" media="screen" />
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
        <div style="direction: rtl">
            <p id="main_header">המשחקים שלי</p>
            <div id="addGameDiv">
             
                <p>יצירת משחק חדש: </p>   
            
    
        <asp:TextBox ID="newGameTxb" runat="server" MaxLength="30" Width="200px"></asp:TextBox>
        <p id="newGameTxbCounter" runat="server">0/30</p>


            <asp:ImageButton ID="addGameBtn" runat="server" ImageUrl="~/images/add.png" OnClick="addGameBtn_Click" Enabled="False" CssClass="btnHover" />
            </div>
            <br />
            <asp:XmlDataSource ID="mygamesDataSource" runat="server" DataFile="~/trees/XMLFile.xml" XPath="/games/game"></asp:XmlDataSource>
            <br />

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="mygamesDataSource" OnRowCommand="GridView1_RowCommand" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="4" BorderStyle="None">
                <Columns>

                    <asp:TemplateField HeaderText="קוד">
                        <ItemTemplate>
                            <asp:Label ID="codeLabel" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@code")%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שם">
                        <ItemTemplate>
                            <asp:Label ID="nameLabel" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@name")%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="עריכה">
                        <ItemTemplate>
                            <asp:ImageButton ID="editImageButton" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/edit.png" CommandName="edit" CssClass="btnHover" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="מחיקה">
                        <ItemTemplate>
                            <asp:ImageButton ID="deleteImageButton" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/garb.png" CommandName="deleteRow" CssClass="btnHover" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="פרסום">
	                <ItemTemplate>
                        <asp:CheckBox ID="publishCbx" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server"  CommandName="publish"  Enabled="false" AutoPostBack="true" OnCheckedChanged="publishCbx_CheckedChanged" CssClass="publishBox" />
	                </ItemTemplate>
                </asp:TemplateField>





                </Columns>
        
                <FooterStyle BackColor="#000c4a" ForeColor="#003399" />
                <HeaderStyle BackColor="#000c4a" Font-Bold="True" ForeColor="white" />
                <PagerStyle BackColor="#FFFFFF" ForeColor="#000c4a" HorizontalAlign="Left" />
                <RowStyle BackColor="#FFFFFF" ForeColor="#000c4a" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
        
            </asp:GridView>

           
         

        </div>
        <%--חלון וידוא מחיקה--%>
        <div id="deleteValidtionDarkScreen" runat="server">
            <div id="deleteValidtion" runat="server">
                <div id="deleteValidtionTextDiv">
                    <p id="deleteValidtionP" runat="server">בטוח שברצונך למחוק את המשחק?</p>
                    <p id="deleteValidtionNameP" runat="server">בעלי חיים נכחדים</p>
                    </div>
                <div id="deleteValidtionBtnDiv">
                    <asp:ImageButton ID="deleteValidtionYesBtn" runat="server" ImageUrl="~/images/yes.png" OnClick="deleteValidtionBtn_Click" CssClass="btnHover" />
                     <asp:ImageButton ID="deleteValidtionNoBtn" runat="server" ImageUrl="~/images/no.png" OnClick="deleteValidtionBtn_Click" CssClass="btnHover" />

                </div>
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
    </form>
</body>
</html>
