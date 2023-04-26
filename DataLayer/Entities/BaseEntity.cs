using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class BaseEntity
    {
        #region Properties and Indexers
        [Key]
        public int Id { get; set; }
        #endregion
    }
}
