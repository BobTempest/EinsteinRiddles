using System;
using System.Collections.Generic;
using System.Text;

namespace EinsteinRiddles
{
    public interface IInputData
    {
        Assertion[] Assertions { get; }
        List<Family> Families { get; }
        String Info { get; }

        string getFamilyNameForItem(string item);
    }
}
