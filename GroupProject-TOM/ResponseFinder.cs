using System;
using System.Collections.Generic;
using System.IO;
using Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_TOM
{
	class ResponseFinder
	{
		public ResponseFinder()
		{
			//read in response_data.json
			LoadJson();

			Console.ReadKey();
		}

		public void GetResponse()
		{

		}

		public void LoadJson()
		{
			string json = System.IO.File.ReadAllText(@"D:\Documents\GitHub\GroupProject-TOM\GroupProject-TOM\response_data.json");

			var responses = JsonParser.Deserialize(json);

			Console.WriteLine(responses.response);
		}
	}
}
