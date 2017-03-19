const int soundDetectShoot = 5;
const int soundDetectMove = 7;
int soundDetectMoveVal = LOW;
int soundDetectShootVal = LOW;

unsigned long shootTimer;
int shootBarrierLow;
int shootBarrierHigh;
bool shootDetectFront;
bool shootDetectBack;

int soundSourceFront[30];
int countFront;
int countBack;
int soundLength;
int soundSumupFront;
int soundSumupBack;
int storeVal;

void setup() {
  Serial.begin(9600);
  pinMode(soundDetectShoot, INPUT);
  pinMode(soundDetectMove, INPUT);
  shootDetectFront = false;
  shootDetectBack = false;
  countFront = -1;
  countBack = -1;
  soundLength = 0;
  soundSumupFront = 0;
  soundSumupBack = 0;
  soundSourceFront[30] = 0;
  shootBarrierLow = 30;
  shootBarrierHigh = 200;
}

void loop() {

  soundDetectMoveVal = digitalRead(soundDetectMove);
  soundDetectShootVal = digitalRead(soundDetectShoot);
  
  if (soundDetectMoveVal == HIGH) {
    Serial.write(2);
    Serial.flush();
    delay(20);
  }
  else if (soundDetectMoveVal == LOW) {
    Serial.write(1);
    Serial.flush();
    delay(20);
  }

  // Transform signal to value
  soundDetectShootVal = digitalRead(soundDetectShoot);
  if (soundDetectShootVal == HIGH) {
    storeVal = 1;
  }
  else {
    storeVal = 0;
  }

  // Second filter: calculating the value of the last 25 signals
  if (countFront < 29) {
    countFront++;
    soundSumupFront -= soundSourceFront[countFront];
    soundSourceFront[countFront] = storeVal;
    soundSumupFront += soundSourceFront[countFront];
  }
  else {
    countFront = 0;
    soundSumupFront -= soundSourceFront[countFront];
    soundSourceFront[countFront] = storeVal;
    soundSumupFront += soundSourceFront[countFront];
  }

// First filter
  if (soundDetectShootVal == HIGH) {
    if (shootDetectFront == false) {
      shootTimer = millis();
      shootDetectFront = true;
    }
    soundLength++;
  }
  else if (soundDetectShootVal == LOW) {
    if ((shootDetectFront == true) && (millis() - shootTimer > shootBarrierLow) && ((soundSumupFront - soundLength)/(30-soundLength) < 0.3f) && (soundSumupFront - soundLength < 7)) {
      shootDetectFront = false;
      shootDetectBack = true;
      soundLength = 0;
      shootTimer = 0;
    }
    else if (shootDetectFront == true) {
      shootDetectFront = false;
      soundLength = 0;
      shootTimer = 0;
    }
  }


  // Third filter: calculating the signals after the second detection
  if (shootDetectBack == true) {
    if (countBack < 12) {
      countBack++;
      soundSumupBack += storeVal;
    }
    else {
      if (soundSumupBack <= 6) {
        Serial.write(3);
        Serial.flush();
        Serial.println("WHOOSH");
        delay(20);
      }
      shootDetectBack = false;
      soundSumupBack = 0;
      countBack = -1;
    }
  }
  
}
