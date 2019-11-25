# NotificationGateway
**Weekend Project : Voice Call by API from Arduino**

Simple project to call a phone number via Twilio service or to launch an IFTTT action to make a voip call.
All invoked by an HTTP API.

The (weekend) project born to give an endpoint to an Arduino connected to an Esp8266. 
At a later time i made a second version with the Etherned Shield.

**The problem:** dad needed a doorbell that call his phone. Clean and simple!
The natural solution should be to buy one on Amazon, BUT, it would have been too simple!

So, first step, connect an Arduino to a WiFi lan, with an ESP8266, to make an HTTP POST call.
I followed this tutorial to do this:
https://www.linkedin.com/pulse/how-make-rest-api-http-post-call-using-arduino-uno-esp8266-taha-ali/

Alternatively you can use an Ethernet Shield. In this case the connection is easier.

I changed the sketch to make the call, only if a switch is pressed.
I followed the tutorial you can find on PDF of this kit: https://www.elegoo.com/download/ (Elegoo UNO R3 The Most Complete Starter Kit), 
Lesson 5, Digital Input.

The final sketches, both wifi than ethernet, is on repository (folder Arduino).

**Next step**, make a simple API C# project, that receive the call from Arduino, and make a voice call to a smartphone!
To make the voice call i use the Twilio Service, here how to integrate in a C# project : 
https://www.twilio.com/docs/voice/quickstart/csharp#make-an-outgoing-phone-call-with-c

The account settings of Twilio are stored in the appsetting.json

Ok, now, we need to host our project! For test, I have decided to host on my machine, on IIS!
Here, how to Host on IIS : 
https://docs.microsoft.com/it-it/aspnet/core/host-and-deploy/iis/?view=aspnetcore-3.0

At this point, i faced with an another problem, my api was not accessible from other devices on LAN! 
So, i followed this tutorial to open the firewall correctly:
https://www.c-sharpcorner.com/article/access-website-hosted-on-iis/

**Finally, everything was set up correctly!**

**Twilio**

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/g_Qdqws4bMw/0.jpg)](https://www.youtube.com/watch?v=g_Qdqws4bMw)

**IFTTT**

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/0ANHrTCnIMU/2.jpg)](https://www.youtube.com/watch?v=0ANHrTCnIMU)

**To Do**
This is only a one day project, so there are a lots of improvement you can do!
