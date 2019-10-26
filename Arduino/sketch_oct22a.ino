#include "ESP8266.h"

const char *SSID     = "";
const char *PASSWORD = "";

int ledPin = 5;
int buttonApin = 7;
byte leds = 0;
unsigned long previousMillis = 0; //will store last time 
unsigned long interval = 10000; //interval (milliseconds)

SoftwareSerial mySerial(10, 11); //SoftwareSerial pins for MEGA/Uno. For other boards see: https://www.arduino.cc/en/Reference/SoftwareSerial
ESP8266 wifi(mySerial); 

void setup(void)
{
  //Start Serial Monitor at any BaudRate
  Serial.begin(115200);
  Serial.println("Begin");

  if (!wifi.init(SSID, PASSWORD))
  {
    Serial.println("Wifi Init failed. Check configuration.");
    while (true) ; // loop eternally
  }

  pinMode(ledPin, OUTPUT);
  pinMode(buttonApin, INPUT_PULLUP);
}


void loop(void)
{
  unsigned long currentMillis = millis();
  int sensorVal = digitalRead(buttonApin);
  if (digitalRead(buttonApin) == LOW)
  {
    if(currentMillis - previousMillis > interval) {
      digitalWrite(ledPin, HIGH);
      Serial.println("Chiama API");
      previousMillis = currentMillis;
      wifi.httpGet();
    }
  }
  else
  {
    digitalWrite(ledPin, LOW);
  }
}
