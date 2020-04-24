namespace SoccerSolutionsApp.Data.Models
{
    using System;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Following : IAuditInfo, IDeletableEntity
    {
        public string UserFollowingId { get; set; }

        public virtual ApplicationUser UserFollowing { get; set; }

        public string UserToFollowId { get; set; }

        public virtual ApplicationUser UserToFollow { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
