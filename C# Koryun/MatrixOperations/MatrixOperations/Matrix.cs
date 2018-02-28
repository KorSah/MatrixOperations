using System;

namespace MatrixOperations
{
    /// <summary>
    /// Matrix type that we will use for matrix operations
    /// </summary>
    public class Matrix
    {
        // Generating random numbers
        Random random = new Random();

        // The row of matrix
        public readonly int Row;

        // The column of matrix
        public readonly int Column;

        // Matrix elements
        public readonly double[,] Elements;

        // Indexator for matrix
        public double this[int row, int column]
        {
            get { return this.Elements[row, column]; }
            set { this.Elements[row, column] = value; }
        }

        // Parameterless Constructor fot Matrix type
        public Matrix()
        {
            Console.Write("Enter the row of matrix: \t");

            // Checking the value of row
            bool validRow = false;
            while (validRow != true)
            {
                try
                {
                    this.Row = int.Parse(Console.ReadLine());
                    if (this.Row <= 1)
                    {
                        throw new Exception("Invalid size of row");
                    }
                    validRow = true;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error. " + e.Message);
                    Console.ResetColor();
                    Console.Write("Enter the row of matrix again: \t");
                }
            }

            Console.Write("Enter the column of matrix: \t");

            // Checking the value of column
            bool validCol = false;
            while (validCol != true)
            {
                try
                {
                    this.Column = int.Parse(Console.ReadLine());
                    if (this.Column <= 1)
                    {
                        throw new Exception("Invalid size of column");
                    }
                    validCol = true;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error. " + e.Message);
                    Console.ResetColor();
                    Console.Write("Enter the column of matrix again: \t");
                }
            }

            // Set matrix elements with random numbers
            this.Elements = new double[this.Row, this.Column];
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    this[i, j] = random.Next(11, 99);
                }
            }
        }

        // Constructor with one parameter where column = row
        public Matrix(int row)
        {
            // Initialize the Row of matrix
            this.Row = row;

            // Initialize the Column of matrix
            this.Column = row;

            // Set matrix elements with random numbers
            this.Elements = new double[row, row];
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    this[i, j] = random.Next(11, 99);
                }
            }
        }

        // Constructor whith two parameters for Matrix type
        public Matrix(int row, int column) 
        {
            this.Row = row;
            this.Column = column;
            this.Elements = new double[this.Row, this.Column];
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    this[i, j] = random.Next(11, 99);
                }
            }
        }

        // Constructor which initialize matrix with default elements
        public Matrix(int row, int column, bool b)
        {
            this.Row = row;
            this.Column = column;
            this.Elements = new double[this.Row, this.Column];
        }

        /// <summary>
        /// + operator for adding two matrices
        /// </summary>
        /// <param name="m1">First matrix</param>
        /// <param name="m2">Second matrix</param>
        /// <returns>Returns a new matrix which is the sum of two matrices</returns>
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            // Checking equality of matrix sizes
            if (m1.Row != m2.Row || m1.Column != m2.Column)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Matrices can not be added because of their different sizes");
                Console.ResetColor();
                return null;
            }
            else
            {
                Matrix addMatrix = new Matrix(m1.Row, m1.Column);
                for (int i = 0; i < m1.Row; i++)
                {
                    for (int j = 0; j < m1.Column; j++)
                    {
                        addMatrix[i, j] = m1[i, j] + m2[i, j];
                    }
                }
                return addMatrix;
            }
        }

        /// <summary>
        /// * operator for multiplying two matrices
        /// </summary>
        /// <param name="m1">First matrix</param>
        /// <param name="m2">Second matrix</param>
        /// <returns>Returns a new matrix. Multiplication of two matrices</returns>
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Column != m2.Row)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Matrices can not be multiplied because of invalid sizes");
                Console.ResetColor();
                return null;
            }
            else
            {
                Matrix multMatrix = new Matrix(m1.Row, m2.Column);
                for (int i = 0; i < multMatrix.Row; i++)
                {
                    for (int j = 0; j < multMatrix.Column; j++)
                    {
                        for (int k = 0; k < m1.Column; k++)
                        {
                            multMatrix[i, j] += m1[i, k] * m2[k, j];
                        }
                    }
                }
                return multMatrix;
            }
        }

        /// <summary>
        /// The scalar is multiplied by each element of the matrix
        /// </summary>
        /// <param name="matrix">Given matrix</param>
        /// <param name="scalar">Scalar number</param>
        /// <returns>Returns a multiplicated matrix</returns>
        public Matrix ScalarMultiplication(double scalar)
        {
            Matrix matrix = new Matrix(this.Row, this.Column, true);
            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Column; j++)
                {
                    matrix[i, j] = scalar * this[i, j];
                }
            }
            return matrix;
        }

        /// <summary>
        /// Matrix transpose method
        /// </summary>
        /// <returns>Returns transposed matrix</returns>
        public Matrix Transpose()
        {
            Matrix matrixT = new Matrix(this.Column, this.Row, true);
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    this[i, j] = this[j, i];
                }
            }
            return matrixT;
        }

        /// <summary>
        /// Method Inverse the matrix
        /// </summary>
        /// <returns>Returns inversed matrix</returns>
        public Matrix Inverse()
        {
            try
            {
                if (this.Row == this.Column)
                {
                    Matrix matrixInv = new Matrix(2 * this.Row, 2 * this.Column);

                    for (int i = 0; i < this.Row; i++)
                    {
                        for (int j = 0; j < 2 * this.Row; j++)
                        {
                            if (j == (i + this.Row))
                                matrixInv[i, j] = 1;
                            else
                                matrixInv[i, j] = 0;
                        }
                    }

                    for (int i = 0; i < this.Row; i++)
                    {
                        for (int j = 0; j < this.Column; j++)
                        {
                            matrixInv[i, j] = this[i, j];
                        }
                    }

                    double tempElement;
                    for (int i = this.Row; i > 1; i--)
                    {
                        if (matrixInv[i - 1, 1] < matrixInv[i, 1])
                        {
                            for (int j = 0; j < 2 * this.Row; j++)
                            {
                                tempElement = matrixInv[i, j];
                                matrixInv[i, j] = matrixInv[i - 1, j];
                                matrixInv[i - 1, j] = 1;
                            }
                        }
                    }

                    for (int i = 0; i < this.Row; i++)
                    {
                        for (int j = 0; j < 2 * this.Column; j++)
                        {
                            if (j != i)
                            {
                                tempElement = matrixInv[j, i] / matrixInv[i, i];
                                for (int k = 0; k < this.Row * 2; k++)
                                {
                                    matrixInv[j, k] -= matrixInv[i, k] * tempElement;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < this.Row; i++)
                    {
                        tempElement = matrixInv[i, i];
                        for (int j = 0; j < 2 * this.Row; j++)
                        {
                            matrixInv[i, j] = matrixInv[i, j] / tempElement;
                        }
                    }

                    Matrix matrixInverse = new Matrix(this.Row, this.Column);
                    for (int i = 0; i < this.Row; i++)
                    {
                        for (int j = this.Row; j < 2 * this.Row; j++)
                        {
                            matrixInverse[i, j - this.Row] = matrixInv[i, j];
                        }
                    }
                    return matrixInverse;
                }
                else
                {
                    throw new Exception("This is not a squared matrix");
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error. " + e.Message);
                Console.ResetColor();
                return null;
            }
        }

        /// <summary>
        /// Method checks if matrix orthogonal
        /// </summary>
        /// <returns>Returns answer</returns>
        public string Orthogonal()
        {
            Matrix inverse = this.Inverse();
            Matrix transpose = this.Transpose();
            if (inverse.Row == transpose.Row && inverse.Column == transpose.Column)
            {
                for (int i = 0; i < inverse.Row; i++)
                {
                    for (int j = 0; j < inverse.Column; j++)
                    {
                        if (inverse[i, j] != transpose[i, j])
                        {
                            return "Matrix is not Orthogonal";
                        }
                    }
                }
                return "Matrix is Orthogonal";
            }
            else
                return "Matrix is not Orthogonal";
        }

        /// <summary>
        /// Translates vector
        /// </summary>
        /// <param name="vector"> Given vector for translation</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        /// <returns>Returns translated vector</returns>
        public static double[] Translate(double[] vector, int x, int y, int z)
        {
            if (vector.Length == 4)
            {
                Matrix identity = new Matrix(vector.Length, vector.Length, true);
                identity.Elements[0, vector.Length - 1] = x;
                identity.Elements[1, vector.Length - 1] = y;
                identity.Elements[2, vector.Length - 1] = z;
                double[] vectorTemp = new double[vector.Length];
                for (int i = 0; i < identity.Row; i++)
                {
                    for (int j = 0; j < identity.Column; j++)
                    {
                        vectorTemp[i] += vector[j] * identity.Elements[i, j];
                    }
                }
                return vectorTemp;
            }
            else
                throw new Exception("Vector length must be equal to four");
        }

        /// <summary>
        /// Method scales vector
        /// </summary>
        /// <param name="vector">Given vector</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        /// <returns>Returns scaled vector</returns>
        public static double[] Scaling(double[] vector, int x, int y, int z)
        {
            if (vector.Length == 4)
            {
                Matrix identity = new Matrix(vector.Length, vector.Length, true);
                identity.Elements[0, 0] = x;
                identity.Elements[1, 1] = y;
                identity.Elements[2, 2] = z;
                double[] vectorTemp = new double[vector.Length];
                for (int i = 0; i < identity.Row; i++)
                {
                    for (int j = 0; j < identity.Column; j++)
                    {
                        vectorTemp[i] += vector[j] * identity.Elements[i, j];
                    }
                }
                return vectorTemp;
            }
            else
                throw new Exception("Vector length must be equal to four");
        }
  
        /// <summary>
        /// Method rotates vector around x
        /// </summary>
        /// <param name="vector">Given vector</param>
        /// <param name="alfa">Rotating angle</param>
        /// <returns>Returns rotated vector</returns>
        public static double[] RotateX(double[] vector, double alfa)
        {
            if (vector.Length == 4)
            {
                alfa *= Math.PI / 180;
                Matrix identity = new Matrix(vector.Length, vector.Length, true);
                for (int i = 0; i < identity.Row; i++)
                {
                    for (int j = 0; j < identity.Column; j++)
                    {
                        if (i == j)
                            identity[i, j] = 1;
                    }
                }

                identity[1, 1] = Math.Cos(alfa);
                identity[1, 2] = -Math.Sin(alfa);
                identity[2, 1] = Math.Sin(alfa);
                identity[2, 2] = Math.Cos(alfa);
                double[] vectorTemp = new double[vector.Length];
                for (int i = 0; i < identity.Row; i++)
                {
                    for (int j = 0; j < identity.Column; j++)
                    {
                        vectorTemp[i] += vector[j] * identity[i, j];
                    }
                }
                return vectorTemp;
            }
            else
                throw new Exception("Vector length must be equal to four");
        }

        /// <summary>
        /// Method rotates vector around y
        /// </summary>
        /// <param name="vector">Given vector</param>
        /// <param name="alfa">Rotating angle</param>
        /// <returns>Returns rotated vector</returns>
        public static double[] RotateY(double[] vector, double alfa)
        {
            if (vector.Length == 4)
            {
                alfa *= Math.PI / 180;
                Matrix identity = new Matrix(vector.Length, vector.Length, true);
                for (int i = 0; i < identity.Row; i++)
                {
                    for (int j = 0; j < identity.Column; j++)
                    {
                        if (i == j)
                            identity[i, j] = 1;
                    }
                }

                identity[0, 0] = Math.Cos(alfa);
                identity[0, 2] = Math.Sin(alfa);
                identity[2, 0] = -Math.Sin(alfa);
                identity[2, 2] = Math.Cos(alfa);
                double[] vectorTemp = new double[vector.Length];
                for (int i = 0; i < identity.Row; i++)
                {
                    for (int j = 0; j < identity.Column; j++)
                    {
                        vectorTemp[i] += vector[j] * identity[i, j];
                    }
                }
                return vectorTemp;
            }
            else
                throw new Exception("Vector length must be equal to four");
        }

        /// <summary>
        /// Method rotates vector around z
        /// </summary>
        /// <param name="vector">Given vector</param>
        /// <param name="alfa">Rotating angle</param>
        /// <returns>Returns rotated vector</returns>
        public static double[] RotateZ(double[] vector, double alfa)
        {
            if (vector.Length == 4)
            {
                alfa *= Math.PI / 180;
                Matrix identity = new Matrix(vector.Length, vector.Length, true);
                for (int i = 0; i < identity.Row; i++)
                {
                    for (int j = 0; j < identity.Column; j++)
                    {
                        if (i == j)
                            identity[i, j] = 1;
                    }
                }

                identity[0, 0] = Math.Cos(alfa);
                identity[0, 1] = -Math.Sin(alfa);
                identity[1, 0] = Math.Sin(alfa);
                identity[1, 1] = Math.Cos(alfa);
                double[] vectorTemp = new double[vector.Length];
                for (int i = 0; i < identity.Row; i++)
                {
                    for (int j = 0; j < identity.Column; j++)
                    {
                        vectorTemp[i] += vector[j] * identity[i, j];
                    }
                }
                return vectorTemp;
            }
            else
                throw new Exception("Vector length must be equal to four");
        }

        /// <summary>
        /// Method finds the smallest element in matrix
        /// </summary>
        /// <param name="matrix">Givent matrix</param>
        /// <returns>Returns the smallest element</returns>
        public double SmallestElement()
        {
            double minimum = this[0, 0];
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    if (minimum >= this[i, j])
                    {
                        minimum = this[i, j];
                    }
                }
            }
            return minimum;
        }

        /// <summary>
        /// Method finds the biggest element in matrix
        /// </summary>
        /// <param name="matrix">Given matrix</param>
        /// <returns>Returns the biggest element</returns>
        public double BiggestElement()
        {
            double maximum = this[0, 0];
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    if (maximum <= this[i, j])
                    {
                        maximum = this[i, j];
                    }
                }
            }
            return maximum;
        }

        /// <summary>
        /// Print method prints the matrix elements 
        /// </summary>
        /// <param name="matrix">matrix for printing</param>
        public static void Print(Matrix matrix)
        {
            // Checking content of matrix
            // If it has elements print them
            if (matrix != null)
            {
                for (int i = 0; i < matrix.Row; i++)
                {
                    for (int j = 0; j < matrix.Column; j++)
                    {
                        Console.Write(matrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no element in matrix");
                Console.ResetColor();
            }
        }
    }
}
