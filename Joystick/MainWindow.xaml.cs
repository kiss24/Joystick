using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Joystick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Test();
        }

        private void Test()
        {
            // Console.WriteLine("Atan2(0,0)" + Math.Atan2(0, 0));
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            double joystickRadius = Joystick.Height * 0.5;
            Vector joystickPos = e.GetPosition(Joystick) -
                new Point(joystickRadius, joystickRadius);
            joystickPos /= joystickRadius;

            // coordinate system
            XMousePos.Text = joystickPos.X.ToString();
            YMousePos.Text = joystickPos.Y.ToString();

            //Polar coord system
            double angle = Math.Atan2(joystickPos.Y, joystickPos.X);
            XPolPos.Text = angle.ToString(); //Angle
            YPolPos.Text = joystickPos.Length.ToString(); //Radius

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (joystickPos.Length > 1.0)
                {
                    joystickPos.X = Math.Cos(angle);
                    joystickPos.Y = Math.Sin(angle);

                    Console.WriteLine(angle);
                }

                UpdateKnobPosition(joystickPos);

                // Run
            }

            if (e.LeftButton == MouseButtonState.Released)
            {
                joystickPos.X = 0;
                joystickPos.Y = 0;

                UpdateKnobPosition(joystickPos);

                // Stop
            }


        }

        private void UpdateKnobPosition(Vector joystickPos)
        {
            double fJoystickRadius = Joystick.Height * 0.5;
            double fKnobRadius = Knob.Width * 0.5;
            Canvas.SetLeft(Knob, Canvas.GetLeft(Joystick) +
                joystickPos.X * fJoystickRadius + fJoystickRadius - fKnobRadius);
            Canvas.SetTop(Knob, Canvas.GetTop(Joystick) +
                joystickPos.Y * fJoystickRadius + fJoystickRadius - fKnobRadius);
        }
    }
}
