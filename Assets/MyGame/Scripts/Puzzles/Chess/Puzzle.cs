using System.Collections.Generic;

namespace UnityEngine.Chess
{
    [CreateAssetMenu(fileName = "Puzzle")]
    public class Puzzle : ScriptableObject
    {
        public string fen;
        public Difficulty difficulty;
        public bool playerIsWhite = true;
        public List<Move> moves;

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        };
    }
}
    