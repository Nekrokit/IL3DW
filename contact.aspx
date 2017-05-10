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
                        <li><a href="about.html">Algorytm</a></li>
                        <li><a href="services.html">Services</a></li>
                        <li><a href="contact.aspx" class="active">Contact Us</a></li>
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
                <!--<div class="left">-->
                    <div class="resize_bg">
                        <h2> Contact Us</h2>
                        <div runat="server" id="thanksDiv" visible ="false">
                            <p>Thank you for your message, we will review it.</p>
                        </div>
                        <form action="#" method="post" id="contactform" runat="server">
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
                                    <input type="submit" name="imageField" id="imageField" value="Send message" class="send" />
                                    <div class="clr"></div>
                                </li>
                            </ol>
                        </form>
                    <!--</div>-->
                </div>
                <!--<div class="right">
                    <div class="resize_bg">
                        <h2>Sidebar<span> Menu</span></h2>
                        <ul>
                            <li><a href="#">Home</a></li>
                            <li><a href="#">TemplateInfo</a></li>
                            <li><a href="#">Style Demo</a></li>
                            <li><a href="#">Blog</a></li>
                            <li><a href="#">Archives</a></li>
                        </ul>
                    </div>
                </div>-->
                <div class="clr"></div>
            </div>
        </div>
        <!--<div class="FBG">
          <div class="FBG_resize">
            <div class="blok">
              <h2>Image <span>Gallery</span></h2>
              <img src="images/gallery_1.jpg" alt="" width="95" height="94" /><img src="images/gallery_2.jpg" alt="" width="95" height="94" /><img src="images/gallery_3.jpg" alt="" width="95" height="94" /> <img src="images/gallery_1.jpg" alt="" width="95" height="94" />
              <div class="clr"></div>
              <h2>Lorem <span>ipsum</span> </h2>
              <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec libero. Suspendisse bibendum. <a href="#">Cras id urna</a>. Morbi tincidunt, orci ac convallis aliquam, lectus turpis. varius lorem, eu posuere nunc justo tempus leo. </p>
            </div>
            <div class="blok">
              <h2>About</h2>
              <img src="images/fbg_1.jpg" alt="" width="96" height="96" />
              <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec libero. Suspendisse bibendum. Cras id urna. Morbi tincidunt, orci ac convallis aliquam, lectus turpis varius lorem, eu turpis varius lorem, eu posuere nunc justo tempus leo. Donec mattis, purus nec placerat bibendum, dui pede condimentum odio, ac blandit ante orci ut diam. <a href="#">Learn more...</a><br />
              </p>
            </div>
            <div class="blok">
              <h2>Contact</h2>
              <p>Praesent dapibus, neque id cursus faucibus, tortor neque egestas augue, eu vulputate magna eros eu erat.<br />
                Donec mattis, purus nec placerat ibendum, dui pede condimentum odio, ac blandit ante orci ut diam.<br />
                <br />
                E-mail: <a href="#">support@yoursite.com</a><br />
                Telephone : +1 (123) 444-5677<br />
                +1 (123) 444-5677<br />
                +1 (123) 444-5677</p>
            </div>
            <div class="clr"></div>
          </div>
        </div>-->
        <div class="footer">
            <div class="footer_resize">
                <p class="lf">All Rights Reserved</p>
                <p class="rf">Get More <a target="_blank" href="https://github.com/Nekrokit/IL3D/">On Git Hub</a></p>
                <div class="clr"></div>
            </div>
            <div class="clr"></div>
        </div>
    </div>
</body>
</html>
