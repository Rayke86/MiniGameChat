using System.Collections.Generic;
using System.Linq;
using PacketLibrary;

namespace Server
{
    public class GameRPSLS : Game
    {
        private List<Dictionary<string, Hands>> Rounds;
        public Dictionary<string, Hands> ChosenHands { get; set; }

        public GameRPSLS(ServerMain serverMain, string player)
            : base(serverMain, player)
        {
            ChosenHands = new Dictionary<string, Hands>();
            Rounds = new List<Dictionary<string, Hands>>();
        }

        public void Set(string player, RockPaperScissorsLizardSpock rockPaperScissorsLizardSpock)
        {
            ChosenHands.Add(player, rockPaperScissorsLizardSpock.YourHand);
            if (ChosenHands.Count == 2)
            {
                Rounds.Add(ChosenHands.ToDictionary( 
                    x => x.Key,
                    y => y.Value
                    ));
                ChosenHands = new Dictionary<string, Hands>();
            }
        }

        public override void GameCheck()
        {
            GameSituation situation = GameSituation.Normal;
            RockPaperScissorsLizardSpock g = new RockPaperScissorsLizardSpock();
            switch (ChosenHands[Players[0]])
            {
                case Hands.Rock:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // TIE
                            situation = GameSituation.Tie;
                            break;
                        case Hands.Paper:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                        case Hands.Scissors:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                        case Hands.Lizard:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                        case Hands.Spock:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                    }
                    break;
                case Hands.Paper:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                        case Hands.Paper:
                            // TIE
                            situation = GameSituation.Tie;
                            break;
                        case Hands.Scissors:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                        case Hands.Lizard:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                        case Hands.Spock:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                    }
                    break;
                case Hands.Scissors:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                        case Hands.Paper:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                        case Hands.Scissors:
                            // TIE
                            situation = GameSituation.Tie;
                            break;
                        case Hands.Lizard:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                        case Hands.Spock:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                    }
                    break;
                case Hands.Lizard:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                        case Hands.Paper:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                        case Hands.Scissors:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                        case Hands.Lizard:
                            // TIE
                            situation = GameSituation.Tie;
                            break;
                        case Hands.Spock:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                    }
                    break;
                case Hands.Spock:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                        case Hands.Paper:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                        case Hands.Scissors:
                            // player 1 wins
                            situation = GameSituation.Win;
                            break;
                        case Hands.Lizard:
                            // player 2 wins
                            situation = GameSituation.Loss;
                            break;
                        case Hands.Spock:
                            // TIE
                            situation = GameSituation.Tie;
                            break;
                    }
                    break;
            }

            Packet packet = new Packet();
            packet.Flag = Flag.RPSLS;
            foreach (string player in Players)
            {
                if (player == Players[1])
                {
                    if(situation == GameSituation.Win)
                        situation = GameSituation.Loss;
                    else if(situation == GameSituation.Loss)
                        situation = GameSituation.Win;
                    g.YourHand = Rounds.Last()[Players[1]];
                    g.OpponentHand = Rounds.Last()[Players[0]];
                }
                else
                {
                    g.YourHand = Rounds.Last()[Players[0]];
                    g.OpponentHand = Rounds.Last()[Players[1]];
                }
                g.Situation = situation;
                packet.Data = g;
                serverMain.SendResolvedGameSituation(player, packet);
            }
        }
    }
}
