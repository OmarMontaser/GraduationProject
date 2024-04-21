using IsFakeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeServices
{
    public interface IEditUser
    {
       // EditProfileViewModel EditData(string id);
        Task UpdateUserData(EditProfileViewModel User);
       // void UpdateUserData(EditProfileViewModel User);

    }
}
