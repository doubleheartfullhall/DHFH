using UnityEngine;

namespace DHFH.Core
{
    /// <summary>
    /// Color palette extracted from painterly reference images.
    /// Warm, storybook aesthetic with soft gradients.
    /// </summary>
    public static class DHFHColors
    {
        // Primary colors
        public static readonly Color WarmCream = new Color32(245, 240, 230, 255);
        public static readonly Color DeepCharcoal = new Color32(40, 35, 30, 255);
        public static readonly Color CoralRed = new Color32(220, 95, 75, 255);
        public static readonly Color SoftGold = new Color32(210, 170, 120, 255);
        
        // Secondary colors
        public static readonly Color MistyBlue = new Color32(180, 200, 210, 255);
        public static readonly Color SageGreen = new Color32(195, 210, 185, 255);
        public static readonly Color WarmOcher = new Color32(200, 150, 100, 255);
        public static readonly Color SoftPeach = new Color32(240, 210, 195, 255);
        
        // UI accents
        public static readonly Color BrushStroke = new Color32(60, 50, 45, 200);
        public static readonly Color HighlightGlow = new Color32(255, 245, 220, 150);
        
        // Utility colors
        public static readonly Color LockOverlay = new Color32(40, 35, 30, 180);
        public static readonly Color SoftShadow = new Color32(40, 35, 30, 80);
    }
}
