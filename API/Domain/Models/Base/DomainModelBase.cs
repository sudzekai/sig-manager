using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Models.Base
{
    public class DomainModelBase : INotifyPropertyChanged
    {
        protected bool _initialized;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (_initialized)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
