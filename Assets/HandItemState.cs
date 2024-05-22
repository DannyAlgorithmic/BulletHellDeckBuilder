using System;

[Flags]
public enum HandItemState : byte
{
    None = 0,
    FOCUSED = 1 << 1,
    DRAGGING = 1 << 2,
    SELECTED = 1 << 3
}

public static class EnumFlagExtentions
{
    public static void SetFlags(this HandItemState _self, HandItemState _other) => _self = _other;
    public static void AddFlag(this HandItemState _self, HandItemState _other) => _self |= _other;
    public static void RemoveFlag(this HandItemState _self, HandItemState _other) => _self &= (~_other);
    public static void ToggleFlag(this HandItemState _self, HandItemState _other) => _self ^= _other;
    public static bool ContainsFlag(this HandItemState _self, HandItemState _other) => (_self & _other) == _other;
    public static bool EqualsFlags(this HandItemState _self, HandItemState _other) => _self == _other;
}