using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email {  get; set; }

    public DateTime? BirthDate { get; set; }

    public int? Age {  get; set; }

    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
}
