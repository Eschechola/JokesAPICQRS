using System;
using Dapper;
using System.Linq;
using Jokes.Infra.Context;
using Jokes.Domain.Entities;
using System.Threading.Tasks;
using Jokes.Infra.Interfaces;
using System.Collections.Generic;

namespace Jokes.Infra.Repositories
{
    public class JokeRepository : IJokesRepository
    {
        private readonly IContext _jokeContext;

        public JokeRepository(IContext jokeContext)
        {
            _jokeContext = jokeContext;
        }

        public async Task<Joke> Create(Joke joke)
        {
            using (var connection = _jokeContext.GetConnection())
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", joke.Id);
                parameters.Add("@Text", joke.Text);
                parameters.Add("@Response", joke.Response);

                var query = @"
                                INSERT INTO [dbo].[Jokes]
                                VALUES
                                (
                                    @Id,
                                    @Text,
                                    @Response
                                );
                            ";

                await connection.QueryAsync(query, parameters);
            }

            return await Get(joke.Id);
        }

        public async Task<Joke> Update(Joke joke)
        {
            using (var connection = _jokeContext.GetConnection())
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", joke.Id);
                parameters.Add("@Text", joke.Text);
                parameters.Add("@Response", joke.Response);

                var query = @"
                                UPDATE [dbo].[Jokes]
                                
                                SET
                                Text = @Text,
                                Response = @Response

                                WHERE 
                                Id = @Id;
                            ";

                await connection.QueryAsync(query, parameters);
            }

            return await Get(joke.Id);
        }

        public async Task<Joke> Get(string id)
        {
            using (var connection = _jokeContext.GetConnection())
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var query = @"
                                SELECT * FROM [dbo].[Jokes]
                                WHERE Id = @Id;
                            ";

                var joke = await connection.QueryAsync<Joke>(query, parameters);

                return joke.ToList()
                           .FirstOrDefault();
            }
        }

        public async Task<IList<Joke>> Get()
        {
            using (var connection = _jokeContext.GetConnection())
            {
                connection.Open();

                var query = @"
                                SELECT * FROM [dbo].[Jokes]
                            ";

                var jokes = await connection.QueryAsync<Joke>(query);

                return jokes.ToList();
            }
        }

        public async Task Remove(string id)
        {
            using (var connection = _jokeContext.GetConnection())
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var query = @"
                                DELETE FROM [dbo].[Jokes]
                                WHERE Id = @Id;
                            ";

                await connection.QueryAsync(query, parameters);
            }
        }
    }
}
