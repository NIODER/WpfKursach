namespace ClinicDatabaseModel
{
    public class ConnectionCredentials
    {
        public string User { get; set; } = "postgres";
        public string Password { get; set; }
        public string Host { get; set; } = "localhost";
        public ushort Port { get; set; } = 5432;
        public string Database { get; set; } = "postgres";

        public ConnectionCredentials(string user, string password, string host, ushort port, string database)
        {
            User = user;
            Password = password;
            Host = host;
            Port = port;
            Database = database;
        }

        public ConnectionCredentials(string connectionString)
        {
            var splitted = connectionString.Split(';')
                .Select(item => new KeyValuePair<string, string>(item.Split('=')[0], item.Split('=')[1]));
            User = splitted.FirstOrDefault(item => item.Key == "Username").Value ?? "postgres";
            Password = splitted.First(item => item.Key == "Password").Value ?? throw new ArgumentNullException("Password");
            Host = splitted.FirstOrDefault(item => item.Key == "Host").Value ?? "localhost";
            Port = ushort.Parse(splitted.FirstOrDefault(item => item.Key == "Port").Value ?? "5432");
            Database = splitted.FirstOrDefault(item => item.Key == "Database").Value ?? User;
        }

        public override string ToString()
        {
            return $"Host={Host};Port={Port};Database={Database};Username={User};Password={Password}";
        }
    }
}
