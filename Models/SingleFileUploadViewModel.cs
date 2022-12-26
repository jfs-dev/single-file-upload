using System.ComponentModel.DataAnnotations;

namespace SingleFileUpload.Models;

public class SingleFileUploadViewModel
{
    [Required(ErrorMessage = "Favor informar o nome do arquivo!")]
    public string? FileName { get; set; }

    [Required(ErrorMessage = "Favor escolher um arquivo!")]
    public IFormFile? File { get; set; }

    public bool IsResponse { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
