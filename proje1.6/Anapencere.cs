/****************************************************************************
**                        SAKARYA UNIVERSITESI
**                BILGISAYAR VE BILISIM BILIMLERI FAKULTESI
**                    BILGISAYAR MUHENDISLIGI BOLUMU 
**                      NESNEYE DAYALI PROGRAMLAMA DERSI 
**
**               OGRENCI NUMARASI.: b161210035
**               OGRENCI ADI......: Alperen Çinkaya
**               DERS GRUBU.......: A              
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace proje1._6
{
    class Anapencere:Form
    {
        Image resim3;
        public int anti_x = 675;
        public int sayac = 0;                                   // ucak nesnesi sayisini tutuyor.
        public int sayac2 = 0;                                  // mermi nesnesi sayisini tutuyor.
        public int sayac3 = 0;                                  // ucak nesnesi icin nesnenin olusabilcek en fazla kac tane oldugunu belirlemek icin.
        public int sayac4 = 0;                                  //mermi nesnesi icin nesnenin olusabilcek en fazla kac tane oldugunu belirlemek icin.
        SoundPlayer player = new SoundPlayer();                 //mermi atis sesi.
        SoundPlayer player2 = new SoundPlayer();                //"game over" sesi.
        public Homer[] h = new Homer[7];                        //ucak nesne dizisi.
        public Mermi[] m = new Mermi[71];                       //mermi nesne dizisi.
        Timer zamanlayici;                                      //ucak ve merminin ilerleme hizi icin  -timer.
        Timer zamanlayici2;                                     //yeni ucak olusmasi icin gereken sure  -timer.
        public Anapencere(int genislik, int yukseklik)
        {
            Image myimage = new Bitmap("arkaplan.png");
            this.BackgroundImage = myimage;
            Width = genislik;
            Height = yukseklik;
            resim3 = Image.FromFile("ucaksavar.png");
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\sound3.wav";
            player2.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\fail.wav";
            Paint += Anapencere_Paint;
            KeyDown += Anapencere_KeyDown;
            DoubleBuffered = true;
            zamanlayici = new Timer();
            zamanlayici.Interval = 100;
            zamanlayici.Tick += Zamanlayici_Tick;
            zamanlayici2 = new Timer();
            zamanlayici2.Interval = 1400;
            zamanlayici2.Tick += Zamanlayici2_Tick;
        }
        public void SagaGit()                                  //ucaksavari saga oteliyor.
        {                                                      
            anti_x += 10;
            if (anti_x > 1330)
                anti_x -= 85;
        }
        public void SolaGit()                                  //ucaksavari sola oteliyor.
        {
            anti_x -= 10;
            if (anti_x < 10)
                anti_x += 85;
        }

        private void Zamanlayici2_Tick(object sender, EventArgs e)             //belli aralıklarla yeni ucak olusturuyor.
        {                                                                      //6 tane nesne olusturuyo sonra tekrar basa sararak aynı nesneleri sirayla olusturuyor.
            if (sayac == 6)
            {
                h[sayac3] = new Homer();
                sayac3++;
                if(sayac3==6)
                {
                    sayac3 = 0;
                }
            }

            else
            {
                h[sayac] = new Homer();
                sayac++;
            }

        }

        private void Zamanlayici_Tick(object sender, EventArgs e)              //ucak ve mermiyi ilerletiyor aynı zamanda carpısmakontrolu yapiyor.
        {
           
            for(int i=0;i<sayac;i++)
            {
                h[i].AsagiGit();
                if (h[i].y > 675 && h[i].y < 700)
                {
                    zamanlayici.Stop();
                    zamanlayici2.Stop();
                    player2.Play();
                    MessageBox.Show("Yenildin ha ha ( Devam etmek için ENTER tuşuna basın..)");
                    sayac = 0;
                    sayac2 = 0;
                    sayac3 = 0;
                    zamanlayici2.Start();
                    zamanlayici.Start();

                }
                for(int v=0;v<sayac2;v++)
                {
                    if(m[v].mermi_x > h[i].x && m[v].mermi_x < h[i].x + 80 && m[v].mermi_y < h[i].y + 80 && m[v].mermi_y > h[i].y + 55)
                    {
                        h[i].y = 780;
                        m[v].mermi_y = -10;
                    }
                }
            }
            for (int d = 0; d < sayac2; d++)
            {
                m[d].YukariGit();
            }
            
            Invalidate();
        }

        private void Anapencere_KeyDown(object sender, KeyEventArgs e)         //Basilan tuslara gore islem yapma
        {
            switch(e.KeyCode)
            {
                case Keys.Right:
                    SagaGit();
                    break;
                case Keys.Left:
                    SolaGit();
                    break;
                case Keys.Enter:
                    {
                        zamanlayici2.Start();
                        zamanlayici.Start();
                    }break;

                case Keys.Space:
                    {
                        if (sayac2 == 70)
                        {
                            m[sayac4] = new Mermi(anti_x);
                            sayac4++;
                            if (sayac4 == 70)
                            {
                                sayac4 = 0;
                            }
                        }

                        else
                        {
                            m[sayac2] = new Mermi(anti_x);
                            sayac2++;
                        }
                        player.Play();

                    }
                    break;
                default:
                    break;
            }
            Invalidate();
        }

        private void Anapencere_Paint(object sender, PaintEventArgs e)       //ucak ve mermileri cizdirme
        {
            e.Graphics.DrawImage(resim3, anti_x, 710, 50, 50);
           
            for (int i = 0; i < sayac; i++)
            {
                h[i].Ciz(e.Graphics);        
            }

            for (int s = 0; s < sayac2; s++)
            {
                m[s].mermiCiz(e.Graphics);
            }

        }
    }
}
