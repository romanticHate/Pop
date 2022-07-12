using EvilCorp.Pop.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
    builder.RegisterService(typeof(Program));

var app = builder.Build();
    app.RegisterPipelineComponent(typeof(Program));
    app.Run();
