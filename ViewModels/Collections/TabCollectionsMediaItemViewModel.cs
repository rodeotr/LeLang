using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using SubProgWPF.Windows.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;


namespace SubProgWPF.ViewModels.Collections
{
    public class TabCollectionsMediaItemViewModel : ViewModelBase
    {


        private readonly ICommand _tabCollectionsCommand;
        private CollectionMediaModel _model;
        private ObservableCollection<Word> _entityList;

        internal void ShowContext(CollectionItemContext context)
        {
            ShowCollectionContextsWindow window = new ShowCollectionContextsWindow(context);
            window.Show();
        }

        public ICommand TabCollectionsCommand => _tabCollectionsCommand;

        public ObservableCollection<Word> EntityList { get => _entityList; set => _entityList = value; }

        public TabCollectionsMediaItemViewModel(CollectionMediaModel model)
        {
            _model = model;
            _entityList = new ObservableCollection<Word>();
            foreach(Word w in model.Words)
            {
                _entityList.Add(w);
            }

            //_tabCollectionsCommand = new TabCollectionsItemCommand(this);
        }

        
        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }



    }
    
    

}
