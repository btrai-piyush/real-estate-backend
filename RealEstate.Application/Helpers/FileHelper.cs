using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Helpers;

public static class FileHelper
{
    public static async Task<string> SaveImage(IFormFile file, int propertyId, string propertyName, string propertyCity, string uploadPath)
    {
        if (file == null || file.Length == 0)
            throw new Exception("No file uploaded.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var ext = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(ext))
            throw new Exception("Invalid file type.");

        var slug = GenerateSlug($"{propertyName} {propertyCity}");

        var uniqueId = Guid.NewGuid().ToString().Substring(0, 8);

        var fileName = $"property-{slug}-{uniqueId}{ext}";

        var fullPath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return fileName;
    }

    private static string GenerateSlug(string text)
    {
        text = text.ToLower();

        // remove invalid chars
        text = System.Text.RegularExpressions.Regex.Replace(text, @"[^a-z0-9\s-]", "");

        // convert spaces to hyphen
        text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", "-").Trim();

        // remove multiple hyphens
        text = System.Text.RegularExpressions.Regex.Replace(text, @"-+", "-");

        return text;
    }
}