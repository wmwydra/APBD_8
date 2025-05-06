using System.ComponentModel.DataAnnotations;

namespace APBD_8.Models;

public class ClientCreateDTO
{
    [Required]
    [StringLength(120)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(120)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(120)]
    public string Email { get; set; }
    
    [Required]
    [StringLength(120)]
    public string Telephone { get; set; }
    
    [Required]
    [StringLength(120)]
    public string PESEL { get; set; }
}