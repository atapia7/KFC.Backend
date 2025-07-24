using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.Entities;

public interface IBase
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

