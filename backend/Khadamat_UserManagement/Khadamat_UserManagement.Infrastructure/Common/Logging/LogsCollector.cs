using Khadamat_UserManagement.Application.Common.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Infrastructure.Common.Logging
{
    public class LogsCollector : ILogsCollector
    {
        private readonly string _connectionString;
        private readonly HttpContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public List<Log> Logs { get; } = new List<Log>();

        public LogsCollector(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
            _contextAccessor = contextAccessor;
            _context = contextAccessor.HttpContext!;
        }

        public void AddLog(Log log)
        {
            log = new Log(log.Id, log.UserId, log.EventType, log.EventBody, _context.TraceIdentifier, _context.Request.Path, log.EventCriticality, log.Notes, log.Message, log.TimeStamp, log.Level, log.Exception);

            Logs.Add(log);
        }
        public async Task CommitLogs()
        {
            if (Logs == null || Logs.Count == 0)
                return;

            var sql = new StringBuilder();
            var parameters = new List<SqlParameter>();
            int paramIndex = 0;

            // Build the base INSERT statement
            sql.AppendLine(@"INSERT INTO Logs 
                        (UserId, EventType, EventBody, RequestId, 
                         RequestPath, EventCriticality, Notes, Message, 
                         TimeStamp, Level, Exception) 
                        VALUES ");

            // Add a VALUES clause for each log
            for (int i = 0; i < Logs.Count; i++)
            {
                if (i > 0)
                    sql.AppendLine(", ");  // Comma between value groups

                var log = Logs[i];

                // Append the parameter placeholders
                sql.Append($"(@UserId{paramIndex}, @EventType{paramIndex}, " +
                           $"@EventBody{paramIndex}, @RequestId{paramIndex}, @RequestPath{paramIndex}, " +
                           $"@EventCriticality{paramIndex}, @Notes{paramIndex}, @Message{paramIndex}, " +
                           $"@TimeStamp{paramIndex}, @Level{paramIndex}, @Exception{paramIndex})");

                // Add all parameters for this log
                parameters.Add(new SqlParameter($"@UserId{paramIndex}", log.UserId ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@EventType{paramIndex}", log.EventType ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@EventBody{paramIndex}", log.EventBody ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@RequestId{paramIndex}", log.RequestId ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@RequestPath{paramIndex}", log.RequestPath ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@EventCriticality{paramIndex}", log.EventCriticality ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@Notes{paramIndex}", log.Notes ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@Message{paramIndex}", log.Message ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@TimeStamp{paramIndex}", log.TimeStamp ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@Level{paramIndex}", log.Level ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter($"@Exception{paramIndex}", log.Exception ?? (object)DBNull.Value));

                paramIndex++;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                using (var command = new SqlCommand(sql.ToString(), connection, transaction))
                {
                    try
                    {
                        command.Parameters.AddRange([.. parameters]);
                        await command.ExecuteNonQueryAsync();  // Single database round-trip
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
