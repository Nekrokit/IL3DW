<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="contact" %>

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
                        <li><a href="History.aspx">History</a></li>
                        <li><a href="contact.aspx?vis=0" class="active">Contact Us</a></li>
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
                        <h2> Contact Us</h2>
                        <div runat="server" id="thanksDiv" visible ="false">
                            <p>Thank you for your message, we will review it.</p>
                        </div>
                        <form action="#" method="post" id="contactform" class="form" runat="server">
                            <ol>
                                <li>
                                    <label for="name">Your Name*</label>
                                    <input id="name" name="name" class="text"  runat="server"/>
                                </li>
                                <li>
                                    <label for="email">E-Mail*</label>
                                    <input id="email" name="email" class="text"  runat="server"/>
                                </li>
                                <li>
                                    <label for="company">Entity</label>
                                    <input id="company" name="company" class="text"  runat="server"/>
                                </li>
                                <li>
                                    <label for="message">Your Message*</label>
                                    <textarea id="message" name="message" rows="6" cols="50" runat="server"></textarea>
                                </li>
                                <li class="buttons">
                                    <asp:Button id="imageField" Text="Send message" OnClick="SaveMessage" runat="server"/>
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
