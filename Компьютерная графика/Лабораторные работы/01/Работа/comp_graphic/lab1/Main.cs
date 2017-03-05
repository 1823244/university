using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace lab1
{
    public partial class Main : Form
    {
        // угол поворота всех квадратов
        float angle = 0;
        // количество квадратов на экране
        int squaresNum = 1;

        public Main()
        {
            InitializeComponent();

            this.SetStyle(
                // вкл. двойную буферизацию == боремся с мерцанием
                ControlStyles.DoubleBuffer |
                // контрол отрисовывает себя сам, вместо ОС
                ControlStyles.UserPaint |
                // игнорирование события WM_ERASEBKGND для уменьшения мерцания
                ControlStyles.AllPaintingInWmPaint |
                // перерисовывание при ресайзе окна
                ControlStyles.ResizeRedraw,
                true
            );
            this.UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            RectangleF area = g.VisibleClipBounds;

            // вычисляем максимально возможный радиус вписанной в area окружности
            float d = (area.Width > area.Height) ? area.Height : area.Width;
            // по условию: отступ от краёв по 10 пикселей 
            float radius = (d - 20) / 2;

            // координаты центра окружности
            var center = new PointF(area.Width / 2, area.Height / 2);

            // задаём начальный размер квадратов, равный чуть меньше, чем половине диаметра
            int squareSize = (int)radius / 2 - 1;

            // уменьшаем размер квадратов, если они не влезают 
            while (! IsFit(squareSize, radius))
            {
                squareSize--;
            }

            // если окно или размер квадратов совсем уж маленькие -- ничего не выводить
            if (radius < squareSize * 2 || squareSize < 1) return;

            // вычисляем реальные радиус и диаметр окружности
            float roundRadius = (radius - squareSize);
            float roundDiameter = roundRadius * 2;

            // находим область отрисовки круга
            var roundArea = new RectangleF(
                center.X - roundRadius,
                center.Y - roundRadius,
                roundDiameter,
                roundDiameter
            );

            // отрисовываем фон, чтобы было видно отступы
            var bg = new RectangleF(
                center.X - radius, 
                center.Y - radius, 
                radius * 2, 
                radius * 2
            );

            g.FillRectangle(Brushes.Beige, bg);

            // зададим левую верхнюю точку квадрата
            var squareStartF = new PointF(center.X - squareSize / 2, center.Y - radius);
            // зададим размеры квадрата (на самом деле прямоугольника)
            var squareSizeF = new SizeF(squareSize, radius);
            // зададим область отрисовки квадрата
            var square = new RectangleF(squareStartF, squareSizeF);

            // инициализируем матрицу трансформаций
            var m = new Matrix();

            for (int i = 0; i < squaresNum; i++)
            {
                // поворачиваем область отрисовки относительно центра
                m.RotateAt(i * 360 / squaresNum + angle, center);
                g.Transform = m;

                // отрисовываем квадрат
                g.FillRectangle(Brushes.Maroon, square);

                // сбрасываем матрицу трансформации и поворот области отрисовки
                m.Reset();
                g.ResetTransform();
            }

            // отрисовываем сам круг
            g.FillEllipse(Brushes.BurlyWood, roundArea);

            base.OnPaint(e);
        }

        private bool IsFit(int squareSize, float radius)
        {
            // расстояние между квадратами
            var spaceBetween = 5;

            int p = squareSize * squaresNum + spaceBetween * (squaresNum + 1);
            double l = 2 * Math.PI * (radius - squareSize);
            
            return p <= l;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            angle += 6;

            // перерисовываем
            Invalidate();
        }

        private void squaresNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            squaresNum = Convert.ToInt32((sender as NumericUpDown).Value);
        }
    }
}
