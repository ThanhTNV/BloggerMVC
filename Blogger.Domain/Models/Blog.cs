using System;
using System.Collections.Generic;

namespace Blogger.Domain.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? BlogSummary { get; set; }

    public string Content { get; set; } = null!;

    public int Status { get; set; }

    public int CategoryId { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<BlogReaction> BlogReactions { get; set; } = new List<BlogReaction>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? UsernameNavigation { get; set; }
}
