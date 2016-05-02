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
            Welcome(); 
            Chat();
            ChatEnd();


			//Console.ReadKey();
		}
        
        static void Welcome() //Runs once. Generates random welcome.
        {
            bool Welcome = true;
            if (Welcome == true)
            {
                Console.WriteLine("-You have been connected to: Tom-"); 
                Thread.Sleep(4000);
                Random Roll = new Random();
                int Greeting = Roll.Next(1, 11);
                switch (Greeting)
                {
                    case 1: string a = "Hi, My name is Tom. How can I help you today?"; DelayOutput(a);
                        Console.WriteLine(a); break;
                    case 2: string b = "Hello. I'm Tom. What seems t o be the problem?"; DelayOutput(b);
                        Console.WriteLine(b); break;
                    case 3: string c = "I'm Tom. What can I help you with?"; DelayOutput(c);
                        Console.WriteLine(c); break;
                    case 4: string d = "Hi there, this is Tom! What's up?"; DelayOutput(d);
                        Console.WriteLine(d); break;
                    case 5: string e = "Greetings, traveler. How can I assist?"; DelayOutput(e);
                        Console.WriteLine(e); break;
                    case 6: string f = "What's Up? My name is Tom, what can I help you with today?"; DelayOutput(f);
                        Console.WriteLine(f); break;
                    case 7: string g = "How can I help today sir? My name is Tom, "; DelayOutput(g);
                        Console.WriteLine(g); break;
                    case 8: string h = "I'm Tom, how can I help?"; DelayOutput(h);
                        Console.WriteLine(h); break;
                    case 9: string i = "You're chatting with Tom. How can I help?"; DelayOutput(i);
                        Console.WriteLine(i); break;
                    case 10: string j = "Tom here! Whats up?"; DelayOutput(j);
                        Console.WriteLine(j); break;
                }
                Welcome = false;
            }
        }

        static void DelayOutput(string Output) //Delays outputs based on the number of chars in string
        {                                      //Also adds "Tom is typing..." and removes it using ClearTyping()
            int num = Output.Length;
            num = num * 95;
            Console.WriteLine("Tom is typing...");
            Thread.Sleep(num);
            ClearTyping();
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
            bool MainLoop = true;
            if (MainLoop == true)
            {
                string Current = "";
                Current = Console.ReadLine();
                if (Current == "")
                {
                    Chat();
                }
                else
                {
                    FormatInput(Current); 
                    UserInput.Add(Current);                                        
                    string delay = responses.ToString();
                    DelayOutput(delay);
					string thisResponse = responses.GetResponse(Current);
					string[] viableResponse = { "Sorry, I don't quite understand. Can you please re-word that?", "Sorry, I still don't understand.", "Sorry, I'm not sure how to answer that. Please email contact@facepunchstudios.com for further help. Is there anything else?"};
					string[] noResponse = { "Sorry, I'm not sure i can help with that. Is there anything else I can help with?", "Sorry, I can't answer that.  Can i help you with anything else?", "Sorry, I can't answer that. Please email contact@facepunchstudios.com for further help. Is there anything else?" };
					switch (thisResponse)
					{
						case "NO_VIABLE_RESPONSE":
							thisResponse = viableResponse[noResponseCounter];
							noResponseCounter++;
							if (noResponseCounter >= 3)
							{
								noResponseCounter = 0;
							}
							break;
						case "NO_RESPONSE_IN_JSON":
							thisResponse = noResponse[noResponseCounter];
							noResponseCounter++;
							if (noResponseCounter >= 3)
							{
								noResponseCounter = 0;
							}
							break;
						case "NO_INPUT":
							break;
						default:
							break;
					}
					Console.WriteLine(thisResponse);
                    Chat();
                    //return;
                }
            }
        }

        static void ChatEnd() //End function. To select chat end via emotion level
        {
            //Console.WriteLine("Was that everything I can help with today?"); //TODO//Random Ending//
            //For now I'm just going to loop chat.
            //TODO add way to escape loop to exit chat.
            Chat();
        }

        static void FormatInput(string Input) //Formats user input
        {
            string UserString = Input;
            string[] elements = new string[28] { "!", "£", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+", 
                                               "|", "<", ">", "?", "/", "[", "]", "{", "}", ";", ":", "@", "'", "#", "~"};

            for (int i = 0; i < elements.Length; i++)
            {
                Input = Input.Replace(elements[i], "");
            }

            return;
        }
	}
}
