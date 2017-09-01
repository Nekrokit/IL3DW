using System;
using System.Collections.Generic;

namespace ITL_1_5_3_DLL
{
    class Blocks
    {
        public Int16[, ,] Matrix;
        private Int64 x;
        private Int64 y;
        private Int64 z;

        public Blocks(Int64 _x, Int64 _y, Int64 _z)
        {
            Matrix = new Int16[_x, _y, _z];
            x = _x;
            y = _y;
            z = _z;
        }

        public void MatchInsideCubes()
        {
            for (int k = 0; k < x; k++)
            {
                for (int l = 0; l < y; l++)
                {
                    for (int m = 0; m < z; m++)
                    {
                        if (Matrix[k, l, m] == 0)
                        {
                            Matrix[k, l, m] = 3;
                        }
                    }
                }
            }
        }

        public void MatchFirstOutsideCube()
        {
            Matrix[0, 0, 0] = 1;
        }

        public void FindAllOutsideCubes(List<Vertex3D> checkingCubesList)
        {
            Vertex3D currentCube = checkingCubesList[0];
            Matrix[(Int64)currentCube.X, (Int64)currentCube.Y, (Int64)currentCube.Z] = 1;
            if (currentCube.X >= 1)
            {
                if (Matrix[(Int64)currentCube.X - 1, (Int64)currentCube.Y, (Int64)currentCube.Z] == 0)
                {
                    Matrix[(Int64)currentCube.X - 1, (Int64)currentCube.Y, (Int64)currentCube.Z] = 1;
                    Vertex3D nova = new Vertex3D(currentCube.X - 1, currentCube.Y, currentCube.Z);
                    checkingCubesList.Add(nova);
                }
            }
            if (currentCube.Y >= 1)
            {
                if (Matrix[(Int64)currentCube.X, (Int64)currentCube.Y - 1, (Int64)currentCube.Z] == 0)
                {
                    Matrix[(Int64)currentCube.X, (Int64)currentCube.Y - 1, (Int64)currentCube.Z] = 1;
                    Vertex3D nova = new Vertex3D(currentCube.X, currentCube.Y - 1, currentCube.Z);
                    checkingCubesList.Add(nova);
                }
            }
            if (currentCube.Z >= 1)
            {
                if (Matrix[(Int64)currentCube.X, (Int64)currentCube.Y, (Int64)currentCube.Z - 1] == 0)
                {
                    Matrix[(Int64)currentCube.X, (Int64)currentCube.Y, (Int64)currentCube.Z - 1] = 1;
                    Vertex3D nova = new Vertex3D(currentCube.X, currentCube.Y, currentCube.Z - 1);
                    checkingCubesList.Add(nova);
                }
            }
            if (currentCube.X < (x - 1))
            {
                if (Matrix[(Int64)currentCube.X + 1, (Int64)currentCube.Y, (Int64)currentCube.Z] == 0)
                {
                    Matrix[(Int64)currentCube.X + 1, (Int64)currentCube.Y, (Int64)currentCube.Z] = 1;
                    Vertex3D nova = new Vertex3D(currentCube.X + 1, currentCube.Y, currentCube.Z);
                    checkingCubesList.Add(nova);
                }
            }
            if (currentCube.Y < (y - 1))
            {
                if (Matrix[(Int64)currentCube.X, (Int64)currentCube.Y + 1, (Int64)currentCube.Z] == 0)
                {
                    Matrix[(Int64)currentCube.X, (Int64)currentCube.Y + 1, (Int64)currentCube.Z] = 1;
                    Vertex3D nova = new Vertex3D(currentCube.X, currentCube.Y + 1, currentCube.Z);
                    checkingCubesList.Add(nova);
                }
            }
            if (currentCube.Z < (z - 1))
            {
                if (Matrix[(Int64)currentCube.X, (Int64)currentCube.Y, (Int64)currentCube.Z + 1] == 0)
                {
                    Matrix[(Int64)currentCube.X, (Int64)currentCube.Y, (Int64)currentCube.Z + 1] = 1;
                    Vertex3D nova = new Vertex3D(currentCube.X, currentCube.Y, currentCube.Z + 1);
                    checkingCubesList.Add(nova);
                }
            }
            checkingCubesList.Remove(currentCube);
        }

        public void MatchOutsideCubes()
        {
            MatchFirstOutsideCube();
            List<Vertex3D> checkingCubesList = new List<Vertex3D>();
            checkingCubesList.Add(new Vertex3D(1, 1, 1));
            while (checkingCubesList.Count > 0)
            {
                FindAllOutsideCubes(checkingCubesList);
            }
        }
    }
}