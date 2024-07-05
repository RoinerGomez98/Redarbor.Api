using Dapper;
using Microsoft.Data.SqlClient;
using Redarbor.Api.Application.Entities;
using System.Data;

namespace Redarbor.Api.Application.Repo
{
    public class Persistence
    {
        private readonly IDbConnection _connection;
        public Persistence(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public async Task<IEnumerable<Redarbors>> GetRedabors()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Redarbors>("SP_GET_REDARBOR", null, commandType: CommandType.StoredProcedure);
        }

        public async Task<Redarbors> GetRedaborById(int Id)
        {
            DynamicParameters parameters = new();
            parameters.Add("Id", Id);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return  conn.QueryAsync<Redarbors>("SP_GET_REDARBOR", parameters, commandType: CommandType.StoredProcedure).Result.FirstOrDefault()!;
        }

        public async Task<int> SaveOrUpdateRedarbors(Redarbors redarbors)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            var save = 0;
            using (var transaction = conn.BeginTransaction())
            {
                save = await conn.ExecuteAsync("SP_INSERT_UPDATE_REDARBORS", redarbors, transaction, commandType: CommandType.StoredProcedure);
                if (save > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            conn.Close();
            return save;
        }

        public async Task<int> DeleteRedarbor(int Id)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            var save = 0;
            using (var transaction = conn.BeginTransaction())
            {
                DynamicParameters parameters = new();
                parameters.Add(nameof(Id), Id);

                save = await conn.ExecuteAsync("SP_DELETE_REDARBORS", parameters, transaction, commandType: CommandType.StoredProcedure);
                if (save > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            conn.Close();
            return save;
        }

        public async Task<Redarbors> GetUser(string Email,string Password)
        {
            DynamicParameters parameters = new();
            parameters.Add("Email", Email);
            parameters.Add("Password", Password);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return conn.QueryAsync<Redarbors>("SP_GET_REDARBOR", parameters, commandType: CommandType.StoredProcedure).Result.FirstOrDefault()!;
        }

    }
}
