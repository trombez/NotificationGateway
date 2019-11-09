#ifndef __APISETTINGS_H__
#define __APISETTINGS_H__

#include "Arduino.h"
#include <SPI.h>
#include <SD.h>

struct EEPROMSettings {
  byte IpServer[4];
  unsigned int IpPort;
  byte IpStatic[4];
  byte IpDns[4];
  char Action[32];
  char Provider[16];
  char Username[32];
  char Password[32];
  char Url[32];
};

class ApiSettings
{
public: 
    const int chipSelect = 4;

    void Load();
    String ReadString(File &dataFile);
    byte ReadByte(File dataFile);
    unsigned int ReadUint(File dataFile);
};

#endif /* #ifndef __APISETTINGS_H__ */