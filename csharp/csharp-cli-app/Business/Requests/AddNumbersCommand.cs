using MediatR;

namespace Business.Requests;

public record AddNumbersCommand(int X, int Y, string? ResultPath) : IRequest
{
    public void Deconstruct(out int x, out int y, out string? resultPath)
    {
        x = X;
        y = Y;
        resultPath = ResultPath;
    }
}