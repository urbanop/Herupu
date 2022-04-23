namespace Herupu.Api.TextToSpeech
{
    public class Authentication
    {
        public static readonly string FetchTokenUri = 
            Environment.GetEnvironmentVariable("smartOneTTSEndpointToken");

        private string subscriptionKey;
        private string token;

        public Authentication()
        {
            this.subscriptionKey = Environment.GetEnvironmentVariable("smartOneTTSKey");
            this.token = FetchTokenAsync(FetchTokenUri, subscriptionKey).Result;
        }

        public string GetAccessToken()
        {
            return this.token;
        }

        private async Task<string> FetchTokenAsync(string fetchUri, string subscriptionKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                UriBuilder uriBuilder = new UriBuilder(fetchUri);

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                Console.WriteLine("Token Uri: {0}", uriBuilder.Uri.AbsoluteUri);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
