using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CodeBridge.Models.DTOs.In;

public class CreateDogDTO : IValidatableObject
{
    public string Name { get; set; }
    public string Color { get; set; }

    [JsonPropertyName("tail_length")]
    public float TailLength { get; set; }
    public float Weight { get; set; }
    

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add(new ValidationResult("Name required"));

        if (string.IsNullOrWhiteSpace(Color))
            errors.Add(new ValidationResult("Color required"));

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add(new ValidationResult("TailLength required"));

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add(new ValidationResult("Weight required"));

        if (TailLength < 1 || TailLength > 30)
            errors.Add(new ValidationResult("Incorect tail length"));

        if (Weight < 1 || Weight > 100)
            errors.Add(new ValidationResult("Incorect weigth"));

        string pattern = @"^[A-Z][a-zA-Z]*$";
        if (!Regex.IsMatch(Name, pattern))
            errors.Add(new ValidationResult("Name must not contain spaces or symbols and must start with a capital letter"));
        

        return errors;
    }
}
