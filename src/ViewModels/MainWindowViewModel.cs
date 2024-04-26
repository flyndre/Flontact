using flontact.Models;
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
        ObservableCollection<ContactPart> _FormatedContact = new();
        public ObservableCollection<ContactPart> FormatedContact
        {
            get => _FormatedContact;
            set { _FormatedContact=value; OnPropertyChanged(nameof(FormatedContact));}
        }
        private RelayCommand? _UnformattedEnterCommand;
        public ICommand UnformattedEnterCommand => _UnformattedEnterCommand ??= new RelayCommand(OnUnfomarredEnter);
        public Array ContactPartTags => Enum.GetValues(typeof(ContactPartTag));


        private void OnUnfomarredEnter()
        {
            FormatedContact.Clear();
            new List<string>(UnformattedContact.Split(" ")).ForEach(contact =>
            {
                FormatedContact.Add(new(contact, ContactPartTag.Name));
            });
            OnPropertyChanged(nameof(FormatedContact));
        }
    }
}
