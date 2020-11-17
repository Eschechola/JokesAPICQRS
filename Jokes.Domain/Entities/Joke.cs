using System;

namespace Jokes.Domain.Entities
{
    public class Joke
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Response { get; set; }

        public Joke(){}

        public Joke(string text, string response)
        {
            Id = Guid.NewGuid().ToString();
            Text = text;
            Response = response;
        }
    }
}
