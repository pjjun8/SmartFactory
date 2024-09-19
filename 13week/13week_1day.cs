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
