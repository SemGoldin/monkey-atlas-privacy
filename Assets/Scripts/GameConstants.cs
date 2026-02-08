using UnityEngine;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Utility class for game-wide constants and helper methods
    /// </summary>
    public static class GameConstants
    {
        // Game Configuration
        public const int TOTAL_BOLTS = 9;
        public const int EMPTY_BOLTS = 2;
        public const int FILLED_BOLTS = TOTAL_BOLTS - EMPTY_BOLTS; // 7
        public const int NUTS_PER_COLOR = 4;
        public const int TOTAL_COLORS = 7;
        public const int TOTAL_NUTS = NUTS_PER_COLOR * TOTAL_COLORS; // 28
        
        // Grid Layout
        public const int GRID_ROWS = 3;
        public const int GRID_COLS = 3;
        public const float BOLT_SPACING = 2f;
        
        // Animation Timings
        public const float NUT_MOVE_DURATION = 0.5f;
        public const float NUT_SHAKE_DURATION = 0.5f;
        public const float VICTORY_DELAY = 1f;
        
        // Audio Settings
        public const float DEFAULT_SFX_VOLUME = 1f;
        public const float DEFAULT_MUSIC_VOLUME = 0.5f;
        
        // PlayerPrefs Keys
        public const string CURRENT_LEVEL_KEY = "CurrentLevel";
        public const string HIGH_SCORE_KEY = "HighScore";
        public const string SFX_VOLUME_KEY = "SFXVolume";
        public const string MUSIC_VOLUME_KEY = "MusicVolume";
        
        /// <summary>
        /// Get the 3D position for a bolt in the grid
        /// </summary>
        public static Vector3 GetBoltGridPosition(int index)
        {
            int row = index / GRID_COLS;
            int col = index % GRID_COLS;
            
            return new Vector3(
                (col - GRID_COLS / 2) * BOLT_SPACING,
                0f,
                (row - GRID_ROWS / 2) * BOLT_SPACING
            );
        }
        
        /// <summary>
        /// Convert NutColor to Unity Color
        /// </summary>
        public static Color GetUnityColor(NutColor nutColor)
        {
            switch (nutColor)
            {
                case NutColor.Red:
                    return Color.red;
                case NutColor.Blue:
                    return Color.blue;
                case NutColor.Green:
                    return Color.green;
                case NutColor.Yellow:
                    return Color.yellow;
                case NutColor.Purple:
                    return new Color(0.5f, 0f, 0.5f);
                case NutColor.Orange:
                    return new Color(1f, 0.5f, 0f);
                case NutColor.Pink:
                    return new Color(1f, 0.75f, 0.8f);
                default:
                    return Color.white;
            }
        }
        
        /// <summary>
        /// Shuffle a list using Fisher-Yates algorithm
        /// </summary>
        public static void Shuffle<T>(System.Collections.Generic.List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }
}
