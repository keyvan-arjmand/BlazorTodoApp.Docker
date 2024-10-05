using System.ComponentModel.DataAnnotations;

namespace ToDo.Domain.Common;

public class BaseEntity
{
    [Key] public int Id { get; set; }
    public bool IsDelete { get; set; } = false;
}