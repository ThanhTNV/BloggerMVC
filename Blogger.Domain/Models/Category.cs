﻿using System;
using System.Collections.Generic;

namespace Blogger.Domain.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
