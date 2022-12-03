using System;

namespace adventofcode.Core;

public class SolutionAttribute : Attribute
{
    private readonly string _problemName;
    
    // This is a positional argument
    public SolutionAttribute(string problemName)
    {
        _problemName = problemName;
    }
    
    public string ProblemName
    {
        get { return _problemName; }
    }
}