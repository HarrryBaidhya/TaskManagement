using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace Blood.infrastructure
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;
        public readonly string? _IsApiCall; 
        public readonly string? _ApiUrl; 
        public readonly string? _ApiBearerUrl;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
         
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
