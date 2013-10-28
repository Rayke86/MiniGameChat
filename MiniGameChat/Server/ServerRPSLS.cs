using System.Collections.Generic;
using System.Linq;
using PacketLibrary;

namespace Server
{
    public class ServerRPSLS : Game
    {
        private List<Dictionary<string, Hands>> Rounds;
        public Dictionary<string, Hands> ChosenHands { get; set; }

        public ServerRPSLS(ServerMain serverMain, string player1, string player2)
            : base(serverMain, player1, player2)
        {
            ChosenHands = new Dictionary<string, Hands>();
            Rounds = new List<Dictionary<string, Hands>>();
        }

        public override void Set(string player, BaseGame baseGame)
        {
            RockPaperScissorsLizardSpock rockPaperScissorsLizardSpock = (RockPaperScissorsLizardSpock) baseGame;
            if (rockPaperScissorsLizardSpock.Situation == GameSituation.Disconnect)
            {
                serverMain.RemoveGame(this);
            }
            else
            {
                ChosenHands.Add(player, rockPaperScissorsLizardSpock.YourHand);
                if (ChosenHands.Count == 2)
                {
                    GameCheck();
                    Rounds.Add(ChosenHands.ToDictionary(
                        x => x.Key,
                        y => y.Value
                        ));
                }
            }
        }

        public override void GameCheck()
        {
            GameSituation situation = GameSituation.Normal;
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
            for (int i = 0; i < Players.Count; i++)
            {
                if (i == 1)
                {
                    if(situation == GameSituation.Win)
                        situation = GameSituation.Loss;
                    else if(situation == GameSituation.Loss)
                        situation = GameSituation.Win;
                }
                RockPaperScissorsLizardSpock g = new RockPaperScissorsLizardSpock(Players[i], Players[(i+1)%Players.Count], situation);
                g.YourHand = ChosenHands[Players[i]];
                g.OpponentHand = ChosenHands[Players[(i + 1)%2]];
                //g.YourHand = Rounds.Last()[Players[i]];
                //g.OpponentHand = Rounds.Last()[Players[(i+1)%Players.Count]];
                packet.Data = g;
                serverMain.SendResolvedGameSituation(Players[i], packet);

                if (situation == GameSituation.Win || situation == GameSituation.Tie || situation == GameSituation.Loss)
                {
                    serverMain.RemoveGame(this);
                }
            }
        }
    }
}
