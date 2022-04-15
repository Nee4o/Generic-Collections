using System;

namespace LR2.Controllers
{
    public class Rectangle
    {
        private int _length;
        private int _width;

        public Rectangle(int length, int width)
        {
            _length = length;
            _width = width;
        }

        public Rectangle()
        {
            _length = 0;
            _width = 0;
        }
    }
}
