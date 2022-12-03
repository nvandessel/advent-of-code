namespace adventofcode.Core;

public interface ISolution
{
    int Year { get; }
    int Day { get; }

    string InputFileName { get; }

    void Solve();
}