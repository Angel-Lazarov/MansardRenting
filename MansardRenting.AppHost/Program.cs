var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MansardRenting_Web>("mansardrenting-web");

builder.Build().Run();
