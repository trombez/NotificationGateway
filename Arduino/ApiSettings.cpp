#include "ApiSettings.h"
#include <EEPROM.h>
#include <Arduino.h>

void ApiSettings::Load()
{
    EEPROMSettings settings;
    int ledPin = 5;

    Serial.print(F("Initializing SD card..."));
    if (!SD.begin(chipSelect)) {
        Serial.println(F("Card not present"));
        return;
    }

    Serial.println(F("card initialized."));
    File dataFile = SD.open("SETTINGS.txt");

    if (dataFile)
    {
        settings.IpServer[0] = dataFile.readStringUntil('.').toInt();
        settings.IpServer[1] = dataFile.readStringUntil('.').toInt();
        settings.IpServer[2] = dataFile.readStringUntil('.').toInt();
        settings.IpServer[3] = dataFile.readStringUntil('\n').toInt();

        settings.IpPort = dataFile.readStringUntil('\n').toInt();

        settings.IpStatic[0] = dataFile.readStringUntil('.').toInt();
        settings.IpStatic[1] = dataFile.readStringUntil('.').toInt();
        settings.IpStatic[2] = dataFile.readStringUntil('.').toInt();
        settings.IpStatic[3] = dataFile.readStringUntil('\n').toInt();

        settings.IpDns[0] = dataFile.readStringUntil('.').toInt();
        settings.IpDns[1] = dataFile.readStringUntil('.').toInt();
        settings.IpDns[2] = dataFile.readStringUntil('.').toInt();
        settings.IpDns[3] = dataFile.readStringUntil('\n').toInt();

        dataFile.readStringUntil('\n').toCharArray(settings.Action,32);
        dataFile.readStringUntil('\n').toCharArray(settings.Provider,16);
        dataFile.readStringUntil('\n').toCharArray(settings.Username,32);
        dataFile.readStringUntil('\n').toCharArray(settings.Password,32);
        dataFile.readStringUntil('\n').toCharArray(settings.Url,32);
        dataFile.close();

        EEPROM.put(0, settings);

        digitalWrite(ledPin, HIGH);
        delay(500);
        digitalWrite(ledPin, LOW);
        delay(500);
        digitalWrite(ledPin, HIGH);
        delay(500);
        digitalWrite(ledPin, LOW);
        delay(500);
        digitalWrite(ledPin, HIGH);
        delay(500);
        digitalWrite(ledPin, LOW);
    }
    else 
    {
        Serial.println("SETTINGS.txt Not present");
    }
}