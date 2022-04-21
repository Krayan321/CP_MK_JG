using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
        private string buttonText;
        private string updateButton;
        private bool canStartUpdating = false;
        private Thread movingThread;
        private Thread updatingThread;


        #endregion private

        #region public API
        public ICommand StartButtonClick { get; set; }
        public ICommand UpdateButtonClick { get; set; }
        public bool IsUpdating { get; set; } = false;

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
                return buttonText;
            }
            set
            {
                buttonText = value;
                RaisePropertyChanged("UpdateButton");
            }
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
            movingThread = new Thread(modelLayer.MoveBalls);
            updatingThread = new Thread(Update);
            CanStartUpdating = true;
            modelLayer.RemoveBalls();
            modelLayer.AddBalls(15);
            modelLayer.RandomizePositions(modelLayer.Width - modelLayer.Radius * 2, modelLayer.Height - modelLayer.Radius * 2);
            movingThread.Start();
            Balls.Clear();
            foreach (ModelBall ball in modelLayer.balls)
            {
                Balls.Add(ball);
            }
            NotStarted = false;
        }

        private void UpdateClick()
        {
            if(!IsUpdating)
            {
                IsUpdating = true;
                updatingThread = new Thread(Update);
                updatingThread.Start();
                UpdateButton = "Stop";
            }
            else
            {
                UpdateButton = "Start";
                IsUpdating = false;
            }
        }


        private void Update()
        {
            while(IsUpdating)
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
