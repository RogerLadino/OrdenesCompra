using Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace Shared.DTOs.Customers;

public partial class CustomerDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public DateTime? BirthDate { get; set; }
}