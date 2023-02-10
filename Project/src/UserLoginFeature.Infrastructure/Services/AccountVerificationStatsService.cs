using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using UserLoginFeature.Application.Abstractions.Services;

namespace UserLoginFeature.Infrastructure.Services
{
    public class AccountVerificationStatsService : IAccountVerificationStatsService
    {
        private readonly SqlConnection _connection;

        public AccountVerificationStatsService(IConfiguration configuration)
        {
            _connection = new(configuration.GetConnectionString("DigitusDb") ?? throw new ArgumentNullException("Connection string is not configured"));
        }

        public async Task<int> GetAverageConfirmationSecondsAsync(DateTime date)
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new("spGetAverageConfirmationSeconds", _connection);
            cmd.Parameters.AddWithValue("@targetDate", date);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dt = await cmd.ExecuteReaderAsync();
            if (dt.HasRows)
            {
                dt.Read();
                string? readRow = dt["AvarageConfirmationSeconds"]?.ToString();
                if (readRow is not null && readRow != string.Empty)
                {
                    int result;
                    if (int.TryParse(readRow, out result))
                    {
                        await _connection.CloseAsync();
                        return result;
                    }
                    else
                    {
                        await _connection.CloseAsync();
                        throw new ArgumentNullException("AvarageConfirmationSeconds");
                    }
                }
            }
            await _connection.CloseAsync();
            return 0;
        }

        public async Task<int> GetEmailUnconfirmedUsersCountAsync()
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new("spGetEmailUnconfirmedUsersCount", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dt = await cmd.ExecuteReaderAsync();
            if (dt.HasRows)
            {
                dt.Read();
                string? readRow = dt["EmailUnconfirmedUsersCount"]?.ToString();
                if (readRow is not null && readRow != string.Empty)
                {
                    int result;
                    if (int.TryParse(readRow, out result))
                    {
                        await _connection.CloseAsync();
                        return result;
                    }
                    else
                    {
                        await _connection.CloseAsync();
                        throw new ArgumentNullException("EmailUnconfirmedUsersCount");
                    }
                }
            }
            await _connection.CloseAsync();
            return 0;
        }

        public async Task<int> GetSuccessfullyRegisteredUsersCountAsync()
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new("spGetSuccessfullyRegisteredUsersCount", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dt = await cmd.ExecuteReaderAsync();
            if (dt.HasRows)
            {
                dt.Read();
                string? readRow = dt["SuccessfullyRegisteredUsersCount"]?.ToString();
                if (readRow is not null && readRow != string.Empty)
                {
                    int result;
                    if (int.TryParse(readRow, out result))
                    {
                        await _connection.CloseAsync();
                        return result;
                    }
                    else
                    {
                        await _connection.CloseAsync();
                        throw new ArgumentNullException("SuccessfullyRegisteredUsersCount");
                    }
                }
            }
            await _connection.CloseAsync();
            return 0;
        }
    }
}
