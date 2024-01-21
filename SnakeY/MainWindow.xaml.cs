using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeY
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point ApplePosition;

        List<Snake> snake = new List<Snake>();
        public int Score  { get; set; }

       

        Point SnakePosition;


        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
           
            timer.Tick += Timer_Tick;

            timer.Interval = TimeSpan.FromSeconds(0.2);

            timer.Start();

            snake.Add(new Snake(10, 10, 10));

            snake[0].MovingSide = Side.Bottom;

            snake[0].ellipse = HeadEllipse;

            this.KeyDown += Player_keyDown;

           ApplePosition= SpawnApples(100,100);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {//Рост змейки и наследственность хвоста
            var oldPositionTail = new Point();

            for (int i = 0; i < snake.Count; i++)
            {
               
                    if (i == 0|| snake.Count==1) 
                    {
                        oldPositionTail = snake[0].Move();
                    }
                    else
                    {
                      oldPositionTail=  snake[i].TailMove(oldPositionTail);
                    }
                
            }
            


           //отрисовка змейки 

            for (int i = 0; i < snake.Count; i++)
            {
                Canvas.SetTop(snake[i].ellipse, snake[i].PositionY);
                Canvas.SetLeft(snake[i].ellipse, snake[i].PositionX);
            }
            //проверка на съеденное яблоко и увеличение змейки

            if (Math.Abs((snake[0].PositionX - ApplePosition.X))<=6 && Math.Abs((snake[0].PositionY - ApplePosition.Y)) <= 6) 
            {
               canvas.Children.RemoveAt(0);

                

                Score += 10;
                  
                Timer_Tick(sender, new EventArgs());
                
                snake.Add(new Snake((int)ApplePosition.X, (int)ApplePosition.Y, 10)
                {
                    ellipse = new Ellipse
                    {
                        Width = 10,
                        Height = 10,
                        Fill = Brushes.Aqua
                    }
                });
                ApplePosition = SpawnApples();

                canvas.Children.Insert(1, snake[snake.Count - 1].ellipse);
  
            }
            //координаты внизу
            label.Content = $"Snake X {snake[0].PositionX} Y {snake[0].PositionY} Apple X{ApplePosition.X} Y {ApplePosition.Y}";
            //границы карты и проигрыш если выходишь за рамки
            if (snake[0].PositionX < 5 || snake[0].PositionX > 590 || snake[0].PositionY < 5 || snake[0].PositionY > 890) 
            {
                MessageBox.Show("YOU LOSE");
                this.Close();
            }
            //проигрыш если змейка скушала сама себя
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[0].PositionX == snake[i].PositionX && snake[0].PositionY == snake[i].PositionY) 
                {
                    MessageBox.Show("YOU LOSE");
                    this.Close();
                }
            }
        }
        
        private void Player_keyDown(object sender, KeyEventArgs e) 
        {
            switch (e.Key)
            {
                case Key.Up:
                    snake[0].MovingSide = Side.Top;
                    break;
                case Key.Down:
                    snake[0].MovingSide = Side.Bottom;
                    break;
                case Key.Left:
                    snake[0].MovingSide = Side.Left;
                    break;
                case Key.Right:
                    snake[0].MovingSide = Side.Right;
                    break;
            }
        }

        private Point SpawnApples() 
        {
            
            var rnd = new Random();

            var ApplePoint = new Point(rnd.Next(0, 89)*10, rnd.Next(0, 59)*10);

            

            var Apple = new Ellipse()
            {
                Height = 10,
                Width = 10,
                Fill = Brushes.Red
                
            };

            Canvas.SetTop(Apple,ApplePoint.Y);

            Canvas.SetLeft(Apple,ApplePoint.X);

            canvas.Children.Insert(0, Apple);

            return ApplePoint;
        }

        private Point SpawnApples(int startx, int starty) 
        {
           

            var ApplePoint = new Point(startx, starty);



            var Apple = new Ellipse()
            {
                Height = 10,
                Width = 10,
                Fill = Brushes.Red

            };

            Canvas.SetTop(Apple, ApplePoint.X);

            Canvas.SetLeft(Apple, ApplePoint.Y);

            canvas.Children.Insert(0, Apple);

            return ApplePoint;
        }

        
    }
}
