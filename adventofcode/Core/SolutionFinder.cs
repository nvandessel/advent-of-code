using System;
using System.Reflection;
using System.Collections.Generic;

namespace adventofcode.Core;

public class SolutionFinder
{
    public static List<Type> FindSolutions()
    {
        return GetTypesWithMyAttribute(Assembly.GetExecutingAssembly());
    }
    
    private static List<Type> GetTypesWithMyAttribute(Assembly assembly)
    {
        List<Type> types = new List<Type>();

        foreach(Type type in assembly.GetTypes())
        {
            if (type.GetCustomAttributes(typeof(SolutionAttribute), true).Length > 0)
                types.Add(type);
        }

        return types;
    }
}