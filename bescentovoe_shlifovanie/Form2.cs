using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bescentovoe_shlifovanie
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Counting(); //перенес сюда, чтобы расчеты открывались мгновенно при запуске Form 2. Кнопка пуска более не целесообразна
        }
       //проверка
        public void Counting()
        {
            //перевод мкм в мм
            EnteredData.R3 *= 0.001;
            EnteredData.d01 *= 0.001;
            EnteredData.d02 *= 0.001;
            EnteredData.d03 *= 0.001;
            EnteredData.d04 *= 0.001;
            EnteredData.Ra *= 0.001;

            //Расчеты
            output_text.Text += $"Присваиваем дробной части отношения b/c величину {EnteredData.b_c}" + Environment.NewLine;

            EnteredData.Dv += 0.1;
            output_text.Text += $"Задаем рациональное значение диаметра ведущего круга для правки в размере {EnteredData.Dv}" + Environment.NewLine;

            EnteredData.h = Math.Round(0.5 * EnteredData.k * (EnteredData.Dv + EnteredData.d) * Math.Sin(Math.Atan(EnteredData.f1)), MidpointRounding.AwayFromZero);
            output_text.Text += $"Величина превышения центра заготовки равна {EnteredData.h}" + Environment.NewLine;

            EnteredData.Alpha = Math.Round(EnteredData.k1*Math.Atan(EnteredData.f1 / EnteredData.f2), 2); //округляем угол скоса до сотых
            output_text.Text += $"Величина угла скоса опорного ножа равна {EnteredData.Alpha}" + Environment.NewLine;

            EnteredData.Svr = EnteredData.t / Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
            output_text.Text += $"Рациональное значение врезной подачи равно {EnteredData.Svr}" + Environment.NewLine;

            //рациональное значение врезной подачи должно перебираться от расчета к наиболее близкому меньшему значению по паспорту станка (станок?? / значение??)
            // НЕ ЗАБЫТЬ СДЕЛАТЬ ПЕРЕБОРКУ

            EnteredData.h1 = EnteredData.h - ((EnteredData.d3 - EnteredData.d) / 2) * (1 - Math.Sin(EnteredData.Alpha));
            

            EnteredData.Epsilon = 2 * (Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h1, 2) * (1 / Math.Pow((EnteredData.d + EnteredData.Dv), 2))) - Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h, 2) * (Math.Pow(EnteredData.d3, 2) / Math.Pow((EnteredData.d3 + EnteredData.Dv), 2))));
            // пока условие истинно - цикл будет обрабатывать блок кода do
            do
            {
                EnteredData.Alpha *= 0.9;

                EnteredData.h1 = EnteredData.h - ((EnteredData.d3 - EnteredData.d) / 2) * (1 - Math.Sin(EnteredData.Alpha));

                EnteredData.Epsilon = 2 * (Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h1, 2) * (1 / Math.Pow((EnteredData.d + EnteredData.Dv), 2))) - Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h, 2) * (Math.Pow(EnteredData.d3, 2) / Math.Pow((EnteredData.d3 + EnteredData.Dv), 2))));

            } while (EnteredData.Epsilon <= 0.4 * EnteredData.Td);

            output_text.Text += $"Конечная величина превышения оси детали над плоскостью расположения осей абразивных кругов после окончания обработки равна {EnteredData.h1}" + Environment.NewLine;
            output_text.Text += $"Конечная погрешность размера по диаметру детали, возникающая из-за особенностей схемы базирования (с учетом сравнения погрешности базирования величиной допуска на диаметр и корректировки величины угла скоса опорного ножа) равна {EnteredData.Epsilon}" + Environment.NewLine;

            if (EnteredData.Cilinder)
            {
                EnteredData.n = Math.Round((EnteredData.z / EnteredData.Svr), MidpointRounding.AwayFromZero);
                output_text.Text += $"Число оборотов заготовки до формирования суммарной погрешности формы: {EnteredData.n}" + Environment.NewLine;

                for (int i = 1; i < EnteredData.n; i++)
                {
                    EnteredData.delta_E += (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
                }
                output_text.Text += $"Суммарная погрешность формы обработанной детали (отклонение круглости) равна {EnteredData.delta_E}" + Environment.NewLine;

                do
                {
                    EnteredData.delta_rb *= 0.9;

                    for (int i = 1; i < EnteredData.n; i++)
                    {
                        EnteredData.delta_E += (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
                    }
                } while (EnteredData.delta_E <= EnteredData.delta_KR);
                output_text.Text += $"Конечная cуммарная погрешность формы обработанной детали (отклонение круглости) равна {EnteredData.delta_E}" + Environment.NewLine;

                EnteredData.x1 = 0.1 * EnteredData.l;
                EnteredData.x2 = EnteredData.l / 2;
                EnteredData.delta_Prod = EnteredData.t * (Math.Pow((EnteredData.l / EnteredData.x1), 0.21) - Math.Pow((EnteredData.l - EnteredData.x2), 0.21));
                output_text.Text += $"Бочкообразность = {EnteredData.delta_Prod}";

                do
                {
                    EnteredData.t *= 0.9;

                    EnteredData.Svr = EnteredData.t / Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
                    output_text.Text += $"Рациональное значение врезной подачи равно {EnteredData.Svr}" + Environment.NewLine;

                    EnteredData.h1 = EnteredData.h - ((EnteredData.d3 - EnteredData.d) / 2) * (1 - Math.Sin(EnteredData.Alpha));

                    EnteredData.Epsilon = 2 * (Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h1, 2) * (1 / Math.Pow((EnteredData.d + EnteredData.Dv), 2))) - Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h, 2) * (Math.Pow(EnteredData.d3, 2) / Math.Pow((EnteredData.d3 + EnteredData.Dv), 2))));
                    // пока условие истинно - цикл будет обрабатывать блок кода do
                    do
                    {
                        EnteredData.Alpha *= 0.9;

                        EnteredData.h1 = EnteredData.h - ((EnteredData.d3 - EnteredData.d) / 2) * (1 - Math.Sin(EnteredData.Alpha));

                        EnteredData.Epsilon = 2 * (Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h1, 2) * (1 / Math.Pow((EnteredData.d + EnteredData.Dv), 2))) - Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h, 2) * (Math.Pow(EnteredData.d3, 2) / Math.Pow((EnteredData.d3 + EnteredData.Dv), 2))));

                    } while (EnteredData.Epsilon <= 0.4 * EnteredData.Td);

                    output_text.Text += $"Конечная величина превышения оси детали над плоскостью расположения осей абразивных кругов после окончания обработки равна {EnteredData.h1}" + Environment.NewLine;
                    output_text.Text += $"Конечная погрешность размера по диаметру детали, возникающая из-за особенностей схемы базирования (с учетом сравнения погрешности базирования величиной допуска на диаметр и корректировки величины угла скоса опорного ножа) равна {EnteredData.Epsilon}" + Environment.NewLine;
                   
                    EnteredData.n = Math.Round((EnteredData.z / EnteredData.Svr), MidpointRounding.AwayFromZero);
                    output_text.Text += $"Число оборотов заготовки до формирования суммарной погрешности формы: {EnteredData.n}" + Environment.NewLine;

                    for (int i = 1; i < EnteredData.n; i++)
                    {
                        EnteredData.delta_E += (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
                    }
                    output_text.Text += $"Суммарная погрешность формы обработанной детали (отклонение круглости) равна {EnteredData.delta_E}" + Environment.NewLine;

                    do
                    {
                        EnteredData.delta_rb *= 0.9;

                        for (int i = 1; i < EnteredData.n; i++)
                        {
                            EnteredData.delta_E += (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
                        }
                    } while (EnteredData.delta_E <= EnteredData.delta_KR);
                    output_text.Text += $"Конечная cуммарная погрешность формы обработанной детали (отклонение круглости) равна {EnteredData.delta_E}" + Environment.NewLine;

                    EnteredData.x1 = 0.1 * EnteredData.l;
                    EnteredData.x2 = EnteredData.l / 2;
                    EnteredData.delta_Prod = EnteredData.t * (Math.Pow((EnteredData.l / EnteredData.x1), 0.21) - Math.Pow((EnteredData.l - EnteredData.x2), 0.21));
                    output_text.Text += $"Бочкообразность = {EnteredData.delta_Prod}" + Environment.NewLine;

                } while (EnteredData.delta_Prod > EnteredData.delta_prod);

                EnteredData.A = (EnteredData.Dsh * EnteredData.d) / (EnteredData.Dsh + EnteredData.d);
                output_text.Text += $"Параметр А = {EnteredData.A}" + Environment.NewLine;

                //перебрать d0i и z0i

                EnteredData.H0 = 0.2 * EnteredData.d0[0];
                EnteredData.RA = 0.206 * ((Math.Pow(EnteredData.H0, 2.9) * EnteredData.S * EnteredData.t * (Math.Pow(EnteredData.d0[0], 0.14) / EnteredData.z0[0] * (EnteredData.Vkr / EnteredData.Vg) * EnteredData.Bkr * Math.Sqrt(EnteredData.A))));
                EnteredData.S = 0.66 * EnteredData.Bkr; //проверить

                do
                {
                    for (int i = 1; i < 4; i++)
                    {
                        EnteredData.S *= 0.9;
                        EnteredData.RA = 0.206 * ((Math.Pow(EnteredData.H0, 2.9) * EnteredData.S * EnteredData.t * (Math.Pow(EnteredData.d0[i], 0.14) / EnteredData.z0[i] * (EnteredData.Vkr / EnteredData.Vg) * EnteredData.Bkr * Math.Sqrt(EnteredData.A))));
                    }
                } while (EnteredData.RA > EnteredData.Ra);

                //выводим h, alpha, Sвр, Dв, t, delta_Prod, delta E, RA
                output_text.Text += $"Ra = {EnteredData.RA}" + Environment.NewLine; 
            }


            else if (EnteredData.Sfera)
            {
                EnteredData.N_min = Math.Ceiling((EnteredData.kn * Math.PI * EnteredData.d) / (4 * EnteredData.R3)); //округление ТОЛЬКО в большую сторону. При надобности заменить на Math.Round
                //output_text.Text += $"Минимально необходимое число оборотов заготовки для обработки всей поверхности сферы = {EnteredData.N_min}" + Environment.NewLine;

                EnteredData.delta_E = ((EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3)) * EnteredData.N_min;

                while (EnteredData.delta_E > EnteredData.delta_SF)
                {
                    EnteredData.delta_rb *= 0.9;

                    EnteredData.delta_E = ((EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3)) * EnteredData.N_min;
                }
                //output_text.Text += $"Суммарная погрешность формы обработанной детали (отклонение круглости) равно {EnteredData.delta_E}" + Environment.NewLine;


                EnteredData.An = Math.Round(1 + 2 * (EnteredData.N_min - 1), MidpointRounding.AwayFromZero);
                //output_text.Text += $"Нужный член ряда, определяющего число пересечений кольцевых лысок при целом значении n_min равен {EnteredData.An}" + Environment.NewLine;

                EnteredData.Sn = (EnteredData.N_min / 2) * (1 + EnteredData.An);
                //output_text.Text += $"Общее число площадок пересечения кольцевых лысок равно {EnteredData.Sn}" + Environment.NewLine;

                EnteredData.N__min = EnteredData.kn * Math.PI * EnteredData.d * ((EnteredData.Sn * EnteredData.R3) / (4 * (Math.Pow(EnteredData.R3, 2)) * EnteredData.Sn * (Math.Pow(EnteredData.d, 2)) * Math.PI));
                //output_text.Text += $"Минимальное число лысок, суммарная ширина которых достаточна для полного покрытия всей сферы, равна {EnteredData.N__min}" + Environment.NewLine;

                EnteredData.Zl = Math.Round(EnteredData.Sn * ((2 * EnteredData.R3) / EnteredData.d), MidpointRounding.AwayFromZero);
                //output_text.Text += $"Количество волн в произвольном сечении сферы (гармоника), равно: {EnteredData.Zl}" + Environment.NewLine;

                EnteredData.Lw = (Math.PI * EnteredData.d) / EnteredData.Zl;
                //output_text.Text += $"Средняя длина шага волн равна {EnteredData.Lw}" + Environment.NewLine;

                EnteredData.Wz = 0.5 * (EnteredData.d - Math.Sqrt(Math.Pow(EnteredData.d, 2) - Math.Pow(EnteredData.Lw, 2)));
                //output_text.Text += $"Высота волн = {EnteredData.Wz}" + Environment.NewLine;

                do
                {
                    EnteredData.R3 *= 0.9;

                    EnteredData.N_min = Math.Ceiling((EnteredData.kn * Math.PI * EnteredData.d) / (4 * EnteredData.R3)); //округление ТОЛЬКО в большую сторону. При надобности заменить на Math.Round
                    //output_text.Text += $"Минимально необходимое число оборотов заготовки для обработки всей поверхности сферы = {EnteredData.N_min}" + Environment.NewLine;

                    EnteredData.delta_E = (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3) * EnteredData.N_min;

                    do
                    {
                        EnteredData.delta_rb *= 0.9;

                        EnteredData.delta_E = (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3) * EnteredData.N_min;
                        
                    } while (EnteredData.delta_E > EnteredData.delta_SF);
                    //output_text.Text += $"Конечная cуммарная погрешность формы обработанной детали (отклонение круглости) после пересчета равна {EnteredData.delta_E}" + Environment.NewLine;

                    EnteredData.An = Math.Round(1 + 2 * (EnteredData.N_min - 1), MidpointRounding.AwayFromZero);
                    //output_text.Text += $"Нужный член ряда, определяющего число пересечений кольцевых лысок при целом значении n_min равен {EnteredData.An}" + Environment.NewLine;

                    EnteredData.Sn = (EnteredData.N_min / 2) * (1 + EnteredData.An);
                    //output_text.Text += $"Общее число площадок пересечения кольцевых лысок равно {EnteredData.Sn}" + Environment.NewLine;

                    EnteredData.N__min = EnteredData.kn * Math.PI * EnteredData.d * ((EnteredData.Sn * EnteredData.R3) / (4 * (Math.Pow(EnteredData.R3, 2)) * EnteredData.Sn * (Math.Pow(EnteredData.d, 2)) * Math.PI));
                    //output_text.Text += $"Минимальное число лысок, суммарная ширина которых достаточна для полного покрытия всей сферы, равна {EnteredData.N__min}" + Environment.NewLine;

                    EnteredData.Zl = Math.Round(EnteredData.Sn * ((2 * EnteredData.R3) / EnteredData.d), MidpointRounding.AwayFromZero); 
                    //output_text.Text += $"Количество волн в произвольном сечении сферы (гармоника), равно: {EnteredData.Zl}" + Environment.NewLine;

                    EnteredData.Lw = (Math.PI * EnteredData.d) / EnteredData.Zl;
                    //output_text.Text += $"Средняя длина шага волн равна {EnteredData.Lw}" + Environment.NewLine;

                    EnteredData.Wz = 0.5 * (EnteredData.d - Math.Sqrt(Math.Pow(EnteredData.d, 2) - Math.Pow(EnteredData.Lw, 2)));
                    //output_text.Text += $"Высота волн = {EnteredData.Wz}" + Environment.NewLine;
                } while (EnteredData.Wz >= EnteredData.delta_V);


                EnteredData.L = EnteredData.Dsh * Math.Acos((EnteredData.Dsh - (2 * EnteredData.t)) / EnteredData.Dsh);
                //output_text.Text += $"Длина дуги контакта шлифовального круга с заготовкой = {EnteredData.L}" + Environment.NewLine;

                EnteredData.RA = 0.196 * Math.Pow(((Math.Pow(EnteredData.d0[0], 0.289) * 1) / (EnteredData.L * EnteredData.z0[0])), 0.257) * 0.001;
                //output_text.Text += $"Среднее арифметическое отклонение микропрофиля при врезном шлифовании (cos B = 1) равно {EnteredData.RA}" + Environment.NewLine;

                do
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (EnteredData.RA <= EnteredData.Ra)
                        {
                            EnteredData.RA = 0.196 * Math.Pow(((Math.Pow(EnteredData.d0[i], 0.289) * 1) / (EnteredData.L * EnteredData.z0[i])), 0.257) * 0.001;
                        }
                    }
                } while (EnteredData.RA > EnteredData.Ra);

                //if (EnteredData.RA <= EnteredData.Ra)
                //{
                //   //output_text.Text += "Произведем перерасчет" + Environment.NewLine;

                //   EnteredData.RA = 0.196 * Math.Pow(((Math.Pow(EnteredData.d03, 0.289) * 1) / (EnteredData.L * EnteredData.z02)), 0.257) * 0.001;
                //   //output_text.Text += $"Среднее арифметическое отклонение микропрофиля при врезном шлифовании (cos B = 1) равно {EnteredData.RA}" + Environment.NewLine;

                //    if (EnteredData.RA <= EnteredData.Ra)
                //    {
                //        //output_text.Text += "Произведем перерасчет" + Environment.NewLine;

                //        EnteredData.RA = 0.196 * Math.Pow(((Math.Pow(EnteredData.d02, 0.289) * 1) / (EnteredData.L * EnteredData.z03)), 0.257) * 0.001;
                //        //output_text.Text += $"Среднее арифметическое отклонение микропрофиля при врезном шлифовании (cos B = 1) равно {EnteredData.RA}" + Environment.NewLine;

                //        if (EnteredData.RA <= EnteredData.Ra)
                //        {
                //             //output_text.Text += "Произведем перерасчет" + Environment.NewLine;

                //             EnteredData.RA = 0.196 * Math.Pow(((Math.Pow(EnteredData.d01, 0.289) * 1) / (EnteredData.L * EnteredData.z04)), 0.257) * 0.001;
                //             //output_text.Text += $"Среднее арифметическое отклонение микропрофиля при врезном шлифовании (cos B = 1) равно {EnteredData.RA}" + Environment.NewLine;
                //        }
                //    }
                //}
                EnteredData.n3 = EnteredData.Nv * (EnteredData.Dv / EnteredData.d3);
                //output_text.Text += $"Частота вращения заготовки равна {EnteredData.n3}" + Environment.NewLine;

                EnteredData.T = EnteredData.N_min / EnteredData.n3;
                //output_text.Text += $"Длительность цикла обработки одной сферической заготовки: {EnteredData.T}" + Environment.NewLine;

                output_text.Text += $"Среднее арифметическое отклонение микропрофиля при врезном шлифовании Ra (cos B = 1) равно {EnteredData.RA}" + Environment.NewLine;
                output_text.Text += $"Высота волн = {EnteredData.Wz}" + Environment.NewLine;
                output_text.Text += $"Длительность цикла обработки одной сферической заготовки: {EnteredData.T}" + Environment.NewLine;
                output_text.Text += $"Суммарная погрешность формы обработанной детали (отклонение круглости) равно {EnteredData.delta_E}" + Environment.NewLine;
            }       
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
