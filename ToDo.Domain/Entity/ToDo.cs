using System.ComponentModel.DataAnnotations.Schema;
using ToDo.Domain.Common;

namespace ToDo.Domain.Entity;

public class ToDo : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    public int UserId { get; set; }
    [ForeignKey("UserId")] public User User { get; set; } = default!;
    public DateTime TimeToDo { get; set; }
}