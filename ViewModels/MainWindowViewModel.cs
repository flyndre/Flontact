using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace flontact.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string _WindowTitle = "Flontact";
        public string WindowTitle
        {
            get => _WindowTitle;
            set
            {
                _WindowTitle = value;
                OnPropertyChanged(nameof(WindowTitle));
            }
        }
        string _UnformattedContact = string.Empty;
        public string UnformattedContact
        {
            get => _UnformattedContact;
            set { _UnformattedContact = value; OnPropertyChanged(nameof(UnformattedContact)); }
        }
        ObservableCollection<string> _FormatedContact = new(new []{"hi","hello","Hallo"});
        public ObservableCollection<string> FormatedContact
        {
            get => _FormatedContact;
            set { _FormatedContact=value; OnPropertyChanged(nameof(FormatedContact));}
        }
        private RelayCommand? _UnformattedEnterCommand;
        public ICommand UnformattedEnterCommand => _UnformattedEnterCommand ??= new RelayCommand(OnUnfomarredEnter);


        private void OnUnfomarredEnter()
        {
            FormatedContact.Add(UnformattedContact);
            OnPropertyChanged(nameof(FormatedContact));
        }
    }
}
