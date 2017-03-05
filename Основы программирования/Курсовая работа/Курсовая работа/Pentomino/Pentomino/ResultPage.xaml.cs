#region using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

#endregion

namespace Pentomino
{
    public sealed partial class BlankPage1 : Page
    {
        #region Variable section

        // Dictionary like Grid > Figures, where Grid — figure in field and Figures — figure in program.
        // «Figures» is struct.
        private Dictionary<Grid, Figures> gridToFigures = new Dictionary<Grid, Figures>();

        // Matrix for current level. Initialize in MainPage().
        private int[,] currentLevel;
        private int currentLevelWidth;
        private int currentLevelHeight;

        // Default ZIndex for each figure, and adjusted for each figure moved to field.
        private int ZIndex = 2;

        // Matrix for Knuth's Algorithm X
        private int[,] solutionMatrix;
        private List<int[]> foundedSolution = new List<int[]>();
        private bool isSolutionFounded = false;

        private Grid figure;
        private CompositeTransform figureTransform;
        private Figures currentFigure;


        #endregion

        #region Struct section

        /* 
         * Struct for mapping figures in field to figures in program
         * @fields:
         *          value           — ID in solutionMatrix
         *          width/height    — ~ in the squared pieces (see XAML)
         *          matrix          — view 
         *          rotatedMatrices — 0°, 90°, 180°, 270°
         *          inField         — is figure in field?
         *          rows            — for Algorithm X matrix
         */
        private struct Figures
        {
            public int value;
            public int width, height;
            public bool[,] matrix;
            public bool[][,] rotatedMatrices;
            public bool inField;
            public List<int> rows;
        }

        #endregion

