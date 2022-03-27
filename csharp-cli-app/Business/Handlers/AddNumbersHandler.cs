using Business.Interfaces;
using Business.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Business.Handlers;

public class AddNumbersHandler : IRequestHandler<AddNumbersCommand>
{
    private readonly ILogger<AddNumbersHandler> _logger;
    private readonly ICalculator _calculator;
    private readonly IFileService _fileService;

    public AddNumbersHandler(
        ILogger<AddNumbersHandler> logger,
        ICalculator calculator,
        IFileService fileService
    )
    {
        _logger = logger;
        _calculator = calculator;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(AddNumbersCommand request, CancellationToken ct)
    {
        var (x, y, resultPath) = request;
        try
        {
            _logger.LogInformation("Adding values {X} and {Y}", x, y);
            var result = _calculator.Add(x, y);
            _logger.LogInformation("Calculated result: {Result}", result);

            if (resultPath is not null)
            {
                _logger.LogInformation("Saving result to file {Path}", resultPath);
                var content = $"Adding {x} + {y} = {result}\n";
                await _fileService.SaveToFileAsync(resultPath, content, ct);
                _logger.LogInformation("Result saved");
            }

            return Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error happened during handling addition of values {X} and {Y}", x, y);
            throw;
        }
    }
}