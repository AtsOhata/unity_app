using System;
using System.Collections.Generic;
using System.Linq;

static public class Utility
{
    /// <summary>
    /// ���X�g�̒��g��s�K���ɕς���
    /// </summary>
    /// <typeparam name="T">�C�ӂ̌^</typeparam>
    /// <param name="List">���X�g</param>
    static public List<T> Randomize<T>(List<T> List)
    {
        return List.OrderBy(i => Guid.NewGuid()).ToList();
    }
}
