using System;
using System.IO;
using System.Threading;

namespace CodeLang
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vars = new int[short.MaxValue / 16];
            int varPos = 0;
            int indVar = 0;
            int foreverLoopStart = 0;
            int whileLoopStartPos = 0;
            int whileIfVarPos1 = 0;
            int whileIfVarPos2 = 0;
            int startForLoop = 0;
            int forCycle = 0;
            int maxForCycle = 0;
            int startTillLoop = 0;
            Random rnd = new Random();

            using (var streamReader = new StreamReader(@"..\..\..\Input\NewCode.txt"))
            {
                var streamReaderOutput = streamReader.ReadLine();
                for (int pos = 0; pos < streamReaderOutput.Length && streamReaderOutput[pos] != ' '; pos++)
                {
                    // varPos++
                    if (streamReaderOutput[pos] == '>')
                    {
                        varPos++;
                    }

                    // varPos--
                    else if (streamReaderOutput[pos] == '<')
                    {
                        varPos--;
                    }

                    // varPos = input
                    else if (streamReaderOutput[pos] == 'g')
                    {
                        varPos = int.Parse(streamReaderOutput[pos + 1].ToString()) * 1000 + int.Parse(streamReaderOutput[pos + 2].ToString()) * 100 + 10 * int.Parse(streamReaderOutput[pos + 3].ToString()) + int.Parse(streamReaderOutput[pos + 4].ToString());
                        pos += 4;
                    }

                    // set var
                    else if (streamReaderOutput[pos] == 'v')
                    {
                        vars[varPos] = indVar;
                    }

                    // read var
                    else if (streamReaderOutput[pos] == 't')
                    {
                        indVar = vars[varPos];
                    }

                    // indVar = input
                    else if (streamReaderOutput[pos] == 's')
                    {
                        indVar = 0;
                        int add = 0;
                        if (streamReaderOutput[pos + 1] == '-')
                        {
                            add = 1;
                        }
                        for (int i = 1 + add; i <= 10 + add; i++)
                        {
                            indVar *= 10;
                            indVar += int.Parse(streamReaderOutput[pos + i].ToString());
                        }
                        if (add != 0)
                        {
                            indVar *= -1;
                        }
                        pos += 10 + add;
                    }

                    // jumps to position
                    else if (streamReaderOutput[pos] == 'j')
                    {
                        pos = vars[varPos];
                    }

                    // start forever loop
                    else if (streamReaderOutput[pos] == 'f' && streamReaderOutput[pos + 1] == '/')
                    {
                        pos++;
                        foreverLoopStart = pos;
                    }

                    // end forever loop
                    else if (streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'f')
                    {
                        pos = foreverLoopStart;
                    }

                    // start while loop
                    else if (streamReaderOutput[pos] == 'w' && streamReaderOutput[pos + 1] == '/')
                    {
                        pos++;
                        whileLoopStartPos = pos;
                    }

                    // end while loop
                    else if (streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'w')
                    {
                        pos++;
                        whileIfVarPos1 = vars[int.Parse(streamReaderOutput[pos + 1].ToString()) * 1000 + int.Parse(streamReaderOutput[pos + 2].ToString()) * 100 + 10 * int.Parse(streamReaderOutput[pos + 3].ToString()) + int.Parse(streamReaderOutput[pos + 4].ToString())];
                        pos += 4;
                        whileIfVarPos2 = vars[int.Parse(streamReaderOutput[pos + 1].ToString()) * 1000 + int.Parse(streamReaderOutput[pos + 2].ToString()) * 100 + 10 * int.Parse(streamReaderOutput[pos + 3].ToString()) + int.Parse(streamReaderOutput[pos + 4].ToString())];
                        pos += 5;
                        if (streamReaderOutput[pos] == '=')
                        {
                            if (whileIfVarPos1.Equals(whileIfVarPos2))
                            {
                                pos = whileLoopStartPos;
                            }
                        }
                        else if (streamReaderOutput[pos] == '>')
                        {
                            if (whileIfVarPos1 > whileIfVarPos2)
                            {
                                pos = whileLoopStartPos;
                            }
                        }
                        else if (streamReaderOutput[pos] == '<')
                        {
                            if (whileIfVarPos1 < whileIfVarPos2)
                            {
                                pos = whileLoopStartPos;
                            }
                        }
                        else if (streamReaderOutput[pos] == '!')
                        {
                            if (whileIfVarPos1 != whileIfVarPos2)
                            {
                                pos = whileLoopStartPos;
                            }
                        }
                    }

                    // start for loop
                    else if (streamReaderOutput[pos] == 'l' && streamReaderOutput[pos + 1] == 'T' && streamReaderOutput[pos + 2] == '/')
                    {
                        pos += 2;
                        startForLoop = pos;
                        maxForCycle = indVar;
                        forCycle = 0;
                    }

                    // end for loop
                    else if (streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'l' && streamReaderOutput[pos + 2] == 'T')
                    {
                        forCycle++;
                        if (forCycle >= maxForCycle)
                        {
                            pos += 2;
                        }
                        else
                        {
                            pos = startForLoop;
                        }
                    }

                    // start loop till exit
                    else if (streamReaderOutput[pos] == 'T' && streamReaderOutput[pos + 1] == '/')
                    {
                        pos++;
                        startTillLoop = pos;
                    }

                    // end till loop
                    else if (streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'T')
                    {
                        pos = startTillLoop;
                    }

                    // exit till loop
                    else if (streamReaderOutput[pos] == 'e' && streamReaderOutput[pos + 1] == 'T')
                    {
                        while (!(streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'T'))
                        {
                            pos++;
                        }
                        pos++;
                    }

                    // start if
                    else if (streamReaderOutput[pos] == 'i' && streamReaderOutput[pos + 1] == '/')
                    {
                        if (streamReaderOutput[pos + 2] == '=')
                        {
                            if (indVar == vars[varPos])
                            {
                                pos += 2;
                            }
                            else
                            {
                                while (!(streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'i'))
                                {
                                    pos++;
                                }
                                pos += 1;
                            }
                        }
                        else if (streamReaderOutput[pos + 2] == '!')
                        {
                            if (indVar != vars[varPos])
                            {
                                pos += 2;
                            }
                            else
                            {
                                while (!(streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'i'))
                                {
                                    pos++;
                                }
                                pos += 2;
                            }
                        }
                        else if (streamReaderOutput[pos + 2] == '>')
                        {
                            if (indVar > vars[varPos])
                            {
                                pos += 2;
                            }
                            else
                            {
                                while (!(streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'i'))
                                {
                                    pos++;
                                }
                                pos += 2;
                            }
                        }
                        else if (streamReaderOutput[pos + 2] == '<')
                        {
                            if (indVar < vars[varPos])
                            {
                                pos += 2;
                            }
                            else
                            {
                                while (!(streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'i'))
                                {
                                    pos++;
                                }
                                pos += 2;
                            }
                        }
                    }

                    // end if
                    else if (streamReaderOutput[pos] == '/' && streamReaderOutput[pos + 1] == 'i')
                    {
                        pos += 2;
                    }

                    // stops the program for some time
                    else if (streamReaderOutput[pos] == 'd')
                    {
                        Thread.Sleep(indVar);
                    }

                    // + numbers
                    else if (streamReaderOutput[pos] == '+')
                    {
                        indVar += vars[varPos];
                    }

                    // - numbers
                    else if (streamReaderOutput[pos] == '-')
                    {
                        indVar -= vars[varPos];
                    }

                    // * numbers
                    else if (streamReaderOutput[pos] == '*')
                    {
                        indVar *= vars[varPos];
                    }

                    // / numbers
                    else if (streamReaderOutput[pos] == ':')
                    {
                        indVar /= vars[varPos];
                    }

                    // ^ numbers
                    else if (streamReaderOutput[pos] == '^')
                    {
                        indVar ^= vars[varPos];
                    }


                    // || numbers
                    else if (streamReaderOutput[pos] == '|')
                    {
                        indVar = (int)MathF.Abs(indVar);
                    }

                    // gives the part of the number that can't be divided
                    else if (streamReaderOutput[pos] == '%')
                    {
                        indVar = indVar % vars[varPos];
                    }

                    // sqare root
                    else if (streamReaderOutput[pos] == 'c' && streamReaderOutput[pos + 1] == 'R')
                    {
                        indVar = (int)Math.Sqrt(indVar);
                        pos++;
                    }

                    else if (streamReaderOutput[pos] == 'r')
                    {
                        indVar = rnd.Next(0, indVar);
                    }

                    // enter
                    else if (streamReaderOutput[pos] == 'e')
                    {
                        Console.WriteLine();
                    }

                    // writes number
                    else if (streamReaderOutput[pos] == 'n' && streamReaderOutput[pos + 1] == 'W')
                    {
                        Console.Write(vars[varPos]);
                        pos++;
                    }

                    // writes characters
                    else if (streamReaderOutput[pos] == 'c' && streamReaderOutput[pos + 1] == 'W')
                    {
                        if (vars[varPos] <= 9 && vars[varPos] >= 0)
                        {
                            Console.Write(vars[varPos]);
                        }
                        else if (vars[varPos] == 10)
                        {
                            Console.Write("a");
                        }
                        else if (vars[varPos] == 11)
                        {
                            Console.Write("b");
                        }
                        else if (vars[varPos] == 12)
                        {
                            Console.Write("c");
                        }
                        else if (vars[varPos] == 13)
                        {
                            Console.Write("d");
                        }
                        else if (vars[varPos] == 14)
                        {
                            Console.Write("e");
                        }
                        else if (vars[varPos] == 15)
                        {
                            Console.Write("f");
                        }
                        else if (vars[varPos] == 16)
                        {
                            Console.Write("g");
                        }
                        else if (vars[varPos] == 17)
                        {
                            Console.Write("h");
                        }
                        else if (vars[varPos] == 18)
                        {
                            Console.Write("i");
                        }
                        else if (vars[varPos] == 19)
                        {
                            Console.Write("j");
                        }
                        else if (vars[varPos] == 20)
                        {
                            Console.Write("k");
                        }
                        else if (vars[varPos] == 21)
                        {
                            Console.Write("l");
                        }
                        else if (vars[varPos] == 22)
                        {
                            Console.Write("m");
                        }
                        else if (vars[varPos] == 23)
                        {
                            Console.Write("n");
                        }
                        else if (vars[varPos] == 24)
                        {
                            Console.Write("o");
                        }
                        else if (vars[varPos] == 25)
                        {
                            Console.Write("p");
                        }
                        else if (vars[varPos] == 26)
                        {
                            Console.Write("q");
                        }
                        else if (vars[varPos] == 27)
                        {
                            Console.Write("r");
                        }
                        else if (vars[varPos] == 28)
                        {
                            Console.Write("s");
                        }
                        else if (vars[varPos] == 29)
                        {
                            Console.Write("t");
                        }
                        else if (vars[varPos] == 30)
                        {
                            Console.Write("u");
                        }
                        else if (vars[varPos] == 31)
                        {
                            Console.Write("v");
                        }
                        else if (vars[varPos] == 32)
                        {
                            Console.Write("w");
                        }
                        else if (vars[varPos] == 33)
                        {
                            Console.Write("x");
                        }
                        else if (vars[varPos] == 34)
                        {
                            Console.Write("y");
                        }
                        else if (vars[varPos] == 35)
                        {
                            Console.Write("z");
                        }
                        else if (vars[varPos] == 36)
                        {
                            Console.Write("A");
                        }
                        else if (vars[varPos] == 37)
                        {
                            Console.Write("B");
                        }
                        else if (vars[varPos] == 38)
                        {
                            Console.Write("C");
                        }
                        else if (vars[varPos] == 39)
                        {
                            Console.Write("D");
                        }
                        else if (vars[varPos] == 40)
                        {
                            Console.Write("E");
                        }
                        else if (vars[varPos] == 41)
                        {
                            Console.Write("F");
                        }
                        else if (vars[varPos] == 42)
                        {
                            Console.Write("G");
                        }
                        else if (vars[varPos] == 43)
                        {
                            Console.Write("H");
                        }
                        else if (vars[varPos] == 44)
                        {
                            Console.Write("I");
                        }
                        else if (vars[varPos] == 45)
                        {
                            Console.Write("J");
                        }
                        else if (vars[varPos] == 46)
                        {
                            Console.Write("K");
                        }
                        else if (vars[varPos] == 47)
                        {
                            Console.Write("L");
                        }
                        else if (vars[varPos] == 48)
                        {
                            Console.Write("M");
                        }
                        else if (vars[varPos] == 49)
                        {
                            Console.Write("N");
                        }
                        else if (vars[varPos] == 50)
                        {
                            Console.Write("O");
                        }
                        else if (vars[varPos] == 51)
                        {
                            Console.Write("P");
                        }
                        else if (vars[varPos] == 52)
                        {
                            Console.Write("Q");
                        }
                        else if (vars[varPos] == 53)
                        {
                            Console.Write("R");
                        }
                        else if (vars[varPos] == 54)
                        {
                            Console.Write("S");
                        }
                        else if (vars[varPos] == 55)
                        {
                            Console.Write("T");
                        }
                        else if (vars[varPos] == 56)
                        {
                            Console.Write("U");
                        }
                        else if (vars[varPos] == 57)
                        {
                            Console.Write("V");
                        }
                        else if (vars[varPos] == 58)
                        {
                            Console.Write("W");
                        }
                        else if (vars[varPos] == 59)
                        {
                            Console.Write("X");
                        }
                        else if (vars[varPos] == 60)
                        {
                            Console.Write("Y");
                        }
                        else if (vars[varPos] == 61)
                        {
                            Console.Write("Z");
                        }
                        else if (vars[varPos] == 62)
                        {
                            Console.Write("!");
                        }
                        else if (vars[varPos] == 63)
                        {
                            Console.Write("@");
                        }
                        else if (vars[varPos] == 64)
                        {
                            Console.Write("#");
                        }
                        else if (vars[varPos] == 65)
                        {
                            Console.Write("$");
                        }
                        else if (vars[varPos] == 66)
                        {
                            Console.Write("%");
                        }
                        else if (vars[varPos] == 67)
                        {
                            Console.Write("^");
                        }
                        else if (vars[varPos] == 68)
                        {
                            Console.Write("&");
                        }
                        else if (vars[varPos] == 69)
                        {
                            Console.Write("*");
                        }
                        else if (vars[varPos] == 70)
                        {
                            Console.Write("(");
                        }
                        else if (vars[varPos] == 71)
                        {
                            Console.Write(")");
                        }
                        else if (vars[varPos] == 72)
                        {
                            Console.Write("-");
                        }
                        else if (vars[varPos] == 73)
                        {
                            Console.Write("_");
                        }
                        else if (vars[varPos] == 74)
                        {
                            Console.Write("=");
                        }
                        else if (vars[varPos] == 75)
                        {
                            Console.Write("+");
                        }
                        else if (vars[varPos] == 76)
                        {
                            Console.Write(";");
                        }
                        else if (vars[varPos] == 77)
                        {
                            Console.Write(":");
                        }
                        else if (vars[varPos] == 78)
                        {
                            Console.Write("'");
                        }
                        else if (vars[varPos] == 79)
                        {
                            Console.Write(",");
                        }
                        else if (vars[varPos] == 80)
                        {
                            Console.Write("<");
                        }
                        else if (vars[varPos] == 81)
                        {
                            Console.Write(".");
                        }
                        else if (vars[varPos] == 82)
                        {
                            Console.Write(">");
                        }
                        else if (vars[varPos] == 83)
                        {
                            Console.Write("/");
                        }
                        else if (vars[varPos] == 84)
                        {
                            Console.Write("?");
                        }
                        else if (vars[varPos] == 85)
                        {
                            Console.Write("[");
                        }
                        else if (vars[varPos] == 86)
                        {
                            Console.Write("{");
                        }
                        else if (vars[varPos] == 87)
                        {
                            Console.Write("]");
                        }
                        else if (vars[varPos] == 88)
                        {
                            Console.Write("}");
                        }
                        else if (vars[varPos] == 89)
                        {
                            Console.Write("\"");
                        }
                        else if (vars[varPos] == 90)
                        {
                            Console.Write("\\");
                        }
                        else if (vars[varPos] == 91)
                        {
                            Console.Write("|");
                        }
                        else if (vars[varPos] == 92)
                        {
                            Console.Write("`");
                        }
                        else if (vars[varPos] == 93)
                        {
                            Console.Write("~");
                        }
                        else if (vars[varPos] == 94)
                        {
                            Console.Write(" ");
                        }
                        pos++;
                    }

                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}
