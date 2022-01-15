using System.Text.Json.Serialization;

namespace norris
{
    public class Joke
    {
        public static readonly Joke NoJoke = new();

        [JsonPropertyName("value")]
        public string Text { get; set; } = "This is no joke - there's no joke!";
    }
}

/*
{
"categories":[],
"created_at":"2020-01-05 13:42:25.099703",
"icon_url":"https://assets.chucknorris.host/img/avatar/chuck-norris.png",
"id":"7WMHbZ6rSrOX9JexhyySjg",
"updated_at":"2020-01-05 13:42:25.099703",
"url":"https://api.chucknorris.io/jokes/7WMHbZ6rSrOX9JexhyySjg",
"value":"Chuck Norris can pluck a live chicken with his toes."
}
*/
