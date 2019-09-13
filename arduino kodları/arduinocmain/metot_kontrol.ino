uint8_t getFingerprintID() {
  uint8_t p = finger.getImage();// Kütüphane içerisinde bulunan getImage fonksiyonu çalıştırılıp , dönen değer değişkene aktarılıyor
  switch (p) {
    case FINGERPRINT_OK:
      Serial.println("Parmak izi alindi");;
      break;
    case FINGERPRINT_NOFINGER:
      Serial.println("Parmak Tespit Edilemedi");
      Serial.println("HATA");
      return p;
    case FINGERPRINT_PACKETRECIEVEERR:
      Serial.println("Baglanti Hatasi");
      Serial.println("HATA");
      return p;
    case FINGERPRINT_IMAGEFAIL:
      Serial.println("Goruntuleme Hatasi");
      Serial.println("HATA");
      return p;
    default:
      Serial.println("Bilinmeyen Hata");
      Serial.println("HATA");
      return p;
  }

  p = finger.image2Tz(); // Kütüphanede bulunan image2Tz fonksiyonu çalıştırılıp , dönen değer değişkene aktarılıyor
  switch (p) {
    case FINGERPRINT_OK:  //Dönüştürme başarılı olursa
      Serial.println("parmak izi aktarildi");
      break;
    case FINGERPRINT_IMAGEMESS: //Alınan resim kalitesiz ise
      Serial.println("dijitallesme hatası");
      Serial.println("HATA");
      return p;
    case FINGERPRINT_PACKETRECIEVEERR: //Portlarda kopukluk veya başka sıkıntı olduğunda
      Serial.println("Baglanti Hatasi");
      Serial.println("HATA");
      return p;
    case FINGERPRINT_FEATUREFAIL:      //Parmak izini tanımlayan özelliklerden bulunmuyorsa
      Serial.println("Parmak izi Ozellikleri Bulunamadi");
      Serial.println("HATA");
      return p;
    case FINGERPRINT_INVALIDIMAGE:
      Serial.println("Parmak izi Ozellikleri Bulunamadi"); //Parmak izini tanımlayan özelliklerden bulunmuyorsa
     Serial.println("HATA");
      return p;
    default:
      Serial.println("Bilinmeyen Hata");
      Serial.println("HATA");
      return p;
  }

  // Dönüştürüldü
  p = finger.fingerFastSearch(); // Hızlı Tarama Özelliği
  if (p == FINGERPRINT_OK) {  // Alınan parmak izi ile kayıtlı parmak izi eşleşirse
    Serial.println("Eslesme Bulundu!");
  } else if (p == FINGERPRINT_PACKETRECIEVEERR) { //Portlarda kopukluk olursa
    Serial.println("Baglanti Hatasi");
    Serial.println("HATA");
    return p;
  } else if (p == FINGERPRINT_NOTFOUND) { //Olası bir eşleşme bulunamazsa
    Serial.println("Eslesme Bulunamadi");
    return p;
  } else {
    Serial.println("Bilinmeyen Hata");  
    Serial.println("HATA");
    return p;
  }   
  
  // 1 eşleşme bulundu
 Serial.println("sonuc"); 
  Serial.println(finger.fingerID); // Parmak izi ID'si
 // Serial.print(" güvenilirlik ");
 // Serial.println(finger.confidence); 
  
}
int getFingerprintIDez() {
  uint8_t p = finger.getImage(); // Kütüphane içerisinde bulunan getImage fonksiyonu çalıştırılıp , dönen değer değişkene aktarılıyor
  if (p != FINGERPRINT_OK)  return -1;

  p = finger.image2Tz();
  if (p != FINGERPRINT_OK)  return -1;

  p = finger.fingerFastSearch();
  if (p != FINGERPRINT_OK)  return -1;
  
  // Eşleşme bulundu
Serial.println("sonuc");
  Serial.println(finger.fingerID); 
  delay(200);
  //Serial.print("güvenilirlik "); 
  //Serial.println(finger.confidence);
  return finger.fingerID; 
}
