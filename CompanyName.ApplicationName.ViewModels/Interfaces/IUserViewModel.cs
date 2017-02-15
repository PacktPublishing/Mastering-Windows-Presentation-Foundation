using System.Windows.Input;
using CompanyName.ApplicationName.DataModels;

namespace CompanyName.ApplicationName.ViewModels.Interfaces
{
    public interface IUserViewModel
    {
        User User { get; set; }

        ICommand SaveCommand { get; }
    }
}
