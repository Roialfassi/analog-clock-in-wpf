using System;
using System.Timers;
using Arction.Gauges;
using Arction.Gauges.Dials;
using System.Windows;
using System.Windows.Controls;

namespace ClockDemo
{
    public partial class MainWindow : Window
    {
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
            timer.Elapsed += new ElapsedEventHandler(Timer_Tick);
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