using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bescentovoe_shlifovanie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void count_button_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();

            EnteredData.k = Double.Parse(k_text.Text);

            bool filled = f1.Controls.OfType<TextBox>().Any(textBox => textBox.TextLength == 0);
            //Proverka
            if (filled && cilinder_radioButton.Checked && (EnteredData.k >= 0.5 && EnteredData.k <= 0.7))
            {
                EnteredData.Cilinder = true;
                bool rightType = f1.Controls.OfType<TextBox>().Any(textBox => textBox.Text != double.TryParse(textBox.Text, out double x).ToString()); //проверка на заполнение всех полей типом данных double (работает почему-то наоборот, хз) зато работает вах
                if (rightType)
                {
                    //иницализация всех параметров при помощи textbox
                    EnteredData.Dv = Double.Parse(Dv_text.Text);
                    EnteredData.Dsh = Double.Parse(Dsh_text.Text);
                    EnteredData.d = Double.Parse(d_text.Text);
                    EnteredData.d3 = Double.Parse(d3_text.Text);
                    EnteredData.t = Double.Parse(t_text.Text);
                    EnteredData.f1 = Double.Parse(f1_text.Text);
                    EnteredData.f2 = Double.Parse(f2_text.Text);
                    EnteredData.z01 = Double.Parse(z01_text.Text);
                    EnteredData.z02 = Double.Parse(z02_text.Text);
                    EnteredData.z03 = Double.Parse(z03_text.Text);
                    EnteredData.z04 = Double.Parse(z04_text.Text);
                    EnteredData.d01 = Double.Parse(d01_text.Text);
                    EnteredData.d02 = Double.Parse(d02_text.Text);
                    EnteredData.d03 = Double.Parse(d03_text.Text);
                    EnteredData.d04 = Double.Parse(d04_text.Text);
                    EnteredData.l = Double.Parse(l_text.Text);
                    EnteredData.R3 = Double.Parse(R3_text.Text);
                    EnteredData.Nv = Double.Parse(nv_text.Text);
                    EnteredData.z = Double.Parse(z_text.Text);
                    EnteredData.Td = Double.Parse(Td_text.Text);
                    EnteredData.Ra = Double.Parse(Ra_text.Text);
                    EnteredData.delta_KR = Double.Parse(delta_kr_text.Text);
                    EnteredData.delta_OGR = Double.Parse(delta_ogr_text.Text);
                    EnteredData.delta_V = Double.Parse(delta_v_text.Text);
                    EnteredData.delta_rb = Double.Parse(delta_rb_text.Text);
                    EnteredData.delta_SF = Double.Parse(delta_sf_text.Text);
                    EnteredData.delta_prod = Double.Parse(delta_prod_text.Text);
                    EnteredData.Vkr = Double.Parse(Vkr_text.Text);
                    EnteredData.Vg = Double.Parse(Vg_text.Text);
                    EnteredData.Bkr = Double.Parse(Bkr_text.Text);

                    Form2 f2 = new Form2();
                    f2.Show();
                }
                else
                {
                    MessageBox.Show("Поля заполнены неверно");
                }
            }
            else if (filled && sfera_radioButton.Checked && (EnteredData.k >= 0.5 && EnteredData.k <= 0.7))
            {
                EnteredData.Sfera = true;
                bool rightType = f1.Controls.OfType<TextBox>().Any(textBox => textBox.Text != double.TryParse(textBox.Text, out double x).ToString()); //проверка на заполнение всех полей типом данных double (работает почему-то наоборот, хз) зато работает вах
                if (rightType)
                {
                    //иницализация всех параметров при помощи textbox
                    EnteredData.Dv = Double.Parse(Dv_text.Text);
                    EnteredData.Dsh = Double.Parse(Dsh_text.Text);
                    EnteredData.d = Double.Parse(d_text.Text);
                    EnteredData.d3 = Double.Parse(d3_text.Text);
                    EnteredData.t = Double.Parse(t_text.Text);
                    EnteredData.f1 = Double.Parse(f1_text.Text);
                    EnteredData.f2 = Double.Parse(f2_text.Text);
                    EnteredData.z01 = Double.Parse(z01_text.Text);
                    EnteredData.z02 = Double.Parse(z02_text.Text);
                    EnteredData.z03 = Double.Parse(z03_text.Text);
                    EnteredData.z04 = Double.Parse(z04_text.Text);
                    EnteredData.d01 = Double.Parse(d01_text.Text);
                    EnteredData.d02 = Double.Parse(d02_text.Text);
                    EnteredData.d03 = Double.Parse(d03_text.Text);
                    EnteredData.d04 = Double.Parse(d04_text.Text);
                    EnteredData.l = Double.Parse(l_text.Text);
                    EnteredData.R3 = Double.Parse(R3_text.Text);
                    EnteredData.Nv = Double.Parse(nv_text.Text);
                    EnteredData.z = Double.Parse(z_text.Text);
                    EnteredData.Td = Double.Parse(Td_text.Text);
                    EnteredData.Ra = Double.Parse(Ra_text.Text);
                    EnteredData.delta_KR = Double.Parse(delta_kr_text.Text);
                    EnteredData.delta_OGR = Double.Parse(delta_ogr_text.Text);
                    EnteredData.delta_V = Double.Parse(delta_v_text.Text);
                    EnteredData.delta_rb = Double.Parse(delta_rb_text.Text);
                    EnteredData.delta_SF = Double.Parse(delta_sf_text.Text);
                    EnteredData.delta_prod = Double.Parse(delta_prod_text.Text);
                    EnteredData.Vkr = Double.Parse(Vkr_text.Text);
                    EnteredData.Vg = Double.Parse(Vg_text.Text);
                    EnteredData.Bkr = Double.Parse(Bkr_text.Text);

                    Form2 f2 = new Form2();
                    f2.Show();
                }
                else
                {
                    MessageBox.Show("Поля заполнены неверно");
                }
            }
            else
            {
                MessageBox.Show("Поля не заполнены или заполнены неверно. Проверьте заполнение");
            }
        }

        private void how_2_use_linklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //вывод readme
            string commandText = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Readme.txt");
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = commandText;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //форма вывода предупреждения о закрытии окна ввода данных, чтобы случайно его не закрыть
            DialogResult result = MessageBox.Show("Вы точно хотите закрыть программу?",
              "Внимание!", //Сообщение пользователю
              MessageBoxButtons.YesNo, //вывод кнопок диалога Да/Нет
              MessageBoxIcon.Question, //иконка вопроса
              MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
                Application.Exit();
        }
    }
}
