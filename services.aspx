<%@ Page Language="C#" AutoEventWireup="true" CodeFile="services.aspx.cs" Inherits="services" %>

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
                        <li><a href="services.aspx" class="active">Services</a></li>
                        <li><a href="History.aspx">History</a></li>
                        <li><a href="contact.aspx?vis=0">Contact Us</a></li>
                    </ul>
                </div>
                <div class="clr"></div>
            </div>
             <div class="headert_text_resize">
                <div class="headert_text">
                    <h2>Лишіть ваші відгуки </h2>
                    <p><span>Ми з радістю прочитаємо та оцінемо ваші відгуки та ідеї. По мірі можливості проект може змінитися на краще, у вас всі шанси </span></p>
                </div>
                <div class="clr"></div>
            </div>
        </div>
         <div class="body">
            <div class="body_resize">
                    <div class="resize_bg">

                        <h2> Send Model</h2>
                        <div id="gif" runat="server" visible="false">
                            <img src="border_helix_line_red_animation_clipart.GIF" />
                        </div>
                        <form id ="form" class="form" runat ="server" enctype="multipart/form-data" method ="post" visible="true">
                            <ol>
                                <li>
                                    <label for="obj">Choose *.obj file </label>
                                    <input type="file" id ="obj" runat="server" aria-haspopup="False" translate="no"/>
                                </li>
                                <li class="buttons">
                                    <input type="submit" id="Submit" value="Save Files" class="send" runat ="server"/>
                                    <div class="clr"></div>
                                </li>
                            </ol>
                        </form>                        
                </div>
                <div class="clr"></div>
            </div>
        </div>
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
