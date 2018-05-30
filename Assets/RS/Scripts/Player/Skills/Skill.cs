using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected float _xp = 0;
    protected int _level = 1;
    protected Dictionary<int, int> Levels = new Dictionary<int, int>
    {
        {1, 0},
        {2, 10},
        {3, 20},
        {4, 40},
        {5, 80},
        {6, 160},
        {7, 320},
        {8, 640},
        {9, 1280},
        {10, 2560},
        {11, 5120},
        {12, 10240},
        {13, 20480},
        {14, 40960},
        {15, 81920},
    };
}
