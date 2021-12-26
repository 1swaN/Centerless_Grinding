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
        //массивы данных d0i и z0i
        public  double[] z0 = new double[4] { EnteredData.z01, EnteredData.z02, EnteredData.z03, EnteredData.z04 };
        public  double[] d0 = new double[4] { EnteredData.d04, EnteredData.d03, EnteredData.d02, EnteredData.d01 };
        public Form2()
        {
            InitializeComponent();
            Counting(); //мгновенная инициализация расчетного метода Counting() для проведения расчетов и вывода их на Form2
        }
       //проверка
        public void Counting()
        {
            //перевод значений мкм в мм
            EnteredData.R3 *= 0.001;
            EnteredData.d01 *= 0.001;
            EnteredData.d02 *= 0.001;
            EnteredData.d03 *= 0.001;
            EnteredData.d04 *= 0.001;
            //перевод м/мин в м/с
            EnteredData.Vg = EnteredData.Vg / 60;

            //Расчеты
            //output_text.Text += $"Присваиваем дробной части отношения b/c величину {EnteredData.b_c}" + Environment.NewLine;

            EnteredData.Dv += 0.1; //Задаем рациональное значение диаметра ведущего круга для правки
                                                                                                           
            EnteredData.h = Math.Round(0.5 * EnteredData.k * (EnteredData.Dv + EnteredData.d) * Math.Sin(Math.Atan(EnteredData.f1)), MidpointRounding.AwayFromZero); //Величина превышения центра заготовки    

            EnteredData.Alpha = Math.Round(EnteredData.k1*Math.Atan(EnteredData.f1 / EnteredData.f2), 2); //Величина угла скоса опорного ножа (округляем угол скоса до сотых)

            EnteredData.Svr = EnteredData.t / Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3); //Рациональное значение врезной подачи

            EnteredData.h1 = EnteredData.h - ((EnteredData.d3 - EnteredData.d) / 2) * (1 - Math.Sin(EnteredData.Alpha)); //величина превышения оси детали над плоскостью расположения осей абразивных кругов после окончания обработки 

            EnteredData.Epsilon = 2 * (Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h1, 2) * ((Math.Pow(EnteredData.d, 2)) / Math.Pow((EnteredData.d + EnteredData.Dv), 2))) - Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h, 2) * (Math.Pow(EnteredData.d3, 2) / Math.Pow((EnteredData.d3 + EnteredData.Dv), 2)))); //вычисление погрешности размера по диаметру детали

            while (EnteredData.Epsilon > 0.4 * EnteredData.Td)
            {
                EnteredData.Alpha *= 0.9; //перерасчет размерности угла скоса опорного ножа

                EnteredData.h1 = EnteredData.h - ((EnteredData.d3 - EnteredData.d) / 2) * (1 - Math.Sin(EnteredData.Alpha)); //перерасчет величины превышения оси с учетом ного значения угла скоса

                EnteredData.Epsilon = 2 * (Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h1, 2) * ((Math.Pow(EnteredData.d, 2)) / Math.Pow((EnteredData.d + EnteredData.Dv), 2))) - Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h, 2) * (Math.Pow(EnteredData.d3, 2) / Math.Pow((EnteredData.d3 + EnteredData.Dv), 2)))); //перерасчет погрешности размера с учетом ранее перерасчитанных значений
            }


            if (EnteredData.Cilinder) //выбор дальнейшего расчета по цилиндрической детали
            {
                EnteredData.n = Math.Round((EnteredData.z / EnteredData.Svr), MidpointRounding.AwayFromZero); //Число оборотов заготовки до формирования суммарной погрешности формы

                for (int i = 1; i < EnteredData.n; i++) //цикл суммирования значений отклонения круглости по числу оборотов заготовки до формирования суммарной погрешности формы
                {
                    EnteredData.delta_E += (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3); //Суммарная погрешность формы обработанной детали (отклонение круглости)
                }

                do
                {
                    //пока условие в while истинно, будет выполняться перерасчет (если оно не истинно, то расчет идет далее, пропуская перерасчет):

                    EnteredData.delta_rb *= 0.9; //перерасчет значения

                    for (int i = 1; i < EnteredData.n; i++)
                    {
                        EnteredData.delta_E += (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3); //перерасчет отклонения круглости
                    }
                } while (EnteredData.delta_E <= EnteredData.delta_KR); //условие прохождения перерасчета (выполняется до тех пор, пока значение перестанет ему соответствовать)

                EnteredData.x1 = 0.1 * EnteredData.l; //расчет параметра х1
                EnteredData.x2 = EnteredData.l / 2; //расчет параметра х2
                EnteredData.delta_Prod = EnteredData.t * (Math.Pow((EnteredData.l / EnteredData.x1), 0.21) - Math.Pow((EnteredData.l / EnteredData.x2), 0.21)); //расчет параметра бочкообразности

                do //блок перерасчета значения бочкообразности
                {
                    EnteredData.t *= 0.9;

                    EnteredData.Svr = EnteredData.t / Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
                    
                    EnteredData.h1 = EnteredData.h - ((EnteredData.d3 - EnteredData.d) / 2) * (1 - Math.Sin(EnteredData.Alpha));

                    EnteredData.Epsilon = 2 * (Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h1, 2) * ((Math.Pow(EnteredData.d, 2)) / Math.Pow((EnteredData.d + EnteredData.Dv), 2))) - Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h, 2) * (Math.Pow(EnteredData.d3, 2) / Math.Pow((EnteredData.d3 + EnteredData.Dv), 2))));

                    while (EnteredData.Epsilon > 0.4 * EnteredData.Td)
                    {
                        EnteredData.Alpha *= 0.9; //перерасчет размерности угла скоса опорного ножа

                        EnteredData.h1 = EnteredData.h - ((EnteredData.d3 - EnteredData.d) / 2) * (1 - Math.Sin(EnteredData.Alpha)); //перерасчет величины превышения оси с учетом ного значения угла скоса

                        EnteredData.Epsilon = 2 * (Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h1, 2) * ((Math.Pow(EnteredData.d, 2)) / Math.Pow((EnteredData.d + EnteredData.Dv), 2))) - Math.Sqrt((Math.Pow(EnteredData.d, 2) / 4) - Math.Pow(EnteredData.h, 2) * (Math.Pow(EnteredData.d3, 2) / Math.Pow((EnteredData.d3 + EnteredData.Dv), 2)))); //перерасчет погрешности размера с учетом ранее перерасчитанных значений
                    }


                    EnteredData.n = Math.Round((EnteredData.z / EnteredData.Svr), MidpointRounding.AwayFromZero);

                    for (int i = 1; i < EnteredData.n; i++)
                    {
                        EnteredData.delta_E += (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
                    }

                    do
                    {
                        EnteredData.delta_rb *= 0.9;

                        for (int i = 1; i < EnteredData.n; i++)
                        {
                            EnteredData.delta_E += (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3);
                        }
                    } while (EnteredData.delta_E <= EnteredData.delta_KR);

                    EnteredData.x1 = 0.1 * EnteredData.l;
                    EnteredData.x2 = EnteredData.l / 2;
                    EnteredData.delta_Prod = EnteredData.t * (Math.Pow((EnteredData.l / EnteredData.x1), 0.21) - Math.Pow((EnteredData.l / EnteredData.x2), 0.21));  

                } while (EnteredData.delta_Prod > EnteredData.delta_prod); //условие, при котором необходимо пересчитывать бочкообразность

                EnteredData.A = (EnteredData.Dsh * EnteredData.d) / (EnteredData.Dsh + EnteredData.d); //вычисление параметра А

                EnteredData.H0 = 0.2 * d0[0]; //вычисление параметра Н0
                EnteredData.S = 0.66 * EnteredData.Bkr;
                EnteredData.RA = 0.206 * ((Math.Pow(EnteredData.H0, 2.9)) * EnteredData.S * EnteredData.t * (Math.Pow(d0[0], 0.14)) / (z0[0] * (EnteredData.Vkr / EnteredData.Vg) * EnteredData.Bkr * Math.Sqrt(EnteredData.A))); //вычисление парметра Ra 

                while (EnteredData.RA > EnteredData.Ra) //сравнение вычисленного парметра шероховатости с допустимым
                {
                    for (int i = 1; i < 4; i++) //цикл перерасчета
                    {
                        EnteredData.S *= 0.9; 
                        EnteredData.RA = 0.206 * ((Math.Pow(EnteredData.H0, 2.9)) * EnteredData.S * EnteredData.t * (Math.Pow(d0[0], 0.14)) / (z0[0] * (EnteredData.Vkr / EnteredData.Vg) * EnteredData.Bkr * Math.Sqrt(EnteredData.A)));           
                    }
                    if (++EnteredData.Check > 3) //выход из цикла при превышении кол-ва итераций перерасчета
                    {
                        break;
                    }
                }
                //вывод параметров
                output_text.Text += $"Величина превышения центра заготовки равна {EnteredData.h}" + Environment.NewLine;
                output_text.Text += $"Величина угла скоса опорного ножа равна {EnteredData.Alpha}" + Environment.NewLine;
                output_text.Text += $"Ra = {EnteredData.RA}" + Environment.NewLine;
                output_text.Text += $"Конечная cуммарная погрешность формы обработанной детали (отклонение круглости) равна {EnteredData.delta_E}" + Environment.NewLine;
                output_text.Text += $"Бочкообразность (конечное значение) = {EnteredData.delta_Prod}" + Environment.NewLine; 
                output_text.Text += $"Рациональное значение врезной подачи равно {EnteredData.Svr}" + Environment.NewLine;
                output_text.Text += $"Задаем рациональное значение диаметра ведущего круга для правки в размере {EnteredData.Dv}" + Environment.NewLine;
                output_text.Text += $"Подача {EnteredData.S}" + Environment.NewLine;
            }

            else if (EnteredData.Sfera) //выбор расчета сферы
            {
                EnteredData.N_min = Math.Ceiling((EnteredData.kn * Math.PI * EnteredData.d) / (4 * EnteredData.R3)); //Минимально необходимое число оборотов заготовки для обработки всей поверхности сферы. Округление ТОЛЬКО в большую сторону. 
                
                EnteredData.delta_E = ((EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3)) * EnteredData.N_min;

                while (EnteredData.delta_E > EnteredData.delta_SF)
                {
                    EnteredData.delta_rb *= 0.9;

                    EnteredData.delta_E = ((EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3)) * EnteredData.N_min; //Суммарная погрешность формы обработанной детали(отклонение круглости)
                }

                EnteredData.An = Math.Round(1 + 2 * (EnteredData.N_min - 1), MidpointRounding.AwayFromZero); //Нужный член ряда, определяющего число пересечений кольцевых лысок при целом значении n_min
                
                EnteredData.Sn = (EnteredData.N_min / 2) * (1 + EnteredData.An); //Общее число площадок пересечения кольцевых лысок
                
                EnteredData.N__min = EnteredData.kn * Math.PI * EnteredData.d * ((EnteredData.Sn * EnteredData.R3) / (4 * (Math.Pow(EnteredData.R3, 2)) * EnteredData.Sn * (Math.Pow(EnteredData.d, 2)) * Math.PI)); //Минимальное число лысок, суммарная ширина которых достаточна для полного покрытия всей сферы
                
                EnteredData.Zl = Math.Round(EnteredData.Sn * ((2 * EnteredData.R3) / EnteredData.d), MidpointRounding.AwayFromZero); //Количество волн в произвольном сечении сферы (гармоника)

                EnteredData.Lw = (Math.PI * EnteredData.d) / EnteredData.Zl; //Средняя длина шага волн

                EnteredData.Wz = 0.5 * (EnteredData.d - Math.Sqrt(Math.Pow(EnteredData.d, 2) - Math.Pow(EnteredData.Lw, 2))); //Высота волн

                do //проверка величины Wz и ее перерасчет
                {
                    EnteredData.R3 *= 0.9; //перерасчет R3

                    EnteredData.N_min = Math.Ceiling((EnteredData.kn * Math.PI * EnteredData.d) / (4 * EnteredData.R3)); //перерасчет Nmin исходя из нового значения R3
  
                    EnteredData.delta_E = (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3) * EnteredData.N_min;

                    do
                    {
                        EnteredData.delta_rb *= 0.9;

                        EnteredData.delta_E = (EnteredData.d3 / EnteredData.Dv) * EnteredData.delta_rb * Math.Pow(Math.Cos(Math.Asin(EnteredData.h / (EnteredData.Dv + EnteredData.d3))), 3) * EnteredData.N_min;
                        
                    } while (EnteredData.delta_E > EnteredData.delta_SF);
                   
                    EnteredData.An = Math.Round(1 + 2 * (EnteredData.N_min - 1), MidpointRounding.AwayFromZero);
                    
                    EnteredData.Sn = (EnteredData.N_min / 2) * (1 + EnteredData.An);
                    
                    EnteredData.N__min = EnteredData.kn * Math.PI * EnteredData.d * ((EnteredData.Sn * EnteredData.R3) / (4 * (Math.Pow(EnteredData.R3, 2)) * EnteredData.Sn * (Math.Pow(EnteredData.d, 2)) * Math.PI));
                    
                    EnteredData.Zl = Math.Round(EnteredData.Sn * ((2 * EnteredData.R3) / EnteredData.d), MidpointRounding.AwayFromZero); 
                    
                    EnteredData.Lw = (Math.PI * EnteredData.d) / EnteredData.Zl;
                    
                    EnteredData.Wz = 0.5 * (EnteredData.d - Math.Sqrt(Math.Pow(EnteredData.d, 2) - Math.Pow(EnteredData.Lw, 2)));
                    
                } while (EnteredData.Wz >= EnteredData.delta_V); //условие проверки величины Wz

                EnteredData.L = EnteredData.Dsh * Math.Acos((EnteredData.Dsh - (2 * EnteredData.t)) / EnteredData.Dsh); //Длина дуги контакта шлифовального круга с заготовкой
                
                EnteredData.RA = 0.196 * Math.Pow(((Math.Pow(d0[0], 0.289) * 1) / (EnteredData.L * z0[0])), 0.257) * 0.001; //Расчет параметра шероховатости Ra при врезном шлифовании (cos B = 1)


                while (EnteredData.RA > EnteredData.Ra)
                {
                    
                    for (int i = 1; i < 4; i++)
                    {
                        EnteredData.RA = 0.196 * Math.Pow(((Math.Pow(d0[i], 0.289) * 1) / (EnteredData.L * z0[i])), 0.257) * 0.001;
                    }

                    if (++EnteredData.Check > 3)
                    {
                        break;
                    }
                }
                EnteredData.n3 = EnteredData.Nv * (EnteredData.Dv / EnteredData.d3); // Частота вращения заготовки

                EnteredData.T = EnteredData.N_min / EnteredData.n3; //Длительность цикла обработки одной сферической заготовки

                //вывод параметров
                output_text.Text += $"Величина превышения центра заготовки равна {EnteredData.h}" + Environment.NewLine;
                output_text.Text += $"Величина угла скоса опорного ножа равна {EnteredData.Alpha}" + Environment.NewLine;
                output_text.Text += $"Рациональное значение врезной подачи равно {EnteredData.Svr}" + Environment.NewLine;
                output_text.Text += $"Задаем рациональное значение диаметра ведущего круга для правки в размере {EnteredData.Dv}" + Environment.NewLine;
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
