using System;
using System.Linq;
using System.Text;

namespace AdventOfCode24.Days.Day09
{
    public class Day09 : ISolution
    {
        public string Part1(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var diskMap = lines[0];

            var disk = new List<int>();
            for (int i = 0; i < diskMap.Length; i += 2)
            {
                int fileLength = diskMap[i] - '0';
                int freeSpace = (i + 1 < diskMap.Length) ? diskMap[i + 1] - '0' : 0;
                var fileId = i / 2;

                // Add file blocks
                for (int j = 0; j < fileLength; j++)
                    disk.Add(fileId);

                // Add free space
                for (int j = 0; j < freeSpace; j++)
                    disk.Add(-1);
            }


            // Compact: move from right to left
            int left = 0, right = disk.Count - 1;
            while (left < right)
            {
                // Find next free space from left
                while (left < disk.Count && disk[left] != -1) left++;
                
                // Find next file block from right  
                while (right >= 0 && disk[right] == -1) right--;
                
                // Swap if valid positions found
                if (left < right)
                {
                    disk[left] = disk[right];
                    disk[right] = -1;
                }
            }

            long sum = 0;
            for (int i = 0; i < disk.Count(); i++)
            {
                if (disk[i] != -1)  // Skip free space
                {
                    sum += disk[i] * i;
                }

            }

            return sum.ToString();
        }

        public string Part2(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var diskMap = lines[0];

            var disk = new List<int>();

            var files = new List<(int id, int size, int startPos)>();
            int position = 0;
            for (int i = 0; i < diskMap.Length; i += 2)
            {
                int fileLength = diskMap[i] - '0';
                int freeSpace = (i + 1 < diskMap.Length) ? diskMap[i + 1] - '0' : 0;
                var fileId = i / 2;


                files.Add((fileId, fileLength, position));
                position += fileLength + freeSpace;
                // Add file blocks
                for (int j = 0; j < fileLength; j++)
                    disk.Add(fileId);

                // Add free space
                for (int j = 0; j < freeSpace; j++)
                    disk.Add(-1);
            }

                        
            // Find all free space gaps
            var freeSpaces = new List<(int size, int startPos)>();
            int reverse = 0;
            while (reverse < disk.Count)
            {
                if (disk[reverse] == -1)
                {
                    int startPos = reverse;
                    int size = 0;
                    while (reverse < disk.Count && disk[reverse] == -1)
                    {
                        size++;
                        reverse++;
                    }
                    freeSpaces.Add((size, startPos));
                }
                else
                {
                    reverse++;
                }
            }
            
            // Sort free spaces by position initially
            freeSpaces.Sort((a, b) => a.startPos.CompareTo(b.startPos));
            
            // Process files from the highest ID to lowest
            for (int fileId = files.Count - 1; fileId >= 0; fileId--)
            {
                var file = files[fileId];
                
                // Find leftmost free space that can fit this file and is to the left of the file
                var suitableSpace = freeSpaces.FirstOrDefault(fs => fs.size >= file.size && fs.startPos < file.startPos);

                if (suitableSpace == default) continue;
                // Step 1: Clear old file position in disk
                for (int i = file.startPos; i < file.startPos + file.size; i++)
                {
                    disk[i] = -1;
                }
                    
                // Step 2: Place file in new position
                for (int i = suitableSpace.startPos; i < suitableSpace.startPos + file.size; i++)
                {
                    disk[i] = file.id;
                }
                    
                // Step 3: Update file position in our records
                files[fileId] = (file.id, file.size, suitableSpace.startPos);
                    
                // Step 4: Update free spaces list
                freeSpaces.Remove(suitableSpace);
                    
                // Add remaining free space if any
                if (suitableSpace.size > file.size)
                {
                    var remainingSpace = (suitableSpace.size - file.size, suitableSpace.startPos + file.size);
                    freeSpaces.Add(remainingSpace);
                }
                    
                // Add new free space where the file used to be
                freeSpaces.Add((file.size, file.startPos));
                
                // Keep free spaces sorted by position to ensure leftmost selection
                freeSpaces.Sort((a, b) => a.startPos.CompareTo(b.startPos));
            }

            long sum = 0;
            for (int i = 0; i < disk.Count(); i++)
            {
                if (disk[i] != -1)  // Skip free space
                {
                    sum += disk[i] * i;
                }
            }

            return sum.ToString();
        }
    }
}