        public BlankPage1()
        {
            this.InitializeComponent();

            Grid[] grids = { F, I, L, N, P, T, U, V, W, X, Y, Z };
            InitializeFigures(grids);

            currentLevel = new int[10, 6];
            currentLevelHeight = 10;
            currentLevelWidth = 6;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void InitializeFigures(Grid[] grids)
        {
            foreach (Grid grid in grids)
            {
                InitializeFigure(grid);
                Canvas.SetZIndex(grid, ZIndex);
            }
        }

        private void InitializeFigure(Grid grid)
        {
            switch (grid.Name)
            {
                #region F

                /* 
                 * Name: F
                 * Value: 1
                 * Width: 3
                 * Height: 3
                 * View: 
                 *       ­–––
                 *      | ××|
                 *      |×× |
                 *      | × |
                 *       –––
                 */
                case "F":
                    Figures F = new Figures();
                    F.value = 1;
                    F.width = 3;
                    F.height = 3;
                    F.matrix = new bool[,]  
                                {   {false, true, true},
                                    {true, true, false},
                                    {false, true, false}    };
                    F.rotatedMatrices = new bool[4][,];
                    F.rotatedMatrices[0] = new bool[,]
                                {   {false, true, true},
                                    {true, true, false},
                                    {false, true, false}    };
                    F.rotatedMatrices[1] = new bool[,]
                                {   {false, true, false},
                                    {true, true, true},
                                    {false, false, true}    };
                    F.rotatedMatrices[2] = new bool[,]
                                {   {false, true, false},
                                    {false, true, true},
                                    {true, true, false}     };
                    F.rotatedMatrices[3] = new bool[,]
                                {   {true, false, false},
                                    {true, true, true},
                                    {false, true, false}    };
                    gridToFigures[grid] = F;
                    break;

                #endregion

                #region I

                /* 
                 * Name: I
                 * Value: 2
                 * Width: 1
                 * Height: 5
                 * View:
                 *       –
                 *      |×|  
                 *      |×|
                 *      |×|
                 *      |×|
                 *      |×|
                 *       –
                 */
                case "I":
                    Figures I = new Figures();
                    I.value = 2;
                    I.width = 1;
                    I.height = 5;
                    I.matrix = new bool[I.height, I.width];
                    I.rotatedMatrices = new bool[4][,];
                    I.rotatedMatrices[0] = new bool[I.height, I.width];
                    I.rotatedMatrices[1] = new bool[I.width, I.height];
                    I.rotatedMatrices[2] = new bool[I.height, I.width];
                    I.rotatedMatrices[3] = new bool[I.width, I.height];
                    for (int i = 0; i < I.height; i++)
                    {
                        I.matrix[i, 0] = true;
                        I.rotatedMatrices[0][i, 0] = true;
                        I.rotatedMatrices[1][0, i] = true;
                        I.rotatedMatrices[2][i, 0] = true;
                        I.rotatedMatrices[3][0, i] = true;
                    }
                    gridToFigures[grid] = I;
                    break;

                #endregion

                #region L

                /* 
                 * Name: L
                 * Value: 3
                 * Width: 2
                 * Height: 4
                 * View:
                 *       ––
                 *      |× |
                 *      |× |
                 *      |× |
                 *      |××|
                 *       ––
                 */
                case "L":
                    Figures L = new Figures();
                    L.value = 3;
                    L.width = 2;
                    L.height = 4;
                    L.matrix = new bool[,]
                                {   {true, false},
                                    {true, false},
                                    {true, false},
                                    {true, true}    };
                    L.rotatedMatrices = new bool[4][,];
                    L.rotatedMatrices[0] = new bool[,]
                                {   {true, false},
                                    {true, false},
                                    {true, false},
                                    {true, true}    };
                    L.rotatedMatrices[1] = new bool[,]
                                {   {true, true, true, true},
                                    {true, false, false, false} };
                    L.rotatedMatrices[2] = new bool[,]
                                {   {true, true},
                                    {false, true},
                                    {false, true},
                                    {false, true}   };
                    L.rotatedMatrices[3] = new bool[,]
                                {   {false, false, false, true},
                                    {true, true, true, true}    };
                    gridToFigures[grid] = L;
                    break;

                #endregion

                #region N

                /* 
                 * Name: N
                 * Value: 4
                 * Width: 4
                 * Height: 2
                 * View:
                 *       ––––
                 *      |  ××|
                 *      |××× |
                 *       ––––
                 */
                case "N":
                    Figures N = new Figures();
                    N.value = 4;
                    N.width = 4;
                    N.height = 2;
                    N.matrix = new bool[,]
                                {   {false, false, true, true},
                                    {true, true, true, false}   };
                    N.rotatedMatrices = new bool[4][,];
                    N.rotatedMatrices[0] = new bool[,]
                                {   {false, false, true, true},
                                    {true, true, true, false}   };
                    N.rotatedMatrices[1] = new bool[,]
                                {   {true, false},
                                    {true, false},
                                    {true, true},
                                    {false, true}   };
                    N.rotatedMatrices[2] = new bool[,]
                                {   {false, true, true, true},
                                    {true, true, false, false}   };
                    N.rotatedMatrices[3] = new bool[,]
                                {   {true, false},
                                    {true, true},
                                    {false, true},
                                    {false, true}   };
                    gridToFigures[grid] = N;
                    break;

                #endregion

                #region P

                /* 
                 * Name: P
                 * Value: 5
                 * Width: 2
                 * Height: 3
                 * View:
                 *       ––
                 *      |××|
                 *      |××|
                 *      |× |
                 *       ––
                 */
                case "P":
                    Figures P = new Figures();
                    P.value = 5;
                    P.width = 2;
                    P.height = 3;
                    P.matrix = new bool[,]
                                {   {true, true},
                                    {true, true},
                                    {true, false}  };
                    P.rotatedMatrices = new bool[4][,];
                    P.rotatedMatrices[0] = new bool[,]
                                {   {true, true},
                                    {true, true},
                                    {true, false}  };
                    P.rotatedMatrices[1] = new bool[,]
                                {   {true, true, true},
                                    {false, true, true} };
                    P.rotatedMatrices[2] = new bool[,]
                                {   {false, true},
                                    {true, true},
                                    {true, true}  };
                    P.rotatedMatrices[3] = new bool[,]
                                {   {true, true, false},
                                    {true, true, true} };
                    gridToFigures[grid] = P;
                    break;

                #endregion

                #region T

                /* 
                 * Name: T
                 * Value: 6
                 * Width: 3
                 * Height: 3
                 * View: 
                 *       –––
                 *      |×××|
                 *      | × |
                 *      | × |
                 *       –––
                 */
                case "T":
                    Figures T = new Figures();
                    T.value = 6;
                    T.width = 3;
                    T.height = 3;
                    T.matrix = new bool[,]
                                {   {true, true, true},
                                    {false, true, false},
                                    {false, true, false}    };
                    T.rotatedMatrices = new bool[4][,];
                    T.rotatedMatrices[0] = new bool[,]
                                {   {true, true, true},
                                    {false, true, false},
                                    {false, true, false}    };
                    T.rotatedMatrices[1] = new bool[,]
                                {   {false, false, true},
                                    {true, true, true},
                                    {false, false, true}    };
                    T.rotatedMatrices[2] = new bool[,]
                                {   {false, true, false},
                                    {false, true, false},
                                    {true, true, true}  };
                    T.rotatedMatrices[3] = new bool[,]
                                {   {true, false, false},
                                    {true, true, true},
                                    {true, false, false}  };
                    gridToFigures[grid] = T;
                    break;

                #endregion

                #region U

                /* 
                 * Name: U
                 * Value: 7
                 * Width: 3
                 * Height: 2
                 * View:
                 *       –––
                 *      |× ×|
                 *      |×××|
                 *       –––
                 */
                case "U":
                    Figures U = new Figures();
                    U.value = 7;
                    U.width = 3;
                    U.height = 2;
                    U.matrix = new bool[,]
                                {   {true, false, true},
                                    {true, true, true}  };
                    U.rotatedMatrices = new bool[4][,];
                    U.rotatedMatrices[0] = new bool[,]
                                {   {true, false, true},
                                    {true, true, true}  };
                    U.rotatedMatrices[1] = new bool[,]
                                {   {true, true},
                                    {true, false},
                                    {true, true}   };
                    U.rotatedMatrices[2] = new bool[,]
                                {   {true, true, true},
                                    {true, false, true} };
                    U.rotatedMatrices[3] = new bool[,]
                                {   {true, true},
                                    {false, true},
                                    {true, true}   };
                    gridToFigures[grid] = U;
                    break;

                #endregion

                #region V

                /* 
                 * Name: V
                 * Value: 8
                 * Width: 3
                 * Height: 3
                 * View: 
                 *       ­–––
                 *      |×  |
                 *      |×  |
                 *      |×××|
                 *       –––
                 */
                case "V":
                    Figures V = new Figures();
                    V.value = 8;
                    V.width = 3;
                    V.height = 3;
                    V.matrix = new bool[,]
                                {   {true, false, false},
                                    {true, false, false},
                                    {true, true, true}  };

                    V.rotatedMatrices = new bool[4][,];
                    V.rotatedMatrices[0] = new bool[,]
                                {   {true, false, false},
                                    {true, false, false},
                                    {true, true, true}  };
                    V.rotatedMatrices[1] = new bool[,]
                                {   {true, true, true},
                                    {true, false, false},
                                    {true, false, false}    };
                    V.rotatedMatrices[2] = new bool[,]
                                {   {true, true, true},
                                    {false, false, true},
                                    {false, false, true}    };
                    V.rotatedMatrices[3] = new bool[,]
                                {   {false, false, true},
                                    {false, false, true},
                                    {true, true, true}  };
                    gridToFigures[grid] = V;
                    break;

                #endregion

                #region W

                /*
                 * Name: W
                 * Value: 9
                 * Width: 3
                 * Height: 3
                 * View:
                 *       –––
                 *      |×  |
                 *      |×× |
                 *      | ××|
                 *       –––
                 */
                case "W":
                    Figures W = new Figures();
                    W.value = 9;
                    W.width = 3;
                    W.height = 3;
                    W.matrix = new bool[,]
                                {   {true, false, false},
                                    {true, true, false},
                                    {false, true, true}  };
                    W.rotatedMatrices = new bool[4][,];
                    W.rotatedMatrices[0] = new bool[,]
                                {   {true, false, false},
                                    {true, true, false},
                                    {false, true, true}  };
                    W.rotatedMatrices[1] = new bool[,]
                                {   {false, true, true},
                                    {true, true, false},
                                    {true, false, false}  };
                    W.rotatedMatrices[2] = new bool[,]
                                {   {true, true, false},
                                    {false, true, true},
                                    {false, false, true}  };
                    W.rotatedMatrices[3] = new bool[,]
                                {   {false, false, true},
                                    {false, true, true},
                                    {true, true, false}  };
                    gridToFigures[grid] = W;
                    break;

                #endregion

                #region X

                /*
                 * Name: X
                 * Value: 10
                 * Width: 3
                 * Height: 3
                 * View:
                 *       –––
                 *      | × |
                 *      |×××|
                 *      | × |
                 *       –––
                 */
                case "X":
                    Figures X = new Figures();
                    X.value = 10;
                    X.width = 3;
                    X.height = 3;
                    X.matrix = new bool[,]
                                {   {false, true, false},
                                    {true, true, true},
                                    {false, true, false}  };
                    X.rotatedMatrices = new bool[4][,];
                    X.rotatedMatrices[0] = new bool[,]
                                {   {false, true, false},
                                    {true, true, true},
                                    {false, true, false}  };
                    X.rotatedMatrices[1] = new bool[,]
                                {   {false, true, false},
                                    {true, true, true},
                                    {false, true, false}  };
                    X.rotatedMatrices[2] = new bool[,]
                                {   {false, true, false},
                                    {true, true, true},
                                    {false, true, false}  };
                    X.rotatedMatrices[3] = new bool[,]
                                {   {false, true, false},
                                    {true, true, true},
                                    {false, true, false}  };
                    gridToFigures[grid] = X;
                    break;

                #endregion

                #region Y

                /*
                 * Name: Y
                 * Value: 11
                 * Width: 4
                 * Height: 2
                 * View:
                 *       ––––
                 *      |  × |
                 *      |××××|
                 *       ––––
                 */
                case "Y":
                    Figures Y = new Figures();
                    Y.value = 11;
                    Y.width = 4;
                    Y.height = 2;
                    Y.matrix = new bool[,]
                                {   {false, false, true, false},
                                    {true, true, true, true}  };
                    Y.rotatedMatrices = new bool[4][,];
                    Y.rotatedMatrices[0] = new bool[,]
                                {   {false, false, true, false},
                                    {true, true, true, true}  };
                    Y.rotatedMatrices[1] = new bool[,]
                                {   {true, false},
                                    {true, false},
                                    {true, true},
                                    {true, false}   };
                    Y.rotatedMatrices[2] = new bool[,]
                                {   {true, true, true, true},
                                    {false, true, false, false}  };
                    Y.rotatedMatrices[3] = new bool[,]
                                {   {false, true},
                                    {true, true},
                                    {false, true},
                                    {false, true}   };
                    gridToFigures[grid] = Y;
                    break;

                #endregion

                #region Z

                /*
                 * Name: Z
                 * Value: 12
                 * Width: 3
                 * Height: 3
                 * View:
                 *       –––
                 *      |×× |
                 *      | × |
                 *      | ××|
                 *       –––
                 */
                case "Z":
                    Figures Z = new Figures();
                    Z.value = 12;
                    Z.width = 3;
                    Z.height = 3;
                    Z.matrix = new bool[,]
                                {   {true, true, false},
                                    {false, true, false},
                                    {false, true, true}  };
                    Z.rotatedMatrices = new bool[4][,];
                    Z.rotatedMatrices[0] = new bool[,]
                                {   {true, true, false},
                                    {false, true, false},
                                    {false, true, true}  };
                    Z.rotatedMatrices[1] = new bool[,]
                                {   {false, false, true},
                                    {true, true, true},
                                    {true, false, false}  };
                    Z.rotatedMatrices[2] = new bool[,]
                                {   {true, true, false},
                                    {false, true, false},
                                    {false, true, true}  };
                    Z.rotatedMatrices[3] = new bool[,]
                                {   {false, false, true},
                                    {true, true, true},
                                    {true, false, false}  };
                    gridToFigures[grid] = Z;
                    break;

                #endregion
            }

            Figures thisFigure = gridToFigures[grid];
            thisFigure.inField = false;
            thisFigure.rows = new List<int>();
            gridToFigures[grid] = thisFigure;
        }

        private void Object_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            figureTransform.TranslateX += e.Delta.Translation.X;
            figureTransform.TranslateY += e.Delta.Translation.Y;

            //var transform = figure.TransformToVisual(Field);
            //Point relativePosition = transform.TransformPoint(new Point(0, 0));

            //Tester.Text = obj.Name + '\n' + '\n';
            //Tester.Text += "rX: " + (int)relativePosition.X + " rY: " + (int)relativePosition.Y + '\n' + '\n';

            if (e.IsInertial)
            {
                e.Complete();
            }
        }

