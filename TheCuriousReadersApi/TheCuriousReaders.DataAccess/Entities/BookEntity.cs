using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheCuriousReaders.DataAccess.Entities
{
    public class BookEntity
    {
        public BookEntity()
        {
            UserSubscriptions = new List<UserSubscriptionEntity>();
            UserComments = new List<CommentEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        public string CoverUri { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int AuthorId { get; set; }
        public AuthorEntity Author { get; set; }

        public int GenreId { get; set; }
        public GenreEntity Genre { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<UserSubscriptionEntity> UserSubscriptions { get; set; }

        public ICollection<CommentEntity> UserComments { get; set; }
    }
}
