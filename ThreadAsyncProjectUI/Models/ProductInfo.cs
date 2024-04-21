using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadAsyncProjectUI.Models;

public class ProductInfo
{
    [Key]
    public int Id {  get; set; }
    [Required]
    [StringLength(64)]
    public string Brand { get; set; }
    [Required]
    [StringLength(64)]
    public string Category { get; set; }
    [Required]
    [StringLength(128)]
    public string Description { get; set; }
    public override string ToString()
    {
        return Brand + " | " + Category + " | " + Description;
    }
}
