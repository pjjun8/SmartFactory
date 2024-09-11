//**가변 저항 손잡이를 오른쪽으로 서서히 돌리면 LED가 점점** **밝아지고 손잡이를 끝까지 돌리면 LED 최대 밝기로 점등.**
void setup() {
  pinMode(10, OUTPUT);
}
void loop() {
   analogWrite(10, 32);
   delay(1000);
   analogWrite(10, 0);
   delay(1000);
}

-------------------------------------
int ledPin = 9;

void setup() {
  pinMode(ledPin, OUTPUT);
}

void loop() {
  int sensorValue = analogRead(A0);
  int brightness = sensorValue / 1023.0 * 255;
  analogWrite(ledPin,  brightness);
  delay(20);
}

======================================
시리얼 모니터를 통해 DC 모터 속도 제어

int duty = 128;
int motorPin = 9;
void setup() {
  pinMode(motorPin, OUTPUT);
  analogWrite(motorPin, duty);
  Serial.begin(9600);
}

void loop() {
  if( Serial.available( ) ) {
    duty = Serial.parseInt( );
    analogWrite(motorPin, duty);
  }
}

=====================================
**PCF8591을 ADC로 동작 시키기**

 **동작 개요 - PCF8591 모듈의 A0핀의 전압을 AD 변환**

- **PCF8591 모듈의 AIN0 핀을 아두이노** **3.3V 핀에 연결**
// pcf8591.h

#define ADDR0_PCF8591 0x48
#define ADDR1_PCF8591 0x49
#define ADC_CH0       0x00
#define ADC_CH1       0x01

------------------------------------------

#include <Wire.h>
#include "pcf8591.h"
void setup() {
  Serial.begin(9600);
  Wire.begin();
}
void loop() {
  Wire.beginTransmission(ADDR0_PCF8591);
  Wire.write(ADC_CH0);
  Wire.endTransmission();

  Wire.requestFrom(ADDR0_PCF8591, 1);
  Serial.println(Wire.read() * 5.0/256);
  delay(100);
}
======================================
**실습**
**BH1750 라이브러리 파일을 아두이노** **IDE에 등록**
**아두이노** **IDE에서 ‘파일/예제/BH1750/BH1750test 선택**

/*

Example of BH1750 library usage.

This example initialises the BH1750 object using the default high resolution
continuous mode and then makes a light level reading every second.

Connections

  - VCC to 3V3 or 5V
  - GND to GND
  - SCL to SCL (A5 on Arduino Uno, Leonardo, etc or 21 on Mega and Due, on
    esp8266 free selectable)
  - SDA to SDA (A4 on Arduino Uno, Leonardo, etc or 20 on Mega and Due, on
    esp8266 free selectable)
  - ADD to (not connected) or GND

ADD pin is used to set sensor I2C address. If it has voltage greater or equal
to 0.7VCC voltage (e.g. you've connected it to VCC) the sensor address will be
0x5C. In other case (if ADD voltage less than 0.7 * VCC) the sensor address
will be 0x23 (by default).

*/

#include <BH1750.h>
#include <Wire.h>

BH1750 lightMeter;

void setup() {
  Serial.begin(9600);

  // Initialize the I2C bus (BH1750 library doesn't do this automatically)
  Wire.begin();
  // On esp8266 you can select SCL and SDA pins using Wire.begin(D4, D3);
  // For Wemos / Lolin D1 Mini Pro and the Ambient Light shield use
  // Wire.begin(D2, D1);

  lightMeter.begin();

  Serial.println(F("BH1750 Test begin"));
}

void loop() {
  float lux = lightMeter.readLightLevel();
  Serial.print("Light: ");
  Serial.print(lux);
  Serial.println(" lx");
  delay(1000);
}
===========================================================
**LIS3DH 모듈**
**시리얼 모니터 실행(Ctrl+Shift+m)**
**출력 형태 형식** **확인 후 시리얼 모니터 닫고 시리얼 풀로더 실행(Ctrl+Shift+l)**
**센서** **모듈을 흔들어 보세요.**

#include "SparkFunLIS3DH.h"
#include "Wire.h"
#include "SPI.h"

LIS3DH myIMU; //Default constructor is I2C, addr 0x19.

void setup() {
  Serial.begin(9600);
  delay(1000);
  //Serial.println("Processor came out of reset.\n");
  myIMU.begin();
}
void loop()
{
  //Get all parameters
  //Serial.print("\nAccelerometer:\n");
  Serial.print(myIMU.readFloatAccelX(), 4);
  Serial.print(",");
  Serial.print(myIMU.readFloatAccelY(), 4);
  Serial.print(",");
  Serial.println(myIMU.readFloatAccelZ(), 4);
  delay(40);    // f = 1/40 x 1000 = 약 25Hz
}
==========================================================
온습도 센서 
Code for DHT11
파일|예제|DHT Sensor Library|DHTtester를 아래와 같이 수정
DHT22의 경우  최소 샘플링 주기는 2초 이상 – delay(2000);
DHT11의 경우  최소 샘플링 주기는 1초 이상 – delay(1000);

#include "DHT.h"
#define DHTPIN 4     // Digital pin connected to the DHT sensor
#define DHTTYPE DHT11   // DHT 22  (AM2302), AM2321

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  Serial.println(F("DHTxx test!"));
  dht.begin();
}

void loop() {

  delay(2000);
  float h = dht.readHumidity();
  float t = dht.readTemperature();
  float f = dht.readTemperature(true);

  if (isnan(h) || isnan(t) || isnan(f)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  float hif = dht.computeHeatIndex(f, h);
  float hic = dht.computeHeatIndex(t, h, false);

  Serial.print(F("Humidity: "));
  Serial.print(h);
  Serial.print(F("%  Temperature: "));
  Serial.print(t);
  Serial.print(F("°C "));
  Serial.print(f);
  Serial.print(F("°F  Heat index: "));
  Serial.print(hic);
  Serial.print(F("°C "));
  Serial.print(hif);
  Serial.println(F("°F"));
}
