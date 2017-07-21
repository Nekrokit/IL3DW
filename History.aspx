<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>IL3D</title>
<link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-image: url(IM1.jpg);">
    <div class="main">
        <div class="header">
            <div class="header_resize">
                <div class="logo">
                    <h1><a href="index.html"><span>I</span>L <small>Coating & filling models </small></a></h1>
                </div>
                <div class="menu">
                    <ul>
                        <li><a href="index.html">Home</a></li>
                        <li><a href="about.aspx">Algorytm</a></li>
                        <li><a href="services.aspx">Services</a></li>
                        <li><a href="History.aspx" class ="active">History</a></li>
                        <li><a href="contact.aspx?vis=0">Contact Us</a></li>
                    </ul>
                </div>
                <div class="clr"></div>
            </div>
             <div class="headert_text_resize">
                <div class="headert_text">
                    <h2>Models history </h2>
                </div>
                <div class="clr"></div>
            </div>
        </div>
        
        <asp:DataGrid ID="grid" runat="server" AllowPaging ="false" DataKeyField="ID" AutoGenerateColumns="false" CssClass ="footer" Width ="600" ForeColor="Black">
            <HeaderStyle ForeColor="White" BorderColor="Black" HorizontalAlign="Center"/>
            <Columns>
                <asp:HyperLinkColumn HeaderText="FileName"
                                    
                                    DataTextField="FileName" 
                                    DataNavigateUrlField="ID"  
                                    DataNavigateUrlFormatString="download.aspx?id={0}">
                    <ItemStyle CssClass="hyperlink" />
                </asp:HyperLinkColumn>
                <asp:BoundColumn HeaderText="Status" DataField="Status" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center">
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>

        <div class="footer">
            <div class="footer_resize">
                <p class="lf">All Rights Reserved</p>
                <p class="rf">Get More <a target="_blank" href="https://github.com/Nekrokit/IL3D/" class="hyperlinkW">On Git Hub</a></p>
                <div class="clr"></div>
            </div>
            <div class="clr"></div>
        </div>
    </div>
</body>
</html>
