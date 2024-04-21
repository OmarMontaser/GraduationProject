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
        IEnumerable<ManageAdminViewModel> GetAll();
        Task <ManageAdminViewModel> GetAdminById(string id);
    }
}
