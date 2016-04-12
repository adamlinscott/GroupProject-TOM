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

		public ResponseFinder(int threshold)
		{
			//read in response_data.json
			this.threshold = threshold;
			JSON = System.IO.File.ReadAllText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\response_data.json");
		}

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
								response = responses.payload.category0.emotion0[matchedArrayIndex][rand.Next(2,9)];
							}
							catch
							{
								response = null;
							}
						}
						Console.ForegroundColor = ConsoleColor.DarkGray;
						Console.WriteLine(responses.payload.category0.emotion0[matchedArrayIndex][0]);
						Console.ForegroundColor = ConsoleColor.Gray;

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
