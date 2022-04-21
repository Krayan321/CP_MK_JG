using Model;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase

    {
        #region private

        private readonly ModelAPI modelLayer = ModelAPI.CreateApi();
        private bool notStarted = true;

        private string textBox;
        private string label;
        private string buttonText;
        private string updateButton;
        private bool canStartUpdating = false;
        private Task movingThread;
        private Task updatingThread;

        #endregion private

        #region public API

        public ICommand StartButtonClick { get; set; }
        public ICommand UpdateButtonClick { get; set; }
        public bool IsUpdating { get; set; } = false;
        public int NumberOfBalls { get; set; }

        public CancellationToken Token { get; set; }

        public bool NotStarted
        {
            get
            {
                return notStarted;
            }
            set
            {
                notStarted = value;
                RaisePropertyChanged("NotStarted");
            }
        }

        public bool CanStartUpdating
        {
            get
            {
                return canStartUpdating;
            }
            set
            {
                canStartUpdating = value;
                RaisePropertyChanged("CanStartUpdating");
            }
        }

        public string ButtonText
        {
            get
            {
                return buttonText;
            }
            set
            {
                buttonText = value;
                RaisePropertyChanged("ButtonText");
            }
        }

        public string UpdateButton
        {
            get
            {
                return updateButton;
            }
            set
            {
                updateButton = value;
                RaisePropertyChanged("UpdateButton");
            }
        }

        public int TextBoxValue()
        {
            Label = "";
            if (Int32.TryParse(textBox, out int value))
            {
                value = Int32.Parse(TextBox);

                if (value > 100)
                {
                    Label = "Too many balls";
                    return 0;
                }
                return value;
            }
            Label = "Wrong input";
            return 0;
        }

        public string Label
        {
            get { return label; }
            set { label = value; RaisePropertyChanged("Label"); }
        }

        public string TextBox
        {
            get { return textBox; }
            set { textBox = value; RaisePropertyChanged("TextBox"); }
        }

        public MainWindowViewModel() : this(ModelAPI.CreateApi())
        {
        }

        public MainWindowViewModel(ModelAPI modelApi)
        {
            modelLayer = modelApi;
            Balls = new ObservableCollection<ModelBall>();
            ButtonText = "Generate";
            UpdateButton = "Start";
            foreach (ModelBall ball in modelLayer.balls)
            {
                Balls.Add(ball);
            }
            StartButtonClick = new RelayCommand(() => StartClick());
            UpdateButtonClick = new RelayCommand(() => UpdateClick());
        }

        public ObservableCollection<ModelBall> Balls { get; set; }

        private void StartClick()
        {
            ButtonText = "Generated";
            movingThread = new Task(modelLayer.MoveBalls);
            updatingThread = new Task(Update);
            int numberOfBalls = TextBoxValue();
            if (numberOfBalls > 0)
            {
                CanStartUpdating = true;
                NotStarted = false;
            }
            modelLayer.RemoveBalls();
            modelLayer.AddBalls(numberOfBalls);
            modelLayer.RandomizePositions(modelLayer.Width - modelLayer.Radius * 2, modelLayer.Height - modelLayer.Radius * 2);
            movingThread.Start();
            Balls.Clear();
            foreach (ModelBall ball in modelLayer.balls)
            {
                Balls.Add(ball);
            }
            
        }

        private void UpdateClick()
        {
            if (!IsUpdating)
            {
                IsUpdating = true;
                updatingThread = new Task(Update, Token);
                updatingThread.Start();
                UpdateButton = "Stop";
            }
            else
            {
                //movingThread.
                UpdateButton = "Start";
                IsUpdating = false;
            }
        }

        private void Update()
        {
            while (IsUpdating)
            {
                ObservableCollection<ModelBall> newBalls = new ObservableCollection<ModelBall>(modelLayer.balls);
                Balls = newBalls;
                RaisePropertyChanged(nameof(Balls));
                //Thread.Sleep(15);
            }
        }

        #endregion public API
    }
}