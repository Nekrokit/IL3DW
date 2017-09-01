using System;
using System.Collections.Generic;
using ObjLoader.Loader.Loaders;
using ObjParser;

namespace ITL_1_5_3_DLL
{
    public class Model
    {
        Int16[,] Matrix2D;
        List<Vertex3D> VertexList;
        List<Face> FaceList;
        public double BlockSize = 20;
        public List<Vertex3D> VerShowList;
        private double minX;
        private double minY;
        private double minZ;
        private double maxAbsX;
        private double maxAbsY;
        private double maxAbsZ;
        private Blocks Matrix;

        public Model(Obj loadData, double newBlockSize)
        {
            VertexList = new List<Vertex3D>();
            FaceList = new List<Face>();
            foreach(var vertex in loadData.VertexList)
            {
                Vertex3D newVertex = new Vertex3D(vertex.X, vertex.Y, vertex.Z);
                VertexList.Add(newVertex);
            }
            foreach(var face in loadData.FaceList)
            {
                Face newFace = new Face();
                for (int i = 0; i < face.VertexIndexList.Length; i++)
                {
                    newFace.VertexList.Add(Math.Abs(face.VertexIndexList[i]));
                }
                FaceList.Add(newFace);
            }
            BlockSize = newBlockSize;
        }
        public Model(LoadResult loadData, double newBlockSize)
        {
            VertexList = new List<Vertex3D>();
            FaceList = new List<Face>();
            foreach(var vertex in loadData.Vertices)
            {
                Vertex3D newVertex = new Vertex3D(vertex.X, vertex.Y, vertex.Z);
                VertexList.Add(newVertex);
            }
            foreach(var group in loadData.Groups)
            {
                foreach(var face in group.Faces)
                {
                    Face newFace = new Face();
                    for(int i = 0; i < face.Count; i++)
                    {
                        newFace.VertexList.Add(face[i].VertexIndex);
                    }
                    FaceList.Add(newFace);
                }
            }
            BlockSize = newBlockSize;
        }

        public void Transform()
        {
            Centring();
            TransformForWork();
            InitializeMatrixAndSize();
            ChooseProection();
            MatchAllCubes();
        }

        public void Centring()
        {
            double minimumX, minimumY, minimumZ, maximumX, maximumY, maximumZ;
            minimumX = maximumX = VertexList[0].X;
            minimumY = maximumY = VertexList[0].Y;
            minimumZ = maximumZ = VertexList[0].Z;
            foreach (Vertex3D vertex in VertexList)
            {
                if (maximumX < vertex.X)
                {
                    maximumX = vertex.X;
                }
                if (maximumY < vertex.Y)
                {
                    maximumY = vertex.Y;
                }
                if (maximumZ < vertex.Z)
                {
                    maximumZ = vertex.Z;
                }
                if (minimumX > vertex.X)
                {
                    minimumX = vertex.X;
                }
                if (minimumY > vertex.Y)
                {
                    minimumY = vertex.Y;
                }
                if (minimumZ > vertex.Z)
                {
                    minimumZ = vertex.Z;
                }
            }
            double centralX = (minimumX + maximumX) / 2;
            double centralY = (minimumY + maximumY) / 2;
            double centralZ = (minimumZ + maximumZ) / 2;
            foreach (Vertex3D vertex in VertexList)
            {
                vertex.X -= centralX;
                vertex.Y -= centralY;
                vertex.Z -= centralZ;
            }
        }

        public double X
        {
            get { return maxAbsX; }
        }

        public void SetMin()
        {
            minX = VertexList[0].X;
            minY = VertexList[0].Y;
            minZ = VertexList[0].Z;
            foreach(Vertex3D temp in VertexList)
            {
                if (minX > temp.X)
                    minX = temp.X;
                if (minY > temp.Y)
                    minY = temp.Y;
                if (minZ > temp.Z)
                    minZ = temp.Z;
            }
        }

