#include<WiFi.h>
#include<string.h>
#include <UnoWiFiDevEd.h>

char ssid[] = "DiasKaravidas";      //SSID of your network
char pass[] = "apoel123";
int status = WL_IDLE_STATUS;     // the Wifi radio's status

IPAddress ip;

// the setup function runs once when you press reset or power the board
void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  pinMode(LED_BUILTIN, OUTPUT);
  WiFi.begin(ssid,pass);
  Serial.print("SSID: ");
  Serial.println(ssid);
  Serial.begin(9600);
  ip = WiFi.localIP();
  Serial.println(WiFi.status());
  Serial.println(ip);
  if ( status != WL_CONNECTED) {
    Serial.println("Couldn't get a wifi connection");
    while(true);
  }
  ip = WiFi.localIP();
  Serial.println(ip);

  }
  Serial.println("Hello World");
  ip = WiFi.localIP();
  Serial.println(ip);
  
}

// the loop function runs over and over again forever
void loop() {
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
  delay(1000);                       // wait for a second
  digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
  delay(1000000);                    
}
