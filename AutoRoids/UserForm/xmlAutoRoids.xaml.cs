using Autodesk.AutoCAD.ApplicationServices;
using AutoRoids.UserFormCode;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using AutoRoids.Common.Static;
using AutoRoids.Engine.EngineLevel;
using AutoRoids.Engine.EngineUpdate;
using AutoRoids.GameLoop.Timers;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AutoRoids.UserForm
{
    /// <summary>
    /// Interaction logic for xmlAutoRoids.xaml
    /// </summary>
    public partial class xmlAutoRoids : UserControl
    {
        public xmlAutoRoids()
        {
            InitializeComponent();
            Init();
        }

        private Boolean bolIsInitalized;
        private Boolean bolRestartButtonPressed;
        private Boolean bolReset;

        private void Init()
        {
            clsInit clsInit = new clsInit();

            clsInit.Init(this);

            Application.Idle += OnIdle;
            StartDispatch();

            bolIsInitalized = true;
        }

        private void OnIdle(object sender, EventArgs e)
        {
            if (this.bolRestartButtonPressed)
            {
                RunReset();
            }
            else
            {
                if (clsExtension.IsDrawingValid())
                {
                    Document acDoc = Application.DocumentManager.MdiActiveDocument;

                    if (StaticRock.strDrawing == acDoc.Name &&
                        this.btnStop.Content.ToString() == "Pause" &&
                        StaticRock.lstEngineRock != null)
                        LoopRock();
                }
                else
                {
                    this.btnStop.Content = "Continue";
                }
            }
        }

        internal void RunReset()
        {
            this.IsEnabled = false;
            {
                clsCreateEngineLevel clsCreateTest = new clsCreateEngineLevel();
                clsCreateTest.CreateGrid(bolReset);
                bolRestartButtonPressed = false;
            }
            this.IsEnabled = true;
        }

        private Boolean bolInRestart;

        private void LoopRock()
        {
            if (StaticRock.lstEngineRock.Count > 0)
                Gameloop();
            else
            {
                if (!bolInRestart)
                {
                    bolInRestart = true;
                    clsTimers.GameStopWatch.StopWatchRestart.Restart();
                }
                else
                {
                    // Wait 2 seconds to restart game
                    if (2000 < GetRestartTime())
                    {
                        bolInRestart = false;
                        bolReset = false;
                        this.bolRestartButtonPressed = true;
                    }
                    else
                        Gameloop();
                }
            }

            if (StaticRock.engineScore.bolReset)
            {
                bolReset = true;
                bolRestartButtonPressed = true;
            }
        }

        internal void Gameloop()
        {
            clsReg clsReg = new clsReg();
            clsReg.GetIdleDelay(out int intIdleDelay);

            int intElapsed = GetIdleTime();

            clsUpdateLoop clsUpdateLoop = new clsUpdateLoop();
            clsUpdateLoop.GameLoop(intElapsed, intIdleDelay);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            StaticRock.strDrawing = acDoc.Name;

            bolReset = true;
            bolRestartButtonPressed = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnStop.Content.ToString() == "Pause")
                this.btnStop.Content = "Continue";
            else
                this.btnStop.Content = "Pause";
        }

        private void chkBox_Checked(object sender, RoutedEventArgs e)
        {
            clsTextInput clsTextInput = new clsTextInput();
            clsTextInput.UpdateChecked(bolIsInitalized, sender);
        }

        public void StartDispatch()
        {
            DispatcherTimer dt = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(15)
            };
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            if (StaticRock.lstEngineRock != null && this.btnStop.Content.ToString() == "Pause")
                ForceMessage();
        }

        internal void ForceMessage()
        {
            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(pt.X, pt.Y);
        }

        internal int GetIdleTime()
        {
            int intElapsed = 0;
            if (clsTimers.GameStopWatch != null)
            {
                intElapsed = Convert.ToInt32(clsTimers.GameStopWatch.StopWatchIdle.ElapsedMilliseconds);

                //Debug.Print(intElapsed.ToString());
                if (intElapsed > 50) intElapsed = 50;

                clsTimers.GameStopWatch.StopWatchIdle.Restart();
            }
            return intElapsed;
        }

        internal int GetRestartTime()
        {
            int intElapsed = 0;
            if (clsTimers.GameStopWatch != null)
            {
                intElapsed = Convert.ToInt32(clsTimers.GameStopWatch.StopWatchRestart.ElapsedMilliseconds);
            }
            return intElapsed;
        }
    }
}