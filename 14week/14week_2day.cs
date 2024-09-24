//BH1750으로 부터 얻은 조도 값을 Phi 보드가 Publishing
#include <WiFi.h>
#include <PubSubClient.h>
#include <Wire.h>
#include <BH1750.h>
BH1750 lightMeter;

const char* ssid = "RiatechA2G";
const char* password = "730124go";

const char* userId = "mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
char *topic = "MyOffice/Indoor/Lux";
//char* topic = "MyOffice/Indoor/Temp";
char* server = "192.168.1.18"; //MQTT broker IP address

WiFiClient wifiClient; 
PubSubClient client(server, 1883, wifiClient);

void setup() {
  Serial.begin(9600);
  Serial.print("\nConnecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
  }
  Serial.println("\nWiFi Connected");
  Serial.print("Local IP address : ");
  Serial.println(WiFi.localIP());
  Serial.println("Connecting to broker");
  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*");
    delay(1000);
  }
  Serial.println("\nConnected to broker");
  Wire.begin();
  lightMeter.begin();
}

void loop() {
  char buf[20];
  String strLuxValue = String( lightMeter.readLightLevel() );
  strLuxValue.toCharArray(buf, strLuxValue.length() );
  
  client.publish(topic, buf);
  Serial.println(String(topic) + " : " + buf);
  delay(2000);

}
=============================================================================
//DHT11으로 부터 얻은 온습도 값을 Phi 보드가 Publishing
#include <WiFi.h>
#include <PubSubClient.h>
//#include <Wire.h>
//#include <BH1750.h>
//BH1750 lightMeter;
// ----- DHT11 ------------------------
#include "DHT.h"
#define DHTPIN 4     
#define DHTTYPE DHT11
DHT dht(DHTPIN, DHTTYPE);
// -------------------------------------

const char* ssid = "RiatechA2G";
const char* password = "730124go";

const char* userId = "mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
char *topic_t = "Sensors/MyOffice/Indoor/Temp";
char *topic_h = "Sensors/MyOffice/Indoor/Humi";
//char *topic = "MyOffice/Indoor/Lux";
//char* topic = "MyOffice/Indoor/Temp";
char* server = "192.168.1.18"; //MQTT broker IP address

WiFiClient wifiClient; 
PubSubClient client(server, 1883, wifiClient);

void setup() {
  Serial.begin(9600);
  Serial.print("\nConnecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
  }
  Serial.println("\nWiFi Connected");
  //Serial.print("Local IP address : ");
  //Serial.println(WiFi.localIP());
  Serial.println("Connecting to broker");
  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*");
    delay(1000);
  }
  Serial.println("\nConnected to broker");
  //Wire.begin();
  //lightMeter.begin();
  dht.begin();
}

void loop() {
  char buf[20];
  float h = dht.readHumidity();
  float t = dht.readTemperature();
  if (isnan(h) || isnan(t) ) {
    Serial.println("Failed to read from DHT sensor!");
    return;
  }

  String str = String(h);
  str.toCharArray(buf, str.length());
  client.publish(topic_h, buf);
  Serial.println(String(topic_h) + " : " + buf);
  
  str = String(t);
  str.toCharArray(buf, str.length());
  client.publish(topic_t, buf);
  Serial.println(String(topic_t)  + " : " + buf);
  delay(2000);

  // char buf[20];
  // String strLuxValue = String( lightMeter.readLightLevel() );
  // strLuxValue.toCharArray(buf, strLuxValue.length() );
  
  // client.publish(topic, buf);
  // Serial.println(String(topic) + " : " + buf);
  // delay(2000);
}

