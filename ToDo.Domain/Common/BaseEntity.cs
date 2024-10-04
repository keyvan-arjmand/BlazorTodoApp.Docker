using System.ComponentModel.DataAnnotations;

namespace ToDo.Domain.Common;

public class BaseEntity
{
    [Key] public string Id { get; set; } = string.Empty;
    public bool IsDelete { get; set; } = false;
}