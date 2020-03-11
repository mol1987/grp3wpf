using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using jkb_rebase.Model;
using System.Windows.Input;
using jkb_rebase.Command;

namespace jkb_rebase.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private Article _Article;
        public Status Status { get; private set; }
        public string TestText { get; set; }
        public List<Article> ArticleItems { get; private set; }
        public Article Article
        {
            get { return _Article; }
        }

        public CustomerViewModel()
        {
            TestText = "TESTTEXT";
            Status = new Status() { StatusMessage = "Hello world" };
            ArticleItems = new List<Article>() { 
                new Article(){Name = "[TEST]", Price = 59.0m},
                new Article(){Name = "[xxxxx]", Price = 169.0m}
            };
            _Article = new Article() { Name = "[Article_Name]", Price = 69.0m };
            UpdateCommand = new CustomerUpdateCommand(this);
        }

        #region Databindings
        public ICommand UpdateCommand
        {
            get;
            private set;
        }
        public void ChangeTextCommand()
        {

        }
        public void UpdateStatusMessage(string text = "")
        {
            Status.StatusMessage = String.Format("{0} was updated", Article.Name);
            OnPropertyChanged("Status");
        }
        public void SaveChanges()
        {

            TestText = "was updated";
            OnPropertyChanged("TestText");
            //Debug.Assert(false, String.Format("{0} was updated", Article.Name));
        }

        public void AddToCart()
        {

        } 
        #endregion
    }
}
