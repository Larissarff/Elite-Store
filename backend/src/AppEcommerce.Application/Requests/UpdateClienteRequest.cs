using System.ComponentModel.DataAnnotations;

namespace AppEcommerce.Application.Requests;

public class UpdateClienteRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Nome { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Senha { get; set; } = string.Empty;
    
    [Required]
    public string Endereco { get; set; } = string.Empty;
}