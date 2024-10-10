//ESP32 만 해본 코드
#include <DHT.h>
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <BH1750.h>

#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET -1
#define SCREEN_ADDRESS 0x3C

Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);
DHT dht(4, DHT11);            // DHT11 센서 설정
BH1750 lightMeter;            // BH1750 조도 센서 설정

const int led1 = 2;  // 진행중 LED
const int led2 = 13; // 오류 발생 LED
const int led3 = 14; // 완료 LED
const int led4 = 0;  // 테스트 완료 LED
const int motor = 12; // 모터 제어 핀
const int tester_reg = 36; // 가변저항 핀

unsigned long sensorPreviousMillis = 0;   // 센서 값 읽기용 타이머
unsigned long processStartMillis = 0;     // 공정 완료 체크용 타이머
const long sensorInterval = 2000;         // 센서 값을 읽는 주기 (2초)
const long processTime = 15000;           // 공정 완료 시간 (15초)
bool processStarted = false;              // 공정 시작 여부
bool processCompleted = false;            // 공정 완료 여부
bool testStarted = false;                 // TEST 공정 시작 여부

void setup() {
  Serial.begin(115200);              // 시리얼 통신 설정
  pinMode(led1, OUTPUT);
  pinMode(led2, OUTPUT);
  pinMode(led3, OUTPUT);
  pinMode(led4, OUTPUT);
  pinMode(motor, OUTPUT);

if (!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
    Serial.println(F("SSD1306 allocation failed"));
    for (;;) {
      // 무한 루프: 에러 발생 시 여기에서 멈춤
    }
  }

  dht.begin();
  Wire.begin();
  lightMeter.begin();
  display.setTextColor(WHITE);

  // 공정 시작 (LED1 켜고 모터 돌리기)
  digitalWrite(led1, HIGH);
  analogWrite(motor, 100);  // 모터를 PWM 값 100으로 동작
  processStartMillis = millis();  // 공정 시작 시간 기록
  processStarted = true;  // 공정 시작 상태
  processCompleted = false;  // 공정 완료 상태 초기화
  testStarted = false;  // TEST 공정 초기화
}

void loop() {
  unsigned long currentMillis = millis();

  // 공정이 완료된 후에는 더 이상 센서 데이터를 읽지 않음
  if (testStarted) {
    return;  // TEST 공정 완료 후 동작을 멈춤
  }

  // 온습도, 조도 센서 값 읽기 (공정 완료 전까지만)
  if (!processCompleted && currentMillis - sensorPreviousMillis >= sensorInterval) {
    sensorPreviousMillis = currentMillis;  // 이전 시간을 현재 시간으로 업데이트

    // 센서 값 읽기
    float temp = dht.readTemperature();
    float humid = dht.readHumidity();
    float lux = lightMeter.readLightLevel();

    if (isnan(temp) || isnan(humid)) {
      Serial.println("Failed to read from DHT sensor!");
    } else {
      // 시리얼 모니터에 센서 값 출력
      Serial.print("Temperature: ");
      Serial.print(temp);
      Serial.print(" °C  |  Humidity: ");
      Serial.print(humid);
      Serial.print(" %  |  Light Level: ");
      Serial.print(lux);
      Serial.println(" lx");

      //display monitor
      display.clearDisplay();
      display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
      display.print("Processing...");
      display.display();
      delay(1000);
    }

    // 오류 발생 시 LED2 켜고 LED1과 모터 끄기
    if (temp > 27 || humid > 70 || lux < 100) {
      digitalWrite(led2, HIGH);   // 오류 등 켜기
      digitalWrite(led1, LOW);    // LED1 끄기
      analogWrite(motor, 0);      // 모터 끄기
      processStarted = false;     // 공정 중지 상태

      //display monitor
      display.clearDisplay();
      display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
      display.print("Process Error!");
      display.display();
      delay(1000);
    } else {
      digitalWrite(led2, LOW);    // 오류 등 끄기
      digitalWrite(led1, HIGH);   // 공정 진행 중 LED1 유지
      analogWrite(motor, 100);    // 모터 계속 동작

      //display monitor
      display.clearDisplay();
      display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
      display.print("Processing...");
      display.display();
      delay(1000);

      // 오류 없이 진행될 때 공정 타이머 시작
      if (!processStarted) {
        processStartMillis = currentMillis;  // 공정 타이머 리셋
        processStarted = true;  // 공정 시작 상태
      }
    }
  }

  // 공정이 시작된 후 15초 경과 시 완료 처리
  if (!processCompleted && processStarted && (currentMillis - processStartMillis >= processTime)) {
    digitalWrite(led3, HIGH);  // 완료 등 켜기
    digitalWrite(led1, LOW);   // LED1 끄기
    analogWrite(motor, 0);     // 모터 끄기
    processCompleted = true;   // 공정 완료 상태 설정
    Serial.println("Process completed. Waiting for potentiometer adjustment...");

    //display monitor
    display.clearDisplay();
    display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
    display.print("Process Done!");
    display.display();
    delay(1000);
  }

  // 공정 완료 후 TEST 공정으로 넘어감 (가변저항 값 받기 시작)
  if (processCompleted && !testStarted) {
    int potValue = analogRead(tester_reg);
    
    // 가변저항 값 시리얼 모니터로 출력
    Serial.print("Potentiometer Value: ");
    Serial.println(potValue);

    // TEST 공정: 가변저항 값이 임계값을 넘으면 LED4 켜기 (테스트 완료)
    if (potValue > 3000) {
      digitalWrite(led4, HIGH);   // LED4 켜기
      digitalWrite(led3, LOW);    // LED3 끄기
      analogWrite(motor, 0);      // 모터 끄기 (테스트 완료)
      testStarted = true;         // TEST 공정 완료
      Serial.println("Test completed.");
    } else {
      digitalWrite(led4, LOW);    // LED4 끄기
    }
  }
}
=======================================================================
//라즈베리 연동한 ESP32 코드
#include <WiFi.h>
#include <PubSubClient.h>
#include <DHT.h>
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <BH1750.h>
#define PAYLOAD_MAXSIZE 64