        public void TransformForWork()
        {
            foreach (Vertex3D temp in VertexList)
            {
                temp.X /= BlockSize;
                temp.Y /= BlockSize;
                temp.Z /= BlockSize;
            }
            SetMin();
            foreach (Vertex3D temp in VertexList)
            {
                temp.X = temp.X - minX + 2;
                temp.Y = temp.Y - minY + 2;
                temp.Z = temp.Z - minZ + 2;
            }
        }

        public void InitializeMatrixAndSize()
        {
            maxAbsX = VertexList[0].X;
            maxAbsY = VertexList[0].Y;
            maxAbsZ = VertexList[0].Z;
            foreach(Vertex3D temp in VertexList)
            {
                if (maxAbsX < temp.X)
                    maxAbsX = temp.X;
                if (maxAbsY < temp.Y)
                    maxAbsY = temp.Y;
                if (maxAbsZ < temp.Z)
                    maxAbsZ = temp.Z;
            }
            Matrix = new Blocks((Int64)maxAbsX + 5, (Int64)maxAbsY + 5, (Int64)maxAbsZ + 5);
        }

        public void ChooseProection()
        {
            foreach(var face in FaceList)
            {
                try
                {
                    double A = Math.Abs((VertexList[face.VertexList[1] - 1].Y - VertexList[face.VertexList[0] - 1].Y) * (VertexList[face.VertexList[2] - 1].Z - VertexList[face.VertexList[0] - 1].Z) - (VertexList[face.VertexList[1] - 1].Z - VertexList[face.VertexList[0] - 1].Z) * (VertexList[face.VertexList[2] - 1].Y - VertexList[face.VertexList[0] - 1].Y));
                    double B = Math.Abs(-(VertexList[face.VertexList[1] - 1].X - VertexList[face.VertexList[0] - 1].X) * (VertexList[face.VertexList[2] - 1].Z - VertexList[face.VertexList[0] - 1].Z) + (VertexList[face.VertexList[1] - 1].Z - VertexList[face.VertexList[0] - 1].Z) * (VertexList[face.VertexList[2] - 1].X - VertexList[face.VertexList[0] - 1].X));
                    double C = Math.Abs((VertexList[face.VertexList[1] - 1].X - VertexList[face.VertexList[0] - 1].X) * (VertexList[face.VertexList[2] - 1].Y - VertexList[face.VertexList[0] - 1].Y) - (VertexList[face.VertexList[1] - 1].Y - VertexList[face.VertexList[0] - 1].Y) * (VertexList[face.VertexList[2] - 1].X - VertexList[face.VertexList[0] - 1].X));
                    if (A >= B && A >= C)
                    {
                        MatchLimitCubes(face, 1);
                    }
                    if (B >= A && B >= C)
                    {
                        MatchLimitCubes(face, 2);
                    }
                    if (C >= A && C >= B)
                    {
                        MatchLimitCubes(face, 3);
                    }
                }
                catch { }
            }
        }

        public void MatchAllCubes()
        {
            Matrix.MatchOutsideCubes();
            Matrix.MatchInsideCubes();
        }

