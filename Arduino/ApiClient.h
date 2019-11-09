#ifndef __APICLIENT_H__
#define __APICLIENT_H__

#include "Arduino.h"
#include <SPI.h>
#include <Ethernet.h>

class ApiClient
{
private:
    String GetData();
    
    unsigned long beginMicros, endMicros;
    unsigned long byteCount = 0;
    bool printWebData = true;
    String GetHeader();
    String GetHost();
public:    
    void Create();
    String MakeCall();
    String GetIpFromEEPROM(byte values[]);
};

#endif /* #ifndef __APICLIENT_H__ */