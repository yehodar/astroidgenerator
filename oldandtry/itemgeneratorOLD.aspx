<%@ Page Language="C#" AutoEventWireup="true" CodeFile="itemgeneratorOLD.aspx.cs" Inherits="generator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="direction: rtl">
    
        <br />
        <asp:RadioButtonList ID="myitems_rbl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="myitems_rbl_SelectedIndexChanged">
        </asp:RadioButtonList>
        <br />
        <br />
    
        <br />
        סוג פריט:
        <br />
        <asp:RadioButtonList ID="type_rbl" runat="server" AutoPostBack="True">
            <asp:ListItem Value="text">טקסט</asp:ListItem>
            <asp:ListItem Value="pic">תמונה</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        נכון/לא נכון:
        <br />
        <asp:RadioButtonList ID="correct_rbl" runat="server">
            <asp:ListItem Value="true">פריט נכון</asp:ListItem>
            <asp:ListItem Value="false">פריט שגוי</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        תוכן פריט:<br />
&nbsp;<br />
        <asp:TextBox ID="content_txtb" runat="server" Width="319px"></asp:TextBox>
    
        <br />
        <br />
        <asp:Button ID="update_btn" runat="server" OnClick="update_btn_Click" Text="עדכן" />
        <br />
        <asp:Button ID="delete_btn" runat="server" Text="מחק" OnClick="delete_btn_Click" />
        <br />
    
    </div>
    </form>
</body>
</html>
