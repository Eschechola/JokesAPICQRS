using System;

namespace Jokes.Domain.Entities
{
    public class Joke
    {
        public Guid Id { get; private set; }
        public string Text { get; private set; }
        public string Response { get; private set; }

        public Joke(){}

        public Joke(string text, string response)
        {
            Id = Guid.NewGuid();
            Text = text;
            Response = response;
        }
    }
}