#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET -1
#define SCREEN_ADDRESS 0x3C

Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);
DHT dht(4, DHT11);            // DHT11 센서 설정
BH1750 lightMeter;            // BH1750 조도 센서 설정

const int led1 = 2;  // 진행중 LED
const int led2 = 13; // 오류 발생 LED
const int led3 = 14; // 완료 LED
const int led4 = 0;  // 테스트 완료 LED
const int motor = 12; // 모터 제어 핀
const int tester_reg = 36; // 가변저항 핀

unsigned long sensorPreviousMillis = 0;   // 센서 값 읽기용 타이머
unsigned long processStartMillis = 0;     // 공정 완료 체크용 타이머
const long sensorInterval = 2000;         // 센서 값을 읽는 주기 (2초)
const long processTime = 15000;           // 공정 완료 시간 (15초)
bool processStarted = false;              // 공정 시작 여부
bool processCompleted = false;            // 공정 완료 여부
bool testStarted = false;                 // TEST 공정 시작 여부

const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
char *topic = "MyOffice/Indoor/SensorValue";
char* server = "192.168.1.18";

WiFiClient wifiClient; 
PubSubClient client(server, 1883, wifiClient);

void setup() {
  Serial.begin(115200);              // 시리얼 통신 설정
  pinMode(led1, OUTPUT);
  pinMode(led2, OUTPUT);
  pinMode(led3, OUTPUT);
  pinMode(led4, OUTPUT);
  pinMode(motor, OUTPUT);

  if (!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
    Serial.println(F("SSD1306 allocation failed"));
    for (;;) {
      // 무한 루프: 에러 발생 시 여기에서 멈춤
    }
  }

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
  Wire.begin();
  lightMeter.begin();
  display.setTextColor(WHITE);

  // 공정 시작 (LED1 켜고 모터 돌리기)
  digitalWrite(led1, HIGH);
  analogWrite(motor, 100);  // 모터를 PWM 값 100으로 동작
  processStartMillis = millis();  // 공정 시작 시간 기록
  processStarted = true;  // 공정 시작 상태
  processCompleted = false;  // 공정 완료 상태 초기화
  testStarted = false;  // TEST 공정 초기화

  //display monitor
      display.clearDisplay();
      display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
      display.print("Processing...");
      display.display();
      //delay(1000);
}

void publishSensorData() {
  char payload[128];  // PAYLOAD_MAXSIZE 크기의 버퍼 선언
  float h = dht.readHumidity();
  float t = dht.readTemperature();
  float l = lightMeter.readLightLevel();

  if (isnan(h) || isnan(t)) {
    Serial.println("Failed to read from sensor!");
    return;
  }

  // 센서 데이터를 문자열로 변환하여 payload에 저장
  String sPayload = "{'Temp':"
                    + String(t, 1)
                    + ",'Humi':"
                    + String(h, 1)
                    + ",'Lux':"
                    + String(l, 1)
                    + "}";
  
  sPayload.toCharArray(payload, 128);
  client.publish(topic, payload);  // MQTT 퍼블리시
  //Serial.print("Published to topic ");
  //Serial.println(topic);
  //Serial.println(payload);
}

