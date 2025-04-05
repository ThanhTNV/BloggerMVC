using System;
using System.Collections.Generic;

namespace Blogger.Domain.Models;

public partial class BlogReaction
{
    public int BlogId { get; set; }

    public string Username { get; set; } = null!;

    public DateTime ReactedAt { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
