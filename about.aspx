﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="about" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
                            <li><a href="about.aspx" class="active">Algorytm</a></li>
                            <li><a href="services.aspx">Services</a></li>
                            <li><a href="History.aspx">History</a></li>
                            <li><a href="contact.aspx?vis=0">Contact Us</a></li>
                        </ul>
                    </div>
                    <div class="clr"></div>
                </div>
                <div class="headert_text_resize">
                    <div class="headert_text">
                        <h2>Deescription of the alghorithm </h2>
                        <p><span>On this page you will be able to read about the alghoritm</span></p>
                        <p>The idea is based on the external and internal coverage of space using the n-plane rectangles, that are commonly used in functional analysis.
                        My alghoritm realises the coating of a 3-D model and her "filling"(which is invisble, because of the cubes) with the use of cubes with a set size without any shifting between them<br></p>
                    </div>
                    <div class="clr"></div>
                </div>
            </div>
            <div class="body">
                <div class="body_resize">
                        <div class="resize_bg">
                            <h2> 2-D Alghoritm</h2>
                            <p1>
                                Input data is a set of vertexes, each previous connected with the next one and the last with the first one, which creates the polygon.
                                We take each segment and cover it with squares, so that, for example, this segment will be located on y = kx + b, and on the ends of the squares in each column the
                                value on the borders of the squares of our line would be contained in the squares, precisely in the column of our squares.
                                <br />
                                That way we are coating the limits of the polygon with squares. Let us say, that the polygon is containted in a particular rectangle, frankly speaking, 
                                we are covering the rectangle with other squares and we are doing this for all the external space of the rectangle, except for the limit squares of the polygon. 
                                If besides of each of the internal squares of the polygon at least one is one of him limit squares(from the left, top, right or bottom) we mark him as the square,
                                that will be external for the polygon.<br />
                                Finally, we make the conclusion, that all squares that are not the limit ones or the external ones in a specific rectangle of our polygon, then they are internal.
                                Of course, the is a possibility of them being no internal as welll, however, we are checking this, as we are transitioning from continuos into a discrete space. 
                              
                            </p1>
                            <h2> 3-D Alghoritm</h2>
                            <div class="clr"></div>
                            <p1>
                                If we are talking about the 3-dimensional space, the idea of the alghoritm is fairly similar. We take the polygon and find the cubes, that are limit cubes for him.
                                To continue, we are making an analogy that the model is contained in a specific 3-dimensional parallelogram, we find the external cubes and in relation to them 
                                we find the internal once. 
                                The difference is in the fact, that when the internal cube will not even be, in fact, internal, we will not even see him when drawing the limit cubes. 
                                <br />
                                When we are coating the polygon with cubes, we find that plane, the angle of which will be less than 45 degrees, we will "throw away" one of the coordinates(for example, the minimum angle
                                between the polygon and the plane XY, then we are "throwing away" the z coordinate) and using the 2-D alghoritm.
                                The choice of the angle gives us a unique feature, which allows us for each of the squares found the amount of cubes will not be more than 3(theoretically 2, 
                                however, it is well known that the computer caulcations are not always precise).
                                
                            </p1>
                        </div>
                    <div class="clr"></div>
                    <form id ="form" class="form" runat ="server" enctype="multipart/form-data" method ="post" visible="true">
                        <ol>
                            <li class="buttons">
                                <input type="submit" id="Submit" value="Download application" class="send" runat ="server"/>
                                <div class="clr"></div>
                            </li>
                        </ol>
                    </form>
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

