using UnityEngine;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Manages level progression and difficulty
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        private const string LEVEL_KEY = "CurrentLevel";
        private const string HIGH_SCORE_KEY = "HighScore";
        
        /// <summary>
        /// Get the current level
        /// </summary>
        public static int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt(LEVEL_KEY, 1);
        }
        
        /// <summary>
        /// Set the current level
        /// </summary>
        public static void SetCurrentLevel(int level)
        {
            PlayerPrefs.SetInt(LEVEL_KEY, level);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Advance to next level
        /// </summary>
        public static void AdvanceLevel()
        {
            int currentLevel = GetCurrentLevel();
            SetCurrentLevel(currentLevel + 1);
        }
        
        /// <summary>
        /// Reset to level 1
        /// </summary>
        public static void ResetProgress()
        {
            SetCurrentLevel(1);
        }
        
        /// <summary>
        /// Get high score
        /// </summary>
        public static int GetHighScore()
        {
            return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        }
        
        /// <summary>
        /// Update high score if new score is better
        /// </summary>
        public static void UpdateHighScore(int score)
        {
            int currentHigh = GetHighScore();
            if (score > currentHigh)
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
                PlayerPrefs.Save();
            }
        }
        
        /// <summary>
        /// Calculate score based on moves and time
        /// </summary>
        public static int CalculateScore(int moves, float timeInSeconds)
        {
            // Simple scoring: fewer moves and less time = higher score
            int baseScore = 1000;
            int movePenalty = moves * 10;
            int timePenalty = (int)(timeInSeconds / 10);
            
            int finalScore = Mathf.Max(0, baseScore - movePenalty - timePenalty);
            return finalScore;
        }
    }
}
