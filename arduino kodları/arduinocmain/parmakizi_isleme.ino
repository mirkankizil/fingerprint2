void kayit()
{Serial.println("Parmagini okut");
  id = readnumber();
  if (id == 0) {
     return;
  }
  while (!  getFingerprintEnroll() );
  }
   void kontrol()
   {
   delay(3000);
  getFingerprintIDez();
  delay(50); 
getFingerprintID(); 
   }
void sil()
{
  uint8_t id = readnumber();
  if (id == 0) {
     return;
  }
  
  deleteFingerprint(id);
}
