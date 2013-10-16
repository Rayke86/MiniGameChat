using System;
using System.Collections.Generic;
using PacketLibrary;

namespace Server
{
    class GameRPSLS : Game
    {
        private List<Dictionary<string, Hands>> Rounds;
        private Dictionary<string, Hands> ChosenHands;

        public GameRPSLS(string player) : base(player)
        {
            ChosenHands = new Dictionary<string, Hands>();
            Rounds = new List<Dictionary<string, Hands>>();
        }

        public void RPSLSCheck(Game rpsls)
        {
            switch (ChosenHands[Players[0]])
            {
                case Hands.Rock:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // TIE
                            break;
                        case Hands.Paper:
                            // player 2 wins
                            break;
                        case Hands.Scissors:
                            // player 1 wins
                            break;
                        case Hands.Lizard:
                            // player 1 wins
                            break;
                        case Hands.Spock:
                            // player 2 wins
                            break;
                    }
                    break;
                case Hands.Paper:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // player 1 wins
                            break;
                        case Hands.Paper:
                            // TIE
                            break;
                        case Hands.Scissors:
                            // player 2 wins
                            break;
                        case Hands.Lizard:
                            // player 2 wins
                            break;
                        case Hands.Spock:
                            // player 1 wins
                            break;
                    }
                    break;
                case Hands.Scissors:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // player 2 wins
                            break;
                        case Hands.Paper:
                            // player 1 wins
                            break;
                        case Hands.Scissors:
                            // TIE
                            break;
                        case Hands.Lizard:
                            // player 1 wins
                            break;
                        case Hands.Spock:
                            // player 2 wins
                            break;
                    }
                    break;
                case Hands.Lizard:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // player 2 wins
                            break;
                        case Hands.Paper:
                            // player 1 wins
                            break;
                        case Hands.Scissors:
                            // player 2 wins
                            break;
                        case Hands.Lizard:
                            // TIE
                            break;
                        case Hands.Spock:
                            // player 1 wins
                            break;
                    }
                    break;
                case Hands.Spock:
                    switch (ChosenHands[Players[1]])
                    {
                        case Hands.Rock:
                            // player 1 wins
                            break;
                        case Hands.Paper:
                            // player 2 wins
                            break;
                        case Hands.Scissors:
                            // player 1 wins
                            break;
                        case Hands.Lizard:
                            // player 2 wins
                            break;
                        case Hands.Spock:
                            // TIE
                            break;
                    }
                    break;
            }
        }
    }
}
