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
				int matchedArrayIndex = 0;
				int highScore = 0;
				var responses = JsonParser.Deserialize(JSON);
				for(int i = 0; i < responses.payload.category0.emotion0.Capacity - 1; i++)
				{
					try
					{
						int quesScore = 0;
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
						Random rand = new Random();
						string response = null;
						while(response == null)
						{
							try
							{
								response = responses.payload.category0.emotion0[matchedArrayIndex][rand.Next(1,8)];
							}
							catch
							{
								response = null;
							}
						}
						//Console.ForegroundColor = ConsoleColor.DarkGray;
						//Console.WriteLine(responses.payload.category0.emotion0[matchedArrayIndex][0]);
						//Console.ForegroundColor = ConsoleColor.Gray;

						return response;
					}
					else
					{
						return "NO_VIABLE_RESPONSE";
					}
				}
				catch
				{
					return "NO_RESPONSE_IN_JSON";
				}
			}
			else
			{
				return "NO_INPUT";
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
