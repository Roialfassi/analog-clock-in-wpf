using System;
using System.Timers;
using Arction.Gauges;
using Arction.Gauges.Dials;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClockDemo
{
    public partial class MainWindow : Window
    {
        // Add the variables to track the mouse interaction
        private bool isDragging = false;
        private Point startPoint;
        public MainWindow()
        {
            InitializeComponent();

            //window title
            this.Title = "WPF Clock Demo";

            //create the clock
            CreateClock();

            //create the timer
            var timer = new Timer()
            {
                Interval = 1000,
                Enabled = true
            };
            //timer.Elapsed += new ElapsedEventHandler(Timer_Tick);
        }

        // Add event handlers for mouse events
        private void Clock1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(Clock1);
        }

        private void Clock1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void Clock1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Get the current mouse position relative to the clock center
                Point currentPosition = e.GetPosition(Clock1);
                double offsetX = currentPosition.X - Clock1.Width / 2;
                double offsetY = currentPosition.Y - Clock1.Height / 2;

                // Calculate the angle based on the mouse position
                double angle = Math.Atan2(offsetY, offsetX) * (180 / Math.PI);

                // Adjust the angle range to match the clock dial
                angle += 90;
                if (angle < 0)
                    angle += 360;

                // Update the clock hands based on the adjusted angle
                Clock1.PrimaryScale.Value = (int)(angle / 30) % 12;
                Clock1.SecondaryScales[0].Value = (int)(angle / 6) % 60;
                Clock1.SecondaryScales[1].Value = (int)(angle / 6) % 60;

                // Update the angle display
                AngleDisplay.Text = $"{(int)angle}°";
            }
        }




        private void CreateClock()
        {
            //some clock default stuff
            Clock1.Width = 300;
            Clock1.Height = 300;

            //create the hour scales
            var Hours = new Scale()
            {
                AngleBegin = 60,
                AngleEnd = 60,
                DialLengthFactor = 0.6,
                DialShape = DialShape.DefaultNeedle,
                Label = new TextBlock(),
                MajorTickCount = 12,
                MinorTickCount = 0,
                RangeBegin = 1,
                RangeEnd = 13,
                ValueIndicatorDistance = -1000
            };

            //create the minute scales
            var Minutes = new Scale()
            {
                AngleBegin = 90,
                AngleEnd = 90,
                DialLengthFactor = 0.8,
                DialShape = DialShape.DefaultNeedle,
                MajorTickCount = 60,
                MinorTickCount = 0,
                RangeBegin = 0,
                RangeEnd = 60,
                MajorTicks = new MajorTicksLine()
                {
                    LabelsEnabled = false
                }
            };

            //create the second scales
            var Seconds = new Scale()
            {
                AngleBegin = 90,
                AngleEnd = 90,
                DialLengthFactor = 0.8,
                DialShape = DialShape.Line,
                MajorTickCount = 60,
                MinorTickCount = 0,
                RangeBegin = 0,
                RangeEnd = 60,
                MajorTicks = new MajorTicksLine()
                {
                    LabelsEnabled = false
                }
            };

            //add the scales to the clock
            Clock1.PrimaryScale = Hours;
            Clock1.SecondaryScales.Add(Minutes);
            Clock1.SecondaryScales.Add(Seconds);

            //set the current time
            Timer_Tick(null, null);
            Clock1.MouseLeftButtonDown += Clock1_MouseLeftButtonDown;
            Clock1.MouseLeftButtonUp += Clock1_MouseLeftButtonUp;
            Clock1.MouseMove += Clock1_MouseMove;
        }


        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                DateTime time = DateTime.Now;

                Clock1.PrimaryScale.Value = time.Hour;
                Clock1.SecondaryScales[0].Value = time.Minute;
                Clock1.SecondaryScales[1].Value = time.Second;
            });
        }
    }
}