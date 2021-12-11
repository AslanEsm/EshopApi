using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Account;

namespace AngularEshop.Services.Intefaces
{
    public interface IJwtService
    {
        Task<UserToken> GenerateAsync(User user);

    }
}
