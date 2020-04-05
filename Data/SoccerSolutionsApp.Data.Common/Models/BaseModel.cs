namespace SoccerSolutionsApp.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
