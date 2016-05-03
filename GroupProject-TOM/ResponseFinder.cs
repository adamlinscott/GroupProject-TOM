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
		public string JSON;
		private int threshold;
		private string lastQuestion;
		private int lastResponseID;
		private int emotion;


		/* ResponseFinder Constructor
		 *
		 * takes an integer and sets the threshold which the responses will be compaired against
		 * sets the public string JSON to the content of the .json tile found in the executing directory 
		 */
		public ResponseFinder(int threshold)
		{
			//read in response_data.json
			this.threshold = threshold;
			JSON = System.IO.File.ReadAllText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\response_data.json");
			emotion = 7;
		}


		/* GetResponse method
		 *  
		 * takes string as input to fins a response for
		 * Returns string as response
		 * 
		 * returns NO_VIABLE_RESPONSE if best response does not meet threshold
		 * returns NO_RESPONSE_IN_JSON if no responses are found in the JSON
		 * returns NO_INPUT if input string is equal to "" or null
		 */
		public string GetResponse(string input)
		{
			if( input != "" && input != null)
			{
				input = input.ToLower();
				if(input[input.Length - 1] == '?' || input[input.Length - 1] == '.' || input[input.Length - 1] == '!')
				{
					input = input.Substring(0, input.Length - 2);
				}
				int matchedArrayIndex = 0;
				int highScore = 0;
				var responses = JsonParser.Deserialize(JSON);
				for(int i = 0; i < responses.payload.category0.emotion0.Capacity - 1; i++)
				{
					try
					{
						

						int quesScore = 0;
						if (responses.payload.category0.emotion0[i][0] == input)
						{
							return responses.payload.category0.emotion0[i][2];
						}

						if (AnyKeywordMatch(input, responses.payload.category0.emotion0[i][0], "gmod", "garrys mod", "garry's mod", "rust"))
						{
							quesScore += 500;
						}

						for(int j = 0; j < responses.payload.category0.emotion0[i][0].Length; j++)
						{
							for(int k = 0; k < input.Length; k++)
							{
								int add = (10 * 10) - ((k - j) * (k - j));
								if(input[k] == responses.payload.category0.emotion0[i][0][j])
								{
									if (add > 0)
									{
										quesScore += PhraseMatch(input, responses.payload.category0.emotion0[i][0], k, j, 0) + add;
									}
								}
							}
						}

						if (input.Length > responses.payload.category0.emotion0[i][0].Length)
						{
							quesScore = quesScore / responses.payload.category0.emotion0[i][0].Length;
						}
						else
						{
							quesScore = quesScore / input.Length;
						}

						if(quesScore > highScore)
						{
							matchedArrayIndex = i;
							highScore = quesScore;
						}
					}
					catch
					{

					}
				}

				try
				{
					if(highScore >= threshold)
					{
						string response = null;
						//Console.ForegroundColor = ConsoleColor.DarkGray;
						//Console.WriteLine(responses.payload.category0.emotion0[matchedArrayIndex][0]);
						//Console.ForegroundColor = ConsoleColor.Gray;
						int i = 0;
						while(response == null && i < 20)
						{
							Random rand = new Random();
							try
							{
								int responseID = rand.Next(2,8);
								if (responseID != lastResponseID)
								{
									response = responses.payload.category0.emotion0[matchedArrayIndex][responseID];
									lastResponseID = responseID;
								}
							}
							catch
							{
								response = null;
							}
							i++;
						}
						if(response == null)
						{
							response = responses.payload.category0.emotion0[matchedArrayIndex][2];
						}
						lastQuestion = responses.payload.category0.emotion0[matchedArrayIndex][0];
						//Console.ForegroundColor = ConsoleColor.DarkGray;
						//Console.WriteLine(responses.payload.category0.emotion0[matchedArrayIndex][0]);
						//Console.ForegroundColor = ConsoleColor.Gray;

						return response;
					}
					else
					{
						//Console.ForegroundColor = ConsoleColor.DarkGray;
						//Console.WriteLine("NO_VIABLE_RESPONSE");
						//Console.ForegroundColor = ConsoleColor.Gray;
						return "NO_VIABLE_RESPONSE";
					}
				}
				catch
				{
					//Console.ForegroundColor = ConsoleColor.DarkGray;
					//Console.WriteLine("NO_RESPONSE_IN_JSON");
					//Console.ForegroundColor = ConsoleColor.Gray;
					return "NO_RESPONSE_IN_JSON";
				}
			}
			else
			{
				//Console.ForegroundColor = ConsoleColor.DarkGray;
				//Console.WriteLine("NO_INPUT");
				//Console.ForegroundColor = ConsoleColor.Gray;
				return "NO_INPUT";
			}
		}

		void changeEmotion(string s)
		{
			int i = Convert.ToInt32(s);
			emotion += i;

			if (emotion < 2)
			{
				emotion = 2;
			}
			else if (emotion > 5)
			{
				emotion = 5;
			}
		}

		/* AnyKeywordMatch method
		 * 
		 * takes 2 target strings and multiple keyword strings
		 * returns true only if both target strings contain any of the same keyword
		 */
		private bool AnyKeywordMatch(string mString1, string mString2, params string[] keyWords)
		{
			bool matching = false;
			foreach(string s in keyWords)
			{
				if(mString1.Contains(s) && mString2.Contains(s))
				{
					matching = true;
				}
			}
			return matching;
		}


		/* AllKeywordMatch method
		 * 
		 * takes 2 target strings and multiple keyword strings
		 * returns true only if both target strings contain all keywords
		 */
		private bool AllKeywordMatch(string mString1, string mString2, params string[] keyWords)
		{
			bool matching = true;
			foreach (string s in keyWords)
			{
				if (!mString1.Contains(s) || !mString2.Contains(s))
				{
					matching = false;
				}
			}
			return matching;
		}


		/* PhraseMatch method
		 * 
		 * takes 2 target strings and 2 character index values as ints and a bonus int
		 * returns a score based on how many characters in a row appear in the same order in 
		 * both target strings from a set position.
		 */
		private int PhraseMatch(string mString1, string mString2, int startChar1, int startChar2, int bonus)
		{
			try
			{
				if (mString1[startChar1] == mString2[startChar2])
				{
					return bonus + PhraseMatch(mString1, mString2, startChar1, startChar2 + 1, bonus + 50);
				}
				else
				{
					return bonus;
				}
			}
			catch
			{
				return bonus;
			}
		}
	}
}
