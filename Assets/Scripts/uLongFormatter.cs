using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public interface uLongFormatter
{
    /// <summary>
    /// Format a BigInteger number to a smaller, more readable notation.
    /// </summary>
    /// <returns></returns>
    string Format(ulong number);
}