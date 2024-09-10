void setup( ) {
  Serial.begin(9600);
  pinMode(13, OUTPUT);
}

void loop( ) {
  char c;
  if (Serial.available() > 0) {
    c = Serial.read();
    if (c == 'a') {  // 작은따옴표로 수정
      digitalWrite(13, HIGH);  // 오타 수정
    } else {
      digitalWrite(13, LOW);
    }
  }
}
-------------------------
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

}

void loop() {
  // put your main code here, to run repeatedly:
  int adcValue = analogRead(A0);
  Serial.println(adcValue);
  delay(100);

}
