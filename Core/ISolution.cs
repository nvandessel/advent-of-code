namespace adventofcode.Core;

public interface ISolution
{
    string Year { get; }
    string Day { get; }

    string InputFileName { get; }

    void Solve();
}