        public void MatchInsideCubes(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (Matrix2D[i, j] == 0)
                    {
                        Matrix2D[i, j] = 3;
                    }
                }
            }
        }

        public void MatchOutsideCubes(int x, int y)
        {
            MatchFirstOutsideCube(x, y);
            List<Vertex2D> checkingCubesList = new List<Vertex2D>();
            checkingCubesList.Add(new Vertex2D(1, 1));
            while (checkingCubesList.Count > 0)
            {
                FindAllOutsideCubes(checkingCubesList, x, y);
            }
        }

        private void FindAllOutsideCubes(List<Vertex2D> checkingCubesList, int x, int y)
        {
            Vertex2D currentCube = checkingCubesList[0];
            Matrix2D[(int)currentCube.X, (int)currentCube.Y] = 1;
            if (currentCube.X > 1)
            {
                if (Matrix2D[(int)currentCube.X - 1, (int)currentCube.Y] == 0)
                {
                    Matrix2D[(int)currentCube.X - 1, (int)currentCube.Y] = 1;
                    Vertex2D newOutSideVertex = new Vertex2D(currentCube.X - 1, currentCube.Y);
                    checkingCubesList.Add(newOutSideVertex);
                }
            }
            if (currentCube.Y > 1)
            {
                if (Matrix2D[(int)currentCube.X, (int)currentCube.Y - 1] == 0)
                {
                    Matrix2D[(int)currentCube.X, (int)currentCube.Y - 1] = 1;
                    Vertex2D newOutSideVertex = new Vertex2D(currentCube.X, currentCube.Y - 1);
                    checkingCubesList.Add(newOutSideVertex);
                }
            }
            if (currentCube.X < (x - 1))
            {
                if (Matrix2D[(int)currentCube.X + 1, (int)currentCube.Y] == 0)
                {
                    Matrix2D[(int)currentCube.X + 1, (int)currentCube.Y] = 1;
                    Vertex2D newOutSideVertex = new Vertex2D(currentCube.X + 1, currentCube.Y);
                    checkingCubesList.Add(newOutSideVertex);
                }
            }
            if (currentCube.Y < (y - 1))
            {
                if (Matrix2D[(int)currentCube.X, (int)currentCube.Y + 1] == 0)
                {
                    Matrix2D[(int)currentCube.X, (int)currentCube.Y + 1] = 1;
                    Vertex2D newOutSideVertex = new Vertex2D(currentCube.X, currentCube.Y + 1);
                    checkingCubesList.Add(newOutSideVertex);
                }
            }
            checkingCubesList.Remove(currentCube);
        }

        public void MatchFirstOutsideCube(int x, int y)
        {
            for (int i = 0; i < x; Matrix2D[i++, 0] = 1) ;
            for (int i = 0; i < y; Matrix2D[0, i++] = 1) ;
        }

        private void MatchLimitCubes(Face currentFace, Int16 side)
        {
            List<Vertex3D> faceVertex = new List<Vertex3D>();
            foreach(int i in currentFace.VertexList)
            {
                faceVertex.Add(VertexList[i - 1]);
            }
            double A = ((faceVertex[1].Y - faceVertex[0].Y) * (faceVertex[2].Z - faceVertex[0].Z) - (faceVertex[1].Z - faceVertex[0].Z) * (faceVertex[2].Y - faceVertex[0].Y));
            double B = (-(faceVertex[1].X - faceVertex[0].X) * (faceVertex[2].Z - faceVertex[0].Z) + (faceVertex[1].Z - faceVertex[0].Z) * (faceVertex[2].X - faceVertex[0].X));
            double C = ((faceVertex[1].X - faceVertex[0].X) * (faceVertex[2].Y - faceVertex[0].Y) - (faceVertex[1].Y - faceVertex[0].Y) * (faceVertex[2].X - faceVertex[0].X));
            double D = A * faceVertex[0].X + B * faceVertex[0].Y + C * faceVertex[0].Z;
            double minThirdCoord;
            double maxThirdCoord;
            double newThirdCoord;
            if (side == 1)
            {
                Matrix2D = new short[(Int64)maxAbsY + 4, (Int64)maxAbsZ + 4];
                List<Vertex2D> vertexIn2DList = new List<Vertex2D>();
                foreach(Vertex3D old3DVertex in faceVertex)
                {
                    Vertex2D new2DVertex = new Vertex2D(old3DVertex.Y, old3DVertex.Z);
                    vertexIn2DList.Add(new2DVertex);
                }
                for (int i = 0; i < vertexIn2DList.Count - 1; i++)
                {
                    MatchTwoPoints(vertexIn2DList[i], vertexIn2DList[i + 1]);
                }
                MatchTwoPoints(vertexIn2DList[0], vertexIn2DList[vertexIn2DList.Count - 1]);
                MatchOutsideCubes((int)maxAbsY + 4, (int)maxAbsZ + 4);
                MatchInsideCubes((int)maxAbsY + 4, (int)maxAbsZ + 4);
                for(Int64 i = 0; i < (Int64)maxAbsY + 4; i++)
                {
                    for(Int64 j = 0; j < (Int64)maxAbsZ + 4; j++)
                    {
                        if(Matrix2D[i,j] != 1)
                        {
                            maxThirdCoord = minThirdCoord = (-B * i - C * j + D) / A;
                            newThirdCoord = (-B * (i + 1) - C * j + D) / A;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            newThirdCoord = (-B * (i + 1) - C * (j + 1) + D) / A;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            newThirdCoord = (-B * i - C * (j + 1) + D) / A;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            for(Int64 p = (Int64)minThirdCoord; p <= (Int64)maxThirdCoord; p++)
                            {
                                Matrix.Matrix[p, i, j] = 2;
                            }
                        }
                    }
                }
            }
            if (side == 2)
            {
                Matrix2D = new short[(Int64)maxAbsX + 4, (Int64)maxAbsZ + 4];
                List<Vertex2D> vertexIn2DList = new List<Vertex2D>();
                foreach (Vertex3D tomp in faceVertex)
                {
                    Vertex2D temp2 = new Vertex2D(tomp.X, tomp.Z);
                    vertexIn2DList.Add(temp2);
                }
                for (int i = 0; i < vertexIn2DList.Count - 1; i++)
                {
                    this.MatchTwoPoints(vertexIn2DList[i], vertexIn2DList[i + 1]);
                }
                this.MatchTwoPoints(vertexIn2DList[0], vertexIn2DList[vertexIn2DList.Count - 1]);
                MatchOutsideCubes((int)maxAbsX + 4, (int)maxAbsZ + 4);
                MatchInsideCubes((int)maxAbsX + 4, (int)maxAbsZ + 4);
                for (Int64 i = 0; i < (Int64)maxAbsX + 4; i++)
                {
                    for (Int64 j = 0; j < (Int64)maxAbsZ + 4; j++)
                    {
                        if (Matrix2D[i, j] != 1)
                        {
                            maxThirdCoord = minThirdCoord = (-A * i - C * j + D) / B;
                            newThirdCoord = (-A * (i + 1) - C * j + D) / B;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            newThirdCoord = (-A * (i + 1) - C * (j + 1) + D) / B;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            newThirdCoord = (-A * i - C * (j + 1) + D) / B;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            for (Int64 p = (Int64)minThirdCoord; p <= (Int64)maxThirdCoord; p++)
                            {
                                Matrix.Matrix[i, p, j] = 2;
                            }
                        }
                    }
                }
            }
            if (side == 3)
            {
                Matrix2D = new short[(Int64)maxAbsX + 4, (Int64)maxAbsY + 4];
                List<Vertex2D> vertexIn2DList = new List<Vertex2D>();
                foreach (Vertex3D tomp in faceVertex)
                {
                    Vertex2D temp2 = new Vertex2D(tomp.X, tomp.Y);
                    vertexIn2DList.Add(temp2);
                }
                for (int i = 0; i < vertexIn2DList.Count - 1; i++)
                {
                    this.MatchTwoPoints(vertexIn2DList[i], vertexIn2DList[i + 1]);
                }
                this.MatchTwoPoints(vertexIn2DList[0], vertexIn2DList[vertexIn2DList.Count - 1]);
                MatchOutsideCubes((int)maxAbsX + 4, (int)maxAbsY + 4);
                MatchInsideCubes((int)maxAbsX + 4, (int)maxAbsY + 4);
                for (Int64 i = 0; i < (Int64)maxAbsX + 4; i++)
                {
                    for (Int64 j = 0; j < (Int64)maxAbsY + 4; j++)
                    {
                        if (Matrix2D[i, j] != 1)
                        {
                            maxThirdCoord = minThirdCoord = (-A * i - B * j + D) / C;
                            newThirdCoord = (-A * (i + 1) - B * j + D) / C;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            newThirdCoord = (-A * (i + 1) - B * (j + 1) + D) / C;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            newThirdCoord = (-A * i - B * (j + 1) + D) / C;
                            if (maxThirdCoord < newThirdCoord)
                            {
                                maxThirdCoord = newThirdCoord;
                            }
                            if (minThirdCoord > newThirdCoord)
                            {
                                minThirdCoord = newThirdCoord;
                            }
                            for (Int64 p = (Int64)minThirdCoord; p <= (Int64)maxThirdCoord; p++)
                            {
                                Matrix.Matrix[i, j, p] = 2;
                            }
                        }
                    }
                }
            }
        }

        private void MatchTwoPoints(Vertex2D firstVertex, Vertex2D secondVertex)
        {
            if ((int)firstVertex.X == (int)secondVertex.X)
            {
                if (firstVertex.Y > secondVertex.Y)
                {
                    for (int i = (int)secondVertex.Y; i < (int)(firstVertex.Y + 1); i++)
                    {
                        Matrix2D[(int)firstVertex.X, i] = 2;
                    }
                }
                else
                {
                    for (int i = (int)firstVertex.Y; i < (int)(secondVertex.Y + 1); i++)
                    {
                        Matrix2D[(int)firstVertex.X, i] = 2;
                    }
                }
            }
            if ((int)firstVertex.Y == (int)secondVertex.Y)
            {
                if (firstVertex.X > secondVertex.X)
                {
                    for (int i = (int)secondVertex.X; i < (int)(firstVertex.X + 1); i++)
                    {
                        Matrix2D[i, (int)firstVertex.Y] = 2;
                    }
                }
                else
                {
                    for (int i = (int)firstVertex.X; i < (int)(secondVertex.X + 1); i++)
                    {
                        Matrix2D[i, (int)firstVertex.Y] = 2;
                    }
                }
            }
            if ((int)firstVertex.Y != (int)secondVertex.Y && (int)firstVertex.X != (int)secondVertex.X)
            {
                double k, b;
                k = (secondVertex.Y - firstVertex.Y) / (secondVertex.X - firstVertex.X);
                b = (secondVertex.X * firstVertex.Y - firstVertex.X * secondVertex.Y) / (secondVertex.X - firstVertex.X);
                if (firstVertex.X < secondVertex.X)
                {
                    MatchLineBetweenTwoPoints(k, b, firstVertex.X, secondVertex.X, firstVertex.Y, secondVertex.Y);
                }
                else
                {
                    MatchLineBetweenTwoPoints(k, b, secondVertex.X, firstVertex.X, secondVertex.Y, firstVertex.Y);
                }
            }
        }

        public void MatchLineBetweenTwoPoints(double k, double b, double vertexFirstX, double vertexSecondX, double vertexFirstY, double vertexSecondY)
        {
            MatchLine(vertexFirstX, vertexFirstY, k * (int)(vertexFirstX + 1) + b);
            MatchLine(vertexSecondX, vertexSecondY, k * (int)(vertexSecondX) + b);
            for (int i = (int)(vertexFirstX + 1); i < (int)vertexSecondX; i++)
            {
                MatchLine(i, k * i + b, k * (i + 1) + b);
            }
        }

        public void MatchLine(double x, double startPoint, double endPoint)
        {
            if (startPoint < endPoint)
            {
                MatchCubes(x, startPoint, endPoint);
            }
            else
            {
                MatchCubes(x, endPoint, startPoint);
            }
        }

        public void MatchCubes(double x, double startPoint, double endPoint)
        {
            for (int j = (int)startPoint; j < (int)(endPoint + 1); j++)
            {
                Matrix2D[(int)x, j] = 2;
            }
        }

        public void Upload(int cubeTypeMark)
        {
            VerShowList = new List<Vertex3D>();
            int x = (int)maxAbsX + 4;
            int y = (int)maxAbsY + 4;
            int z = (int)maxAbsZ + 4;
            for (int k = 0; k < x; k++)
            {
                for (int l = 0; l < y; l++)
                {
                    for (int m = 0; m < z; m++)
                    {
                        if (Matrix.Matrix[k, l, m] == cubeTypeMark)
                        {
                            Vertex3D newVertex = new Vertex3D((k - 2 + minX) * BlockSize, (l - 2 + minY) * BlockSize, (m - 2 + minZ) * BlockSize);
                            VerShowList.Add(newVertex);
                        }
                    }
                }
            }
        }
    }
}