        private void RightTap_Rotate(object sender, RightTappedRoutedEventArgs e)
        {
            figure = sender as Grid;
            figureTransform = (CompositeTransform)figure.RenderTransform;
            currentFigure = gridToFigures[figure];

            Point cursorPos = e.GetPosition(figure);

            if (currentFigure.inField && (figure.Opacity == 1))
            {
                PasteOrCutFigure(true);
            }
            figure.Opacity = 0.75;
            RaiseZIndex();
            figureTransform.CenterX = cursorPos.X;
            figureTransform.CenterY = cursorPos.Y;
            //figureTransform.CenterX = figure.Width / 2;
            //figureTransform.CenterY = figure.Height / 2;

            figureTransform.Rotation = (figureTransform.Rotation + 90) % 360;
            //bool[,] newMatrix = new bool[currentFigure.width, currentFigure.height];

            //for (int i = 0; i < currentFigure.height; i++)
            //{
            //    for (int j = 0; j < currentFigure.width; j++)
            //    {
            //        newMatrix[j, currentFigure.height - i - 1] = currentFigure.matrix[i, j];
            //    }
            //}

            //currentFigure.matrix = newMatrix;
            currentFigure.matrix = currentFigure.rotatedMatrices[(int)figureTransform.Rotation / 90];
            //int tmp = currentFigure.width;
            //currentFigure.width = currentFigure.height;
            //currentFigure.height = tmp;
            Swap(ref currentFigure.width, ref currentFigure.height);
            gridToFigures[figure] = currentFigure;

            //Tester.Text = ((int)Math.Round(objTransform.Rotation)).ToString();

            ToGrid();
            //PrintMatrix(ClassicOne);

            if (!currentFigure.inField)
            {
                figure.Opacity = 1;
            }
        }

