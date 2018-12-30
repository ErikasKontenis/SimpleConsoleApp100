using SimpleConsoleApp.Extensions;
using System;
using System.Linq;
using System.Text;

namespace SimpleConsoleApp.Services
{
    public class CharacterChunkService : ICharacterChunkService
    {
        public string Chunk(string[] lines, int chunkSize)
        {
            if (lines == null || lines.Length == 0)
            {
                throw new ArgumentException("lines parameter cannot be null or empty");
            }

            if (chunkSize <= 0)
            {
                throw new ArgumentException("chunkSize parameter must be greater than 0");
            }

            var stringBuilder = new StringBuilder();

            for (var l = 0; l < lines.Length; l++)
            {
                // if line does not need chunking then no further logic required
                if (lines[l].Length <= chunkSize)
                {
                    if (l + 1 == lines.Length)
                    {
                        stringBuilder.Append(lines[l]);
                    }
                    else
                    {
                        stringBuilder.AppendLine(lines[l]);
                    }

                    continue;
                }

                var words = lines[l].Split(' ');
                for (var w = 0; w < words.Length; w++)
                {
                    for (var i = 0; i < words[w].Length; i++)
                    {
                        stringBuilder.Append(words[w][i]);
                        if ((i + 1) % chunkSize == 0)
                        {
                            // No need to make new line for last line of last word
                            if (lines.Length == l + 1 && words[w].Length == i + 1)
                            {
                                continue;
                            }

                            stringBuilder.AppendLine();
                        }
                    }

                    // If last character is joining character then no chunk join logic required
                    if (stringBuilder[stringBuilder.Length - 1] == '\n' || stringBuilder[stringBuilder.Length - 1] == ' ')
                    {
                        continue;
                    }

                    var lastWordLengthOfCurrentLine = (words.ElementAtOrDefault(w + 1)?.Length);
                    var nextLineLength = lines.ElementAtOrDefault(l + 1)?.Length;
                    // Check if there is space for another line to be extended in the same line or new line should be inserted
                    if (stringBuilder.ToString(stringBuilder.LastIndexOf('\n') + 1).Length + (lastWordLengthOfCurrentLine ?? nextLineLength) + 1 > chunkSize)
                    {
                        stringBuilder.AppendLine();
                    }
                    else
                    {
                        // Do not add space for last word
                        if (!lastWordLengthOfCurrentLine.HasValue && !nextLineLength.HasValue)
                        {
                            continue;
                        }

                        stringBuilder.Append(' ');
                    }
                }
            }
            
            return stringBuilder.ToString();
        }
    }
}
