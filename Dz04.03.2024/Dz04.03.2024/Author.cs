using System;
using System.Collections.Generic;

namespace Dz04._03._2024;

public partial class Author {
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
