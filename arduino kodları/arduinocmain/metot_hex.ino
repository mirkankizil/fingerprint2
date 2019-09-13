uint8_t uploadFingerpintTemplate(uint16_t id)
{
 uint8_t p = finger.loadModel(id);
  switch (p) {
    case FINGERPRINT_OK:
      Serial.print("Sablon "); Serial.print(id); Serial.println("yuklendi");
      break;
    case FINGERPRINT_PACKETRECIEVEERR:
      Serial.println("Baglanti Hatasi");
      return p;
    default:
      Serial.print("Bilinmeyen Hata "); Serial.println(p);
      return p;
  }

  // OK success!

  p = finger.getModel();
  switch (p) {
    case FINGERPRINT_OK:
      Serial.print("Sablon "); Serial.print(id); Serial.println(" transfer ediliyor");
      break;
   default:
      Serial.print("Bilinmeyen Hata "); Serial.println(p);
      return p;
  }
  
  uint8_t templateBuffer[256];
  memset(templateBuffer, 0xff, 256);  //Sıfırlama arabelleği
  int index=0;
  uint32_t starttime = millis();
  while ((index < 256) && ((millis() - starttime) < 1000))
  {
    if (mySerial.available())
    {
      templateBuffer[index] = mySerial.read();
      index++;
    }
  }
  
  Serial.print(index); Serial.println(" Byte Okundu.");
  
  //Şablonu 16 satırlık herbiri 16 byte olarak yazar.
  Serial.println("HEXKOD");
  for (int count= 0; count < 16; count++)
  {
    for (int i = 0; i < 16; i++)
    {
      Serial.print("0x");
      Serial.print(templateBuffer[count*16+i], HEX);
      Serial.print(", ");
    }
  
  }
   Serial.println();
}
