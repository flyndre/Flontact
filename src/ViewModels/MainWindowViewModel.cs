using flontact.Interfaces;
using flontact.Models;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace flontact.ViewModels
{
    /// <summary>
    /// ViewModel of the MainView.
    /// Author: Lukas Burkhardt
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties

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

        ObservableCollection<ContactPart> _FormatedContact = [];
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

        public static Array ContactPartTags => Enum.GetValues(typeof(ContactPartTag));
        public static Array Genders => Enum.GetValues(typeof(Gender));

        #endregion 

        #region Fields
        private readonly IParserService _parserService;
        #endregion 

        public MainWindowViewModel(IParserService parserService)
        {
            _parserService = parserService;
        }

        #region Methods
        /// <summary>
        /// Method to read the content of the input field and start the pasrsing of it.
        /// </summary>
        private void OnUnfomarredEnter()
        {            
            FormatedContact = new(_parserService.Parse(UnformattedContact));
        }

        /// <summary>
        /// Method to save the edited parsed input to the database and display the formal form of address.
        /// </summary>
        private void OnSaveEnter()
        {
            var contact = _parserService.ToContact([.. FormatedContact],Gender);
            FormatedContactText = _parserService.ToString(contact);
        }

        /// <summary>
        /// Helper method to get the gender out of the salutation. It looks for title tags and then serach for a gender. 
        /// </summary>
        /// <param name="parts">A ObservableCollection of ContacParts to determine the gender of.</param>
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
        #endregion 
    }
}
