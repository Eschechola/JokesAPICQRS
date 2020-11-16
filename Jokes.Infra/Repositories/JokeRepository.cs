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
        private readonly IContext _readContext;
        private readonly IContext _writeContext;

        public JokeRepository(ReadContext readContext, WriteContext writeContext)
        {
            _readContext = readContext;
            _writeContext = writeContext;
        }

        public async Task<Joke> Create(Joke joke)
        {
            using (var connection = _writeContext.GetConnection())
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
            using (var connection = _writeContext.GetConnection())
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

        public async Task<Joke> Get(Guid id)
        {
            using (var connection = _readContext.GetConnection())
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
            using (var connection = _readContext.GetConnection())
            {
                connection.Open();

                var query = @"
                                SELECT * FROM [dbo].[Jokes]
                            ";

                var jokes = await connection.QueryAsync<IList<Joke>>(query);

                return jokes.FirstOrDefault();
            }
        }

        public async Task Remove(Guid id)
        {
            using (var connection = _readContext.GetConnection())
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var query = @"
                                DELETE * FROM [dbo].[Jokes]
                                WHERE Id = @Id;
                            ";

                await connection.QueryAsync(query, parameters);
            }
        }
    }
}
