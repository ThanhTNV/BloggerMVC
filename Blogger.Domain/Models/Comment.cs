using System;
using System.Collections.Generic;

namespace Blogger.Domain.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string Value { get; set; } = null!;

    public DateTime CommentedAt { get; set; }

    public int BlogId { get; set; }

    public string Username { get; set; } = null!;

    public int? ParentId { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();

    public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

    public virtual Comment? Parent { get; set; }

    public virtual User UsernameNavigation { get; set; } = null!;
}
