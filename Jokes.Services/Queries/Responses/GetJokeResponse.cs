using System;

namespace Jokes.Services.Queries.Responses
{
    public class GetJokeResponse
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Response { get; set; }
    }
}
