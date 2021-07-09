using System.Collections.Generic;
using TheCuriousReaders.DataAccess.Entities;

namespace TheCuriousReaders.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserEntity user, IList<string> roles);
    }
}
