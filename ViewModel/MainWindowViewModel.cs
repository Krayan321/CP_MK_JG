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
        private string buttonText = "Start";
        private string textBox;
        private string inputMessage;

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

        public int TextBoxValue()
        {
            
            while (Int32.TryParse(textBox, out int value))
            {
                value = Int32.Parse(textBox);
                
                
                while (value > 25)
                {
                    inputMessage = "Za dużo kulek";
                    return 0;
                }
                return value;
            }
            inputMessage = "Zły input";
            return 0;
        }

        public string InputMessage
        {
            get { return inputMessage; }
            set { inputMessage = value; RaisePropertyChanged(); }
        }

        public string TextBox
        {
            get {  return textBox; }
            set { textBox = value; RaisePropertyChanged(); }
        }

        public MainWindowViewModel() : this(ModelAPI.CreateApi())
        {

        }

        public MainWindowViewModel(ModelAPI modelApi)
        {
            modelLayer = modelApi;
            Balls = new ObservableCollection<ModelBall>();
            ButtonText = "Start";
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
            ButtonText = "Started";
            modelLayer.RemoveBalls();
            modelLayer.AddBalls(15);
            modelLayer.RandomizePositions(modelLayer.Width - modelLayer.Radius, modelLayer.Height - modelLayer.Radius);
            Balls.Clear();
            foreach (ModelBall ball in modelLayer.balls)
            {
                Balls.Add(ball);
                //RaisePropertyChanged(nameof(ball));
            }
            NotStarted = false;
        }

        private void UpdateClick()
        {
            modelLayer.MoveBalls();
            Balls.Clear();
            foreach (ModelBall ball in modelLayer.balls)
            {
                Balls.Add(ball);
            }
        }

        #endregion public API
    }
}
