using Calculator.Fonts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Calculator.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Private Members

        private string numerics;
        private string operation;
        private string inputDisplay = "0";
        private double secondNumber;
        private double firstNumber;
        private string firstNumberDisplay = "";
        private string solutionDisplay = "0";

        #endregion

        #region Public Members

        public Command<string> NumericInputCommand { get; }
        public Command<string> OperationCommand { get; }
        public Command BackspaceCommand { get; }
        public Command PlusMinusCommand { get; }
        public Command ClearScreenCommmand { get; }
        public Command EqualToCommand { get; }
        public Command LightThemeCommand { get; }
        public Command DarkThemeCommand { get; }


        public string Numerics
        {
            get { return numerics; }
            set { SetProperty(ref numerics, value); }
        }
        
        public string Operation
        {
            get { return operation; }
            set { SetProperty(ref operation, value); }
        }
        
        public string InputDisplay
        {
            get { return inputDisplay; }
            set 
            { 
                SetProperty(ref inputDisplay, value);
            }
        }
        
        public string SolutionDisplay
        {
            get { return solutionDisplay; }
            set { SetProperty(ref solutionDisplay, value); }
        }


        public string FirstNumberDisplay
        {
            get { return firstNumberDisplay; }
            set { SetProperty(ref firstNumberDisplay, value); }
        }
        
        public double FirstNumber
        {
            get { return firstNumber; }
            set { SetProperty(ref firstNumber, value); }
        }
        

        public double SecondNumber
        {
            get { return secondNumber; }
            set { SetProperty(ref secondNumber, value); }
        }


        #endregion

        #region Constructor

        public MainPageViewModel()
        {
            
            NumericInputCommand = new Command<string>(NumericValue);
            OperationCommand = new Command<string>(OperationValue);
            EqualToCommand = new Command(OnEqualTo);
            PlusMinusCommand = new Command(OnPlusMinus);
            BackspaceCommand = new Command(OnBackspace);
            ClearScreenCommmand = new Command(OnClearAll);
            LightThemeCommand = new Command(LightTheme);
            DarkThemeCommand = new Command(DarkTheme);
        }

        #endregion

        #region Helper Methods

        private void NumericValue(string input)
        {
            if (InputDisplay == "0")
            {
                InputDisplay = string.Empty;
            }
            
            if (input == ".")
            {
                if (!InputDisplay.Contains("."))
                {
                    InputDisplay = InputDisplay + input;
                }
            }
            else
            {
                InputDisplay = InputDisplay + input;
            }

        }

        private void OnPlusMinus()
        {
            if (InputDisplay.Contains("-"))
            {
                InputDisplay = InputDisplay.Remove(0, 1);
            }
            else
            {
                InputDisplay = "-" + InputDisplay;
            }
        }

        private void OperationValue(string input)
        {

            if (FirstNumberDisplay.Length > 0 && InputDisplay.Length > 0)
            {
                SecondNumber = Double.Parse(InputDisplay);
                double ops;
                switch (Operation)
                {
                    case FontIcons.Plus:
                        ops = FirstNumber + SecondNumber;
                        FirstNumberDisplay = Convert.ToString(ops);
                        FirstNumber = ops;
                        break;
                    case FontIcons.Minus:
                        ops = FirstNumber - SecondNumber;
                        FirstNumberDisplay = Convert.ToString(ops);
                        FirstNumber = ops;
                        break;
                    case FontIcons.Close:
                        ops = FirstNumber * SecondNumber;
                        FirstNumberDisplay = Convert.ToString(ops);
                        FirstNumber = ops;
                        break;
                    case FontIcons.Division:
                        ops = FirstNumber / SecondNumber;
                        FirstNumberDisplay = Convert.ToString(ops);
                        FirstNumber = ops;
                        break;
                    default:
                        break;
                }

                InputDisplay = "";
            }
            else if (FirstNumberDisplay.Length > 0 && InputDisplay.Length == 0)
            {
                return;
            }
            else if (FirstNumberDisplay.Length == 0)
            {
                FirstNumber = Double.Parse(InputDisplay);
                FirstNumberDisplay = InputDisplay;
                InputDisplay = "";
               
            }

            if (input == "+")
            {
                Operation = FontIcons.Plus;
            }
            else if (input == "-")
            {
                Operation = FontIcons.Minus;
            }
            else if (input == "*")
            {
                Operation = FontIcons.Close;
            }
            else if (input == "/")
            {
                Operation = FontIcons.Division;
            }

            //InputDisplay = InputDisplay + " " +Operation + " ";
        }

        private void OnBackspace(object obj)
        {

            if (InputDisplay.Length > 0)
            {
                InputDisplay = InputDisplay.Remove(InputDisplay.Length - 1, 1);
            }
            
            if (InputDisplay == "")
            {
                if (FirstNumberDisplay.Length > 0)
                {
                    InputDisplay = FirstNumberDisplay;
                    FirstNumberDisplay = "";
                    SolutionDisplay = "0";
                    Operation = "";
                }
                else
                {
                    InputDisplay = "0";
                }
            }
        }
        
        private void OnClearAll(object obj)
        {
            FirstNumberDisplay = "";
            SolutionDisplay = "0";
            Operation = "";
            FirstNumber = 0;
            InputDisplay = "0";
        }
        
        private void OnEqualTo(object obj)
        {
            if (InputDisplay == "")
            {
                InputDisplay = "0";
            }

            SecondNumber = Double.Parse(InputDisplay);

            switch (Operation)
            {
                case FontIcons.Plus:
                    SolutionDisplay = Convert.ToString(FirstNumber + SecondNumber);
                    Debug.WriteLine(FirstNumber + SecondNumber);
                    break;
                case FontIcons.Minus:
                    SolutionDisplay = Convert.ToString(FirstNumber - SecondNumber);
                    Debug.WriteLine(FirstNumber + SecondNumber);
                    break;
                case FontIcons.Close:
                    SolutionDisplay = Convert.ToString(FirstNumber * SecondNumber);
                    Debug.WriteLine(FirstNumber + SecondNumber);
                    break;
                case FontIcons.Division:
                    SolutionDisplay = Convert.ToString(FirstNumber / SecondNumber);
                    Debug.WriteLine(FirstNumber + SecondNumber);
                    break;
                default:
                    break;
            }
        }
        
        private void LightTheme(object obj)
        {
            App.Current.UserAppTheme = OSAppTheme.Light;
            barColors.SetLightTheme(System.Drawing.Color.FromArgb(255, 255, 255));
            Preferences.Set("AppTheme","Light");
        }

        private void DarkTheme(object obj)
        {
            App.Current.UserAppTheme = OSAppTheme.Dark;
            barColors.SetDarkTheme(System.Drawing.Color.FromArgb(34, 37, 45));
            Preferences.Set("AppTheme", "Dark");
        }
        #endregion
    }
}
