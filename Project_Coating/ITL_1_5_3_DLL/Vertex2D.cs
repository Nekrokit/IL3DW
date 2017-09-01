namespace ITL_1_5_3_DLL
{
    class Vertex2D
    {
        private double x;
        private double y;

        public Vertex2D(double _x, double _y)
        {
            x = _x;
            y = _y;
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
    }
}
