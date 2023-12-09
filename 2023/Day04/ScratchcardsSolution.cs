using System.Collections.Generic;
using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2023.D04
{
    [SolutionAttribute("Scratchcards Solution")]
    public class ScratchcardsSolution : Solution
    {
        public override int Year => 2023;
        public override int Day => 04;

        public override void Solve()
        {
            var input = GetInput();
            var cards = input
                .Split('\n')
                .Select(line => new Card(line))
                .ToList();
            
            var partOne = cards.Sum(card => card.Score);
            OutputAnswer($"Part One: {partOne}");
            
            var partTwo = cards.Aggregate(0, (result, card) => result + ProcessCard(card, cards));
            OutputAnswer($"Part Two: {partTwo}");
        }

        private static int ProcessCard(Card card, IList<Card> cards)
        {
            var result = 1;
            card.Remove();
            if (card.Number == cards.Last().Number || card.MatchingCards == 0)
            {
                return result;
            }

            var nextCards = cards.Skip(cards.IndexOf(card) + 1).Take(card.MatchingCards);
            foreach (var nextCard in nextCards)
            {
                nextCard.Add();
                result += ProcessCard(nextCard, cards);
            }
            return result;
        }
    }
}
