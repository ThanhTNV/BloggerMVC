using System;
using System.Collections.Generic;

namespace Blogger.Domain.Models;

public partial class CommentReaction
{
    public int CommentId { get; set; }

    public string Username { get; set; } = null!;

    public DateTime ReactedAt { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
