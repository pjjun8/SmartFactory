/*오전 첫 수업에는 학과 수업을 듣고 왔다. 송교수님이 책을 쓰신다는 이야기와 잡설 들을 듣고 왔다.
둘째 수업 부터 스팩에 참여해서 한혁이형 노션을 참조하여 오전에 못한 수업 내용(구글 코랩으로 파이썬 해보기)을 따라 잡았다.*/
//코드 들은 노션에 캡쳐 해 놓았다.

//오후에는 Influx DB on Raspberrypi 를 했다.

// Phi_Publish_DHT11.ino
#include <WiFi.h>
#include <PubSubClient.h>

// ----- DHT11 ------------------------
#include "DHT.h"
#define DHTPIN 4     
#define DHTTYPE DHT11  //DHT11
DHT dht(DHTPIN, DHTTYPE);
// -------------------------------------

const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
char *topic_t = "MyOffice/Indoor/Temp";
char *topic_h = "MyOffice/Indoor/Humi";
char* server = "192.168.1.18";

WiFiClient wifiClient; 
PubSubClient client(server, 1883, wifiClient);

void setup() {
  Serial.begin(9600);

  Serial.print("\nConnecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print("."); delay(500);
  }
  Serial.println("\nWiFi Connected\nConnecting to broker");

  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*"); delay(500);
  }
  Serial.println("\nConnected to broker");
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

}
---------------------------------------------------------------
#nano subHumiTempInsert.py

from influxdb import InfluxDBClient

import paho.mqtt.client as mqtt
dbClient = InfluxDBClient(host='localhost', port=8086, username='influx_ship', password='1234', database='db_riatech')

def on_connect( client, userdata, flags, reason_code, properties ):
   print(f"Connect with result code:{reason_code}")
   client.subscribe("MyOffice/Indoor/#")

def on_message( client, userdata, msg ):
   print( msg.topic +" "+str(msg.payload) )
   topic = msg.topic.split('/')
   json_body =[]
   data_point = {
        'measurement': 'sensors', #dbTableName
        'tags': {'Location':' ', 'SubLocation':' '},
        'fields': {'Temp': 0.0, 'Humi':0.0}
        }

   data_point['tags']['Location'] = topic[0]
   data_point['tags']['SubLocation'] = topic[1]
   data_point['fields'][topic[2]] = float(msg.payload)

   json_body.append(data_point)
   dbClient.write_points( json_body )

mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
mqttc.username_pw_set(username="mqtt_ship", password="1234")
mqttc.on_connect = on_connect
mqttc.on_message = on_message
mqttc.connect("localhost", 1883, 60)
mqttc.loop_forever( )
