using System;
using System.Collections.Generic;

namespace Dal.InMemory.EF;

public partial class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Gender { get; set; }

}
