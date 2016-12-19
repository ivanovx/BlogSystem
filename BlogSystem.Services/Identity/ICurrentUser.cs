using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Services.Identity
{
    public interface ICurrentUser : IService
    {
        ApplicationUser Get();
    }
}
