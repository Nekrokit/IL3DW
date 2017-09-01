namespace ITL_1_5_3_DLL
{
    public class Vertex3D
    {
        private double x;
        private double y;
        private double z;

        public Vertex3D(double _x, double _y, double _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Z
        {
            get { return z; }
            set { z = value; }
        }
    }
}