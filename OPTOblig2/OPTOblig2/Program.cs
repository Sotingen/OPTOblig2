using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPTOblig2
{
    class Program
    {
        //1.    2-point cross over
        //  1.1     Velg range select from parents.
        //2.    Mutation (LES)
        //3.    Finn stopp kriterier

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
        static char[] parent1 = new char[mf];
        static char[] parent2 = new char[mf];
        static char[] child1 = new char[mf];
        static char[] child2 = new char[mf];

        static void Main(string[] args)
        {
            char[] init1 = InitSolution();
            char[] init2 = InitSolution();

            parent1 = init1;
            parent2 = init2;
            Console.Write("Fittness: " + GetFittness(parent1) + "\n");
            PrintCharArray(parent1);
            Console.Write("Fittness: " + GetFittness(parent2) + "\n");
            PrintCharArray(parent2);
            

            SelectParents(parent1, parent2, child1, child2);
            Humps();
            Console.Write("Child 1 Fittness: " + GetFittness(child1) + "\n");
            PrintCharArray(child1);
            Console.Write("Child 2 Fittness: " + GetFittness(child2) + "\n");
            PrintCharArray(child2);
            PrintTest();
            Console.ReadLine();
            
        }
        //Selects parents
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
        //UNDER TESTING MATING
        static public void Humps()
        {
            int range1 = Rundum(graph.GetLength(0));
            int range2 = Rundum(graph.GetLength(0));
            while(range1 == range2)
            {
                range2 = Rundum(graph.GetLength(0));
            }
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if((i > range1 && i < range2) || (i < range1 && i > range2))
                {
                    child1[i] = parent1[i];
                    child2[i] = parent2[i];
                }
                else
                {
                    child2[i] = parent1[i];
                    child1[i] = parent2[i];

                }
            }
            Console.Write("Range1: " + range1.ToString() + "\nRange2: " + range2.ToString() + "\n");
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
