using CLI;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSerilogLogging()
    .BuildServiceProvider();


