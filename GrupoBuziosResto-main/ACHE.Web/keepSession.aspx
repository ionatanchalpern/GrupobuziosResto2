<%@ Page Language="C#" AutoEventWireup="true" CodeFile="keepSession.aspx.cs" Inherits="keepSession" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
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
