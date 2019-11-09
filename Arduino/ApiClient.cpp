#include "ApiClient.h"
#include "ApiSettings.h"
#include <EEPROM.h>

// Enter a MAC address for your controller below.
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };
IPAddress server(192,168,0,3);  // numeric IP 
// Set the static IP address to use if the DHCP fails to assign
IPAddress ip(192, 168, 0, 150);
IPAddress myDns(192, 168, 0, 1);
EthernetClient client;
// Variables to measure the speed
unsigned long beginMicros, endMicros;
unsigned long byteCount = 0;
bool printWebData = true;  // set to false for better speed measurement

ApiSettings apiSettings;
EEPROMSettings settings;

void ApiClient::Create()
{
    apiSettings.Load();

    EEPROM.get(0,settings);

    server.fromString(GetIpFromEEPROM(settings.IpServer));
    myDns.fromString(GetIpFromEEPROM(settings.IpDns));
    ip.fromString(GetIpFromEEPROM(settings.IpStatic));
    
    Ethernet.begin(mac, ip, myDns);
    Serial.println(Ethernet.localIP());

    delay(1000);
}

String ApiClient::GetIpFromEEPROM(byte values[])
{
    String ipAddress;
    ipAddress.concat(values[0]);
    ipAddress.concat(".");
    ipAddress.concat(values[1]);
    ipAddress.concat(".");
    ipAddress.concat(values[2]);
    ipAddress.concat(".");
    ipAddress.concat(values[3]);
    return ipAddress;
}

String ApiClient::GetData()
{
    String data = "{";
    data.concat("\"Action\": \"");
    data.concat(settings.Action);
    data.trim();
    data.concat("\",");
    data.concat("\"Provider\": \"");
    data.concat(settings.Provider);
    data.trim();
    data.concat("\",");
    data.concat("\"UserName\":\"");
    data.concat(settings.Username);
    data.trim();
    data.concat("\",");
    data.concat("\"Password\":\"");
    data.concat(settings.Password);
    data.trim();
    data.concat("\"");
    data.concat("}");
    return data;
}

String ApiClient::GetHeader()
{
    String header = "POST ";
    header.concat(settings.Url);
    header.trim();
    header.concat(" HTTP/1.1");
    return header;
}

String ApiClient::GetHost()
{
    String host = "Host: ";
    host.concat(GetIpFromEEPROM(settings.IpServer));
    host.concat(":");
    host.concat(settings.IpPort);
    return host;
}

String ApiClient::MakeCall()
{
    Serial.print("connecting to ");
    Serial.print(server);

    // if you get a connection, report back via serial:
    if (client.connect(server, settings.IpPort)) {
        Serial.print("connected to ");
        Serial.println(client.remoteIP());
        // Make a HTTP request:
        String data = GetData();
        String header = GetHeader();
        String host = GetHost();
        client.println(header);
        client.println(host);
        client.println("Content-Type: application/json");
        client.println("Connection: close");
        client.print("Content-Length: ");
        client.println(data.length());// number of bytes in the payload
        client.println();// important need an empty line here
        client.print(data);
        Serial.println(data);
        client.flush();
    } else {
        // if you didn't get a connection to the server:
        Serial.println("connection failed");
    }
    //beginMicros = micros();

    // if there are incoming bytes available
    // from the server, read them and print them:
    // int len = client.available();
    // if (len > 0) {
    //     byte buffer[80];
    //     if (len > 80) len = 80;
    //     client.read(buffer, len);
    //     if (printWebData) {
    //     Serial.write(buffer, len); // show in the serial monitor (slows some boards)
    //     }
    //     byteCount = byteCount + len;
    // }

    // // if the server's disconnected, stop the client:
    // if (!client.connected()) {
    //     endMicros = micros();
    //     Serial.println();
    //     Serial.println("disconnecting.");
    //     client.stop();
    //     Serial.print("Received ");
    //     Serial.print(byteCount);
    //     Serial.print(" bytes in ");
    //     float seconds = (float)(endMicros - beginMicros) / 1000000.0;
    //     Serial.print(seconds, 4);
    //     float rate = (float)byteCount / seconds / 1000.0;
    //     Serial.print(", rate = ");
    //     Serial.print(rate);
    //     Serial.print(" kbytes/second");
    //     Serial.println();
    // }

    return "";
}