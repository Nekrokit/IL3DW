using System.Collections.Generic;

namespace ITL_1_5_3_DLL
{
    public class ShowList
    {
        public List<Vertex3D> UpLoad;
        
        public ShowList(Model source, int cubeTypeMark)
        {
            UpLoad = new List<Vertex3D>();
            source.Upload(cubeTypeMark);
            UpLoad = source.VerShowList;
        }

        public ShowList()
        {

        }
    }
}
