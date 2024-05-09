using flontact.Interfaces;
using flontact.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace flontact.ViewModels
{
    /*   
    *ViewModel of the MainView.
    *
    *implements the ViewModelBase
    * 
    * Author: Lukas Burkhardt
    */
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IParserService _parserService;

        public MainWindowViewModel(IParserService parserService)
        {
            _parserService = parserService;
        }

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
            set { 
                _FormatedContact=value;
                SetGender(value);
                OnPropertyChanged(nameof(FormatedContact));}
        }

        string _FormatedContactText = string.Empty;

        public string FormatedContactText
        {
            get=> _FormatedContactText;
            private set
            {
                _FormatedContactText = value;
                OnPropertyChanged(nameof(FormatedContactText));
            }
        }

        private Gender _Gender = Gender.Neutral;
        public Gender Gender
        {
            get => _Gender;
            set {
                _Gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        private RelayCommand? _UnformattedEnterCommand;
        public ICommand UnformattedEnterCommand => _UnformattedEnterCommand ??= new RelayCommand(OnUnfomarredEnter);
        private RelayCommand? _SaveEnterCommand;
        public ICommand SaveEnterCommand => _SaveEnterCommand ??= new RelayCommand(OnSaveEnter);

        

        public Array ContactPartTags => Enum.GetValues(typeof(ContactPartTag));
        public Array Genders => Enum.GetValues(typeof(Gender));


        private void OnUnfomarredEnter()
        {            
            FormatedContact = new(_parserService.Parse(UnformattedContact));
        }
        private void OnSaveEnter()
        {
            var contact = _parserService.ToContact([.. FormatedContact],Gender);
            FormatedContactText = _parserService.ToString(contact);
        }

        private void SetGender(ObservableCollection<ContactPart> parts)
        {
            foreach (var part in parts)
            {
                if (part.Tag.Equals(ContactPartTag.Title))
                {
                    Gender = _parserService.GetGender(part.Text);
                }
            }
        }
    }
}
