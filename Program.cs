using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Application1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el mensaje a enviar");
            Console.WriteLine("Mensajes con alta prioridad deben iniciar con 'HP:'");
            string MessageToBeSend = Console.ReadLine();
            
            
            //Creacion de la cola
            MessageQueue MyQueue;
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                MyQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                MyQueue = MessageQueue.Create(@".\Private$\MyQueue");
            }

            //Creación del mensaje
            Message MyMessage = new System.Messaging.Message();


            //Dar Formato al mensaje
            MyMessage.Formatter = new BinaryMessageFormatter();
            MyMessage.Body = MessageToBeSend;
            MyMessage.Label = "App1Message";

            if (MessageToBeSend.Contains("HP"))
            {
                MyMessage.Priority = MessagePriority.Highest;
            }
            else
            {
                MyMessage.Priority = MessagePriority.Normal;
            }
            MyQueue.Send(MyMessage);
            Console.WriteLine("Gracias por enviar el mensaje");
            Console.ReadKey();

        }
    }
}
