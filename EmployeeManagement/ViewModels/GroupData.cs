namespace EmployeeManagement.ViewModels
{
    public class GroupData<T>
    {
        public string Key { get; set; } = default!;
        public IEnumerable<T> Persons { get; set; } = [];
    }
}
