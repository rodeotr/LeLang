using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Commands.Storage;
using SubProgWPF.Models;
using SubProgWPF.Windows;
using SubProgWPF.Windows.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Storage
{
    public class TabStorageWordsViewModel : ViewModelBase
    {
        private ObservableCollection<Models.WordMember> _members;
        private ObservableCollection<Models.WordMember> _currentMembers;
        private List<LangDataAccessLibrary.Models.Collections> _collections;
        private StorageWordsModel _membersModel;
        private ICommand _command;
        private string _pageNum;
        private string _totalWordString;
        private bool _wordVisibility;
        
        public TabStorageWordsViewModel(StorageWordsModel memberModel)
        {
            _command = new TabStorageCommand(this);


            _collections = getCollections();

            _membersModel = memberModel;
            _currentMembers = _membersModel.CurrentMembers;
            _wordVisibility = _currentMembers.Count == 0 ? false : true;
        }

        private List<LangDataAccessLibrary.Models.Collections> getCollections()
        {
            return CollectionServices.getCollections();
        }

        public ICommand GridCommand { get { return _command; }}

        public StorageWordsModel StorageMemberModel { get => _membersModel; set => _membersModel = value; }
        public ObservableCollection<WordMember> CurrentMembers { get => _currentMembers; set { _currentMembers = value; OnPropertyChanged(nameof(CurrentMembers)); } }

        internal void launchWordEditWindow(WordMember wM)
        {
            EditWordWindow window = new EditWordWindow(wM);
            window.Show();
        }

        internal void Refresh()
        {
            _membersModel = new StorageWordsModel(WordServices.getAllWords());
            _currentMembers = _membersModel.CurrentMembers;
        }

        internal void launchAddToCollectionWindow(WordMember wM)
        {
            AddToCollectionWindow window = new AddToCollectionWindow(wM, _collections);
            window.Show();
        }

        public string PageNum { get => _pageNum; set { _pageNum = value; OnPropertyChanged(nameof(PageNum)); } }

        public string TotalWordString { get => _totalWordString; set => _totalWordString = value; }
        public bool WordVisibility { get => _wordVisibility; set => _wordVisibility = value; }

        public override void updateTheFields()
        {
            _membersModel = new StorageWordsModel(WordServices.getAllWords());
            _currentMembers = _membersModel.CurrentMembers;
            _wordVisibility = _currentMembers.Count == 0 ? false : true;
        }
        public void launchContextWindow(StorageContext context)
        {
            ShowContextsWindow contextsWindow = new ShowContextsWindow(context);
            contextsWindow.Show();
        }

        public void raisePropertyChangedEvent(string name)
        {
            OnPropertyChanged(name);
        }

        //MainWindow window = new MainWindow() { DataContext = new MainViewModel(_navigationStore, mediaPlayer, false) };
        //window.Show();
    }
}

