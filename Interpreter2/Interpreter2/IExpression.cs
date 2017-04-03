using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter2
{
    interface IExpression
    {
        int Interpret(Context context);
    }
}
