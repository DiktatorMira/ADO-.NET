using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Dz04._03._2024 {
    public class BaseVM {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class MainVM : BaseVM {
        public ObservableCollection<AuthorsVM>? listAuthors { get; set; }
        public ObservableCollection<BooksVM>? listBooks { get; set; }
        public ObservableCollection<BooksVM>? templistBooks { get; set; }
        private string fullname, title, editbook, editauthor;
        private bool isbook = false, isauthor = false, issafe = false, iscombo = true, isexecute = true, isfilter = false;
        private int selectedAuthor = -1, selectedBook = -1 , value;
        private Command saveChanges, addAuthor, editAuthor, deleteAuthor, addBook, editBook, deleteBook;
        public MainVM(IQueryable<AuthorsM> authors, IQueryable<BooksM> books) {
            listAuthors = new ObservableCollection<AuthorsVM>(authors.Select(a => new AuthorsVM(a)));
            listBooks = new ObservableCollection<BooksVM>(books.Select(b => new BooksVM(b)));
            templistBooks = listBooks;
        }
        private bool IsSelectAuthor() { return SelectedAuthor != -1; }
        private bool IsSelectAuthorAndBook() { return SelectedAuthor != -1 && SelectedBook != -1; }
        public ICommand SaveChangesCommand {
            get {
                if (saveChanges == null) saveChanges = new Command(exec => SaveChanges(), null);
                return saveChanges;
            }
        }
        private void SaveChanges() {
            switch (value) {
                case 0:
                    if (EditAuthor.IsNullOrEmpty()) return;
                    AuthorsVM newAuthor = new AuthorsVM(new AuthorsM { FullName = EditAuthor });
                    listAuthors?.Add(newAuthor);
                    break;
                case 1:
                    if (EditAuthor.IsNullOrEmpty()) return;
                    listAuthors[SelectedAuthor].FullName = EditAuthor;
                    break;
                case 2:
                    if (EditBook.IsNullOrEmpty()) return;
                    BooksVM newBook = new BooksVM(new BooksM { Title = EditBook, AuthorId = listAuthors?[SelectedAuthor].Id });
                    listBooks?.Add(newBook);
                    break;
                case 3:
                    if (EditBook.IsNullOrEmpty()) return;
                    listBooks[SelectedBook].Title = EditBook;
                    break;
            }
            IsAuthor = IsBook = IsSafe = false;
            IsCombo = IsExecute = true;
            EditAuthor = EditBook = string.Empty;
        }
        public ICommand AddAuthorCommand {
            get {
                if (addAuthor == null) addAuthor = new Command(exec => AddAuthor(), null);
                return addAuthor;
            }
        }
        private void AddAuthor() {
            IsAuthor = IsSafe = true;
            IsCombo = IsExecute = false;
            value = 0;
            EditAuthor = "Имя автора";
        }
        public ICommand EditAuthorCommand {
            get {
                if (editAuthor == null) editAuthor = new Command(exec => EdAuthor(), can => IsSelectAuthor());
                return editAuthor;
            }
        }
        private void EdAuthor() {
            IsAuthor = IsSafe = true;
            IsCombo = IsExecute = false;
            value = 1;
            EditAuthor = listAuthors[SelectedAuthor].FullName;
        }
        public ICommand DeleteAuthorCommand {
            get {
                if (deleteAuthor == null) deleteAuthor = new Command(exec => DelAuthor(), can => IsSelectAuthor());
                return deleteAuthor;
            }
        }
        private void DelAuthor() {
            DialogResult res = MessageBox.Show("Вы точно хотите удалить автора? Все его книги тоже удалятся.", "Авторы и книги",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes) {
                listAuthors?.Remove(listAuthors?[SelectedAuthor]);
                for (int i = listBooks.Count - 1; i >= 0; i--) {
                    if (listBooks[i].AuthorId == listAuthors[SelectedAuthor].Id) listBooks.RemoveAt(i);
                }
            }
        }
        public ICommand AddBookCommand {
            get {
                if (addBook == null) addBook = new Command(exec => AddBook(), can => IsSelectAuthor());
                return addBook;
            }
        }
        private void AddBook() {
            IsBook = IsSafe = true;
            IsCombo = IsExecute = false;
            value = 2;
            EditAuthor = listAuthors[SelectedAuthor].FullName;
            EditBook = "Название книги";
        }
        public ICommand EditBookCommand {
            get {
                if (editBook == null) editBook = new Command(exec => EdBook(), can => IsSelectAuthorAndBook());
                return editBook;
            }
        }
        private void EdBook() {
            IsBook = IsSafe = true;
            IsCombo = IsExecute = false;
            value = 3;
            EditAuthor = listAuthors[SelectedAuthor].FullName;
            EditBook = listBooks[SelectedBook].Title;
        }
        public ICommand DeleteBookCommand {
            get {
                if (deleteBook == null) deleteBook = new Command(exec => DelBook(), can => IsSelectAuthorAndBook());
                return deleteBook;
            }
        }
        private void DelBook() {
            DialogResult res = MessageBox.Show("Вы точно хотите удалить книгу?", "Авторы и книги",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) listBooks?.Remove(listBooks?[SelectedBook]);
        }
        private void Filter() {
            if(SelectedAuthor != -1) {
                if (IsFilter) {
                    listBooks = new ObservableCollection<BooksVM>(listBooks.Where(
                    b => b.AuthorId == listAuthors?[SelectedAuthor].Id));
                }
                else listBooks = new ObservableCollection<BooksVM>(templistBooks);
            }
        }
        public int SelectedAuthor {
            get { return selectedAuthor; }
            set {
                selectedAuthor = value;
                OnPropertyChanged(nameof(SelectedAuthor));
            }
        }
        public int SelectedBook {
            get { return selectedBook; }
            set {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }
        public string FullName {
            get { return fullname; }
            set {
                fullname = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string Title {
            get { return title; }
            set {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string EditBook {
            get { return editbook; }
            set {
                editbook = value;
                OnPropertyChanged(nameof(EditBook));
            }
        }
        public string EditAuthor{
            get { return editauthor; }
            set {
                editauthor = value;
                OnPropertyChanged(nameof(EditAuthor));
            }
        }
        public bool IsBook {
            get { return isbook; }
            set {
                isbook = value;
                OnPropertyChanged(nameof(IsBook));
            }
        }
        public bool IsAuthor {
            get { return isauthor; }
            set {
                isauthor = value;
                OnPropertyChanged(nameof(IsAuthor));
            }
        }
        public bool IsSafe {
            get { return issafe; }
            set {
                issafe = value;
                OnPropertyChanged(nameof(IsSafe));
            }
        }
        public bool IsCombo {
            get { return iscombo; }
            set {
                iscombo = value;
                OnPropertyChanged(nameof(IsCombo));
            }
        }
        public bool IsExecute {
            get { return isexecute; }
            set {
                isexecute = value;
                OnPropertyChanged(nameof(IsExecute));
            }
        }
        public bool IsFilter {
            get { return isfilter; }
            set {
                isfilter = value;
                OnPropertyChanged(nameof(IsFilter));
                Filter();
            }
        }
    }
    public class AuthorsVM : BaseVM {
        private AuthorsM author;
        public AuthorsVM(AuthorsM a) => author = a;
        public int Id {
            get { return author.Id; }
        }
        public string FullName {
            get { return author?.FullName!; }
            set {
                author.FullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
    }
    public class BooksVM : BaseVM {
        private BooksM book;
        public BooksVM(BooksM b) => book = b;
        public string Title {
            get { return book?.Title!; }
            set {
                book.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public int? AuthorId {
            get { return book.AuthorId; }
        }
    }
}