        private void Swap(ref int first, ref int second)
        {
            int tmp = first;
            first = second;
            second = tmp;
        }

        private void ToGrid()
        {
            var transform = figure.TransformToVisual(Field);
            Point relativePosition = transform.TransformPoint(new Point(0, 0));

            int minX = -20, minY = -20;
            int maxX = (int)Math.Round(Field.Width) + 20, maxY = (int)Math.Round(Field.Height) + 20;
            int width = (int)Math.Round(figure.Width), height = (int)Math.Round(figure.Height);
            int roundX = (int)Math.Round(relativePosition.X), roundY = (int)Math.Round(relativePosition.Y);

            switch ((int)Math.Round(figureTransform.Rotation))
            {
                case 0:
                    maxX -= width;
                    maxY -= height;
                    break;
                case 90:
                    maxX -= height;
                    maxY -= width;
                    roundX -= height;
                    break;
                case 180:
                    maxX -= width;
                    maxY -= height;
                    roundX -= width;
                    roundY -= height;
                    break;
                case 270:
                    maxX -= height;
                    maxY -= width;
                    roundY -= width;
                    break;
            }

            //Tester.Text = "roundX: " + roundX + " roundY: " + roundY + '\n' + '\n';

            //Tester.Text += "rX: " + (int)relativePosition.X + " rY: " + (int)relativePosition.Y + '\n' + '\n';

            //Tester.Text += "X: " + (int)objTransform.TranslateX + " Y: " + (int)objTransform.TranslateY + '\n' + '\n';

            //Tester.Text += "minX: " + minX + " maxX: " + maxX + '\n'
            //            + "minY: " + minY + " maxY: " + maxY + '\n' + '\n';

            int gotoX = 0, gotoY = 0;
            //Figures currentFigure = gridToFigures[figure];

            if ((roundX > minX) && (roundX < maxX) && (roundY > minY) && (roundY < maxY))
            {
                int mod40X;
                if (roundX < 0)
                {
                    mod40X = 40 - Math.Abs(roundX);
                }
                else
                {
                    mod40X = (roundX % 40);
                }

                //Tester.Text += "mod40X: " + mod40X + '\n';

                if (mod40X >= 22)
                {
                    gotoX = 41 - mod40X;
                    //objTransform.TranslateX = Math.Round(objTransform.TranslateX + 41 - mod40X); ;
                }
                else
                {
                    gotoX = 1 - mod40X;
                    //objTransform.TranslateX = Math.Round(objTransform.TranslateX - mod40X + 1);
                }

                int mod40Y;
                if (roundY < 0)
                {
                    mod40Y = 40 - Math.Abs(roundY);
                }
                else
                {
                    mod40Y = (roundY % 40);
                }

                //Tester.Text += "mod40Y: " + mod40Y + '\n' + '\n';

                if (mod40Y >= 22)
                {
                    gotoY = 41 - mod40Y;
                    //objTransform.TranslateY = Math.Round(objTransform.TranslateY + 41 - mod40Y);
                }
                else
                {
                    gotoY = 1 - mod40Y;
                    //objTransform.TranslateY = Math.Round(objTransform.TranslateY - mod40Y + 1);
                }

                if (isFreeZone((roundX + gotoX) / 40, (roundY + gotoY) / 40, currentLevel))
                {
                    figureTransform.TranslateX += gotoX;
                    figureTransform.TranslateY += gotoY;
                    figure.Opacity = 1;
                    PasteOrCutFigure(false);
                    Canvas.SetZIndex(figure, 2);
                }
                else
                {
                    //Tester.Text = (roundX + gotoX) / 40 + " " + (roundY + gotoY) / 40 + '\n' + width + " " + height + '\n' + '\n';
                }
                currentFigure.inField = true;
            }
            else
            {
                currentFigure.inField = false;
            }

            gridToFigures[figure] = currentFigure;

            //Tester.Text += "X: " + (int)Math.Round(objTransform.TranslateX) + " Y: " + (int)Math.Round(objTransform.TranslateY) + '\n' + '\n';
        }

