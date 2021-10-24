using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Muazun.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public const string PageTitlePropertyName = "PageTitle";

        private string pageTitle;
        public string PageTitle
        {
            get => pageTitle;
            set { pageTitle = value; OnPropertyChanged(); }
        }

        protected BaseViewModel()
        { }

        public abstract Task Init();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class BaseViewModel<TParam> : BaseViewModel
    {
        protected BaseViewModel()
        { }
    }
}
