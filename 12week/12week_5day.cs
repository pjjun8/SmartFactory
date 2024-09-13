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
==================================
#include <ZumoShield.h>
#include <ZumoMotors.h>
#define NUM_SENSORS  6 
#define TIMEOUT  1200 	// 센서 감조 조절 
#define EMITTER_PIN  2 
QTRSensorsRC qtrrc( (unsigned char[]) {4, A3, 11, A0, A2, 5}, NUM_SENSORS, TIMEOUT, EMITTER_PIN);

ZumoMotors motors;
unsigned int sensorValues[NUM_SENSORS];
void setup() {
 Serial.begin(9600);
 delay(1000);
}
void loop() {
 qtrrc.read(sensorValues);
 int CenterRight = digitalRead(A0);
 int MiddleRight = digitalRead(A2);
 int FarRight = digitalRead(5);
 int CenterLeft = digitalRead(11);
 int MiddleLeft = digitalRead(A3);
 int FarLeft = digitalRead(4);

 if(CenterLeft == 1 && CenterRight == 1){
  motors.setSpeeds(200, 200);
 }
 else if(MiddleLeft == 1){
  motors.setSpeeds(75, 200);
 }
 else if(MiddleRight == 1){
  motors.setSpeeds(200, 75);
 }
 else if (FarLeft == 1){
  motors.setSpeeds(75, 300);
 }
 else if (FarRight == 1){
  motors.setSpeeds(300, 75);
 }
 Serial.print(FarLeft); Serial.print('\t'); 
 Serial.print(MiddleLeft); Serial.print('\t');
 Serial.print(CenterLeft); Serial.print('\t'); 
 Serial.print(CenterRight); Serial.print('\t'); 
 Serial.print(MiddleRight); Serial.print('\t'); 
 Serial.println(FarRight); 
}

