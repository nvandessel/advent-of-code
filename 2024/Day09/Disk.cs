using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2024.D09
{
    public class Disk
    {
        public List<long> Blocks;

        public Disk(string map)
        {
            Blocks = new List<long>();
            var blockId = 0;
            for (int i = 0; i < map.Length; i++)
            {
                var value = char.GetNumericValue(map[i]);
                if (i % 2 == 0)
                {
                    AddBlocks(blockId, value);
                    blockId++;
                }
                else
                {
                    AddBlocks(-1, value);
                }
            }

            void AddBlocks(long blockValue, double blockCount)
            {
                for (int j = 0; j < blockCount; j++)
                {
                    Blocks.Add(blockValue);
                }
            }
        }

        public void PrintBlock()
        {
            var output = "";
            foreach (var item in Blocks)
            {
                if (item == -1)
                {
                    output += ".";
                }
                else
                {
                    output += item;
                }
            }
            Console.WriteLine(output);
        }

        // Finds all free space chunks
        public List<(long size, int startIndex)> GetFreeSpaces()
        {
            var freeSpaces = new List<(long, int)>();
            var currentChunkSize = 0;
            var chunkStartIndex = -1;
            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i] == -1)
                {
                    currentChunkSize++;
                    if (chunkStartIndex == -1)
                    {
                        chunkStartIndex = i;
                    }

                    continue;
                }

                if (currentChunkSize > 0)
                {
                    freeSpaces.Add((currentChunkSize, chunkStartIndex));
                }

                currentChunkSize = 0;
                chunkStartIndex = -1;
            }

            return freeSpaces;
        }

        // Swap the position of the values at the sourceIndex and the targetIndex
        public void SwapValues(int sourceIndex, int targetIndex, int length = 1)
        {
            for (int i = 0; i < length; i++)
            {
                var temp = Blocks[targetIndex + i];
                Blocks[targetIndex + i] = Blocks[sourceIndex + i];
                Blocks[sourceIndex + i] = temp;
            }
        }

        public bool CanFitInRange(int startIndex, int length)
        {
            if (startIndex + length > Blocks.Count) return false;
            return Blocks.Skip(startIndex).Take(length).All(b => b == -1);
        }

        public long CalculateChecksum()
        {
            long result = 0;
            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i] == -1L)
                {
                    continue;
                }
                result += (long)i * Blocks[i];
            }

            return result;
        }
    }
}
