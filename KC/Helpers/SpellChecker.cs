using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace KC.Helpers
{
    public class SpellChecker
    {
        public string ApiKey { get; set; }

        public SpellChecker(IConfiguration config)
        {
            ApiKey = config.GetConnectionString("BingSpellchecker");
        }

        public async Task<SpellCheckResult> CheckSpelling(string text)
        {
            //string url = "https://api.bing.microsoft.com/v7.0/spellcheck?text=bread";

            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, Constants.Spelling.Endpoint + text.Replace(" ",","));
            requestMessage.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

            var httpResponse = await client.SendAsync(requestMessage);
            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            SpellingAPIResponse response = JsonSerializer.Deserialize<SpellingAPIResponse>(stringResponse);
            if (response.flaggedTokens.Count != 0)
                return new SpellCheckResult()
                {
                    IsCorrectable = true,
                    CorrectedVersions = new List<string>() { GetCorrectedVersionsFromResponse(response) }
                };
            return new SpellCheckResult() { IsCorrectable = false };
        }

        public static string GetCorrectedVersionsFromResponse(SpellingAPIResponse response)
        {
            List<string> correctedWords = new List<string>();
            
            foreach (var word in response.flaggedTokens)
            {
                correctedWords.Add(word.suggestions[0].suggestion);
            }

            string correctedVersion = string.Join(" ", correctedWords.ToArray());

            return correctedVersion;
        }
    }

    public class SpellCheckResult
    {
        public bool IsCorrectable { get; set; }
        public List<string> CorrectedVersions { get; set; }
    }

    public class Suggestion
    {
        public string suggestion { get; set; }
        public double score { get; set; }
    }

    public class FlaggedToken
    {
        public int offset { get; set; }
        public string token { get; set; }
        public string type { get; set; }
        public IList<Suggestion> suggestions { get; set; }
    }

    public class SpellingAPIResponse
    {
        public string _type { get; set; }
        public IList<FlaggedToken> flaggedTokens { get; set; }
    }
}
