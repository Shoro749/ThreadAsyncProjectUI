using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadAsyncProjectUI.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    [Required]
    public int Balance { get; set; }
    [Required]
    [StringLength(32)]
    public string Role { get; set; }
}
