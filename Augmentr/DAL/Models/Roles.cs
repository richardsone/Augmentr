using System;

namespace Augmentr.Dal.Models
{
    [Flags]
    public enum Roles
    {
        None = 0,
        User = 1,
        Admin = 2,
        SuperAdmin = 4
    }
}