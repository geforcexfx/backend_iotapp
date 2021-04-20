#include <WiFi.h>
#include <PubSubClient.h>
#include <Arduino.h>
#include <analogWrite.h>
#include <Ticker.h>  //Ticker Library
#define SERIAL_SPEED  19200//; ///< Set the baud rate for Serial I/O


TaskHandle_t Task1;
TaskHandle_t Task2;
int dhtPin = 4;
// LED pins
const int led1 = 2;
const int led2 = 4;
int lightVal;   // light reading

const int sensorPin = 2;
const char* ssid = "pna";
const char* password =  "kaffekop";
const char* mqttServer = "euve249939.serverprofi24.net";
//const char* mqttServer = "192.168.10.122";
const int mqttPort = 1883;
const char* mqttUser = "bmeSensor";
const char* mqttPassword = "YourMqttUserPassword";
int failTime=0;
bool flagMeasure=false;
Ticker measure;
WiFiClient espClient;
PubSubClient client(espClient);
float tempVal=0;
float humidVal=0;
float presVal=0;
float gasVal=0;
float casioVal=0;
float noa=0;
float wiki=0;
int devideTime=0;
int timer=900;
int loopTime=0;
const int ledPinG = 25; 
const int ledPinB = 26; 
const int ledPinR = 33; 
int fade_speed=50;
String recv;
bool home_mode = false;
bool emergency = false;
int light_val=0;
Ticker heart_beat;
bool heart_flag;
int obstaclePin = 27;
int hasObstacle = HIGH;
bool up_bool=false;
void setup() {
  Serial.begin(115200); 
   analogWrite(ledPinB, 0);
   analogWrite(ledPinG, 0);
   analogWrite(ledPinR, 100);
    pinMode(obstaclePin, INPUT);
  //create a task that will be executed in the Task1code() function, with priority 1 and executed on core 0
  xTaskCreatePinnedToCore(
                    Task1code,   /* Task function. */
                    "Task1",     /* name of task. */
                    10000,       /* Stack size of task */
                    NULL,        /* parameter of the task */
                    1,           /* priority of the task */
                    &Task1,      /* Task handle to keep track of created task */
                    0);          /* pin task to core 0 */                  
  delay(500); 

  //create a task that will be executed in the Task2code() function, with priority 1 and executed on core 1
  xTaskCreatePinnedToCore(
                    Task2code,   /* Task function. */
                    "Task2",     /* name of task. */
                    10000,       /* Stack size of task */
                    NULL,        /* parameter of the task */
                    1,           /* priority of the task */
                    &Task2,      /* Task handle to keep track of created task */
                    1);          /* pin task to core 1 */
   Serial.begin(115200);


    // Print the header
  //output = "Timestamp [ms], raw temperature [°C], pressure [hPa], raw relative humidity [%], gas [Ohm], IAQ, IAQ accuracy, temperature [°C], relative humidity [%], Static IAQ, CO2 equivalent, breath VOC equivalent";

  
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.println("Connecting to WiFi..");
  }
  Serial.println("Connected to the WiFi network");
 
  client.setServer(mqttServer, mqttPort);
  client.setCallback(callback);
 
  while (!client.connected()) {
    Serial.println("Connecting to MQTT...");
 
    if (client.connect("LED_STRIP1", mqttUser, mqttPassword )) {
 
      Serial.println("connected");  
 
    } else {
 
      Serial.print("failed with state ");
      Serial.print(client.state());
      delay(2000);
 
    }
  }
  
  //client.publish("BME680/", "Hello from ESP8266");
  client.subscribe("LED_STRIP1/");
  analogWrite(ledPinR, 0);
    
    delay(500); 
    heart_beat.attach(150,triggerSend);
}
void triggerSend() {
 heart_flag=true;
}
void emergency_call(){
  if(emergency){
        emergency=false;
        analogWrite(ledPinB, 255);
        analogWrite(ledPinG, 255);
        analogWrite(ledPinR, 255);
        delay(60000);

       emergency=false; 
      }
  
}
//Task1code: Perform LED with different mode
void Task1code( void * pvParameters ){
  Serial.print("Task1 running on core ");
  Serial.println(xPortGetCoreID());

  for(;;){
    //lightVal = analogRead(sensorPin); // read the current light levels
    //Serial.println(recv);
    if(home_mode){
       
        for(int dutyCycle = 1023; dutyCycle > 0; dutyCycle=dutyCycle-4){
        // changing the LED brightness with PWM
         if(home_mode==false){
            analogWrite(ledPinB, 0);
            analogWrite(ledPinG, 0);
            analogWrite(ledPinR, 0);
            delay(500);
            break;
         }
        analogWrite(ledPinR, dutyCycle);
    
        //Serial.println(dutyCycle);
        delay(fade_speed);
        emergency_call();
      }
      for(int dutyCycle = 1023; dutyCycle > 0; dutyCycle=dutyCycle-4){
        // changing the LED brightness with PWM
         if(home_mode==false){
            analogWrite(ledPinB, 0);
            analogWrite(ledPinG, 0);
            analogWrite(ledPinR, 0);
            delay(500);
            break;
         }
        analogWrite(ledPinB, dutyCycle);
    
        //Serial.println(dutyCycle);
        delay(fade_speed);
        emergency_call();
      }
      for(int dutyCycle = 1023; dutyCycle > 0; dutyCycle=dutyCycle-4){
        // changing the LED brightness with PWM
         if(home_mode==false){
            analogWrite(ledPinB, 0);
            analogWrite(ledPinG, 0);
            analogWrite(ledPinR, 0);
            delay(500);
            break;
         }
        analogWrite(ledPinG, dutyCycle);
    
        //Serial.println(dutyCycle);
        delay(fade_speed);
        emergency_call();
      }
      ////////////////////////////////////////////////////////////////////////////////////
    
      for(int dutyCycle = 0; dutyCycle < 1024; dutyCycle=dutyCycle+4){
        // changing the LED brightness with PWM
         if(home_mode==false){
            analogWrite(ledPinB, 0);
            analogWrite(ledPinG, 0);
            analogWrite(ledPinR, 0);
            delay(500);
            break;
         }
        analogWrite(ledPinB, dutyCycle);
        analogWrite(ledPinG, dutyCycle);
        analogWrite(ledPinR, dutyCycle);
        //Serial.println(dutyCycle);
        delay(fade_speed);
        emergency_call();
      }
       
      for (int period = 0; period < 300; period++){
        //int random_w=random(0, 1024);
         if(home_mode==false){
            analogWrite(ledPinB, 0);
            analogWrite(ledPinG, 0);
            analogWrite(ledPinR, 0);
            delay(500);
            break;
         }
        int random_r=random(0, 1024);
        int random_g=random(0, 1024);
        int random_b=random(0, 1024);
        analogWrite(ledPinB, random_r);
        analogWrite(ledPinG, random_g);
        analogWrite(ledPinR, random_b);
        delay(1000);
        emergency_call();
      }
      
        delay(1500);
    } 
    else{
      if(emergency){
        emergency=false;
        Serial.println("emergency start here");
        if(light_val<30){
          analogWrite(ledPinB, 255);
          analogWrite(ledPinG, 255);
          analogWrite(ledPinR, 255);
          delay(90000);
          
          analogWrite(ledPinB, 0);
          analogWrite(ledPinG, 0);
          analogWrite(ledPinR, 0);
        }
        emergency=false;
      }
      //Serial.println("away");
      
      delay(1000);
      
    }
    if(up_bool){
      analogWrite(ledPinB, 255);
      analogWrite(ledPinG, 255);
      analogWrite(ledPinR, 255);
    }
    else{
      analogWrite(ledPinB, 0);
      analogWrite(ledPinG, 0);
      analogWrite(ledPinR, 0);
    }
    }
    
}
void callback(char* topic, byte* payload, unsigned int length) {
 
  Serial.print("Message arrived in topic: ");
  Serial.println(topic);
  String stringOne;
  String tickertime;
  
  Serial.print("Message:");
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
    stringOne += (char)payload[i];
    if (i>1){
      tickertime +=(char)payload[i];
    }
    
  }
  recv=stringOne;
  Serial.println(stringOne.substring(0, 2));
  Serial.println(tickertime);
  Serial.println("-----------------------");
  if (stringOne.substring(0, 2).equals("Le")){
    Serial.print("timer: ");
    timer=tickertime.toInt();
    Serial.println(timer);
    if(timer>2){
      Serial.println("set time");
      home_mode=true;
     Serial.println(home_mode);
      delay(500);
      //measure.attach(timer, BMEStart);
    }
    if(timer<2){
      home_mode=false;
      Serial.println(home_mode);
      
    }
    
    
  }
  if (stringOne.substring(0, 2).equals("Me")){
      Serial.println("emeger");
      emergency=true;
    }
   if (stringOne.substring(0, 2).equals("Li")){
      Serial.print("Light Val: ");
      timer=tickertime.toInt();
      Serial.println(timer);
      light_val=timer;
      //emergency=true;
    }
     if (stringOne.substring(0, 2).equals("Up")){
      Serial.print("timer: ");
      timer=tickertime.toInt();
      Serial.println(timer);
     if(timer>2){
      Serial.println("set time");
      up_bool=true;
     Serial.println(up_bool);
      delay(500);
      //measure.attach(timer, BMEStart);
    }
    if(timer<2){
      up_bool=false;
      Serial.println(up_bool);
      Serial.println("false up");
    }
      //emergency=true;
    }
      
  //delay(2000);
}
//Task2code: Get data from obstacle sensor and MQTT task
void Task2code( void * pvParameters ){
  Serial.print("Task2 running on core ");
  Serial.println(xPortGetCoreID());

  for(;;){
    hasObstacle = digitalRead(obstaclePin);
    if (hasObstacle == LOW)
  {
   
    Serial.println("emeger");
      
      if(!emergency){
      client.publish("RASPCAM1/", "record_onB");
      }
      emergency=true;
  }

      client.loop();
      yield();
  }
}

void loop() {
   if(heart_flag){

     while(!client.publish("MachineData1/", (char*) "s")){

  
      failTime=failTime+1 ;
      Serial.println("fail ");
      Serial.println(failTime);
      delay(1000);
      if(failTime>=6){
        Serial.println("restart due to fail ");
        
        ESP.restart();
      }
    }
    heart_flag=false;
  }
}