        private void PasteOrCutFigure(bool mode)
        {
            var transform = figure.TransformToVisual(Field);
            Point relativePosition = transform.TransformPoint(new Point(0, 0));
            int roundX = (int)Math.Round(relativePosition.X) / 40,
                roundY = (int)Math.Round(relativePosition.Y) / 40;

            int value = mode ? 0 : currentFigure.value;

            switch ((int)Math.Round(figureTransform.Rotation))
            {
                case 90:
                    roundX -= currentFigure.width;
                    break;
                case 180:
                    roundX -= currentFigure.width;
                    roundY -= currentFigure.height;
                    break;
                case 270:
                    roundY -= currentFigure.height;
                    break;
            }

            for (int i = 0; i < currentFigure.height; i++)
            {
                for (int j = 0; j < currentFigure.width; j++)
                {
                    if (currentFigure.matrix[i, j])
                    {
                        currentLevel[i + roundY, j + roundX] = value;
                    }
                }
            }

            gridToFigures[figure] = currentFigure;
        }

        private bool isFreeZone(int roundX, int roundY, int[,] mode)
        {
            for (int i = 0; i < currentFigure.height; i++)
            {
                for (int j = 0; j < currentFigure.width; j++)
                {
                    if ((mode[roundY + i, roundX + j] > 0) && currentFigure.matrix[i, j])
                    {
                        //Tester.Text += "Yeahoo!!!!";
                        return false;
                    }
                }
            }

            return true;
        }

