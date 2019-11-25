# NotificationGateway
**Weekend Project : Voice Call by API from Arduino**

Simple project to call a phone number via Twilio service or to launch an IFTTT action to make a voip call.
All invoked by an HTTP API.

The (weekend) project born to give an endpoint to an Arduino connected to an Ethernet Shield. 

Here the Arduino code to make the Call:
https://github.com/trombez/ArduinoHttpApi

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


**To Do**
This is only a one day project, so there are a lots of improvement you can do!
