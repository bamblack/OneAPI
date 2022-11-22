using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAPI.Models.Lib
{
    public enum ParamType
    {
        EQUALS = 0,
        NEQUALS = 1,
        INCLUDE = 2,
        EXCLUDE = 3,
        EXISTS = 4,
        NEXISTS = 5,
        REGEX = 6,
        NREGEX = 7,
        LESS = 8,
        LTE = 9,
        GREATER = 10,
        GTE = 11
    }
}
