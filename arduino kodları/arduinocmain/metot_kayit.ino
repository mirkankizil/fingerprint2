uint8_t readnumber(void) {
  uint8_t num = 0;
  
  while (num == 0) {
    while (Serial.available()>0);
    num = Serial.parseInt();
  }
  return num;
}

uint8_t getFingerprintEnroll() {

  int p = -1;
  Serial.println("PARMAGINIZI OKUTUN"); 
  while (p != FINGERPRINT_OK) {
    p = finger.getImage();
    switch (p) {
    case FINGERPRINT_OK:
      Serial.println("Parmak izi alindi");
      break;
    case FINGERPRINT_NOFINGER:
    if(sayac==0){
      Serial.println("Parmak tespit edilemedi");
      sayac=1;
    }
      break;
    case FINGERPRINT_PACKETRECIEVEERR:
    {
   //   Serial.println("Baglanti hatasi");
    //      Serial.println("HATA");
        getFingerprintEnroll();
    }
      break;
    case FINGERPRINT_IMAGEFAIL:
    {

   //   Serial.println("Goruntuleme hatasi");
       //     Serial.println("HATA");
       getFingerprintEnroll();
    }
      break;
    default:
    {

       //    Serial.println("Bilinmeyen hata");
            //     Serial.println("HATA");
            getFingerprintEnroll();
    }
      break;
    }
  }
   p = finger.image2Tz(1);
  switch (p) {
    case FINGERPRINT_OK:
      Serial.println("Parmak izi dijitallesti");
      break;
    case FINGERPRINT_IMAGEMESS:
    {

      Serial.println("Parmak izi dijitallesme sorunu");
            Serial.println("HATA");
    }
      return p;
    case FINGERPRINT_PACKETRECIEVEERR:
    {

      Serial.println("Baglanti hatasi");
            Serial.println("HATA");
    }
      return p;
    case FINGERPRINT_FEATUREFAIL:
    {

      Serial.println("Parmak izi ozellikleri bulunamadi");
            Serial.println("HATA");
    }
      return p;
    case FINGERPRINT_INVALIDIMAGE:
    {
      Serial.println("Parmak izi ozellikleri bulunamadi");
    Serial.println("HATA");
    }
      return p;
    default:
    {
      Serial.println("Bilinmeyen hata");
    Serial.println("HATA");
    }
      return p;
  }
  Serial.println("Parmagini kaldir");
  delay(2000);
  Serial.println("Parmagini okut");
  p = 0;
  while (p != FINGERPRINT_NOFINGER) {
    p = finger.getImage();
  }
  p = -1;
  Serial.println("Bu parmak izi hafizada bulunuyor!");
  while (p != FINGERPRINT_OK) {
    p = finger.getImage();
    switch (p) {
    case FINGERPRINT_OK:
      Serial.println("Parmak izi alindi");
      break;
    case FINGERPRINT_NOFINGER:
   if(sayac1==0){
      Serial.println("Parmak tespit edilemedi");
      sayac1=1;
    }
      break;
    case FINGERPRINT_PACKETRECIEVEERR:
    {

      Serial.println("Baglanti hatasi");
            Serial.println("HATA");
    }
      break;
    case FINGERPRINT_IMAGEFAIL:
    {
      Serial.println("Goruntuleme hatasi");
      Serial.println("HATA");
    }
      break;
    default:
    {
      Serial.println("Bilinmeyen hata");
    Serial.println("HATA");
    }
      break;
    }
  }
   p = finger.image2Tz(2);
  switch (p) {
    case FINGERPRINT_OK:
      Serial.println("paramak izi dijitallesti");
      break;
    case FINGERPRINT_IMAGEMESS:
    {

     Serial.println("dijitalleşme hatası");
           Serial.println("HATA");
    }
      return p;
    case FINGERPRINT_PACKETRECIEVEERR:
    {

     Serial.println("Baglanti hatasi");
           Serial.println("HATA");
    }
      return p;
    case FINGERPRINT_FEATUREFAIL:
    {
     Serial.println("Parmak izi ozellikleri bulunamadi");
      Serial.println("HATA");
    }
      return p;
    case FINGERPRINT_INVALIDIMAGE:
    {

    Serial.println("Parmak izi ozellikleri bulunamadi");
          Serial.println("HATA");
    }
      return p;
    default:
    {

     Serial.println("Bilinmeyen hata");
           Serial.println("HATA");
    }
      return p;
  }
  
  
  p = finger.createModel();
  if (p == FINGERPRINT_OK) {
     
    Serial.println("Eslesme bulundu!");
  } else if (p == FINGERPRINT_PACKETRECIEVEERR) {
   Serial.println("Baglanti hatasi");
         Serial.println("HATA");
    return p;
  } else if (p == FINGERPRINT_ENROLLMISMATCH) {

   Serial.println("Eslesme bulunamadi");
         Serial.println("HATA");
    return p;
  } else {

    Serial.println("Bilinmeyen hata");
          Serial.println("HATA");
    return p;
  }   
  
  p = finger.storeModel(id);
  if (p == FINGERPRINT_OK) {
   
    Serial.println("Kaydedildi!");
  } else if (p == FINGERPRINT_PACKETRECIEVEERR) {

    Serial.println("baglantı hatası");
          Serial.println("HATA");
    return p;
  } else if (p == FINGERPRINT_BADLOCATION) {
   Serial.println("Bu konumda depolanamaz");
         Serial.println("HATA");
    return p;
  } else if (p == FINGERPRINT_FLASHERR) {
  Serial.println("Yazma hatasi");
        Serial.println("HATA");
    return p;
  } else {
  Serial.println("Bilinmeyen hata");
        Serial.println("HATA");
    return p;
  }   
}
