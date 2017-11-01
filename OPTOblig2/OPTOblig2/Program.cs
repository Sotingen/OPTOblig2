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
        //1.1   Velg range select from parents.
        //2.    Mutation (LES)
        //3.    Finn stopp kriterier

        static Random rn = new Random();
        //Hardcoded graph
        static int[,] graph = GenerateGraph(20);
        //{
        //    {0,1,0,1,0},
        //    {1,0,1,0,1},
        //    {0,1,0,1,1},
        //    {1,0,1,0,0},
        //    {0,1,1,0,0}
        //    };
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
            int iter = 0;
            //Initalising parent solustions
            parent1 = init1;
            parent2 = init2;

            int fitOld1 = GetFittness(parent1);
            int fitOld2 = GetFittness(parent2);
            int fitNew1 = int.MaxValue;
            int fitNew2 = int.MaxValue;

            Print2DArray(graph);

            while (iter != 10) {
                //Mates the parents
                Humps();
                //20% chance of mutation
                Mutation();
                //Selects the new parents
                SelectParents(parent1, parent2, child1, child2);
                fitNew1 = GetFittness(parent1);
                fitNew2 = GetFittness(parent2);
                if ((fitNew1 + fitNew2) < (fitOld1 + fitOld2)) {
                    iter = 0;
                }
                else
                    iter++;
                fitOld1 = GetFittness(parent1);
                fitOld2 = GetFittness(parent2);
                PrintBestParent();
            }

            
            Console.ReadLine();
            
        }
        static public int[,] GenerateGraph(int h) {
            int[,] gruph = new int[h, h];
            int edge = 0;
            int count = 0;
            for(int i = 0; i < h; i++) {
                count = 0;
                for (int j = i; j < h; j++) {
                    if (i == j)
                        continue;
                    edge = Rundum(2);
                    if (edge == 1)
                        count++;
                    gruph[i, j] = edge;
                    gruph[j, i] = edge;
                }
                if (count == 0 && i > h-1) {
                    i--;
                    
                }
                    
            }

            return gruph;
        }
        //Selects parents
        static public void SelectParents(char[] a, char[] b, char[] c, char[] d)
        {
            List<char[]> solutions = new List<char[]>();
            solutions.Clear();
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
        //Mates the two parents and creates two children
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
                if((i >= range1 && i <= range2) || (i <= range1 && i >= range2))
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
            //Console.Write("Range1: " + range1.ToString() + "\nRange2: " + range2.ToString() + "\n");
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
        //Returns index of the best fittness
        static public int CompareFit(int[] fit, int ignore)
        {
            int fittest = int.MaxValue;
            int index = mf-1;
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
        //20% change for mutation of a child
        static public void Mutation() {
            int rnd = Rundum(10);
            int rndIndex = Rundum(graph.GetLength(0));
            if(rnd <= 1) {
                char childnode = child1[rndIndex];
                char childmut = col[Rundum(3)];
                while (childnode.Equals(childmut)) {
                    childmut = col[Rundum(3)];
                }
                child1[rndIndex] = childmut;
            }
            else if(rnd >= 9) {
                char childnode = child2[rndIndex];
                char childmut = col[Rundum(3)];
                while (childnode.Equals(childmut)) {
                    childmut = col[Rundum(3)];
                }
                child2[rndIndex] = childmut;
            }
        }
        //Prints the best out of the two parents
        static public void PrintBestParent() {
            if (GetFittness(parent1) < GetFittness(parent2))
                Console.WriteLine(GetFittness(parent1));
            else
                Console.WriteLine(GetFittness(parent2));
        }
        
        static public void PrintTest()
        {
            
            Console.Write("Parent1, with fittness: " + GetFittness(parent1) + " : " );
            PrintCharArray(parent1);
            Console.WriteLine("Parent2, with fittness: " + GetFittness(parent2) + " : ");
            PrintCharArray(parent2);
            Console.Write("\n");

        }
    }

}
