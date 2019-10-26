#include <Arduino.h>
#include <SPI.h>
#include <Ethernet.h>
#include "ApiClient.h"

ApiClient apiclient;

int ledPin = 5;
int buttonApin = 7;
byte leds = 0;
unsigned long previousMillis = 0; //will store last time 
unsigned long interval = 10000; //interval (milliseconds)

void setup() {
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
  Serial.println("Begin");
  apiclient.Create();

  pinMode(ledPin, OUTPUT);
  pinMode(buttonApin, INPUT_PULLUP);
}

void loop() {
  unsigned long currentMillis = millis();
  if (digitalRead(buttonApin) == LOW)
  {
    if(currentMillis - previousMillis > interval) {
      digitalWrite(ledPin, HIGH);
      Serial.println("Chiama API");
      previousMillis = currentMillis;
      apiclient.MakeCall();
    }
  }
  else
  {
    digitalWrite(ledPin, LOW);
  }
}