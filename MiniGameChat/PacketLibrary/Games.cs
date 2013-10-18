using System;

namespace PacketLibrary
{
    [Serializable]
    public abstract class BaseGame
    {
        public string You { get; set; }
        public string Opponent { get; set; }
        public GameSituation Situation { get; set; }

        public BaseGame(string you, string opponent, GameSituation situation)
        {
            You = you;
            Opponent = opponent;
            Situation = situation;
        }
    }

    [Serializable]
    public class ConnectFour : BaseGame
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string SetPlayedBy { get; set; }
        public bool ItIsYourTurn { get; set; }

        public ConnectFour(string you, string opponent, GameSituation situation) : base(you, opponent, situation)
        {
        }
    }

    [Serializable]
    public class RockPaperScissorsLizardSpock : BaseGame
    {
        public Hands YourHand { get; set; }
        public Hands OpponentHand { get; set; }

        public RockPaperScissorsLizardSpock(string you, string opponent, GameSituation situation) : base(you, opponent, situation)
        {
        }
    }
}
