using System;
using System.Collections.Generic;

namespace Blogger.Domain.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Status { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public virtual ICollection<BlogReaction> BlogReactions { get; set; } = new List<BlogReaction>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
