using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Chess
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        #region Global Variables
        int boardRows = 8;
        const int boardHeight = 100;
        int boardWidth = 100;
        bool done = false;
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
            createChessBoard();
            peiceSetup();
        }

        private void createChessBoard()
        {
            //removes board if it already exists
            try
            {
                grdContainer.Children.Remove(FindName("ChessBoard") as Grid);
            }
            catch
            {
            }

            //create new grid named chessBoard and assign default values.
            Grid grdChessBoard = new Grid();
            grdChessBoard.Name = "ChessBoard";
            grdChessBoard.Height = boardHeight * boardRows;
            grdChessBoard.Width = boardWidth * boardRows;
            grdChessBoard.Margin = new Thickness(5);
            grdChessBoard.Background = new SolidColorBrush(Colors.Gray);
            grdChessBoard.SetValue(Grid.ColumnProperty, 1);
            grdChessBoard.SetValue(Grid.RowProperty, 1);
            grdChessBoard.HorizontalAlignment = HorizontalAlignment.Center;
            grdChessBoard.VerticalAlignment = VerticalAlignment.Top;

            // add rows and coloums to chess board grid
            for (int i = 0; i < boardRows; i++)
            {
                grdChessBoard.ColumnDefinitions.Add(new ColumnDefinition());
                grdChessBoard.RowDefinitions.Add(new RowDefinition());
            }

            // add the chessboard to container grid
            grdContainer.Children.Add(grdChessBoard);

            // add a border to each square on the grid

            createBorders();



        }

        public void createBorders()
        {
            Border border;
            Grid board = FindName("ChessBoard") as Grid;
            int R, C;

            for (R = 0; R < boardRows; R++) // rows
            {
                for (C = 0; C < boardRows; C++)  // col on row
                {
                    border = new Border();
                    // give it height, width, horizontal & vertical align in centre
                    border.Height = boardHeight * 0.95;
                    border.Width = boardWidth * 0.95;
                    // asign name to peice based off of row and coloum
                    border.Name = R.ToString() + C.ToString();
                    border.HorizontalAlignment = HorizontalAlignment.Center;
                    border.VerticalAlignment = VerticalAlignment.Center;
                    // set the col and row propertys
                    border.SetValue(Grid.RowProperty, R);
                    border.SetValue(Grid.ColumnProperty, C);
                    // colour board
                    border.Background = new SolidColorBrush(Colors.DarkGray);
                    if (0 == (R + C) % 2)
                    {
                        border.Background = new SolidColorBrush(Colors.White);
                    }
                    //add to chess board 
                    board.Children.Add(border);

                } // end C
            } // end of R
        }


        private void peiceSetup()
        {

            Rectangle peice;
            Grid board = FindName("ChessBoard") as Grid;

            ImageBrush iBrush = new ImageBrush();


            //black pawns
            ImageBrush bpBrush = new ImageBrush();
            bpBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/blackpawn.png"));
            for (int i = 0; i < 8; i++)
            {
                peice = new Rectangle();
                peice.Name = "blackPawn" + i;
                peice.Width = boardWidth * 0.85;
                peice.Height = boardHeight * 0.85;
                peice.Fill = bpBrush;
                peice.HorizontalAlignment = HorizontalAlignment.Center;
                peice.VerticalAlignment = VerticalAlignment.Center;
                peice.SetValue(Grid.RowProperty, 1);
                peice.SetValue(Grid.ColumnProperty, i);
                peice.Tapped += Peice_Tapped;
                peice.Tag = "1" + i;
                board.Children.Add(peice);

            }

            //white pawns
            ImageBrush wpBrush = new ImageBrush();
            wpBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/whitepawn.png"));
            for (int i = 0; i < 8; i++)
            {
                peice = new Rectangle();
                peice.Name = "whitePawn" + i;
                peice.Width = boardWidth * 0.85;
                peice.Height = boardHeight * 0.85;
                peice.Fill = wpBrush;
                peice.HorizontalAlignment = HorizontalAlignment.Center;
                peice.VerticalAlignment = VerticalAlignment.Center;
                peice.SetValue(Grid.RowProperty, 6);
                peice.SetValue(Grid.ColumnProperty, i);
                peice.Tapped += Peice_Tapped;
                peice.Tag = "6" + i;
                board.Children.Add(peice);
            }

            //black rooks
            ImageBrush brBrush = new ImageBrush();
            brBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/blackRook.png"));

            peice = new Rectangle();
            peice.Name = "blackRook1";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = brBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 0);
            peice.SetValue(Grid.ColumnProperty, 0);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "00";
            board.Children.Add(peice);

            peice = new Rectangle();
            peice.Name = "blackRook2";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = brBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 0);
            peice.SetValue(Grid.ColumnProperty, 7);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "07";
            board.Children.Add(peice);

            //white rooks
            ImageBrush wrBrush = new ImageBrush();
            wrBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/whiteRook.png"));

            peice = new Rectangle();
            peice.Name = "whiteRook1";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = wrBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 7);
            peice.SetValue(Grid.ColumnProperty, 0);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "70";
            board.Children.Add(peice);

            peice = new Rectangle();
            peice.Name = "whiteRook2";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = wrBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 7);
            peice.SetValue(Grid.ColumnProperty, 7);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "77";
            board.Children.Add(peice);
            //black knights
            ImageBrush bkBrush = new ImageBrush();
            bkBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/blackKnight.png"));

            peice = new Rectangle();
            peice.Name = "blackKnight1";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = bkBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 0);
            peice.SetValue(Grid.ColumnProperty, 1);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "01";
            board.Children.Add(peice);

            peice = new Rectangle();
            peice.Name = "blackKnight2";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = bkBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 0);
            peice.SetValue(Grid.ColumnProperty, 6);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "06";
            board.Children.Add(peice);
            //white knights
            ImageBrush wkBrush = new ImageBrush();
            wkBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/whiteKnight.png"));

            peice = new Rectangle();
            peice.Name = "whiteKnight1";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = wkBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 7);
            peice.SetValue(Grid.ColumnProperty, 1);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "71";
            board.Children.Add(peice);

            peice = new Rectangle();
            peice.Name = "whiteKnight2";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = wkBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 7);
            peice.SetValue(Grid.ColumnProperty, 6);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "76";
            board.Children.Add(peice);
            //black bishops
            ImageBrush bbBrush = new ImageBrush();
            bbBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/blackBishop.png"));

            peice = new Rectangle();
            peice.Name = "blackBishop1";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = bbBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 0);
            peice.SetValue(Grid.ColumnProperty, 2);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "02";
            board.Children.Add(peice);

            peice = new Rectangle();
            peice.Name = "blackBishop2";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = bbBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 0);
            peice.SetValue(Grid.ColumnProperty, 5);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "05";
            board.Children.Add(peice);
            //white bishops
            ImageBrush wbBrush = new ImageBrush();
            wbBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/whiteBishop.png"));

            peice = new Rectangle();
            peice.Name = "whiteBishop1";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = wbBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 7);
            peice.SetValue(Grid.ColumnProperty, 2);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "72";
            board.Children.Add(peice);

            peice = new Rectangle();
            peice.Name = "whiteBishop2";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = wbBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 7);
            peice.SetValue(Grid.ColumnProperty, 5);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "75";
            board.Children.Add(peice);
            //black queen
            ImageBrush bqBrush = new ImageBrush();
            bqBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/blackqueen.png"));
            peice = new Rectangle();
            peice.Name = "blackQueen";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = bqBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 0);
            peice.SetValue(Grid.ColumnProperty, 3);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "03";
            board.Children.Add(peice);
            //white queen
            ImageBrush wqBrush = new ImageBrush();
            wqBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/whitequeen.png"));
            peice = new Rectangle();
            peice.Name = "whiteQueen";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = wqBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 7);
            peice.SetValue(Grid.ColumnProperty, 4);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "74";
            board.Children.Add(peice);
            //black king
            ImageBrush bkingBrush = new ImageBrush();
            bkingBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/blackKing.png"));
            peice = new Rectangle();
            peice.Name = "blackKing";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = bkingBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 0);
            peice.SetValue(Grid.ColumnProperty, 4);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "04";
            board.Children.Add(peice);
            //white king
            ImageBrush wkingBrush = new ImageBrush();
            wkingBrush.ImageSource = new BitmapImage(new Uri("ms-appx://Chess/PeiceImages/whiteKing.png"));
            peice = new Rectangle();
            peice.Name = "whiteKing";
            peice.Width = boardWidth * 0.85;
            peice.Height = boardHeight * 0.85;
            peice.Fill = wkingBrush;
            peice.HorizontalAlignment = HorizontalAlignment.Center;
            peice.VerticalAlignment = VerticalAlignment.Center;
            peice.SetValue(Grid.RowProperty, 7);
            peice.SetValue(Grid.ColumnProperty, 3);
            peice.Tapped += Peice_Tapped;
            peice.Tag = "73";
            board.Children.Add(peice);

            foreach(var p in board.Children)
            {
                try
                {
                    Rectangle tp = (Rectangle)p;
                    Canvas.SetZIndex(tp, 98);
                }
                catch
                {

                }
            }

        }

        Rectangle movePeice;
        private void Peice_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //reset borders

            Grid board = FindName("ChessBoard") as Grid;
            foreach (var b in board.Children)
            {
                try
                {
                    Border brdr = (Border)b;
                    board.Children.Remove(brdr);
                }
                catch { }
            }
            createBorders();

            Rectangle current = (Rectangle)sender;
            movePeice = current;
            //pawnMovement
            if (current.Name.Contains("Pawn"))
            {
                int moveToC;
                int moveToR;
                try
                {
                    if (current.Name.Contains("white"))
                    {
                        moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                        moveToC = (int)current.GetValue(Grid.ColumnProperty);
                    }
                    else
                    {
                        moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                        moveToC = (int)current.GetValue(Grid.ColumnProperty);
                    }
                    Border border;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
      
            }
            if (current.Name.Contains("Rook"))
            {
                int moveToC;
                int moveToR;
                Border border;
                //left
                moveToR = (int)current.GetValue(Grid.RowProperty);
                moveToC = (int)current.GetValue(Grid.ColumnProperty)-1;
                while (moveToC >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }
                    if (done)
                    {
                        break;
                    }


                }
                //right
                moveToR = (int)current.GetValue(Grid.RowProperty);
                moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                while (moveToC < 8)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC++;
                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }
                    if (done)
                    {
                        break;
                    }


                }
                //down
                moveToR = (int)current.GetValue(Grid.RowProperty)-1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty);
                while (moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToR--;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
                //up
                moveToR = (int)current.GetValue(Grid.RowProperty)+1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty);
                while (moveToR < 8)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToR++;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }




            }
            if (current.Name.Contains("Knight"))
            {
                int moveToC;
                int moveToR;
                Border border;
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) + 2;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) + 2;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) - 2;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) - 2;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) + 2;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) - 2;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) - 2;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) + 2;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                }
                catch { }
            }
            if (current.Name.Contains("Bishop"))
            {
                int moveToC;
                int moveToR;
                Border border;
                //up
                moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                while (moveToC < 8 && moveToR < 8 && moveToC >= 0 && moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>() )
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
                moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                while (moveToC < 8 && moveToR < 8 && moveToC >= 0 && moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC++;
                    moveToR++;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
                moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                while (moveToC < 8 && moveToR < 8 && moveToC >= 0 && moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC++;
                    moveToR--;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
                moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                while (moveToC < 8 && moveToR < 8 && moveToC >= 0 && moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR--;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
            }
            if (current.Name.Contains("Queen"))
            {
                int moveToC;
                int moveToR;
                Border border;
                //up
                moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                while (moveToC < 8 && moveToR < 8 && moveToC >= 0 && moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
                moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                while (moveToC < 8 && moveToR < 8 && moveToC >= 0 && moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC++;
                    moveToR++;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
                moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                while (moveToC < 8 && moveToR < 8 && moveToC >= 0 && moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC++;
                    moveToR--;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
                moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                while (moveToC < 8 && moveToR < 8 && moveToC >= 0 && moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR--;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }
                }

                //left
                moveToR = (int)current.GetValue(Grid.RowProperty);
                moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                while (moveToC >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }
                    if (done)
                    {
                        break;
                    }


                }
                //right
                moveToR = (int)current.GetValue(Grid.RowProperty);
                moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                while (moveToC < 8)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC++;
                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }
                    if (done)
                    {
                        break;
                    }


                }
                //down
                moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty);
                while (moveToR >= 0)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToR--;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }
                //up
                moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                moveToC = (int)current.GetValue(Grid.ColumnProperty);
                while (moveToR < 8)
                {
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToR++;

                    done = false;
                    foreach (Rectangle peice in board.Children.OfType<Rectangle>())
                    {
                        if (border.Name == (string)peice.Tag)
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }

                }


            }
            if (current.Name.Contains("King"))
            {
                int moveToC;
                int moveToR;
                Border border;

                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty);
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) + 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty);
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty) - 1;
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty);
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) + 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;
                }
                catch { }
                try
                {
                    moveToR = (int)current.GetValue(Grid.RowProperty);
                    moveToC = (int)current.GetValue(Grid.ColumnProperty) - 1;
                    border = FindName(moveToR.ToString() + moveToC.ToString()) as Border;
                    border.Background = new SolidColorBrush(Colors.AliceBlue);
                    border.Tag = "valid";
                    border.Tapped += Brdr_Tapped;
                    moveToC--;
                    moveToR++;
                }
                catch { }


            }
        }

        private void Brdr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Border current = (Border)sender;

            movePeice.SetValue(Grid.RowProperty, current.GetValue(Grid.RowProperty));
            movePeice.SetValue(Grid.ColumnProperty, current.GetValue(Grid.ColumnProperty));
            movePeice.Tag = (String)movePeice.GetValue(Grid.RowProperty).ToString() + movePeice.GetValue(Grid.ColumnProperty).ToString();
            Grid board = FindName("ChessBoard") as Grid;
            foreach (var b in board.Children)
            {
                try
                {
                    Border brdr = (Border)b;
                    board.Children.Remove(brdr);
                }
                catch { }
            }
            createBorders();

        }
    }
}
