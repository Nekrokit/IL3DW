using System;
using System.IO;
using System.Linq;
using ObjLoader.Loader.Loaders;
using ITL_1_5_3_DLL;
using ObjParser;
using ObjParser.Types;

namespace Coating
{
    public class OverwriteObj:Obj
    {
        new private void processLine(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "v":
                        Vertex v = new Vertex();
                        v.LoadFromStringArray(parts);
                        VertexList.Add(v);
                        v.Index = VertexList.Count();
                        break;
                    case "f":
                        Face f = new Face();
                        f.LoadFromStringArray(parts);
                        f.UseMtl = UseMtl;
                        FaceList.Add(f);
                        break;
                    case "vt":
                        TextureVertex vt = new TextureVertex();
                        vt.LoadFromStringArray(parts);
                        TextureList.Add(vt);
                        vt.Index = TextureList.Count();
                        break;

                }
            }
        }
    }

    public class IL3DCDLL
    {
        public static string NewFileSource;
        public static string oldFileSource;
        public static float dx = 0;
        public static float dy = 0;
        public static float dz = 0;
        private int VertexInFileCount = 0;
        
        public IL3DCDLL()
        {
            
        }

        public bool TryTransform(string source)
        {
            try
            {
                Directory.SetCurrentDirectory(Path.GetDirectoryName(source));
                FileStream fileStream = new FileStream(source, FileMode.Open);
                OverwriteObj result = new OverwriteObj();
                result.LoadObj(fileStream);
                fileStream.Close();
                UInt32 zoom = (UInt32)(170 / FindMaxAbsCoordOfVert(result, 2));
                if (zoom == 0)
                {
                    zoom = 1;
                }
                float blockSize = (float)FindRecomendedBlockSize(FindMaxAbsCoordOfVert(result, 3), result);
                Model startModel = new Model(result, blockSize);
                startModel.Transform();
                ShowList insideCubes = new ShowList(startModel, 3);
                ShowList limitCubes = new ShowList(startModel, 2);
                OverwriteObj obj = new OverwriteObj();
                WritePolygons(obj, insideCubes, blockSize);
                WritePolygons(obj, limitCubes, blockSize);
                string a = "First upload result";
                var fileStreamC = new FileStream(source.Substring(0,source.Length - 4) + "_Result.obj", FileMode.Create);
                fileStreamC.Close();
                obj.WriteObjFile(source.Substring(0, source.Length - 4) + "_Result.obj", a.Split(' ')); 

                return true;
            }
            catch(Exception a)
            {
                return false;
            }
        }

        public void WritePolygons(Obj fileToWrite, ShowList cubesSource, float blockSize)
        {
            int tempForEndIndex = VertexInFileCount + cubesSource.UpLoad.Count;
            for (int i = VertexInFileCount; i < tempForEndIndex; i++)
            {
                Vertex tmpV = new Vertex();
                tmpV.Index = i * 8 + 1;
                tmpV.X = cubesSource.UpLoad[i - VertexInFileCount].X;
                tmpV.Y = cubesSource.UpLoad[i - VertexInFileCount].Y;
                tmpV.Z = cubesSource.UpLoad[i - VertexInFileCount].Z;
                fileToWrite.VertexList.Add(tmpV);

                tmpV = new Vertex();
                tmpV.Index = i * 8 + 2;
                tmpV.X = cubesSource.UpLoad[i - VertexInFileCount].X + blockSize;
                tmpV.Y = cubesSource.UpLoad[i - VertexInFileCount].Y;
                tmpV.Z = cubesSource.UpLoad[i - VertexInFileCount].Z;
                fileToWrite.VertexList.Add(tmpV);

                tmpV = new Vertex();
                tmpV.Index = i * 8 + 3;
                tmpV.X = cubesSource.UpLoad[i - VertexInFileCount].X;
                tmpV.Y = cubesSource.UpLoad[i - VertexInFileCount].Y + blockSize;
                tmpV.Z = cubesSource.UpLoad[i - VertexInFileCount].Z;
                fileToWrite.VertexList.Add(tmpV);

                tmpV = new Vertex();
                tmpV.Index = i * 8 + 4;
                tmpV.X = cubesSource.UpLoad[i - VertexInFileCount].X;
                tmpV.Y = cubesSource.UpLoad[i - VertexInFileCount].Y;
                tmpV.Z = cubesSource.UpLoad[i - VertexInFileCount].Z + blockSize;
                fileToWrite.VertexList.Add(tmpV);

                tmpV = new Vertex();
                tmpV.Index = i * 8 + 5;
                tmpV.X = cubesSource.UpLoad[i - VertexInFileCount].X + blockSize;
                tmpV.Y = cubesSource.UpLoad[i - VertexInFileCount].Y + blockSize;
                tmpV.Z = cubesSource.UpLoad[i - VertexInFileCount].Z;
                fileToWrite.VertexList.Add(tmpV);

                tmpV = new Vertex();
                tmpV.Index = i * 8 + 6;
                tmpV.X = cubesSource.UpLoad[i - VertexInFileCount].X + blockSize;
                tmpV.Y = cubesSource.UpLoad[i - VertexInFileCount].Y + blockSize;
                tmpV.Z = cubesSource.UpLoad[i - VertexInFileCount].Z + blockSize;
                fileToWrite.VertexList.Add(tmpV);

                tmpV = new Vertex();
                tmpV.Index = i * 8 + 7;
                tmpV.X = cubesSource.UpLoad[i - VertexInFileCount].X;
                tmpV.Y = cubesSource.UpLoad[i - VertexInFileCount].Y + blockSize;
                tmpV.Z = cubesSource.UpLoad[i - VertexInFileCount].Z + blockSize;
                fileToWrite.VertexList.Add(tmpV);

                tmpV = new Vertex();
                tmpV.Index = i * 8 + 8;
                tmpV.X = cubesSource.UpLoad[i - VertexInFileCount].X + blockSize;
                tmpV.Y = cubesSource.UpLoad[i - VertexInFileCount].Y;
                tmpV.Z = cubesSource.UpLoad[i - VertexInFileCount].Z + blockSize;
                fileToWrite.VertexList.Add(tmpV);

                TextureVertex tmpT = new TextureVertex();
                tmpT.Index = i + 1;
                tmpT.X = tmpT.Y = 1;
                fileToWrite.TextureList.Add(tmpT);

                Face tmpF = new Face();
                int[] A1 = { i * 8 + 1, i * 8 + 2, i * 8 + 5, i * 8 + 3 };
                tmpF.VertexIndexList = A1;
                int[] B = { i };
                tmpF.TextureVertexIndexList = B;
                fileToWrite.FaceList.Add(tmpF);

                tmpF = new Face();
                int[] A2 = { i * 8 + 4, i * 8 + 7, i * 8 + 6, i * 8 + 8 };
                tmpF.VertexIndexList = A2;
                tmpF.TextureVertexIndexList = B;
                fileToWrite.FaceList.Add(tmpF);

                tmpF = new Face();
                int[] A3 = { i * 8 + 1, i * 8 + 3, i * 8 + 7, i * 8 + 4 };
                tmpF.VertexIndexList = A3;
                tmpF.TextureVertexIndexList = B;
                fileToWrite.FaceList.Add(tmpF);

                tmpF = new Face();
                int[] A4 = { i * 8 + 2, i * 8 + 5, i * 8 + 6, i * 8 + 8 };
                tmpF.VertexIndexList = A4;
                tmpF.TextureVertexIndexList = B;
                fileToWrite.FaceList.Add(tmpF);

                tmpF = new Face();
                int[] A5 = { i * 8 + 1, i * 8 + 2, i * 8 + 8, i * 8 + 4 };
                tmpF.VertexIndexList = A5;
                tmpF.TextureVertexIndexList = B;
                fileToWrite.FaceList.Add(tmpF);

                tmpF = new Face();
                int[] A6 = { i * 8 + 3, i * 8 + 5, i * 8 + 6, i * 8 + 7 };
                tmpF.VertexIndexList = A6;
                tmpF.TextureVertexIndexList = B;
                fileToWrite.FaceList.Add(tmpF);
            }
            VertexInFileCount += cubesSource.UpLoad.Count;
        }

        public static UInt32 FindMaxAbsCoordOfVert(OverwriteObj newModel, UInt16 coordParametr)
        {
            double maxX, maxY, maxZ;
            maxX = maxY = maxZ = 0;
            foreach (var vertex in newModel.VertexList)
            {
                if (maxX < Math.Abs(vertex.X))
                {
                    maxX = Math.Abs(vertex.X);
                }
                if (maxY < Math.Abs(vertex.Y))
                {
                    maxY = Math.Abs(vertex.Y);
                }
                if (maxZ < Math.Abs(vertex.Z))
                {
                    maxZ = Math.Abs(vertex.Z);
                }
            }
            if (coordParametr == 3)
            {
                return (UInt32)Convert.ToInt32(Math.Max(maxX, Math.Max(maxY, maxZ)) + 1);
            }
            else
            {
                return (UInt32)Convert.ToInt32(Math.Max(maxX, maxY) + 1);
            }
        }

        public static UInt32 FindMaxAbsCoordOfVert(LoadResult newModel, UInt16 coordParametr)
        {
            double maxX, maxY, maxZ;
            maxX = maxY = maxZ = 0;
            foreach (var vertex in newModel.Vertices)
            {
                if (maxX < Math.Abs(vertex.X))
                {
                    maxX = Math.Abs(vertex.X);
                }
                if (maxY < Math.Abs(vertex.Y))
                {
                    maxY = Math.Abs(vertex.Y);
                }
                if (maxZ < Math.Abs(vertex.Z))
                {
                    maxZ = Math.Abs(vertex.Z);
                }
            }
            if (coordParametr == 3)
            {
                return (UInt32)Convert.ToInt32(Math.Max(maxX, Math.Max(maxY, maxZ)) + 1);
            }
            else
            {
                return (UInt32)Convert.ToInt32(Math.Max(maxX, maxY) + 1);
            }
        }

        public static double FindRecomendedBlockSize(UInt32 startBlockSize, OverwriteObj newModel)
        {
            UInt64 startTime, endTime;
            double checkBlockSize = startBlockSize;
            ShowList insideCubes;
            ShowList limitCubes;
            do
            {
                checkBlockSize /= 2;
                startTime = (UInt64)TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")).TotalSeconds;
                Model temp = new Model(newModel, checkBlockSize);
                temp.Transform();
                insideCubes = new ShowList(temp, 3);
                limitCubes = new ShowList(temp, 2);
                endTime = (UInt64)TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")).TotalSeconds;
            } while (StopPoint((endTime - startTime), (UInt64)(insideCubes.UpLoad.Count + limitCubes.UpLoad.Count)));
            return ((double)Convert.ToInt64(checkBlockSize * 100)) / 100;
        }

        public static double FindRecomendedBlockSize(UInt32 startBlockSize, LoadResult newModel)
        {
            UInt64 startTime, endTime;
            double checkBlockSize = startBlockSize;
            ShowList insideCubes;
            ShowList limitCubes;
            do
            {
                checkBlockSize /= 2;
                startTime = (UInt64)TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")).TotalSeconds;
                Model temp = new Model(newModel, checkBlockSize);
                temp.Transform();
                insideCubes = new ShowList(temp, 3);
                limitCubes = new ShowList(temp, 2);
                endTime = (UInt64)TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")).TotalSeconds;
            } while (StopPoint((endTime - startTime), (UInt64)(insideCubes.UpLoad.Count + limitCubes.UpLoad.Count)));
            return ((double)Convert.ToInt64(checkBlockSize * 100)) / 100;
        }

        public static bool StopPoint(UInt64 time, UInt64 count)
        {
            if (time > 60 || count > 10000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}