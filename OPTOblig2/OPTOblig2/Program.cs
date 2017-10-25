using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPTOblig2
{
    class Program
    {
        static Random rn = new Random();
        //Hardcoded graph
        static int[,] graph =
        {
            {0,1,0,1,0},
            {1,0,1,0,1},
            {0,1,0,1,1},
            {1,0,1,0,0},
            {0,1,1,0,0}
            };
        //Colour Array
        static char[] col = { 'R', 'W', 'B' };
        static int mf = graph.GetLength(0);
        static int[] fittness = { mf, mf, mf, mf };
        static char[] parent1;
        static char[] parent2;
        
        static void Main(string[] args)
        {
            char[] init1 = InitSolution();
            char[] init2 = InitSolution();
            char[] init3 = InitSolution();
            char[] init4 = InitSolution();
            
            Console.Write("Fittness: " + GetFittness(init1) + "\n");
            PrintCharArray(init1);
            Console.Write("Fittness: " + GetFittness(init2) + "\n");
            PrintCharArray(init2);
            Console.Write("Fittness: " + GetFittness(init3) + "\n");
            PrintCharArray(init3);
            Console.Write("Fittness: " + GetFittness(init4) + "\n");
            PrintCharArray(init4);
            

            SelectParents(init1, init2, init3, init4);

            PrintTest();
            Console.ReadLine();
            
        }
        static public void SelectParents(char[] a, char[] b, char[] c, char[] d)
        {
            List<char[]> solutions = new List<char[]>();
            fittness[0] = GetFittness(a);
            fittness[1] = GetFittness(b);
            fittness[2] = GetFittness(c);
            fittness[3] = GetFittness(d);
            solutions.Add(a);
            solutions.Add(b);
            solutions.Add(c);
            solutions.Add(d);
            int best1 = CompareFit(fittness, int.MaxValue);
            parent1 = solutions[best1];
            parent2 = solutions[CompareFit(fittness, best1)];
            
            

        }
        //Calculates fittness of the solution
        static public int GetFittness(char[] c)
        {
            int fits = 0;
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = i; j < graph.GetLength(0); j++)
                {
                    if(graph[i,j] == 1)
                    {
                        if (c[i].Equals(c[j]))
                        {
                            fits++;
                        }
                    }
                }
            }

            return fits;
        }
        static public char [] InitSolution()
        {
            char[] initS = new char[graph.GetLength(0)];
            for(int i = 0; i < graph.GetLength(0); i++)
            {
                initS[i] = col[Rundum(3)];
            }

            return initS;
        }
        //Pick random number from range
        static public int Rundum(int range)
        {
            int a = rn.Next(0, range);

            return a;
        }
        //Print Char array
        static public void PrintCharArray(char[] a)
        {
            for(int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + ", ");
            }
            Console.Write("\n");
        }
        //Prints 2D int Array
        static public void Print2DArray(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++){
                for(int j = 0; j < a.GetLength(0); j++)
                {
                    Console.Write(a[i, j] + ", ");
                }
                Console.Write("\n");
            }
        }
        static public int CompareFit(int[] fit, int ignore)
        {
            int fittest = mf;
            int index = mf;
            for(int i = 0; i < fit.GetLength(0); i++)
            {
                if (ignore == i)
                    continue;
                if(fit[i] < fittest)
                {
                    fittest = fit[i];
                    index = i;
                }
            }
            return index;
        }
        
        static public void PrintTest()
        {
            Console.Write("\n");
            Print2DArray(graph);
            Console.Write("\n");
            Console.Write("Parent1, with fittness: " + GetFittness(parent1) + " : " );
            PrintCharArray(parent1);
            Console.Write("Parent2, with fittness: " + GetFittness(parent2) + " : ");
            PrintCharArray(parent2);

        }
    }

}
