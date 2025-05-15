var builder = DistributedApplication.CreateBuilder(args);

var controller = builder
    .AddProject<Projects.DFrameController>("controller")
    .WithHttpEndpoint(name: "web", port: 1111, targetPort: 7312)
    .WithHttpEndpoint(name: "worker-listen", port: 2222, targetPort: 7313);

builder
    .AddProject<Projects.DFrameWorker>("worker")
    .WithEnvironment(context =>
    {
        //context.EnvironmentVariables.Add("CONTROLLER_ADDRESS", controller.GetEndpoint("worker-listen").Url);
        var endpoint = controller.GetEndpoint("worker-listen");
        context.EnvironmentVariables.Add("CONTROLLER_ADDRESS", $"{endpoint.Scheme}://{endpoint.Host}:{endpoint.Port}");
    })
    .WithEnvironment("APPLICATION_BASE_ADDRESS", "http://localhost:1111")
    .WithReplicas(3)
    .WithReference(controller);

builder
    .AddProject<Projects.WebApplication1>("web")
    .WithReference(controller)
    .WithHttpsEndpoint(port: 7023, targetPort: 7023, isProxied: false);

builder.Build().Run();