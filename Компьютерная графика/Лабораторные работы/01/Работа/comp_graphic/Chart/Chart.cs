using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Chart
{
    public partial class Chart : UserControl
    {
        private float _min = -3.500201F;
        private float _max = -3.5001F;
        private int _step = 150;
        private Color _color = Color.Black;

        public Chart()
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
            if (this.DesignMode)
            {
                base.OnPaint(e);
                return;
            }

            Step = 150;

            base.OnPaint(e);

            var graph = new Graph();

            graph.Func = f;

            graph.X = new XAxis(Color.Black, Min, Max, Step);

            graph.Y = new YAxis(Color.Black, Step);

            graph.Curve = new Curve(new Function(f), Color);

            graph.Draw(e.Graphics);
        }

        private float f(float x)
        {
            // x + sin(x)
            return x * x * x;
        }

        public float Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        public float Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
            }
        }

        public int Step
        {
            get
            {
                return _step;
            }
            set
            {
                _step = value;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
    }

    public delegate float Function(float x);

    public class Graph
    {
        public XAxis X { get; set; }
        public YAxis Y { get; set; }
        public Curve Curve { get; set; }
        public Font Font { get; set; }
        public Function Func { get; set; }

        public Graph()
        {
        }

        public void Draw(Graphics g)
        {
            int axisWidth = 1;
            int curveWidth = 2;

            if (Font == null)
            {
                Font = new Font("Consolas", 10);
            }

            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Near;

            SizeF stringMeasure = g.MeasureString("99.999999", Font);

            var clip = g.VisibleClipBounds;
            // отступ снизу для оси ординат
            var paddingBottom = stringMeasure.Height + 4;
            // отступ слева для оси абсцисс
            var paddingLeft = clip.Left + stringMeasure.Width + 3;

            var tmp = g.VisibleClipBounds;
            tmp.Offset(paddingLeft, 0);
            g.Clip = new Region(tmp);

            X.Steps = new List<float>();
            X.Draw(g, axisWidth, Font);

            tmp = clip;
            tmp.Offset(0, -paddingBottom);
            g.Clip = new Region(tmp);

            Curve.Xs = X.Steps;
            Curve.XMin = X.Min;
            Curve.XMax = X.Max;

            var values = X.Steps;
            values = values.Select(x => Func(x)).ToList();

            Curve.Ys = values;

            Y.Steps = new List<float>();
            Y.Min = values.Min();
            Y.Max = values.Max();

            Y.Draw(g, axisWidth, Font);

            tmp = clip;
            tmp.Offset(paddingLeft, -paddingBottom);
  
            g.Clip = new Region(tmp);

            Curve.YMin = Y.Min;
            Curve.YMax = Y.Max;

            Curve.Draw(g, curveWidth, stringMeasure);
        }
    }

    public class Curve
    {
        public Function F { get; set; }
        public Color Color { get; set; }
        public List<float> Xs { get; set; }
        public List<float> Ys { get; set; }
        public float XMin { get; set; }
        public float XMax { get; set; }
        public float YMin { get; set; }
        public float YMax { get; set; }

        public Curve(Function func, Color c)
        {
            F = func;
            Color = c;
        }

        internal void Draw(Graphics g, int width, SizeF offsets)
        {
            var pen = new Pen(Color, width);
            var clip = g.VisibleClipBounds;

            var xReal = (XMax - XMin) / clip.Width;
            var yReal = (YMax - YMin) / clip.Height;

            var points = Xs.Zip(Ys, (x, y) => new PointF(
                            // clip.X -- отступ для оси ординат
                            (x - XMin) / xReal + clip.X,
                            (YMax - y) / yReal 
                         ));

            g.DrawCurve(pen, points.ToArray<PointF>());
        }
    }

    public class Axis
    {
        public Color Color { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public int Step { get; set; }
        public List<float> Steps { get; set; }

        public Axis(Color color, float min, float max, int h)
        {
            Color = color;
            Min = min;
            Max = max;
            Step = h;
        }

        protected uint CountPrecision(float f)
        {
            uint zeros = 0;
            var numStr = (f - Math.Truncate(f)).ToString();
            string num;

            if (numStr.Length > 2)
            {
                num = numStr.Substring(2);
            }
            else
            {
                num = null;
            }

            while (num != null && num[0] == '0')
            {
                zeros++;
                num = num.Substring(1);
            }

            return zeros > 0 ? ++zeros : zeros;
        }

        protected void CalculateSteps(ref float k, ref float h)
        {
            int m = 1, n = 0, step;
            float prev = k;
            var ms = new int[] { 1, 2, 5 };
            var found = false;

            if (k > h)
            {
                step = -1;
                ms = ms.Reverse().ToArray<int>();

                n += step;

                while (!found)
                {
                    float new1 = k * (float)Math.Pow((double)10, (double)n);
                    float[] arr = ms.Select(x => x * new1).ToArray();

                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] < h)
                        {
                            found = true;
                            prev = arr[i];
                            m = ms[i];

                            break;
                        }
                    }

                    if (!found)
                    {
                        prev = arr[arr.Length - 1];
                        m = ms[arr.Length - 1];
                        n += step;
                    }
                }

                k = m * (float)Math.Pow((double)10, (double)n);
            }
            else
            {
                step = 1;

                while (!found)
                {
                    float new1 = k * (float)Math.Pow((double)10, (double)n);
                    float[] arr = ms.Select(x => x * new1).ToArray();

                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] > h)
                        {
                            found = true;

                            if (i != 0)
                            {
                                prev = arr[i - 1];
                                m = ms[i - 1];
                                n++;
                            }

                            break;
                        }
                    }

                    if (!found)
                    {
                        prev = arr.Last();
                        m = ms.Last();
                        n += step;
                    }
                }

                k = m * (float)Math.Pow((double)10, (double)(n - 1));
            }

            h = prev;
        }
    }

    public class XAxis : Axis
    {
        public XAxis(Color color, float min, float max, int h)
            : base(color, min, max, h)
        {

        }

        internal void Draw(Graphics g, int width, Font font)
        {
            var clip = g.VisibleClipBounds;
            var pen = new Pen(Color, width);

            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Near;
            SizeF stringMeasure = g.MeasureString("99.999999", font);

            // 3 здесь отступ от текста до риски
            var bottom = clip.Bottom - stringMeasure.Height - 3;
            
            PointF pStart = new PointF(clip.Left, bottom),
                   pEnd = new PointF(clip.Right, bottom);

            // отрисовываем саму ось
            g.DrawLine(pen, pStart, pEnd);

            float stepWorld = clip.Width / (Max - Min),
                  stepReal = Step;

            CalculateSteps(ref stepWorld, ref stepReal);

            Steps.Add(Min);
            float tmpWorld = (float)Math.Round(Min / stepWorld) * stepWorld,
                  // здесь -1, т. к. иначе последняя риска не влезает
                  tmpReal = clip.Left + stepReal - 1,
                  stepHalfSize = 5,
                  stepWidth = 1;

            if (tmpWorld < Min)
            {
                Steps.Add(tmpWorld);
                tmpWorld += stepWorld;
            }
            
            // просто какая-то магия
            tmpReal -= (1 - (tmpWorld - Min) / stepWorld) * stepReal;

            while (tmpWorld <= Max)
            {
                Steps.Add(tmpWorld);

                PointF pStepTop = new PointF(tmpReal,
                                             bottom - stepHalfSize),
                       pStepBottom = new PointF(tmpReal,
                                                bottom + stepHalfSize),
                       pGridTop = new PointF(tmpReal, 0);

                Pen penStep = new Pen(Color, stepWidth),
                    penGrid = new Pen(ControlPaint.LightLight(Color),
                                      stepWidth);

                g.DrawLine(penGrid, pGridTop, pStepTop);
                g.DrawLine(penStep, pStepTop, pStepBottom);

                var num = tmpWorld.ToString("G6");
                g.DrawString(num,
                             font,
                             new SolidBrush(ControlPaint.Light(Color)),
                             pStepBottom,
                             format);
                
                tmpWorld += stepWorld;
                tmpReal += stepReal;
            }

            Steps.Add(Max);
        }
    }

    public class YAxis : Axis
    {
        public YAxis(Color color, int h, float min = 0, float max = 0)
            : base(color, min, max, h)
        {

        }

        internal void Draw(Graphics g, int width, Font font)
        {
            var clip = g.VisibleClipBounds;
            var pen = new Pen(Color, width);

            var format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            format.LineAlignment = StringAlignment.Center;
            SizeF stringMeasure = g.MeasureString("99.999999", font);

            // 3 здесь отступ от текста до риски
            var left = clip.Left + stringMeasure.Width + 3;

            PointF pStart = new PointF(left, 0),
                   pEnd = new PointF(left, clip.Bottom);

            g.DrawLine(pen, pStart, pEnd);

            float stepWorld = clip.Height / (Max - Min),
                  stepReal = Step;

            CalculateSteps(ref stepWorld, ref stepReal);

            Steps.Add(Min);
            float tmpWorld = (float)Math.Round(Min / stepWorld) * stepWorld,
                  // здесь -1, т. к. иначе последняя риска не влезает
                  tmpReal = clip.Top + stepReal - 1,
                  stepHalfSize = 5,
                  stepWidth = 1;

            if (tmpWorld < Min) tmpWorld += stepWorld;

            Steps.Add(tmpWorld);
            tmpWorld += stepWorld;

            var bottom = clip.Bottom - tmpReal;

            while (tmpWorld <= Max)
            {
                Steps.Add(tmpWorld);

                PointF pStepLeft = new PointF(left - stepHalfSize,
                                             bottom),
                       pStepRight = new PointF(left + stepHalfSize,
                                                bottom),
                       pGridRight = new PointF(clip.Right, bottom);

                Pen penStep = new Pen(Color, stepWidth),
                    penGrid = new Pen(ControlPaint.LightLight(Color),
                                      stepWidth);

                g.DrawLine(penGrid, pStepRight, pGridRight);
                g.DrawLine(penStep, pStepLeft, pStepRight);

                var num = tmpWorld.ToString("G6");
                g.DrawString(num,
                             font,
                             new SolidBrush(ControlPaint.Light(Color)),
                             pStepLeft,
                             format);

                tmpWorld += stepWorld;
                tmpReal += stepReal;
                bottom = clip.Bottom - tmpReal;
            }

            Steps.Add(Max);
        }
    }
}
