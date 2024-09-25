// esp32_subscribe_DHT11.ino
### **Phi 보드를 Subscriber로 만들기**
**목표 동작** **: ESP32 보드가 MyOffice/Indoor/#라는 topic으로 구독 신청하고 해당 topic에 대한 메시지 받기**
#include <WiFi.h>
#include <PubSubClient.h>
const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_ship";
const char* userPw = "1234";
const char* clientId = userId;
const char *topic = "MyOffice/Indoor/#";
const char* serverIPAddress = "192.168.1.18";
char messageBuf[100];

void callback(char* topic, byte* payload, unsigned int length) { 
  Serial.println("Message arrived!\nTopic: " + String(topic));
  Serial.println("Length: "+ String(length, DEC));
  
  strncpy(messageBuf, (char*)payload, length);
  messageBuf[length] = '\0';
  Serial.println("Payload: "+ String(messageBuf) + "\n\n");
}
WiFiClient wifiClient; 

PubSubClient client(serverIPAddress, 1883, callback, wifiClient);
void setup() {
   Serial.begin(9600);
   Serial.print("\nConnecting to "); Serial.println(ssid);
   WiFi.begin(ssid, password);
   while (WiFi.status() != WL_CONNECTED) {
      Serial.print("."); delay(500);
   }
   Serial.println("\nWiFi Connected\nConnecting to broker");
    while ( !client.connect(clientId, userId, userPw) ){ 
      Serial.print("*"); delay(500);
   }
   Serial.println("\nConnected to broker");
   client.subscribe(topic);
}
void loop() {
   client.loop();
}
=========================================================================
// esp32_subscribe_LED.ino
  ### **실습 과제 – Phi 보드를 Subscriber로 만들기**
**동작 목표**
- **Publisher – Broker – Subscriber**
- **Software – PC – ESP32 board**
- **Publisher가 토픽, “led”로 “on” 또는 “off”라는 메시지 발행.**
- **Subscriber는 토픽, “led”를 구독하고 그 값이 “on”이면 LED를 점등, “off”면 소등.**
- **ESP32 보드의 내장 LED는 LED_BUILTIN(GPIO2)핀에 연결되어 있음**
### 짝꿍 라즈베리 파이에 구독신청하기!!
#include <WiFi.h>
#include <PubSubClient.h>
const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_girl";
const char* userPw = "1234";
const char* clientId = userId;
char *topic = "MyOffice/Indoor/Lamp";
const char* serverIPAddress = "192.168.1.3";
char messageBuf[100];

void callback(char* topic, byte* payload, unsigned int length) { 
  Serial.println("Message arrived!\nTopic: " + String(topic));
  Serial.println("Length: "+ String(length, DEC));
  
  strncpy(messageBuf, (char*)payload, length);
  messageBuf[length] = '\0';
  String ledState = String(messageBuf);
  Serial.println("Payload: "+ ledState + "\n\n");
  if( ledState == "off"  ){      digitalWrite(LED_BUILTIN, LOW);}
  else if (ledState=="on") { digitalWrite(LED_BUILTIN, HIGH);}
  else { Serial.println("Wrong Message"); }
}

WiFiClient wifiClient; 
PubSubClient client(serverIPAddress, 1883, callback, wifiClient);

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print("."); delay(500);
  }
  Serial.println("\nWiFi Connected\nConnecting to broker");

  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*"); delay(500);
  }
  Serial.println("\nConnected to broker");
  Serial.println(String("Subscribing! topic = ") + topic);
  client.subscribe(topic);
}
void loop() {
  client.loop();
}
=============================================================
// Phi_SubPub.ino
  **Phi 보드를 Subscriber이자 Publisher로** **만들기**

**하나의 보드가 Subscriber 역할과 Publisher 역할을 동시에 수행하기**

**예를 들어 아래 두 가지 기능을 한 보드에 구현**

- **Led라는 토픽에 대한 정보를 구독하여 LED를 점등 또는 소등을 하는 Subscriber 동작**
- **센싱한 조도 값을 Publish하는 Publisher 동작**

#include <WiFi.h>
#include <PubSubClient.h>
#include <Wire.h>
#include <BH1750.h>

BH1750 lightMeter;

const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_ship";
const char* userPw = "1234";
const char* clientId = userId;
char *topicSub = "MyOffice/Indoor/lamp";
char *topicPub = "MyOffice/Indoor/Lux";
char* server = "192.168.1.18";
char messageBuf[100];

void callback(char* topic, byte* payload, unsigned int length) { 
  Serial.println("Message arrived!\nTopic: " + String(topic));
  Serial.println("Length: "+ String(length, DEC));
  
  strncpy(messageBuf, (char*)payload, length);
  messageBuf[length] = '\0';
  String ledState = String(messageBuf);
  Serial.println("Payload: "+ ledState + "\n\n");
  if( ledState == "off"  ){      digitalWrite(LED_BUILTIN, LOW);}
  else if (ledState=="on") { digitalWrite(LED_BUILTIN, HIGH);}
  else { Serial.println("Wrong Message"); }
}

WiFiClient wifiClient; 
PubSubClient client(server, 1883, callback, wifiClient);

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);
  Wire.begin();
  lightMeter.begin();

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print("."); delay(500);
  }
  Serial.println("\nWiFi Connected\nConnecting to broker");

  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*"); delay(500);
  }
  Serial.println("\nConnected to broker");
  Serial.print("Subscribing! topic = ");
  Serial.println(topicSub);
  client.subscribe(topicSub);
}
// Phi_SubPub_upgrade.ino
unsigned long int preTime = 0, currentTime;

void loop() {
  client.loop();
  currentTime = millis();
  if( currentTime - preTime > 7000 ) {
    preTime = currentTime;
    char buf[20];
    String strLuxValue = String( lightMeter.readLightLevel() );
    strLuxValue.toCharArray(buf, strLuxValue.length() );
    client.publish(topicPub, buf);
    Serial.println(String(topicPub) + " : " + buf);  
  }

}
