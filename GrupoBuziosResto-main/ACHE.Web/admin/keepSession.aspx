<%@ Page Language="C#" AutoEventWireup="true" Inherits="admin_keepSession" CodeFile="keepSession.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta id="MetaRefresh" http-equiv="refresh" content="30;url=KeepSessionAlive.aspx" runat="server" />

    <script language="javascript">
        window.status = "<%=WindowStatusText%>";
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%=WindowStatusText%>
        </div>
    </form>
</body>
</html>
