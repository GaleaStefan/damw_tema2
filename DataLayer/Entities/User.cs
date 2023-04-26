using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity
{
    #region Properties and Indexers
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    public string Role { get; set; }
    #endregion
}
