namespace Infrastructure.Databse
{
    internal class SiGManagerDB
    {
        private readonly string _connectionString;

        public SiGManagerDB(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
