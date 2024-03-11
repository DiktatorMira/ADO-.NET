using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Dz06._03._2024 {
    public class BaseVM {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class MainVM : BaseVM {
        public ObservableCollection<PositionsVM>? Positions { get; set; }
        public ObservableCollection<EmployeesVM>? Employees { get; set; }
        public ObservableCollection<EmployeesVM>? tempEmployees { get; set; }
        private string fullname, title, editName, editPosition;
        private bool isName = false, isPosition = false, isExecute = true, isSave = false, isFilter = false;
        private int selectedPosition = -1, selectedEmployee = -1, value;
        private Command saveChanges, addEmployee, changeEmployee, deleteEmployee, addPosition, changePosition, deletePosition;
        public MainVM(IQueryable<PositionsM> positions, IQueryable<EmployeesM> employees) {
            Positions = new ObservableCollection<PositionsVM>(positions.Select(p => new PositionsVM(p)));
            Employees = new ObservableCollection<EmployeesVM>(employees.Select(e => new EmployeesVM(e)));
            tempEmployees = Employees;
        }
        private bool IsSelectEmployee() { return SelectedEmployee != -1; }
        private bool IsSelectedPosition() { return SelectedPosition != 1; }
        public ICommand SaveChangesCommand {
            get {
                if (saveChanges == null) saveChanges = new Command(exec => SaveChanges(), null);
                return saveChanges;
            }
        }
        private void SaveChanges() {
            switch (value) {
                case 0:
                    if (EditName == string.Empty) return;
                    if (!Positions.Any(p => p.Title != null && p.Title == EditPosition)) return;
                    PositionsVM? selectedPositionVM = Positions.FirstOrDefault(p => p.Title == EditPosition);
                    PositionsM selectedPosition = selectedPositionVM.ToModel();
                    EmployeesVM newEmployee = new EmployeesVM(
                        new EmployeesM {
                            Id = Employees.Count + 1,
                            FullName = EditName,
                            Position = selectedPosition
                        }
                    );
                    Employees.Add(newEmployee);
                    break;
                case 1:
                    if (EditName == string.Empty) return;
                    Employees[SelectedEmployee].FullName = EditName;
                    break;
                case 2:
                    if (EditPosition == string.Empty) return;
                    PositionsVM newPosition = new PositionsVM(
                        new PositionsM {
                            Id = Positions.Count + 1,
                            Title = EditPosition,
                        }
                    );
                    Positions.Add(newPosition);
                    break;
                case 3:
                    if (EditPosition == string.Empty) return;
                    Positions[SelectedPosition].Title = EditPosition;
                    break;
            }
            IsName = IsPosition = IsSave = false;
            IsExecute = true;
            EditName = EditPosition = string.Empty;
        }
        public ICommand AddEmployeeCommand {
            get {
                if (addEmployee == null) addEmployee = new Command(exec => AddEmployee(), null);
                return addEmployee;
            }
        }
        private void AddEmployee() {
            value = 0;
            IsName = IsPosition = IsSave = true;
            IsExecute = false;
            EditName = "Полное имя сотрудника";
            EditPosition = "Название должности";
        }
        public ICommand ChangeEmployeeCommand {
            get {
                if (changeEmployee == null) changeEmployee = new Command(exec => ChangeEmployee(), can => IsSelectEmployee());
                return changeEmployee;
            }
        }
        public void ChangeEmployee() {
            IsName = IsSave = true;
            IsExecute = false;
            value = 1;
            EditName = Employees[SelectedEmployee].FullName;
            EditPosition = Employees[SelectedEmployee].Title ?? string.Empty;
        }
        public ICommand DeleteEmployeeCommand {
            get {
                if (deleteEmployee == null) deleteEmployee = new Command(exec => DeleteEmployee(), can => IsSelectEmployee());
                return deleteEmployee;
            }
        }
        private void DeleteEmployee() {
            DialogResult res = MessageBox.Show("Вы точно хотите удалить сотрудника?", "Сотрудники и должности",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) Employees?.Remove(Employees[SelectedEmployee]);
        }
        public ICommand AddPositionCommand {
            get {
                if (addPosition == null) addPosition = new Command(exec => AddPosition(), null);
                return addPosition;
            }
        }
        private void AddPosition() {
            value = 2;
            IsPosition = IsSave = true;
            IsExecute = false;
            EditPosition = "Название должности";
        }
        public ICommand ChangePositionCommand {
            get {
                if (changePosition == null) changePosition = new Command(exec => ChangePosition(), can => IsSelectedPosition());
                return changePosition;
            }
        }
        private void ChangePosition() {
            value = 3;
            IsPosition = IsSave = true;
            IsExecute = false;
            EditPosition = Positions[SelectedPosition].Title;
        }
        public ICommand DeletePositionCommand {
            get {
                if (deletePosition == null) deletePosition = new Command(exec => DeletePosition(), can => IsSelectedPosition());
                return deletePosition;
            }
        }
        private void DeletePosition() {
            DialogResult res = MessageBox.Show("Вы точно хотите удалить должность? Все её сотрудники тоже удалятся.", "Авторы и книги",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) {
                Positions?.Remove(Positions[SelectedPosition]);
                for (int i = Employees.Count - 1; i >= 0; i--) {
                    if (Employees[i].Title == Positions?[SelectedPosition].Title) Employees.RemoveAt(i);
                }
            }
        }
        private void Filter() {
            if (SelectedPosition != -1) {
                if (IsFilter) {
                    Employees = new ObservableCollection<EmployeesVM>(Employees.Where(
                    e => e.Title == Positions?[SelectedPosition].Title));
                }
                else Employees = new ObservableCollection<EmployeesVM>(tempEmployees);
            }
        }
        public string FullName {
            get => fullname;
            set {
                fullname = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string Title {
            get => title;
            set {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string EditName {
            get => editName;
            set {
                editName = value;
                OnPropertyChanged(nameof(EditName));
            }
        }
        public string EditPosition {
            get => editPosition;
            set {
                editPosition = value;
                OnPropertyChanged(nameof(EditPosition));
            }
        }
        public bool IsName {
            get => isName;
            set {
                isName = value;
                OnPropertyChanged(nameof(IsName));
            }
        }
        public bool IsPosition {
            get => isPosition;
            set
            {
                isPosition = value;
                OnPropertyChanged(nameof(IsPosition));
            }
        }
        public bool IsExecute {
            get => isExecute;
            set {
                isExecute = value;
                OnPropertyChanged(nameof(IsExecute));
            }
        }
        public bool IsSave {
            get => isSave;
            set {
                isSave = value;
                OnPropertyChanged(nameof(IsSave));
            }
        }
        public bool IsFilter {
            get => isFilter;
            set {
                isFilter = value;
                OnPropertyChanged(nameof(IsFilter));
                Filter();
            }
        }
        public int SelectedPosition {
            get => selectedPosition;
            set {
                selectedPosition = value;
                OnPropertyChanged(nameof(SelectedPosition));
            }
        }
        public int SelectedEmployee {
            get => selectedEmployee;
            set {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
    }
    public class PositionsVM : BaseVM {
        private PositionsM position;
        public PositionsVM(PositionsM p) => position = p ?? throw new ArgumentNullException(nameof(p));
        public int Id => position.Id;
        public string Title {
            get => position.Title!;
            set {
                if (position.Title != value) {
                    position.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public PositionsM ToModel() {
            return new PositionsM {
                Id = this.Id,
                Title = this.Title
            };
        }
    }
    public class EmployeesVM : BaseVM {
        private EmployeesM employee;
        public EmployeesVM(EmployeesM e) => employee = e ?? throw new ArgumentNullException(nameof(e));
        public string? Title => employee.Position?.Title;
        public string FullName {
            get => employee.FullName;
            set {
                if (employee.FullName != value) {
                    employee.FullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
    }
}
