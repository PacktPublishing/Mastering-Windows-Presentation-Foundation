using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;

namespace CompanyName.ApplicationName.ViewModels.Interfaces
{
    public interface ITestViewModel
    {
        BaseAnimatableCollection<User> Users { get; set; }
    }
}
