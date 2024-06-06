using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum FactionEnum : byte
{
    None = 0,
    PLAYER = 1 << 1,
    SPIDER = 1 << 2,
    SNAKE = 1 << 3,
    WOLF = 1 << 4,
    AI_ALL = SPIDER | SNAKE | WOLF
}