﻿using Model;
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
        private string buttonText;
        private Task updating;
        

        #endregion private

        #region public API
        public ICommand StartButtonClick { get; set; }
        public ICommand UpdateButtonClick { get; set; }
        public bool IsUpdating { get; set; } = false;
        public CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

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

        public MainWindowViewModel() : this(ModelAPI.CreateApi())
        {

        }

        public MainWindowViewModel(ModelAPI modelApi)
        {
            modelLayer = modelApi;
            Balls = new ObservableCollection<ModelBall>();
            ButtonText = "";
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
            modelLayer.AddBalls(15);
            modelLayer.RandomizePositions(780 - modelLayer.Radius, 350 - modelLayer.Radius);
            Balls.Clear();
            foreach (ModelBall ball in modelLayer.balls)
            {
                Balls.Add(ball);
                //RaisePropertyChanged(nameof(ball));
            }
            
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