//Declaração das variáveis. 
int rpm = 0;
int velocidade = 0; 
int gasolina = 30;
char teste;

void setup() {
  Serial.begin(9600); //Configura a taxa de transferência em bits por segundo (baud rate) para transmissão serial.
                      // Vale resaltar que essa taxa necessita ser a mesma do Visual Studio. 
}

void loop()
{
  if(Serial.available())// Checa a comunicação serial.
  {
    teste = Serial.read();// Lê o que foi enviado do Visual Studio para a Serial, em seguida testa os seus possíveis casos.

    if(teste == 'x')
    {
      velocidade = analogRead(A0);// Lê o valor de um pino analógico(utilizando um potenciômetro) e o enviamos para velocidade.
      Serial.println(velocidade/10);// Envia o valor da velocidade para a serial.
    }
    if(teste == 'y')
    {
      if( gasolina == 0)
        gasolina = 30;
      else
       gasolina = gasolina - 1;
      Serial.println(gasolina);// Envia o valor da gasolina para a serial.
    }
    if(teste == 'z')
    {
      if(rpm == 600)
        rpm = 100;
      else
        rpm = rpm +100;
      Serial.println(rpm);// Envia o valor do rpm para a serial
    }
    Serial.flush();//Espera a transmissão de dados seriais enviados terminar.
  }
}
