using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AddClassDto
    {
        #region Properties and Indexers
        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;
        #endregion
    }
}
