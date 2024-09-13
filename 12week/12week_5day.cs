//LED하나 깜빡임
#include "Timer.h"
#define USER_LED	12
Timer t;

void setup() {
  pinMode(USER_LED, OUTPUT);
  t.oscillate(USER_LED, 1000, LOW);
}

void loop() {
  t.update();
}
================================
//LED 2개 교차 깜빡임
#include "Timer.h"
#define USER_LED  12
#define BUILTIN_LED 13

Timer t;

void flip1(void) {
  static int ledState = LOW;
  digitalWrite(USER_LED, ledState );
  ledState = !ledState;
}

void flip2(void) {
  static int ledState = LOW;
  digitalWrite(BUILTIN_LED, ledState );
  ledState = !ledState;
}

void setup() {
  pinMode(USER_LED, OUTPUT);
  pinMode(BUILTIN_LED, OUTPUT);
  t.every(1000, flip1);
  t.every(500, flip2);
}

void loop() {
  t.update();
}

