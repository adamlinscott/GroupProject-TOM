using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_TOM
{
	class Program
	{
		

		static void Main(string[] args)
		{           
            ResponseFinder responses = new ResponseFinder();
           
            Welcome(); //Run Welcome Function
            Chat(); //Main Chat Function
            ChatEnd(); //Ends the chat


			Console.ReadKey();
		}

        static void Welcome() //Will only be ran once. 
        {
            bool Welcome = true;
            if (Welcome == true)
            {
                Console.WriteLine("Hi, My name is Tom, How can I assist you today?"); //TODO/Random Welcome//
                Welcome = false;
            }
        }

        static void Chat()
        {
            List<string> UserInput = new List<string>();
            bool MainLoop = true; 
            if (MainLoop == true)
            {
                string Current = "";
                Current = Console.ReadLine();
                FormatInput(Current); //Format the string (No spaces, punctuation)
                UserInput.Add(Current);
            }
        }

        static void ChatEnd()
        {
            Console.WriteLine("Was that everything I can help with today?"); //TODO//Random Ending//
            string ChatEnd = Console.ReadLine();
            bool ChatOver = false;
            //TODO//Take ChatEnd and use ResponseFinder to find response before looping or closing.
            FormatInput(ChatEnd);
            if (ChatOver == true)
            {
                //TODO// Place random Goodbye message
                //TODO// Terminate Application
            }
        }

        static void FormatInput(string Input)
        {
            string UserString = Input;
            Input = Input.Replace(" ", "");
            Input = Input.Replace("!", "");
            Input = Input.Replace("£", "");
            Input = Input.Replace("$", "");
            Input = Input.Replace("%", "");
            Input = Input.Replace("^", "");
            Input = Input.Replace("&", "");
            Input = Input.Replace("*", "");
            Input = Input.Replace("(", "");
            Input = Input.Replace(")", "");
            Input = Input.Replace("-", "");
            Input = Input.Replace("_", "");
            Input = Input.Replace("=", "");
            Input = Input.Replace("+", "");
            Input = Input.Replace("|", "");
            Input = Input.Replace("<", "");
            Input = Input.Replace(">", "");
            Input = Input.Replace("?", "");
            Input = Input.Replace("/", "");
            Input = Input.Replace("[", "");
            Input = Input.Replace("]", "");
            Input = Input.Replace("{", "");
            Input = Input.Replace("}", "");
            Input = Input.Replace(";", "");
            Input = Input.Replace(":", "");
            Input = Input.Replace("@", "");
            Input = Input.Replace("'", "");
            Input = Input.Replace("#", "");
            Input = Input.Replace("~", "");
            return;
        }

	}
}
