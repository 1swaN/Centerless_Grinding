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
            Form1 f1 = new Form1(); //объявление формы 1

            EnteredData.k = Double.Parse(k_text.Text); //коэффициент уменьшения

            bool filled = f1.Controls.OfType<TextBox>().Any(textBox => textBox.TextLength == 0); //проверка на простое заполнение всех полей
           
            if (filled && cilinder_radioButton.Checked && (EnteredData.k >= 0.5 && EnteredData.k <= 0.7)) //условие выбора типа расчета (заполены все поля && выбрана цилиндрическая деталь && k соответствует заданным рамкам)
            {
                EnteredData.Cilinder = true;
                bool rightType = f1.Controls.OfType<TextBox>().Any(textBox => textBox.Text != double.TryParse(textBox.Text, out double x).ToString()); //проверка на заполнение всех полей типом данных double
                if (rightType)
                {
                    //иницализация всех параметров при помощи textbox
                    EnteredData.Dv = Double.Parse(Dv_text.Text); //Диаметр ведущего круга
                    EnteredData.Dsh = Double.Parse(Dsh_text.Text); //Диаметр шлифовального круга
                    EnteredData.d = Double.Parse(d_text.Text); //диаметр обработанной детали
                    EnteredData.d3 = Double.Parse(d3_text.Text); //диаметр заготовки
                    EnteredData.t = Double.Parse(t_text.Text); //глубина резания
                    EnteredData.f1 = Double.Parse(f1_text.Text); //коэффициент трения заготовки с опорным ножом
                    EnteredData.f2 = Double.Parse(f2_text.Text); //коэффициент трения заготовки с ведущим кругом
                    EnteredData.z01 = Double.Parse(z01_text.Text); //количество абразивных зерент на едиинице
                    EnteredData.z02 = Double.Parse(z02_text.Text); //количество абразивных зерент на едиинице
                    EnteredData.z03 = Double.Parse(z03_text.Text); //количество абразивных зерент на едиинице
                    EnteredData.z04 = Double.Parse(z04_text.Text); //количество абразивных зерент на едиинице
                    EnteredData.d01 = Double.Parse(d01_text.Text); //диаметр зерен шлифовального круга
                    EnteredData.d02 = Double.Parse(d02_text.Text); //диаметр зерен шлифовального круга
                    EnteredData.d03 = Double.Parse(d03_text.Text); //диаметр зерен шлифовального круга
                    EnteredData.d04 = Double.Parse(d04_text.Text); //диаметр зерен шлифовального круга
                    EnteredData.l = Double.Parse(l_text.Text); 
                    EnteredData.R3 = Double.Parse(R3_text.Text); //радиус округления вершин абразивных зерен шлифовального круга
                    EnteredData.Nv = Double.Parse(nv_text.Text); //частота вращения ведущего круга
                    EnteredData.z = Double.Parse(z_text.Text); //величина припуска на сторону
                    EnteredData.Td = Double.Parse(Td_text.Text); //допуск на размер детали
                    EnteredData.Ra = Double.Parse(Ra_text.Text); //допускаемый параметр шероховатости
                    EnteredData.delta_KR = Double.Parse(delta_kr_text.Text); //допускаемое значение круглости
                    EnteredData.delta_OGR = Double.Parse(delta_ogr_text.Text); //допускаемое значение огранки
                    EnteredData.delta_V = Double.Parse(delta_v_text.Text); //допускаемое значение волнистости
                    EnteredData.delta_rb = Double.Parse(delta_rb_text.Text); //допускаемая величина радиального биения ведущего круга
                    EnteredData.delta_SF = Double.Parse(delta_sf_text.Text); //допускаемое значение сферичности
                    EnteredData.delta_prod = Double.Parse(delta_prod_text.Text); //
                    EnteredData.Vkr = Double.Parse(Vkr_text.Text); //скорость вращения шлифовального круга
                    EnteredData.Vg = Double.Parse(Vg_text.Text); //скорость ведущего круга
                    EnteredData.Bkr = Double.Parse(Bkr_text.Text); //ширина круга

                    Form2 f2 = new Form2(); //объявление формы 2
                    f2.Show(); //открытие формы 2
                }
                else
                {
                    MessageBox.Show("Поля заполнены неверно"); //сообщение об ошибке при неверном заполнении полей
                }
            }
            else if (filled && sfera_radioButton.Checked && (EnteredData.k >= 0.5 && EnteredData.k <= 0.7)) //условие выбора типа расчета (заполены все поля && выбрана сферическая деталь && k соответствует заданным рамкам)
            {
                EnteredData.Sfera = true;
                bool rightType = f1.Controls.OfType<TextBox>().Any(textBox => textBox.Text != double.TryParse(textBox.Text, out double x).ToString()); //проверка на заполнение всех полей типом данных double 
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
                    MessageBox.Show("Поля заполнены неверно"); //сообщение об ошибке при неверном заполнении полей
                }
            }
            else
            {
                MessageBox.Show("Поля не заполнены или заполнены неверно. Проверьте заполнение"); //неверное заполнение или пустота полей
            }
        }

        private void how_2_use_linklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)  //вывод readme
        {
            string commandText = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Readme.txt");
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = commandText;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //кнопка "Закрыть"
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

        private void Exit_button_Click(object sender, EventArgs e) //метод кнопки выхода
        {
                Application.Exit(); //выход из приложения
        }
    }
}   
