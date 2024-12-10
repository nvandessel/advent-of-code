using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2024.D09
{
    public static class Fragmenter
    {
        public static void SortBySequence(this Disk disk)
        {
            var sortedIndexes = new HashSet<int>();
            for (int i = disk.Blocks.Count-1; i >= 0; i--)
            {
                if (disk.Blocks[i] == -1)
                {
                    continue;
                }

                var firstFreeSpace = disk.Blocks.IndexOf(-1);
                if (sortedIndexes.Contains(firstFreeSpace))
                {
                    break;
                }

                disk.Blocks[firstFreeSpace] = disk.Blocks[i];
                disk.Blocks[i] = -1;
                sortedIndexes.Add(firstFreeSpace);
                sortedIndexes.Add(i);
            }
        }

        public static void SortByFiles(this Disk disk)
        {
            var usedIndexes = new HashSet<int>();
            for (int i = disk.Blocks.Count - 1; i >= 0;)
            {
                if (disk.Blocks[i] == -1)
                {
                    i--;
                    continue;
                }

                var size = GetFileSize(disk.Blocks[i], i);
                var freeSpaceIndex = disk.Blocks.IndexOf(-1);

                while (freeSpaceIndex != -1)
                {
                    var requiredIndexes = Enumerable.Range(freeSpaceIndex, size);
                    if (!requiredIndexes.Any(usedIndexes.Contains) && disk.CanFitInRange(freeSpaceIndex, size))
                    {
                        var sourceIndex = i + 1 - size;
                        disk.SwapValues(sourceIndex, freeSpaceIndex, size);
                        UpdateUsedIndexes(sourceIndex, size);
                        UpdateUsedIndexes(freeSpaceIndex, size);
                        break;
                    }
                    var freeSpaceSize = GetFreeSpaceSize(freeSpaceIndex);
                    freeSpaceIndex = disk.Blocks.IndexOf(-1, freeSpaceIndex + freeSpaceSize); // Find next free space.
                    if (freeSpaceIndex > i)
                    {
                        break;
                    }
                }

                i -= size;
            }

            void UpdateUsedIndexes(int startIndex, int length)
            {
                for (int i = 0; i < length; i++)
                {
                    usedIndexes.Add(startIndex + i);
                }
            }

            /// Walk backwards down blocks, to determine length of value.
            int GetFileSize(long value, int startIndex)
            {
                var len = 0;
                for (int i = startIndex; i >= 0; i--)
                {
                    if (disk.Blocks[i] != value)
                    {
                        break;
                    }
                    len++;
                }
                return len;
            }

            // Walk up the blocks to determine size of freeSpace
            int GetFreeSpaceSize(int startIndex)
            {
                var len = 0;
                for (int i = startIndex; i < disk.Blocks.Count; i++)
                {
                    if (disk.Blocks[i] != -1)
                    {
                        break;
                    }
                    len++;
                }
                return len;
            }

        }
    }
}
