// esp32_subscribe_LED.ino
#include <WiFi.h>
#include <PubSubClient.h>
const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_girl";
const char* userPw = "1234";
const char* clientId = userId;
char *topic = "MyOffice/Indoor/Lamp";
const char* serverIPAddress = "192.168.1.18";
char messageBuf[100];

void callback(char* topic, byte* payload, unsigned int length) { 
  Serial.println("Message arrived!\nTopic: " + String(topic));
  Serial.println("Length: "+ String(length, DEC));
  
  strncpy(messageBuf, (char*)payload, length);
  messageBuf[length] = '\0';
  String ledState = String(messageBuf);
  Serial.println("Payload: "+ ledState + "\n\n");
  if( ledState == "off"  ){      digitalWrite(4, LOW);}
  else if (ledState=="on") { digitalWrite(4, HIGH);}
  else { Serial.println("Wrong Message"); }
}

WiFiClient wifiClient; 
PubSubClient client(serverIPAddress, 1883, callback, wifiClient);

void setup() {
  pinMode(4, OUTPUT);
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
-------------------------------------------------------------
namespace SSH_Test01
{
    using System;
    using Renci.SshNet;
    internal class Program
    {
        static void Main(string[] args)
        {
            // 라즈베리 파이의 IP 주소 및 SSH 정보 설정
            string host = "192.168.1.18";
            string username = "admin";  // 기본 사용자명
            string password = "admin";  // 기본 비밀번호 (변경 가능)

            using (var client = new SshClient(host, username, password))
            {
                try
                {
                    client.Connect();  // SSH 연결 시작

                    Console.WriteLine("Connected to Raspberry Pi!");

                    // 라즈베리 파이에서 명령어 실행 (예: 'ls' 명령)

                    var cmd = client.CreateCommand("mosquitto_pub -t MyOffice/Indoor/Lamp -m off -u mqtt_boy -P 1234");
                    var result = cmd.Execute();

                    // 결과 출력
                    Console.WriteLine(result);

                    client.Disconnect();  // 연결 종료
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
