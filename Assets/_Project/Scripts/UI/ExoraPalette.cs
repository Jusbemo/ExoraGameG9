using UnityEngine;

/// <summary>
/// Shared color palette for EXORA's UI (see project README, "EXORA Color Palette").
/// Menus and HUD polish should pull colors from here instead of hardcoding hex
/// values, so the whole game stays visually consistent.
/// </summary>
public static class ExoraPalette
{
    public static readonly Color DarkBlue = new Color32(0x03, 0x2D, 0x42, 0xFF);  // #032D42 - primary dark / backgrounds
    public static readonly Color Accent = new Color32(0x63, 0xDF, 0x4E, 0xFF);    // #63DF4E - glow / highlighted text
    public static readonly Color Purple = new Color32(0x7D, 0x4E, 0x8C, 0xFF);    // #7D4E8C - secondary / borders
    public static readonly Color DarkTeal = new Color32(0x04, 0x43, 0x55, 0xFF);  // #044355 - panels / shadow depth
    public static readonly Color TextWhite = new Color32(0xE8, 0xF4, 0xF2, 0xFF); // soft off-white for body text
}
