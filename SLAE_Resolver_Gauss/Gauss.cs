using System;

namespace SLAE_Resolver_Gauss
{
    static class Gauss
    {
        static void ChangeRows(double[,] m, double[] fv, int i1, int i2)
        {
            double rv;
            for (int j = 0; j < m.GetLength(1); j++) 
            { 
                rv = m[i1, j];
                m[i1, j] = m[i2, j]; 
                m[i2, j] = rv; 
            }
            rv = fv[i1]; 
            fv[i1] = fv[i2]; 
            fv[i2] = rv; 
        }

        static void SubtractionMtpRow(double[,] m, double[] fv, int i1, int i2, double mtp)
        {
            for (int j = 0; j < m.GetLength(1); j++)
            {
                m[i1, j] -= m[i2, j] * mtp;
            }
            fv[i1] -= fv[i2] * mtp;
        }

        static void MatrixToRightUpperTriangularMatrix(double[,] m, double[] fv)
        {
            int d = m.GetLength(0);
            for (int j = 0; j < d - 1; j++)
            {
                int ir = j;
                for (int i = j; i < d; i++)
                {
                    if (Math.Abs(m[i, j]) > Math.Abs(m[ir, j]))
                    {
                        ir = i;
                    }
                }
                if (ir != j) 
                { 
                    ChangeRows(m, fv, ir, j); 
                }
                for (int i = d - 1; i > j; i--)
                {
                    SubtractionMtpRow(m, fv, i, j, m[i, j] / m[j, j]);
                }
            }
        }

        public static double[] Solve(double[,] em, double[] fv)
        {
            int d = em.GetLength(0);
            double[] res = new double[d];
            MatrixToRightUpperTriangularMatrix(em, fv);
            for (int i = d - 1; i >= 0; i--)
            {
                for (int j = d - 1; j > i; j--)
                {
                    fv[i] -= em[i, j] * fv[j];
                }
                fv[i] = fv[i] / em[i, i];
            }
            return fv;
        }
    }
}
