namespace BoltNutPuzzle
{
    /// <summary>
    /// Represents the different states of the game
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Initial state: spawning nuts, setting colors, saving positions
        /// </summary>
        Initialization,
        
        /// <summary>
        /// Waiting for player to click on a bolt or button
        /// </summary>
        PlayerInput,
        
        /// <summary>
        /// Moving a nut from one bolt to another with validation
        /// </summary>
        NutMovement,
        
        /// <summary>
        /// Showing success or failure animation and playing sounds
        /// </summary>
        SuccessSignaling,
        
        /// <summary>
        /// All nuts are sorted by color - player won
        /// </summary>
        Victory
    }
}
