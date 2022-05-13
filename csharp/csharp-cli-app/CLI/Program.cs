using Business;
using Business.Handlers;
using Business.Interfaces;
using Business.Requests;
using CLI;
using CLI.Services;
using MediatR;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceProvider = new ServiceCollection()
    .AddSerilogLogging()
    .AddMediatR(typeof(AddNumbersHandler).Assembly)
    .AddSingleton<ICalculator, Calculator>()
    .AddSingleton<IFileService, FileService>()
    .BuildServiceProvider();

var app = new CommandLineApplication();
app.Name = "CSharp CLI Application";
app.Description = "Simple C# console application";
app.HelpOption("-h|--help");

var mediator = serviceProvider.GetRequiredService<IMediator>();
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

app.Command("add", (opt) =>
{
    var x = opt.Argument(
        name: "<x>",
        description: "first number"
    );
    var y = opt.Argument(
        name: "<y>",
        description: "second number"
    );
    var resultPath = opt.Option(
        template: "-rp|--result-path",
        description: "file path to save result",
        CommandOptionType.SingleValue
    );
    opt.HelpOption("-h|--help");

    opt.OnExecute(async () =>
    {
        try
        {
            if (!int.TryParse(x.Value, out var xInt))
            {
                throw new ArgumentException($"First argument {x.Value} is not a number");
            }
            if (!int.TryParse(y.Value, out var yInt))
            {
                throw new ArgumentException($"Second argument {y.Value} is not a number");
            }

            var command = new AddNumbersCommand
            (
                X: xInt,
                Y: yInt,
                ResultPath: resultPath.Values.Count > 0 ? resultPath.Values[0] : null
            );
            await mediator.Send(command);
            return 0;
        }
        catch (ArgumentException e)
        {
            logger.LogError("Error happened: {Message}", e.Message);
            return 1;
        }
        catch (Exception)
        {
            return 1;
        }
    });
});

app.Execute(args);