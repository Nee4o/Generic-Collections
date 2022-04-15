namespace LR2.Controllers
{
    internal class Rectanlge
    {
        private float _length;
        private float _width;
        public float Square;

        public Rectanlge(float length, float width)
        {
            _length = length;
            _width = width;
            Square = _length * _width;
        }

        public Rectanlge()
        {
            _length = 0;
            _width = 0;
        }
    }
}