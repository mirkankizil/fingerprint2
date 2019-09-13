
uint8_t deleteFingerprint(uint8_t id) {
  uint8_t p = -1;
  
  p = finger.deleteModel(id);

  if (p == FINGERPRINT_OK) {
 //   Serial.println("Silindi!");
  } else if (p == FINGERPRINT_PACKETRECIEVEERR) {
 //   Serial.println("Baglantı hatası");
 //   Serial.println("HATA");
    return p;
  } else if (p == FINGERPRINT_BADLOCATION) {
 //   Serial.println("HATA");
    return p;
  } else if (p == FINGERPRINT_FLASHERR) {
  //  Serial.println("Hafıza yazma hatası");
 //   Serial.println("HATA");
    return p;
  } else {
//    Serial.print("Bilinmeyen hata");
 //   Serial.println("HATA"); 
    return p;
  }   
}


void deleteiz() {
  for(int i=2;i<128;i++)
  {
  finger.deleteModel(i);

  }   
}
