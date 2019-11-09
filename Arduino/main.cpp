#include <Arduino.h>
#include <SPI.h>
#include <Ethernet.h>
#include "ApiClient.h"
#include "pitches.h"

ApiClient apiclient;
int ledPin = 5;
int buttonApin = 7;
int buzzerpin = 8;
byte leds = 0;
unsigned long previousMillis = 0; //will store last time 
unsigned long interval = 10000; //interval (milliseconds)
int melody[] = {
  NOTE_C5, NOTE_D5, NOTE_E5, NOTE_F5, NOTE_G5, NOTE_A5, NOTE_B5, NOTE_C6};
int duration = 2000;  

void setup() {
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
  Serial.println(F("Begin"));
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
      tone(buzzerpin, melody[2], duration);
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