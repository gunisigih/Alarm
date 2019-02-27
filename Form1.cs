using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Alarm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string _alarmZamani;

        private void Form1_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Text = DateTime.Now.ToShortTimeString();
        }

        private void tmrSaat_Tick(object sender, EventArgs e)
        {
            //tmrSaat, her saniye sistem saatini alıp, form' un başlığında gösterecek
            this.Text = DateTime.Now.ToLongTimeString();
        }

        private void tmrZamanKontrolu_Tick(object sender, EventArgs e)
        {//eğer alarmZamani değişkeni ile sistem saatinin değeri aynı ise, alarm vakti geldi demektir.
            //tmrAlarmIsleri isimli timer' ı çalıştırdık, tmrZamanKontrolu isimli timer' ı durdurduk
            if (_alarmZamani == DateTime.Now.ToShortTimeString())

            {
                tmrAlarmIsleri.Start();
                tmrZamanKontrolu.Stop();
            }
        }

        private void btnAlarmKur_Click(object sender, EventArgs e)
        {
            try
            {
                //btnAlarmKur butonu tıklandığında, alarmZamani isimli değişkene, txtSaat' in text' ini değer olarak atadık
                _alarmZamani = maskedTextBox1.Text;
                tmrZamanKontrolu.Start();
                MessageBox.Show(string.Format(@"Alarm {0} olarak kuruldu", _alarmZamani ));
                this.Opacity = 0;
                this.ShowInTaskbar = false;
            }
            catch
            {
                MessageBox.Show(@"Uygun bir saat giriniz");
            }
        }

        private void tmrAlarmIsleri_Tick(object sender, EventArgs e)
        {
            //Windows Ding.wav dosyasını bin\Debug klasörüne ekledik
            SoundPlayer sp = new SoundPlayer();
            //sp.SoundLocation = "C:\\Windows Ding.wav";

            sp.SoundLocation = string.Format(@"{0}\Windows Ding.wav", Application.StartupPath);
            sp.Play();

            //formu görünür yaptık
            this.Opacity = 1;
            //görev çubuğunda gösterdik
            this.ShowInTaskbar = true;
            //formu öne taşıdık
            this.BringToFront();

            //formu her zaman en önde tutar.
            this.TopMost = true;


            //aşağıdaki işlemler 400 kere tekrarlanacak
            for (int i = 0; i < 400; i++)
            {
                pictureBox1.Left += 2;
                pictureBox1.Left -= 2;
                pictureBox1.Top += 2;
                pictureBox1.Top -= 2;
            }

            if (txtAlarmMetni.BackColor == Color.White)
            {
                txtAlarmMetni.BackColor = Color.Red;
                txtAlarmMetni.ForeColor = Color.White;
            }
            else
            {
                txtAlarmMetni.BackColor = Color.White;
                txtAlarmMetni.ForeColor = Color.Red;
            }
        }

        private void btnAlarmDurdur_Click(object sender, EventArgs e)
        {
            tmrAlarmIsleri.Stop();
        }

        private void acToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 1;
            this.ShowInTaskbar = true;
            //Formu ekranın ortasına getirir.
            this.WindowState = FormWindowState.Normal;
        }

        private void cikisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MsjBox' ta YES NO butonları çıkar, eğer YES' e basılırsa DialogResult.YES demektir bu.
            if (MessageBox.Show(@"Çıkmak istediğinizden emin misiniz?", @"ÇIKIŞ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //
        }
    }
}
