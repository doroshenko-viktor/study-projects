using Business.Interfaces;

namespace Business;

public class Calculator : ICalculator
{
    public int Add(int x, int y)
    {
        return x + y;
    }
}