void loop() {
  unsigned long currentMillis = millis();
  
  // 공정이 완료된 후에는 더 이상 센서 데이터를 읽지 않음
  if (testStarted) {
    return;  // TEST 공정 완료 후 동작을 멈춤
  }

  // 온습도, 조도 센서 값 읽기 (공정 완료 전까지만)
  if (!processCompleted && currentMillis - sensorPreviousMillis >= sensorInterval) {
    sensorPreviousMillis = currentMillis;  // 이전 시간을 현재 시간으로 업데이트
    
    publishSensorData();

    char payload[PAYLOAD_MAXSIZE];
    // 센서 값 읽기
    float temp = dht.readTemperature();
    float humid = dht.readHumidity();
    float lux = lightMeter.readLightLevel();

    if (isnan(temp) || isnan(humid)) {
      Serial.println("Failed to read from DHT sensor!");
    } else {
      // 시리얼 모니터에 센서 값 출력
      Serial.print("Temperature: ");
      Serial.print(temp);
      Serial.print(" °C  |  Humidity: ");
      Serial.print(humid);
      Serial.print(" %  |  Light Level: ");
      Serial.print(lux);
      Serial.println(" lx");
    }

    // 오류 발생 시 LED2 켜고 LED1과 모터 끄기
    if (temp > 27 || humid > 70 || lux < 100) {
      digitalWrite(led2, HIGH);   // 오류 등 켜기
      digitalWrite(led1, LOW);    // LED1 끄기
      analogWrite(motor, 0);      // 모터 끄기
      processStarted = false;     // 공정 중지 상태

      //display monitor
      display.clearDisplay();
      display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
      display.print("Process Error!");
      display.display();
      //delay(1000);
    } else {
      digitalWrite(led2, LOW);    // 오류 등 끄기
      digitalWrite(led1, HIGH);   // 공정 진행 중 LED1 유지
      analogWrite(motor, 100);    // 모터 계속 동작

      //display monitor
      display.clearDisplay();
      display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
      display.print("Processing...");
      display.display();
      //delay(1000);

      // 오류 없이 진행될 때 공정 타이머 시작
      if (!processStarted) {
        processStartMillis = currentMillis;  // 공정 타이머 리셋
        processStarted = true;  // 공정 시작 상태
      }
    }
  }

  // 공정이 시작된 후 15초 경과 시 완료 처리
  if (!processCompleted && processStarted && (currentMillis - processStartMillis >= processTime)) {
    digitalWrite(led3, HIGH);  // 완료 등 켜기
    digitalWrite(led1, LOW);   // LED1 끄기
    analogWrite(motor, 0);     // 모터 끄기
    processCompleted = true;   // 공정 완료 상태 설정
    Serial.println("Process completed. Waiting for potentiometer adjustment...");

    //display monitor
    display.clearDisplay();
    display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
    display.print("Process Done!");
    display.display();
    //delay(1000);
  }

  // 공정 완료 후 TEST 공정으로 넘어감 (가변저항 값 받기 시작)
  if (processCompleted && !testStarted) {
    int potValue = analogRead(tester_reg);
    
    // 가변저항 값 시리얼 모니터로 출력
    Serial.print("Potentiometer Value: ");
    Serial.println(potValue);

    // TEST 공정: 가변저항 값이 임계값을 넘으면 LED4 켜기 (테스트 완료)
    if (potValue > 3000) {
      digitalWrite(led4, HIGH);   // LED4 켜기
      digitalWrite(led3, LOW);    // LED3 끄기
      analogWrite(motor, 0);      // 모터 끄기 (테스트 완료)
      testStarted = true;         // TEST 공정 완료
      Serial.println("Test completed.");

      //display monitor
    display.clearDisplay();
    display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
    display.print("Process Test Done!");
    display.display();
    } else {
      digitalWrite(led4, LOW);    // LED4 끄기
    }
  }
}
================================================================
//라즈베리 파이썬코드
from influxdb import InfluxDBClient

import paho.mqtt.client as mqtt
dbClient = InfluxDBClient(host='localhost', port=8086, username='influx_ship', password='1234', database='db_riatech')


def on_connect( client, userdata, flags, reason_code, properties ):
   print(f"Connect with result code:{reason_code}")
   client.subscribe("MyOffice/Indoor/#")

def on_message( client, userdata, msg ):
   print( msg.topic +" "+str(msg.payload) )
   topic = msg.topic.split('/')
   loc = topic[1]
   subloc = topic[2]
   payload = eval(msg.payload)
   json_body =[]
   data_point = {
        'measurement': 'sensors', #dbTableName
        'tags': {'Location':' ', 'SubLocation':' '},
        'fields': {'Temp':0.0, 'Humi':0.0, 'Lux':0.0}
        }

   data_point['tags']['Location'] = loc
   data_point['tags']['SubLocation'] = subloc
   data_point['fields']['Temp'] = payload['Temp']
   data_point['fields']['Humi'] = payload['Humi']
   data_point['fields']['Lux'] = payload['Lux']

   json_body.append(data_point)
   dbClient.write_points( json_body )

mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
mqttc.username_pw_set(username="mqtt_ship", password="1234")
mqttc.on_connect = on_connect
mqttc.on_message = on_message
mqttc.connect("localhost", 1883, 60)
mqttc.loop_forever( )
