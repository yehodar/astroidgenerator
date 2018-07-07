<%@ Page Language="C#" AutoEventWireup="true" CodeFile="generatorLogin.aspx.cs" Inherits="generatorLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="margin:0px; height: 100%;">
<head runat="server">
    <title>ארוחת אסטרואידים - התחברות למחולל</title>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script src="script/JavaScript.js" type="text/javascript"></script>
</head>
<body style="margin:0px; height: 100%;">
    <form id="generatorLoginForm" runat="server">
    <div id="generatorLoginContainer" style="margin:0px; height: 100%; width: 100%;">
     <!--תפריט ניווט עליון-->
        <div id="topToolbar">
              
            <a href="#" id="helpLink">עזרה</a>                   
            <a href="#" onclick="changeInfoPopup('block')">אודות</a>
            <a href="gameHome.html">למשחק</a> 
        </div>
        <a href="gameHome.html" id="gameLogoLink"></a>
        

<%--        פאנל התחברות--%>

        <div id="loginBox">
        <asp:Label ID="loginH1" runat="server" Text="ארוחת אסטרואידים - עורך"></asp:Label>
            <br /> <br />
        <asp:Label ID="userNameLBL" runat="server" Text="שם משתמש:"></asp:Label>
        <asp:TextBox ID="userNameTBX" runat="server"></asp:TextBox>
            <br /> <br />
        <asp:Label ID="passwordLBL" runat="server" Text="סיסמא:"></asp:Label>
        <asp:TextBox ID="passwordTBX" runat="server" TextMode="Password"></asp:TextBox>
     <br />
            <br /> 
        <asp:Button ID="loginBtn" runat="server" Text="כניסה" OnClick="loginBtn_Click" />
                   <br /> 
         <asp:Label ID="feedbackLBL" runat="server" Text=""></asp:Label>
        
        </div>
        
          <!--חלונית אודות-->
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
            <img src="images/ex.png" id="infoPopupCloseBTN" onclick="changeInfoPopup('none')" />
        </div>
   
        

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
