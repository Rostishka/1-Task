﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    public abstract class Command : ICommand
    {
            public abstract void Execute();
    }
}
