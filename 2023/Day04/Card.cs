using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2023.D04;

public class Card
{
    public int Number { get; private set; }
    public int Count { get; private set; }
    public int Score { get; private set; }
    public int MatchingCards { get; private set; }

    private IEnumerable<int> _winningNumbers;
    private IEnumerable<int> _gameNumbers;
    
    public Card(string input)
    {
        var data = input.Split(':');
        if (data.Length !=  2)
        {
            throw new ArgumentException();
        }
        
        Count = 1;
        Number = ExtractCardNumber(data[0]);
        ExtractGameNumbers(data[1]);
        Score = CalculateScore();
        MatchingCards = CalculateMatchingCards();
    }
    
    public void Add() => Count++;
    public void Remove() => Count--;
    
    private int ExtractCardNumber(string input)
    {
        var result = input.Split(' ');
        if (result.Length !=  2)
        {
            return -1;
        }
        return int.Parse(result[1]);
    }

    private void ExtractGameNumbers(string input)
    {
        var sets = input.Split('|');
        if (sets.Length !=  2)
        {
            throw new ArgumentException();
        }
        _winningNumbers = ExtractNumbers(sets[0]);
        _gameNumbers = ExtractNumbers(sets[1]);
    }

    private IEnumerable<int> ExtractNumbers(string input)
    {
        return input.Split(' ')
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Select(int.Parse);
    }
    
    private int CalculateScore()
    {
        var count = _winningNumbers.Intersect(_gameNumbers).Count();
        if (count == 0)
        {
            return 0;
        }
        var result = 1;
        for (var i = 1; i < count; i++)
        {
            result *= 2;
        }
        return result;
    }

    private int CalculateMatchingCards()
    {
        return _winningNumbers.Intersect(_gameNumbers).Count();
    }
}