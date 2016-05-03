using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroupProject_TOM
{
	class Program
	{		
		static void Main()
		{
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();  
            Welcome(); 
            Chat();
            ChatEnd();
		}
        
        static void Welcome() //Runs once. Generates random welcome.
        {
            bool Welcome = true;
            if (Welcome == true)
            {
                Console.ForegroundColor = ConsoleColor.Black;                
                Console.WriteLine("-You have been connected to: Tom-"); 
                Thread.Sleep(4000);
                Random Roll = new Random();
                int Greeting = Roll.Next(1, 11);
                switch (Greeting)
                {
                    case 1: string a = "Hi, My name is Tom. How can I help you today?"; DelayOutput(a); AiText();
                        Console.WriteLine("Tom: {0}",a); break;
                    case 2: string b = "Hello. I'm Tom. What seems t o be the problem?"; DelayOutput(b); AiText();
                        Console.WriteLine("Tom: {0}",b); break;
                    case 3: string c = "I'm Tom. What can I help you with?"; DelayOutput(c); AiText();
                        Console.WriteLine("Tom: {0}",c); break;
                    case 4: string d = "Hi there, this is Tom! What's up?"; DelayOutput(d); AiText();
                        Console.WriteLine("Tom: {0}",d); break;
                    case 5: string e = "Greetings, traveler. How can I assist?"; DelayOutput(e); AiText();
                        Console.WriteLine("Tom: {0}",e); break;
                    case 6: string f = "What's Up? My name is Tom, what can I help you with today?"; DelayOutput(f); AiText();
                        Console.WriteLine("Tom: {0}",f); break;
                    case 7: string g = "How can I help today sir? My name is Tom, "; DelayOutput(g); AiText();
                        Console.WriteLine("Tom: {0}",g); break;
                    case 8: string h = "I'm Tom, how can I help?"; DelayOutput(h); AiText();
                        Console.WriteLine("Tom: {0}",h); break;
                    case 9: string i = "You're chatting with Tom. How can I help?"; DelayOutput(i); AiText();
                        Console.WriteLine("Tom: {0}",i); break;
                    case 10: string j = "Tom here! Whats up?"; DelayOutput(j); AiText();
                        Console.WriteLine("Tom: {0}",j); break;
                }
                Welcome = false;
            }
        }


        static void ClearTyping() //Clears "Tom is typing" in DelayOutput()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

		static int noResponseCounter = 0;

        static void Chat() //Main chat function
        {
            List<string> UserInput = new List<string>();
            ResponseFinder responses = new ResponseFinder(200); //threshold
            string[] gbykeywords = new string[] {"goodbye", "bye", "cya", "farewell", "fuck off", "goodnight", "take care", "have a good one", "Peace", "Adios", "Ciao", "cya later", "got to go", "no thanks", "no thank you", "see you later"};
            bool MainLoop = true;
            if (MainLoop == true)
            {
                string Current = "";
                UserText(); 
                Current = Console.ReadLine();
                ClearTyping();
                Console.WriteLine("You: {0}", Current);
                if (Current == "")
                {
                    Chat();
                }
                else
                {
                    for (int i = 0; i < gbykeywords.Length; i++ ) //change this to only check the last 15 characters of the Current string.
                    {
                        if (Current.Contains(gbykeywords[i]))
                        {
                            return;
                        }
                    }                                                            
                    AiText();
                   
                    UserInput.Add(Current);                                        
                    string delay = responses.ToString();
                    DelayOutput(delay);            
					string thisResponse = responses.GetResponse(Current);
					string[] viableResponse = { "Sorry, I don't quite understand. Can you please re-word that?", "Sorry, I still don't understand.", "Sorry, I'm not sure how to answer that. Please email contact@facepunchstudios.com for further help. Is there anything else?"};
					string[] noResponse = { "Sorry, I'm not sure i can help with that. Is there anything else I can help with?", "Sorry, I can't answer that.  Can i help you with anything else?", "Sorry, I can't answer that. Please email contact@facepunchstudios.com for further help. Is there anything else?" };
					if (thisResponse == "NO_VIABLE_RESPONSE")
					{
						thisResponse = viableResponse[noResponseCounter];
						noResponseCounter++;
						if (noResponseCounter >= 3)
						{
							noResponseCounter = 0;
						}
					}
					else if(thisResponse == "NO_RESPONSE_IN_JSON")
					{
						thisResponse = noResponse[noResponseCounter];
						noResponseCounter++;
						if (noResponseCounter >= 3)
						{
							noResponseCounter = 0;
						}
					}
					else if (thisResponse == "NO_INPUT")
					{

					}
					else
					{
						noResponseCounter = 0;
					}

					Console.WriteLine("Tom: {0}", thisResponse);
					Chat();
                    //return;
                }
            }
        }

        static void ChatEnd() //End function. To select chat end via emotion level
        {
            Random Roll = new Random();
            int Farewell = Roll.Next(1, 11);            
            switch (Farewell)
            {
                case 1: string a = "Cya later, Alligator."; DelayOutput(a); AiText();
                    Console.WriteLine("Tom: {0}",a); break;
                case 2: string b = "In a while, Crocodile"; DelayOutput(b); AiText();
                    Console.WriteLine("Tom: {0}",b); break;
                case 3: string c = "No problem, catch you later!"; DelayOutput(c); AiText();
                    Console.WriteLine("Tom: {0}",c); break;
                case 4: string d = "Happy to have been of assistance."; DelayOutput(d); AiText();
                    Console.WriteLine("Tom: {0}",d); break;
                case 5: string e = "If there's anything else, dont hesitate to come speak to me again! Farewell."; DelayOutput(e); AiText();
                    Console.WriteLine("Tom: {0}",e); break;
                case 6: string f = "Happy gaming! I hope I helped!"; DelayOutput(f); AiText();
                    Console.WriteLine("Tom: {0}",f); break;
                case 7: string g = "I hope I was able to help you with everything today. Good bye!"; DelayOutput(g); AiText();
                    Console.WriteLine("Tom: {0}",g); break;
                case 8: string h = "Wait.. please don't leave me..."; DelayOutput(h); AiText();
                    Console.WriteLine("Tom: {0}",h); break;
                case 9: string i = "Bye bye!"; DelayOutput(i); AiText();
                    Console.WriteLine("Tom: {0}",i); break;
                case 10: string j = "Cya later. All the best."; DelayOutput(j); AiText();
                    Console.WriteLine("Tom: {0}",j); break;
            }

            Console.ForegroundColor = ConsoleColor.Black; 
            Console.WriteLine("-Please press Enter to close chat-");
            Console.ReadLine();
        }

        static void AiText() //Change AI text to Green.
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.White;
        }

        static void UserText() //Change userinput text to Red.
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
        }



        static void DelayOutput(string Output) //Delays outputs based on the number of chars in string
        {                                      //Also adds "Tom is typing..." and removes it using ClearTyping()
            AiText();
            int num = Output.Length;
            num = num * 95;
            Console.WriteLine("Tom is typing...");
            Thread.Sleep(num);
            ClearTyping();
        }
	}
}
