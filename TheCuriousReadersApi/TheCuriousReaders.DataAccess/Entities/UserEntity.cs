using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TheCuriousReaders.DataAccess.Entities
{
    public class UserEntity : IdentityUser
    {
        public UserEntity()
        {
            UserSubscriptions = new List<UserSubscriptionEntity>();
            UserComments = new List<CommentEntity>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AddressId { get; set; }

        public AddressEntity Address { get; set; }

        public ICollection<CommentEntity> UserComments { get; set; }

        public ICollection<UserSubscriptionEntity> UserSubscriptions { get; set; }
    }
}
