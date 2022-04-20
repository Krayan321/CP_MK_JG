using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase

    {
        #region private

        private IList<ModelBall> b_BallsCollection;
        private ModelAPI ModelLayer = ModelAPI.CreateApi();

        #endregion private

        #region public API

        public MainWindowViewModel() : this(ModelAPI.CreateApi())
        {

        }

        public MainWindowViewModel(ModelAPI modelApi)
        {
            ModelLayer = modelApi;
            //ButtomClick = new RelayCommand(() => ClickHandler());
        }

        public IList<ModelBall> CirclesCollection
        {
            get
            {
                return b_BallsCollection;
            }
            set
            {
                if (value.Equals(b_BallsCollection))
                    return;
                RaisePropertyChanged("BallsCollection");
            }
        }

        //public ICommand ButtomClick { get; set; }

        private void ClickHandler()
        {
            // do something usefull
        }

        #endregion public API
    }
}
