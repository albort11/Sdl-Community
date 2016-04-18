﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Sdl.Community.StarTransit.Shared.Models;
using Sdl.Community.StarTransit.Shared.Services;
using Sdl.Community.StarTransit.UI.Annotations;
using Sdl.ProjectAutomation.Core;

namespace Sdl.Community.StarTransit.UI.ViewModels
{
    public class PackageDetailsViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private static string _textLocation;
        private static string _txtName;
        private static string _txtDescription;
        private static List<ProjectTemplateInfo> _studioTemplates;
        private static bool _hasDueDate;
        private static DateTime? _dueDate;
        private static string _sourceLanguage;
        private static string _targetLanguage;
        private static PackageModel _packageModel;
        private ICommand _browseCommand;
        private static bool _canExecute;
        private static ProjectTemplateInfo _template;

        public ObservableCollection<ProjectTemplateInfo> _templates;

        public PackageDetailsViewModel(PackageModel package)
        {
            _packageModel = package;
            _txtName = package.Name;
            _txtDescription = package.Description;
            _studioTemplates = package.StudioTemplates;
            _textLocation = package.Location;
            _sourceLanguage = package.SourceLanguage.DisplayName;
            _templates =new ObservableCollection<ProjectTemplateInfo>(package.StudioTemplates);
            _hasDueDate = false;
           var targetLanguage = string.Empty;
            foreach (var language in package.TargetLanguage)
            {
                targetLanguage = targetLanguage + language.DisplayName;
            }
            _targetLanguage = targetLanguage;

            _canExecute = true;
        }

        public ICommand BrowseCommand
        {
            get { return _browseCommand ?? (_browseCommand = new CommandHandler(Browse, _canExecute)); }
           
        }

     

        public void Browse()
        {
            var folderDialog = new FolderBrowserDialog();
            var result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
               TextLocation = folderDialog.SelectedPath;
              
            }
        }

        public ObservableCollection<ProjectTemplateInfo> Templates
        {
            get
            {
                return _templates;
            }
            set
            {
                if (Equals(value, _templates))
                {
                    return;
                }
                OnPropertyChanged();
            }
        }

        public string TextLocation
        {
            get { return _textLocation; }
            set
            {
                if (Equals(value, _textLocation))
                {
                    return;
                }
                _textLocation = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _txtName; }
            set
            {
                if (Equals(value, _txtName))
                {
                    return;
                }
                _txtName = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _txtDescription; }
            set
            {
                if (Equals(value, _txtDescription))
                {
                    return;
                }
                _txtDescription = value;
                OnPropertyChanged();
            }
        }

        public List<ProjectTemplateInfo> StudioTemplates
        {
            get
            {
                return _studioTemplates;
            }
            set
            {
                if (Equals(value, _studioTemplates))
                {
                    return;
                }
                OnPropertyChanged();
            }
        }

        public ProjectTemplateInfo Template
        {
            get { return _template; }
            set
            {
                if (Equals(value, _template))
                {
                    return;
                }
                _template = value;
                OnPropertyChanged();
            }
        }

        public bool HasDueDate
        {
            get { return _hasDueDate; }
            set
            {
                if (Equals(value,_hasDueDate))
                {
                    return;
                }
                _hasDueDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DueDate
        {
            get { return _dueDate; }
            set
            {
                if (Equals(value, _dueDate))
                {
                    return;
                }
                _dueDate = value;
                OnPropertyChanged();
            }
        }

        public string SourceLanguage
        {
            get { return _sourceLanguage; }
            set
            {
                if (Equals(value, _sourceLanguage))
                {
                    return;
                    
                }
                OnPropertyChanged();
            }
        }

        public string TargetLanguage
        {
            get { return _targetLanguage; }
            set
            {
                if (Equals(value, _targetLanguage))
                {
                    return;
                }
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "TextLocation")
                {if(string.IsNullOrEmpty(TextLocation))
                        
                    return "Location is required";
                }
                if (columnName == "Template" && Template==null)
                {
                    
                        return "Template is rquired";
                   
                }
                return null;
            }
           
        }

        public string Error { get; }

        public static PackageModel GetPackageModel()
        {
            _packageModel.Name = _txtName;
            _packageModel.Description = _txtDescription;
            _packageModel.Location = _textLocation;
            if (_hasDueDate)
            {
                _packageModel.DueDate = _dueDate;
            }
           
            _packageModel.ProjectTemplate = _template;
            _packageModel.HasDueDate = _hasDueDate;
            
           return _packageModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
