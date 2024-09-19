ESP32 → OLED 출력
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 64 // OLED display height, in pixels
#define OLED_RESET -1
#define SCREEN_ADDRESS 0x3C
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);
void setup() {
   Serial.begin(9600);
   if(!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
      Serial.println(F("SSD1306 allocation failed"));
      for(;;); // Don't proceed, loop forever
   }
    display.clearDisplay();
   display.setTextColor(WHITE);
   display.println("Hello World!");
   display.print("I'm ");
   display.println("Happy!");
   display.display();
}
void loop() { }
=====================================================
//ESP32 → Timer Interrupt
hw_timer_t *timer0 = NULL;
volatile bool has_expired = false;
void IRAM_ATTR isr_timer0Interrupt() {
  has_expired = true;
}
void setup() {
  Serial.begin(115200);
  pinMode(LED_BUILTIN, OUTPUT);
  // Set timer frequency to 1MHz
  timer0 = timerBegin(1000000); 
  // Attach isr_timer0Interrupt to our Timer0. 
  timerAttachInterrupt(timer0, &isr_timer0Interrupt); 
  // Set alarm to call isr_timer0Interrupt every second(value in microseconds)
  // Repeat the alarm (third parameter) with unlimited count = 0(fourth parameter)
  timerAlarm(timer0, 1000000, true, 0);
}
void loop() {
  if (has_expired){
    digitalWrite(LED_BUILTIN, !digitalRead(LED_BUILTIN));
    has_expired = false;
  }
}
===================================================
//ESP32 → Timer Interrupt   2개
hw_timer_t *timer0 = NULL;
hw_timer_t *timer1 = NULL;
volatile bool has_expired0 = false;
volatile bool has_expired1 = false;

void IRAM_ATTR isr_timer0Interrupt() {
 has_expired0 = true;
}
void IRAM_ATTR isr_timer1Interrupt() {
 has_expired1 = true;
}

void setup() {
  Serial.begin(115200);
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(17, OUTPUT);

  timer0 = timerBegin(1000000); 
  timerAttachInterrupt(timer0, &isr_timer0Interrupt); 
  timerAlarm(timer0, 1000000, true, 0);

  timer1 = timerBegin(1000000); 
  timerAttachInterrupt(timer1, &isr_timer1Interrupt); 
  timerAlarm(timer1, 200000, true, 0);
}
void loop() {
  if (has_expired0){
    digitalWrite(LED_BUILTIN, !digitalRead(LED_BUILTIN));
    has_expired0 = false;
  }
  if (has_expired1){
    digitalWrite(17, !digitalRead(17));
    has_expired1 = false;
  }
}
=================================================
//ESP32 → 블로투스 연결
#include "BluetoothSerial.h"
String device_name = "ESP32-BT-Slave PSW";
BluetoothSerial SerialBT;

void setup() {
  Serial.begin(115200);
  SerialBT.begin(device_name); 
  Serial.print("\nNow you can pair it with Bluetooth!\n");
  Serial.printf("The device with name \"%s\" is started.", device_name.c_str());
}
 void loop() {
  if (Serial.available()) {
    SerialBT.write(Serial.read());
  }
  if (SerialBT.available()) {
    Serial.write(SerialBT.read());
  }
  delay(20);
}
================================================
//인터넷연결 시간 출력
#include <NTPClient.h>
// change next line to use with another board/shield
//#include <ESP8266WiFi.h>
#include <WiFi.h> // for WiFi shield
//#include <WiFi101.h> // for WiFi 101 shield or MKR1000
#include <WiFiUdp.h>

const char *ssid     = "pcroom";
const char *password = "12345678";

WiFiUDP ntpUDP;
NTPClient timeClient(ntpUDP);

void setup(){
  Serial.begin(115200);
  WiFi.begin(ssid, password);

  while ( WiFi.status() != WL_CONNECTED ) {
    delay ( 500 );
    Serial.print ( "." );
  }

  timeClient.begin();
  timeClient.setTimeOffset(32400); // 9시간, 9 * 3600 = 32400
}

void loop() {
  timeClient.update();

  Serial.println(timeClient.getFormattedTime());

  delay(1000);
}
=================================================
//ESP32 → DS3231(RTC)
// File name : RTC_DS3231_modified_example.ino
#include "RTClib.h"
RTC_DS3231 rtc;
char daysOfTheWeek[7][12] = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};
void setup () {
 Serial.begin(57600);
 if (! rtc.begin()) {
    Serial.println("Couldn't find RTC");
    Serial.flush();
    while (1) delay(10);
  }
 if (rtc.lostPower()) {
    Serial.println("RTC lost power, let's set the time!");

    rtc.adjust(DateTime(F(__DATE__), F(__TIME__)));

  }
}
void loop () {
    DateTime now = rtc.now();
    
    Serial.print(now.year(), DEC);      Serial.print('/');
    Serial.print(now.month(), DEC);     Serial.print('/');
    Serial.print(now.day(), DEC);
    Serial.print(" (");
    Serial.print(daysOfTheWeek[now.dayOfTheWeek()]);
    Serial.print(") ");
    Serial.print(now.hour(), DEC);      Serial.print(':');
    Serial.print(now.minute(), DEC);    Serial.print(':');
    Serial.print(now.second(), DEC);    Serial.println();
    Serial.print("Temperature: ");
    Serial.print(rtc.getTemperature());
    Serial.println(" C");
    delay(3000);
}

