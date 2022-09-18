using System;
using System.Collections.Generic;
using System.Linq;

static public class Utility
{
    /// <summary>
    /// リストの中身を不規則に変える
    /// </summary>
    /// <typeparam name="T">任意の型</typeparam>
    /// <param name="List">リスト</param>
    static public List<T> Randomize<T>(List<T> List)
    {
        return List.OrderBy(i => Guid.NewGuid()).ToList();
    }
}
