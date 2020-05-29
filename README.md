# NotificationGateway

Simple study project to call a phone number via Twilio service or to launch an IFTTT action to make a voip call.
All invoked by an HTTP API.

This (weekend) project born to give an endpoint to an Arduino connected to an Ethernet Shield. 

## Arduino Project

Here the Arduino code to make the Call:
https://github.com/trombez/ArduinoHttpApi

## Provider

#### Twilio

To make the voice call i use the Twilio Service, here how to integrate in a C# project : 
[Twilio Integration](https://www.twilio.com/docs/voice/quickstart/csharp)

The account settings of Twilio are stored in the appsetting.json

#### IFTTT

To make a VOIP call directly with IFTTT and Webhooks

[IFTTT Webhooks](https://ifttt.com/maker_webhooks)

The account settings of IFTTT are stored in the appsetting.json

## Local Envinroment

Ok, we need to host our project! 

For test, I have decided to host on my machine, on IIS!
Here the instruction to host Asp.Net Core 3 on local IIS : 

https://docs.microsoft.com/it-it/aspnet/core/host-and-deploy/iis/?view=aspnetcore-3.0

At this point, i faced with an another problem, my api was not accessible from other devices on LAN! 
So, i followed this tutorial to open the firewall correctly:
https://www.c-sharpcorner.com/article/access-website-hosted-on-iis/

**Now, everything was set up correctly!**

## Docker Integration

As project during the Covid Lockdown, i decided to study Docker, and to deploy this application on an Ubuntu Server with docker. You can find on project files the docker-compose configuration file, with all the configuration to execute the Asp.Net Web API behind NGINX reverse proxy.

## Next Step

Azure Function

AWS Lambda
