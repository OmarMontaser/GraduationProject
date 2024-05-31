using IsFakeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeServices
{
    public interface IManageAdmin
    {
        Task<IEnumerable<ManageAdminViewModel>> GetAll();
        Task<IEnumerable<ManageAdminViewModel>> GetAllUsers();

        DetailsOfUserViewModel GetAdminData(string id);
    }
}
