using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Domain.UserAggregate
{
    public enum UserRole
    {
        Admin = -1,

        ListUsers = 1 << 0,
        AddUser = 1 << 1,
        UpdateUser = 1 << 2,
        DeleteUser = 1 << 3,
        ManageUsers = ListUsers | AddUser | UpdateUser | DeleteUser

    }
}