        private void Object_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            ToGrid();
            if (!currentFigure.inField)
            {
                figure.Opacity = 1;
            }
            //PrintMatrix(ClassicOne);
        }

        private void RaiseZIndex()
        {
            Canvas.SetZIndex(figure, ++ZIndex);
            //Tester.Text += "obj: " + obj.ToString() + '\n' + "zindex: " + Canvas.GetZIndex(obj) + '\n';

            //foreach (KeyValuePair<Grid, Figures> grid in pentaminoFigures)
            //{
            //    if (grid.Key != obj)
            //    {
            //        Canvas.SetZIndex(grid.Key, 10);
            //        Tester.Text += "grid: " + grid.ToString() + '\n' + "zindex: " + Canvas.GetZIndex(grid.Key) + '\n';

            //    }
            //}
        }

        private void Object_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            figure = sender as Grid;
            figureTransform = (CompositeTransform)figure.RenderTransform;
            currentFigure = gridToFigures[figure];

            if (currentFigure.inField && (figure.Opacity == 1))
            {
                PasteOrCutFigure(true);
            }
            figure.Opacity = 0.75;
            RaiseZIndex();
        }

        private void PrintMatrix(int[,] matrix)
        {
            Tester.Text += "\n" + "\n";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Tester.Text += matrix[i, j] + " ";
                }
                Tester.Text += "\n";
            }
        }

        private void PrintMatrix(char[,] matrix)
        {
            Tester.Text = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Tester.Text += matrix[i, j] + " ";
                }
                Tester.Text += "\n";
            }
        }

        private void ResetPage(object sender, RoutedEventArgs e)
        {
            var _Frame = Window.Current.Content as Frame;
            _Frame.Navigate(_Frame.Content.GetType());
            _Frame.GoBack();
        }

        private void RecursiveFinder(int number, HashSet<int> notNeedRows, HashSet<int> notNeedCols, List<int[]> solution)
        {
            notNeedCols.Add(number);
            HashSet<int> notNeedCols_backup = new HashSet<int>();
            notNeedCols_backup.UnionWith(notNeedCols);
            HashSet<int> notNeedRows_backup = new HashSet<int>();
            notNeedRows_backup.UnionWith(notNeedRows);
            for (int i = 0; i < solutionMatrix.GetLength(0); i++)
            {
                if ((solutionMatrix[i, number] > 0) && (!notNeedRows.Contains(i)))
                {
                    if (isSolutionFounded) return;
                    List<int> rows = new List<int>();
                    List<int> cols = new List<int>();
                    int[] currentSolution = ArrayExtensions.GetRow(solutionMatrix, i);
                    solution.Add(currentSolution);
                    rows.Add(i);
                    foreach (KeyValuePair<Grid, Figures> pair in gridToFigures)
                    {
                        if (pair.Value.value == solutionMatrix[i, number])
                        {
                            foreach (int row in pair.Value.rows)
                            {
                                rows.Add(row);
                            }
                        }
                    }
                    for (int j = 0; j < solutionMatrix.GetLength(1); j++)
                    {
                        if (solutionMatrix[i, j] > 0)
                        {
                            cols.Add(j);
                            for (int k = 0; k < solutionMatrix.GetLength(0); k++)
                            {
                                if (solutionMatrix[k, j] > 0)
                                {
                                    rows.Add(k);
                                }
                            }
                        }
                    }
                    //notNeedRows = new HashSet<int>();
                    //notNeedRows.UnionWith(notNeedRows_backup);
                    foreach (int row in rows)
                    {
                        notNeedRows.Add(row);
                    }
                    //notNeedCols = new HashSet<int>();
                    //notNeedCols.UnionWith(notNeedCols_backup);
                    foreach (int col in cols)
                    {
                        notNeedCols.Add(col);
                    }
                    int nextCol = 0;
                    for (int j = 0; j < solutionMatrix.GetLength(1); j++)
                    {
                        if (!notNeedCols.Contains(j))
                        {
                            nextCol = j;
                            break;
                        }
                    }
                    if (nextCol > 0)
                    {
                        RecursiveFinder(nextCol, notNeedRows, notNeedCols, solution);
                    }
                    else
                    {
                        isSolutionFounded = true;
                        foundedSolution = solution.ToList();
                    }
                    solution.Remove(currentSolution);
                    notNeedRows = new HashSet<int>();
                    notNeedRows.UnionWith(notNeedRows_backup);
                    notNeedCols = new HashSet<int>();
                    notNeedCols.UnionWith(notNeedCols_backup);
                }
            }
        }

        private int[,] FindAllPositions(Figures figure)
        {
            int maxWidth = currentLevel.GetLength(1);
            int maxHeight = currentLevel.GetLength(0);
            int scale = maxHeight * maxWidth;
            int[,] positions = new int[0, 0];
            bool[,] matrix = figure.matrix;

            for (int iteration = 0; iteration < 4; iteration++)
            {
                int iterationX = maxWidth - matrix.GetLength(1) + 1;
                int iterationY = maxHeight - matrix.GetLength(0) + 1;
                int[,] allPositions = new int[iterationX * iterationY, scale];
                int index = 0;
                int[] startedPosition = new int[scale];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j])
                        {
                            startedPosition[maxWidth * i + j] = figure.value;
                        }
                    }
                }

                for (int i = 0; i < iterationY; i++)
                {
                    for (int j = 0; j < iterationX; j++)
                    {
                        int counter = 0;
                        for (int k = 0; (k < scale) && (counter < 5); k++)
                        {
                            if (startedPosition[k] > 0)
                            {
                                counter++;
                                allPositions[index, k + maxWidth * i + j] = figure.value;
                            }
                            // if 5 iterations done - go out
                        }
                        index++;
                    }
                }

                int prevHeight = positions.GetLength(0);
                int[,] tmp = new int[prevHeight + allPositions.GetLength(0), scale];

                for (int i = 0; i < prevHeight; i++)
                {
                    for (int j = 0; j < scale; j++)
                    {
                        tmp[i, j] = positions[i, j];
                    }
                }

                for (int i = 0; i < allPositions.GetLength(0); i++)
                {
                    for (int j = 0; j < scale; j++)
                    {
                        tmp[i + prevHeight, j] = allPositions[i, j];
                    }
                }

                positions = tmp;

                bool[,] newMatrix = new bool[matrix.GetLength(1), matrix.GetLength(0)];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        newMatrix[j, matrix.GetLength(0) - i - 1] = matrix[i, j];
                    }
                }

                matrix = newMatrix;
            }

            return positions;
        }

        private int[,] BuildMatrix()
        {
            int[,] fullMatrix = new int[0, 0];
            foreach (KeyValuePair<Grid, Figures> pair in gridToFigures)
            {
                int[,] positions = FindAllPositions(pair.Value);
                int prevHeight = fullMatrix.GetLength(0);
                int[,] tmp = new int[prevHeight + positions.GetLength(0), positions.GetLength(1)];

                for (int i = 0; i < prevHeight; i++)
                {
                    for (int j = 0; j < positions.GetLength(1); j++)
                    {
                        tmp[i, j] = fullMatrix[i, j];
                    }
                }

                for (int i = 0; i < positions.GetLength(0); i++)
                {
                    for (int j = 0; j < positions.GetLength(1); j++)
                    {
                        tmp[i + prevHeight, j] = positions[i, j];
                    }
                    pair.Value.rows.Add(i + prevHeight);
                }

                fullMatrix = tmp;
            }

            return fullMatrix;
        }

        private void FindOneSolution(object sender, RoutedEventArgs e)
        {
            solutionMatrix = BuildMatrix();
            HashSet<int> notNeedRows = new HashSet<int>();
            HashSet<int> notNeedCols = new HashSet<int>();
            List<int[]> solution = new List<int[]>();
            //21k+
            Stopwatch watcher = new Stopwatch();
            watcher.Reset();
            watcher.Start();
            RecursiveFinder(0, notNeedRows, notNeedCols, solution);
            watcher.Stop();
            Tester.Text = watcher.ElapsedMilliseconds.ToString();

            int[,] result = new int[2, 12];

            foreach (int[] solve in foundedSolution)
            {
                for (int i = 0; i < solve.Length; i++)
                {
                    if (solve[i] > 0)
                    {
                        if (result[0, solve[i] - 1] > i)
                        {
                            result[0, solve[i] - 1] = i;
                        }

                        if (result[1, solve[i] - 1] < i)
                        {
                            result[1, solve[i] - 1] = i;
                        }

                        currentLevel[i / 6, i % 6] = solve[i];
                    }
                }
            }

            //for (int i = 0; i < 12; i++)
            //{
            //    foreach (KeyValuePair<Grid, Figures> pair in pentaminoFigures)
            //    {
            //        if (pair.Value.value == (i + 1))
            //        {
            //            bool equals = false;
            //            while(!equals)
            //            {
            //                 for (int j = 0; j <= ((result[1, i] / 6) - ; j++)
            //                 {
            //                     for (int k = (result[0, i] % 6); k <= (result[1, i] % 6); k++)
            //                     {
            //                        if (ClassicOne[j, k])
            //                     }
            //                 }
            //                 equals = true;
            //                bool[,] newMatrix = new bool[currentFigure.width, currentFigure.height];

            //for (int i = 0; i < currentFigure.height; i++)
            //{
            //    for (int j = 0; j < currentFigure.width; j++)
            //    {
            //        newMatrix[j, currentFigure.height - i - 1] = currentFigure.matrix[i, j];
            //    }
            //}

            //currentFigure.matrix = newMatrix;
            //int tmp = currentFigure.width;
            //currentFigure.width = currentFigure.height;
            //currentFigure.height = tmp;
            //pentaminoFigures[obj] = currentFigure;
            //            }

            //        }
            //    }
            //}

            PrintMatrix(currentLevel);
            Tester.Text += '\n' + isSolutionFounded.ToString();
        }

        private void MoveFigure(Figures movedFigure)
        {
            figure = F;
            var transform = figure.TransformToVisual(Field);
            Point relativePosition = transform.TransformPoint(new Point(0, 0));
            figureTransform = (CompositeTransform)figure.RenderTransform;
            int x = 3, y = 5;
            Tester.Text = relativePosition.X + "\n" + relativePosition.Y;
            figureTransform.TranslateX -= (relativePosition.X - 1 - x * 40);
            figureTransform.TranslateY -= (relativePosition.Y - 1 - y * 40);

            Tester.Text += "\n" + relativePosition.X + "\n" + relativePosition.Y;

        }
    }

    public static class ArrayExtensions
    {
        public static T[] GetRow<T>(T[,] data, int i)
        {
            T[] row = new T[data.GetLength(1)];
            //T[] row = new T[data.GetLength(i)];
            for (int j = 0; j < /*data.GetLength(i)*/ row.Length; j++)
            {
                row[j] = data[i, j];
            }
            return row;
        }
    }
}