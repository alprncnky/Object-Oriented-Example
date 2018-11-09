using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1._6
{
    class Homer 
    {
        Image resim;
        public int x;
        public int y;

        public Homer()
        {
            resim = Image.FromFile("plane.png");
            Random rnd = new Random();
            x = rnd.Next(0,1300);
        }
        public void YukariGit()
        {
            y -= 10;
        }
        public void AsagiGit()
        {
            y += 10;
        }


        public void Ciz(Graphics g)
        {
            g.DrawImage(resim, x, y, 80, 80);
        }
    }
}
