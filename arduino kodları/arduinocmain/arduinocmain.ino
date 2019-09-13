#include <Adafruit_Fingerprint.h> //parmak izi sensörü kütüphanesi 
#include <SoftwareSerial.h> // yeni seri haberleşme kütüphanesi
SoftwareSerial mySerial(2, 3);  //yeni seri haberleşme pinimiz 2(tx) ve 3(rx) 
Adafruit_Fingerprint finger = Adafruit_Fingerprint(&mySerial); // parmak izi sensörü yeni seri haberleşme üzerinden haberleşecegini bildirdik
uint8_t id;
char gelenveri;
int getFingerprintIDez();
int sayac=0;
int sayac1=0;
void setup() {
 Serial.begin(9600);
  while (!Serial);  
  delay(100);
  Serial.println("\n\nSensor hazir");

  finger.begin(57600); // yeni oluşturdugumuz seri haberleşme  baund aktif ettik
  
  if (finger.verifyPassword()) {
    Serial.println("sensor bulundu :) ");
  } else {
    Serial.println("sensör bulunamadi:(");
    while (1) { delay(1); }
  }
}

void loop() {
  sayac=0;
  sayac1=0;
  gelenveri='0';

 if (Serial.available() > 0) {
   gelenveri = Serial.read();

   if(gelenveri=='B')
{
  kayit();
  uploadFingerpintTemplate(id);
}
   if(gelenveri=='K')
{
  kontrol();

}
   if(gelenveri=='S')
{
  sil();
 
}
   if(gelenveri=='T')
{
  deleteiz();
  
}
  }


}
