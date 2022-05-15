namespace paint_курс2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Width = 900; //Ширина  
            this.Height = 700; //Высота
            bm = new Bitmap(pic.Width, pic.Height); //Создание поля с задаными высотой и шириной
            g = Graphics.FromImage(bm); //Создание объекта класса Graphics для Bitmap
            g.Clear(Color.White); //Заполнение поля белым цветом
            pic.Image = bm; //Прорисовка Bitmap на PictureBox
        }

        Bitmap bm; //Поле на котором мы рисуем (холст)
        Graphics g; //Инструмент с помощью которого мы рисуем
        bool paint = false; //Флажок указывающие на нажатие мыши
        Point px, py; //Координаты (х;у)
        Pen p = new Pen(Color.Black, 5); //Карандаш знач. по ум. 
        Pen erase = new Pen(Color.White, 10); //Ластик
        Color new_color;
        ColorDialog cd = new ColorDialog(); //Палитра цветов 

        int index;
        int x, y, sX, sY, cX, cY; //Координаты (х;у)


        //Событие движения мыши
        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint) //Если мышь зажата
            {
                if (index == 2) //Выбран режим ластика
                {
                    px = e.Location;
                    g.DrawLine(erase, px, py); //Нарисовать линию с помощью erase (Ластика)
                    py = px;
                }
                if (index == 1) //Выбран режим карандаша 
                {
                    px = e.Location;
                    g.DrawLine(p, px, py); //Нарисовать линию с помощью p (Карандаша)
                    py = px;
                }
            }
            pic.Refresh(); // Обновить PictureBox

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;
        }

        //Отжатие мыши
        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false; //изменение флажка, мышь отжата

            sX = x - cX;
            sY = y - cY;

            if (index == 3) //Выбран режим рисования элипса
            {
                g.DrawEllipse(p, cX, cY, sX, sY); //Нарисовать элипс
            }
            if (index == 4) //Выбран режим рисования прямоугольника
            {
                g.DrawRectangle(p, cX, cY, sX, sY); //Нарисовать прямоугольник
            }
            if (index == 5) //Выбран режим рисования прямой линии
            {
                g.DrawLine(p, cX, cY, x, y); //Нарисовать линию
            }
        }

        //Заливка
        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            if (index == 7) //Выбран режим заливки
            {
                Point point = set_point(pic, e.Location); //Координаты клика
                FillPointsClass.Fill(bm, point.X, point.Y, new_color); //Заливка
            }
        }

        //Зажатие мыши
        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true; //изменение флажка, мышь зажата
            py = e.Location;

            cX = e.X;
            cY = e.Y;
        }

        //фигуры и линии
        private void btn_pencil_Click(object sender, EventArgs e)
        {
            index = 1; //Установка индекса на режим карандаш
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;//Выравнивание линии
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;//Выравнивание линии
        }
        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 2; //Установка индекса на режим ластик
        }
        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            index = 3; //Установка индекса на режим элипс
        }
        private void btn_rect_Click(object sender, EventArgs e)
        {
            index = 4; //Установка индекса на режим прямоугольник
        }
        private void btn_line_Click(object sender, EventArgs e)
        {
            index = 5; //Установка индекса на режим линия
        }
        private void btn_fill_Click(object sender, EventArgs e)
        {
            index = 7; //Установка индекса на режим заливка
        }



        //Всплывающее окно с выбором цвета
        private void btn_color_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) // Если нажали ОК
            {
                p.Color = colorDialog1.Color; //Изменить цвет Pen p
                pic_color.BackColor = colorDialog1.Color; //Изменить цвет кнопки pic_color
                new_color = cd.Color; //Запись выбранного цвета           
            }
        }

        //Очистка поля
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            g.Clear(pic.BackColor); // Очистить Bitmap
            pic.Image = bm; // Обновить PictureBox
        }

        //Сохраненить изображение
        private void btn_saver_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg"; // Задаём формат
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) // Если нажали ОК
            {
                if (pic.Image != null) // Если картика не пустая
                {
                    pic.Image.Save(saveFileDialog1.FileName); // Сохранить картинку в заданом пути
                }
            }
        }


        //Рисование
        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (paint) // Если мышь зажата
            {
                if (index == 3) //Выбран режим рисования элипса
                {
                    g.DrawEllipse(p, cX, cY, sX, sY); //Нарисовать элипс
                }
                if (index == 4) //Выбран режим рисования прямоугольника
                {
                    g.DrawRectangle(p, cX, cY, sX, sY); //Нарисовать прямоугольник
                }
                if (index == 5) //Выбран режим рисования прямой линии
                {
                    g.DrawLine(p, cX, cY, x, y); //Нарисовать линию
                }
            }
        }

        //Перевод координат в Point относительно PictureBox
        static Point set_point(PictureBox pb, Point pt)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }

        //Пипетка
        private void color_picker_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = set_point(color_picker, e.Location);
            pic_color.BackColor = ((Bitmap)color_picker.Image).GetPixel(point.X, point.Y);// Получить цвет фона
            new_color = pic_color.BackColor; 

            p.Color = pic_color.BackColor; // Обновить цвет Pen p
        }
    }
}