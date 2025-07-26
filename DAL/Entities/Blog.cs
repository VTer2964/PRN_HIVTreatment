using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int? AuthorId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? PublishedAt { get; set; }

    public bool IsApproved { get; set; }

    public string? ImageUrl { get; set; }

    public virtual User? Author { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
