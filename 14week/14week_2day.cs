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

