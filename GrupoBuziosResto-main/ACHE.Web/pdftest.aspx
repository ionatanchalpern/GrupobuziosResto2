<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pdftest.aspx.cs" Inherits="pdftest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnGetPdt" style="margin: 50px;">
            <asp:Label ID="Label1" runat="server" AssociatedControlID="tbUrl">Enter here the URL of the page you want to convert to PDF:</asp:Label>
            <asp:TextBox runat="server" ID="tbUrl" style="width:100%; display:block;" Text="http://www.grupobuziosresto.com/test.html"></asp:TextBox>
            <asp:Button runat="server" ID="btnGetPdt" Text="Get PDF" onclick="btnGetPdt_Click"/>
            <asp:Literal runat="server" ID="litError" Mode="PassThrough"></asp:Literal>
        </asp:Panel>
    </form>
</body>
</html>
