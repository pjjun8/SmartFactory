//조도센서 OLED로 출력
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
BH1750 lightMeter;

void setup() {
  Serial.begin(9600);
  if (!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
    Serial.println(F("SSD1306 allocation failed"));
    for (;;) {
      // 무한 루프: 에러 발생 시 여기에서 멈춤
    }
  }

  Wire.begin();
  lightMeter.begin();
  display.setTextColor(WHITE);
}

void loop() {
  float lux = lightMeter.readLightLevel();
  display.clearDisplay();
  display.setCursor(0, 0); // 텍스트를 표시할 위치 설정
  display.print(lux);
  display.print(" [lx]");
  display.display();
  delay(1000);
}

=================================================
//조도, 온습도 센서 추가해서 OLED에 출력
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <DHT.h>
#include <BH1750.h>

#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64

#define OLED_RESET -1
#define SCREEN_ADDRESS 0x3C

#define DHTPIN 4     
#define DHTTYPE DHT11   

Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);
BH1750 lightMeter;

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  if (!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
    Serial.println(F("SSD1306 allocation failed"));
    for (;;) {
      // 무한 루프: 에러 발생 시 여기에서 멈춤
    }
  }

  Serial.println(F("DHTxx and BH1750 test!"));
  dht.begin();

  Wire.begin();
  lightMeter.begin();
  display.setTextColor(WHITE);
  display.clearDisplay();  // OLED 화면 초기화
}

void loop() {
  // BH1750 조도 센서로부터 값 읽기
  float lux = lightMeter.readLightLevel();

  // DHT11 온도, 습도 값 읽기
  float h = dht.readHumidity();
  float t = dht.readTemperature(); // 섭씨 온도

  // DHT 센서 오류 처리
  if (isnan(h) || isnan(t)) {       
    Serial.println(F("Failed to read from DHT sensor!"));
    display.clearDisplay();
    display.setCursor(0, 0);
    display.println(F("Failed to read from DHT sensor!"));
    display.display();
    delay(2000); // 오류 시 2초 대기
    return;
  }

  // OLED에 값 출력
  display.clearDisplay();
  display.setCursor(0, 0);

  // 조도 센서 출력
  display.print(F("Light: "));
  display.print(lux);
  display.println(F(" lx"));

  // 온도 출력
  display.print(F("Temp: "));
  display.print(t);
  display.println(F(" C"));

  // 습도 출력
  display.print(F("Humidity: "));
  display.print(h);
  display.println(F(" %"));

  display.display(); // OLED에 표시된 내용을 화면에 갱신

  delay(2000); // 2초 간격으로 업데이트
}

