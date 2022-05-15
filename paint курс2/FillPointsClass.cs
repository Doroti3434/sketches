using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint_курс2
{
    internal static class FillPointsClass
    {
        //Проверка пикселя на замену цвета и добавление в стек
        private static void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color old_color, Color new_color)
        {
            Color cx = bm.GetPixel(x, y);//Получить цвет пикселя по координатам
            if (cx == old_color) //Если это изменяемый цвет
            {
                sp.Push(new Point(x, y));//Закинуть в стек пиксель
                bm.SetPixel(x, y, new_color);//Покрасить пиквель в новый цвет
            }
        }

        //Заливка
        public static void Fill(Bitmap bm, int x, int y, Color new_clr) // загадка вселенной
        {
            Color old_color = bm.GetPixel(x, y); //Получить старый цвет
            Stack<Point> pixel = new Stack<Point>(); //Стек пикселями, которые надо поменять
            pixel.Push(new Point(x, y)); //Закинуть пиксель на который нажали
            bm.SetPixel(x, y, new_clr); //Обновить цвет в пикселе на новый
            if (old_color == new_clr) return; //Если цвет один и тот же завершить

            while (pixel.Count > 0) //Пока стек не пустой
            {
                Point pt = (Point)pixel.Pop(); //Получить координаты пикселя
                if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1) //Проверка на выход за рамки
                {
                    validate(bm, pixel, pt.X - 1, pt.Y, old_color, new_clr); //Проверить левую клетку
                    validate(bm, pixel, pt.X, pt.Y - 1, old_color, new_clr); //Проверить верхнюю клетку
                    validate(bm, pixel, pt.X + 1, pt.Y, old_color, new_clr); //Проверить правую клетку
                    validate(bm, pixel, pt.X, pt.Y + 1, old_color, new_clr); //Проверить нижнюю клетку
                }
            }

        }
    }
}
