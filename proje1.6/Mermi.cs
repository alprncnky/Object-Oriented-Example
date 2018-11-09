using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1._6
{
    class Mermi
    {
        Image resim2;
        public int mermi_x;
        public int mermi_y = 690;

        public Mermi(int temp2)
        {
            resim2 = Image.FromFile("mermi.png");
            mermi_x = temp2+19;
        }

        public void YukariGit()
        {
            mermi_y -= 10;
        }
        public void mermiCiz(Graphics g)
        {
            g.DrawImage(resim2, mermi_x, mermi_y, 10, 17);
        }
    }
}
