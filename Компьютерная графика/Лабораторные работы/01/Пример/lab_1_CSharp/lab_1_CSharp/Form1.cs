using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace lab_1_CSharp
{
	public partial class Form1 : Form
	{
		// Угол поворота лепестков
		int angle = 10;

		public Form1()
		{
			InitializeComponent();
			// Включаем двойную буферизацию
			this.SetStyle(ControlStyles.DoubleBuffer |
				 ControlStyles.UserPaint |
				 ControlStyles.AllPaintingInWmPaint | 
				 ControlStyles.ResizeRedraw, // Перерисовывать при изменении размера окна
				 true);
			this.UpdateStyles();
		}



		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			RectangleF bounds = g.VisibleClipBounds;

			// Радиус большей окружности
			float radius;
			if (bounds.Width > bounds.Height)
				radius = (bounds.Height - 20) / 2; // Отступ от краёв - 10 пикселей
			else radius = (bounds.Width - 20) / 2;

			// Если размеры окна маленькие, ничего не выводить
			if (bounds.Width < 30 || bounds.Height < 30)
				return;

			// Координаты центра окружности
			PointF center = new PointF(bounds.Width / 2, bounds.Height / 2);

			// Задаём область прорисовки круга
			RectangleF rect = new RectangleF(center.X - radius, center.Y - radius, radius*2, radius*2);

			// Рисуем круг
			g.FillEllipse(Brushes.OrangeRed, rect);
			
			// Рисуем ещё три круга меньшего диаметра и разными цветами
			float[] radiuses = new float[3] {radius*0.9f, radius*0.3f, radius*0.2f };
			Brush[] brushes = new Brush[3] { Brushes.Black, Brushes.OrangeRed, Brushes.Black};

			for (int i = 0; i < 3; i++) {
				rect = new RectangleF(center.X - radiuses[i], center.Y - radiuses[i], radiuses[i] * 2, radiuses[i] * 2);
				g.FillEllipse(brushes[i], rect);
				// Отсекаем лепестки
				if (i == 1) {
					Rectangle rect2 = new Rectangle((int)(center.X - radius*0.95), (int)(center.Y - radius*0.95), (int)(radius*0.95*2), (int)(radius*0.95*2));
			    g.FillPie(Brushes.OrangeRed, rect2, angle, 60);
					g.FillPie(Brushes.OrangeRed, rect2, angle + 120, 60);
					g.FillPie(Brushes.OrangeRed, rect2, angle + 240, 60);
				}
			}

			base.OnPaint(e);
		}



		// Обработчик события прокрутки колеса мыши
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			angle -= e.Delta / 60;
			Invalidate(); // Обновляем окно
			base.OnMouseWheel(e);
		}
	}
}