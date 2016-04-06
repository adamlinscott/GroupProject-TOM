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
			ResponseFinder responses = new ResponseFinder(15);

			Console.WriteLine(responses.GetResponse(Console.ReadLine()));

			Console.ReadKey();
		}
	}
}
