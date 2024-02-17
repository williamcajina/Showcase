
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Regression
{
    
    public struct Fraction
    {
        public Fraction(int n, int d)
        {
            N = n;
            D = d;
        }

        public int N { get; private set; }
        public int D { get; private set; }

        public static Fraction RealToFraction(double value, double error)
        {
            if (error <= 0.0 || error >= 1.0)
            {
                throw new ArgumentOutOfRangeException("error", "Must be between 0 and 1 (exclusive).");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            if (sign != 0)
            {
                // error is the maximum relative error; convert to absolute
                error *= value;
            }

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < error)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - error < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + error) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    upper_n = middle_n;
                    upper_d = middle_d;
                }
                else if (middle_n < (value - error) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    lower_n = middle_n;
                    lower_d = middle_d;
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction((n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }

        public override string ToString()
        { if (D ==1)
                return N+"" ;
        else
            return N + "/" + D;
        }
    }
    class Program
    {
        public static void printArray(double[,] array)
        {
            int row = array.GetLength(0);
            int col = array.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(array[i, j] + "    ");
                }
                Console.WriteLine();
            }
        }
       public static double[,] setMatrix( double[] inputs , double[] outputs)
        {
            int arraySize = inputs.GetLength(0);
          


            double[,] matrix = new double[arraySize, arraySize+1];
            for (int i = 0; i < arraySize ; i++)
            {
                int k = arraySize-1;
                for (int j = 0; j < arraySize + 1; j++)
                {
                    if (j < arraySize )
                    {
                        matrix[i, j] = Math.Pow(inputs[i], k);
                        k--;
                    }
                    else matrix[i, j] = outputs[i];






                }
            }


         

            return matrix;        
        }

        public static void solveMatrix(double[,] array)
        {
            int row = array.GetLength(0);
            int col = array.GetLength(1);
            double[] temp = new double[col];
            double div = 0;


            for (int i = 0; i < row; i++)
            {

                int m = i + 1;


                for (int k = 0; k < col; k++)
                {
                    temp[k] = array[i, k];
                }



                do
                {
                    if (m == row)
                        m = 0;
                    div = array[m, i] / array[i, i];
                    for (int n = 0; n < col; n++)
                    {
                        array[m, n] = array[m, n] - temp[n] * div;
                    }
                    m++;

                    if (m > row - 1)
                        m = 0;
                    if (m == i)
                        break;




                } while ((m < row));


                for (int k = 0; k < row; k++)
                {
                    div = array[k, k];
                    for (int n = 0; n < col; n++)
                    {
                        array[k, n] = array[k, n] / div;

                    }
                }



            }
        }
        /*
        public static double[,] Identity(double[,] matrix)
        {
            int arraySize = matrix.GetLength(0);
            double[,] matrix2 = new double[arraySize, arraySize * 2];

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize * 2; j++)
                {
                    if (j < arraySize)
                        matrix2[i, j] = matrix[i, j];

                    if (j > arraySize - 1 && j - arraySize == i)
                        matrix2[i, j] = 1;




                }
            }
            return matrix2;
        }
        */
        public static void getSolution(double[,] matrix)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            double[] answers = new double[row];
            int j = row - 1;
            for (int k = 0; k < row; k++)
            {
                answers[k] = matrix[k, col - 1];

            }

            //Console.Clear();
            double e = 0.000000001;
            for (int i = 0; i < row; i++)
            {
               

                if (j > 1) 
                    Console.Write(Fraction.RealToFraction(Math.Round(answers[i],10), e) + " x^" + j +" + ");
                if (j == 1)
                    Console.Write(Fraction.RealToFraction(Math.Round(answers[i], 10), e) + " x + ");
                if (j == 0)
                    Console.Write(Fraction.RealToFraction(Math.Round(answers[i], 10), e));
                j--;
                   
                



            }
            
         

        }
        static void Main(string[] args)
        {
            Stopwatch a = new Stopwatch();
            a.Start();
            double[] inputs ;
            double[] outputs;
            int i = 0;
            int arraySize;

            Console.Write("How many inputs ? ");
            arraySize = Convert.ToInt32(Console.ReadLine());
            inputs = new double[arraySize];
            outputs = new double[arraySize];

            Console.Write("\nWrite some Input and Output\n\n");


            do
            {
                Console.Write("Input : ");
                String inputString = Console.ReadLine();
                if (inputString == "pi")
                    inputs[i] = Math.PI;
                else if(inputString.Contains('/'))
                {
                    String[] splits = inputString.Split('/');
                    if(splits[0] == "pi")
                    inputs[i] = Math.PI / Convert.ToDouble(splits[1]);


                }
                else
                inputs[i] = Convert.ToDouble(inputString);

                Console.Write("Output: ");
                String outputString = Console.ReadLine();
                outputs[i] = Convert.ToDouble(outputString);
                Console.WriteLine();


                i++;

            }
            while (i<arraySize);


            double[,] matrix = setMatrix(inputs, outputs);
            solveMatrix(matrix);
            getSolution(matrix);

          




            Console.Read();
        }
    }
}
