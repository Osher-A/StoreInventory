using System.ComponentModel;

namespace StoreManager.Services.OrderServices
{
    public class PaymentAmounts : INotifyPropertyChanged
    {
        private float _amountPaid;
        private float _amountOwed;

        public float AmountPaid
        {
            get { return _amountPaid; }
            set 
            { 
                _amountPaid = value;
                OnPropertyChanged(nameof(AmountPaid));
            }
        }
        public float AmountOwed
        {
            get { return _amountOwed; }
            set 
            { 
                _amountOwed = value;
                OnPropertyChanged(nameof(AmountOwed));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}