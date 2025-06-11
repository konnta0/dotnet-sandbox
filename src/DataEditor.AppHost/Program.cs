var builder = DistributedApplication.CreateBuilder(args);

const string password = "P@$$w0rd";
var passwordResource = builder.AddParameter(name: "rdb-password", value: password, secret: true);
builder.AddMySql(name: "data-editor", password: passwordResource, port: 3306)
    .WithDataBindMount("mysql-data")
    .WithLifetime(ContainerLifetime.Session)
    .AddDatabase("data-editor");

builder.AddProject<Projects.DataEditor_UI>("data-editor-ui")
    .WithEnvironment("MYSQL_HOST", "localhost")
    .WithEnvironment("MYSQL_PORT", "3306")
    .WithEnvironment("MYSQL_USER", "root")
    .WithEnvironment("MYSQL_PASSWORD", password);
    
builder.Build().Run();