using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace LoggingMicroservice.Models;

public class Log
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public Guid RequestId { get; set; } = Guid.NewGuid(); 

    [Column(TypeName = "jsonb")]
    public string RequestObject { get; set; }

    [Required]
    public string RouteURL { get; set; } 

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;   
}