using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CodeBridge.Models.Entities;

public class Dog 
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    public string Color { get; set; }

    [Required]
    [Range(0, 30)]
    [JsonPropertyName("tail_length")]
    public float TailLength { get; set; }

    [Required]
    [Range(0, 100)]
    public float Weight { get; set; }
    

}
