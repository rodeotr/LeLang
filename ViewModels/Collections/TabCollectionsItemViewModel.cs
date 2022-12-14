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
    public class TabCollectionsItemViewModel : ViewModelBase
    {


        private readonly ICommand _tabCollectionsCommand;
        private LangDataAccessLibrary.Models.Collections _collection;
        private ObservableCollection<CollectionItem> _entityList;

        internal void ShowContext(CollectionItemContext context)
        {
            ShowCollectionContextsWindow window = new ShowCollectionContextsWindow(context);
            window.Show();
        }

        public ICommand TabCollectionsCommand => _tabCollectionsCommand;

        public ObservableCollection<CollectionItem> EntityList { get => _entityList; set => _entityList = value; }

        public TabCollectionsItemViewModel(LangDataAccessLibrary.Models.Collections collection)
        {
            _collection = collection;
            _entityList = new ObservableCollection<CollectionItem>();
            foreach(CollectionEntity cE in _collection.Entities)
            {
                ObservableCollection<CollectionItemContext> contexts = new ObservableCollection<CollectionItemContext>();
                foreach(CollectionEntityContext cec in cE.EntityContexts)
                {
                    contexts.Add(new CollectionItemContext()
                    {
                        Text = cec.Text,
                        Address = cec.Address,
                        MediaType = cec.MediaType,
                        Word = cE.Text
                    });
                }
                _entityList.Add(new CollectionItem()
                {
                    Name = cE.Text,
                    Contexts = contexts
                });
            }

            _tabCollectionsCommand = new TabCollectionsItemCommand(this);

        }

        
        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }



    }
    public class CollectionItem
    {
        public string Name { get; set; }
        public ObservableCollection<CollectionItemContext> Contexts { get; set; }
    }
    public class CollectionItemContext
    {
        public string Word { get; set; }
        public string Text { get; set; }
        public MediaTypes.TYPE MediaType { get; set; }
        public CollectionEntityContextAddress Address { get; set; }
    }

}
