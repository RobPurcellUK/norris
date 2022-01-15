using System.Net.Http;
using System.Text.Json;

namespace norris
{
    internal interface IJokeRetriever
    {
        Joke GetJoke();
    }

    internal class JokeRetriever : IJokeRetriever
    {
        const string apiUrl = "https://api.chucknorris.io/jokes/random";
        readonly HttpClient httpClient;

        public JokeRetriever()
        {
            httpClient = new HttpClient();
        }

        public Joke GetJoke()
        {
            // TODO: Handle HTTP errors here
            // TODO: await async up the chain of calls
            var apiResponse = httpClient.GetStringAsync(apiUrl).Result;
            var joke = JsonSerializer.Deserialize<Joke>(apiResponse);
            return joke ?? Joke.NoJoke;
        }
    }
}
