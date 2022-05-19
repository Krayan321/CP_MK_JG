using Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase

    {
        #region private

        private readonly ModelAPI Model = ModelAPI.CreateApi();
        private bool notStarted = true;

        private string textBox;
        private string label;
        private string buttonText;
        private string updateButton;
        private bool canStartUpdating = false;
        private Task movingThread;
        private Task updatingThread;

        #endregion private

        public ICommand StartButtonClick { get; set; }
        public ICommand UpdateButtonClick { get; set; }
        public bool IsUpdating { get; set; } = false;
        public int NumberOfBalls { get; set; }
        public ObservableCollection<IBall> Balls { get; set; }

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
            Model = modelApi;
            Balls = new ObservableCollection<IBall>();
            IDisposable observer = Model.Subscribe<IBall>(x => Balls.Add(x));
            ButtonText = "Generate";
            UpdateButton = "Start";
            foreach (ModelBall ball in Model.balls)
            {
                Balls.Add(ball);
            }
            StartButtonClick = new RelayCommand(() => StartClick());
            UpdateButtonClick = new RelayCommand(() => UpdateClick());
        }

        private void StartClick()
        {
            ButtonText = "Generated";
            int numberOfBalls = TextBoxValue();
            if (numberOfBalls > 0)
            {
                CanStartUpdating = true;
                NotStarted = false;
            }

            Model.AddNewBalls(numberOfBalls);
            Model.RandomizePositions(Model.Size[0] - Model.Radius * 2, Model.Size[1] - Model.Radius * 2);
            Balls.Clear();

            foreach (ModelBall ball in Model.balls)
            {
                Balls.Add(ball);
            }
        }

        private void UpdateClick()
        {
            Model.MoveBalls(!IsUpdating);
            IsUpdating = !IsUpdating;
            CanStartUpdating = false;
        }
    }
}