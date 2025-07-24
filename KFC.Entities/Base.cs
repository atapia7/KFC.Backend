using KFC.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.Entities;

public abstract class Base
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public StatusEnum Status { get; set; }

    public virtual void setEnable()
    {
        this.Status = StatusEnum.Enabled;
    }

    public virtual void setDisable()
    {
        this.Status = StatusEnum.Disabled;
    }
}

