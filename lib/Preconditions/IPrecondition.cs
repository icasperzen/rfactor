﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rfactor.Lib.Preconditions
{
    interface IPrecondition
    {
        abstract IPreconditionResult Check();

    }
}
