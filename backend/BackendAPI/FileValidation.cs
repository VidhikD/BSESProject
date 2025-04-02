using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxSize;
    public MaxFileSizeAttribute(int maxSize)
    {
        _maxSize = maxSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null && file.Length > _maxSize)
        {
            return new ValidationResult($"File size cannot exceed {_maxSize / 1024 / 1024}MB.");
        }
        return ValidationResult.Success; // ✅ Use it as a static property
    }

}

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_extensions.Contains(extension))
            {
                return new ValidationResult($"File type not allowed. Allowed types: {string.Join(", ", _extensions)}");
            }
        }
        return ValidationResult.Success; // ✅ Use it as a static property
    }

}
