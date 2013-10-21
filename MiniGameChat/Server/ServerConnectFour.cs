﻿
using System;
using System.Collections.Generic;
using PacketLibrary;

namespace Server
{
    public class ServerConnectFour : Game
    {
        private List<List<int>> game;
        private int sets;
        private ConnectFour lastSet;

        public ServerConnectFour(ServerMain serverMain, string player1, string player2)
            : base(serverMain, player1, player2)
        {
            sets = 0;
            game = new List<List<int>>();
            for (int column = 0; column < 8; column++)
            {
                List<int> col = new List<int>();
                for (int row = 0; row < 6; row++)
                {
                    col.Add(0);
                }
                game.Add(col);
            }
        }

        public override void Set(string player, BaseGame baseGame)
        {
            ConnectFour setConnectFour = (ConnectFour)baseGame;
            lastSet = setConnectFour;
            sets++;
            if (player == Players[0])
            {
                game[setConnectFour.X][setConnectFour.Y] = 1;
            }
            else
            {
                game[setConnectFour.X][setConnectFour.Y] = 2;
            }
            GameCheck();
        }

        public override void GameCheck()
        {
            GameSituation situation = GameSituation.Normal;
            if (checkRow(1) || checkColumn(1) || checkDiagonal(1))
            {
                situation = GameSituation.Win;
            }
            if (checkRow(2) || checkColumn(2) || checkDiagonal(2))
            {
                situation = GameSituation.Loss;
            }

            if (sets == 48 && situation == GameSituation.Normal)
            {
                situation = GameSituation.Tie;
            }
            Packet packet = new Packet();
            packet.Flag = Flag.Connect4;
            for (int i = 0; i < Players.Count; i++)
            {
                if (i == 1)
                {
                    if (situation == GameSituation.Win)
                        situation = GameSituation.Loss;
                    else if (situation == GameSituation.Loss)
                        situation = GameSituation.Win;
                }
                ConnectFour g = new ConnectFour(Players[i], Players[(i + 1) % Players.Count], situation);
                g.X = lastSet.X;
                g.Y = lastSet.Y;
                g.SetPlayedBy = lastSet.SetPlayedBy;
                g.ItIsYourTurn = (lastSet.SetPlayedBy != Players[i]);
                packet.Data = g;
                serverMain.SendResolvedGameSituation(Players[i], packet);
            }
        }

        private bool checkRow(int player)
        {
            foreach (List<int> row in game)
            {
                int inRow = 0;
                if (row[0] == player)
                    inRow++;
                for (int i = 1; i < row.Count; i++)
                {
                    if (row[i-1] != row[i])
                        inRow = 0;
                    if (row[i] == player)
                        inRow++;
                }
                if (inRow == 4) return true;
            }
            return false;
        }

        private bool checkColumn(int player)
        {
            for (int y = 0; y < game[0].Count; y++)
            {
                int inCol = 0;
                if (game[y][0] == player)
                    inCol++;
                for (int i = 1; i < game.Count; i++)
                {
                    if (game[y][i] != game[y][i - 1])
                        inCol = 0;
                    if (game[y][i] == player)
                        inCol++;
                }
                if(inCol == 4) return true;
            }
            return false;
        }

        private bool checkDiagonal(int player)
        {
            int boundXmin = 0;
            int boundXmax = game.Count-1;
            int boundYmin = 0;
            int boundYmax = game[0].Count-1;
            //check from topLeft to bottomRight:
            for (int startY = boundYmin; startY < boundYmax-3; startY++)
            {
                for (int startX = boundXmax; startX >= 0; startX--)
                {
                    int inDiagonal = 0;
                    int checkX = startX + 1;
                    int checkY = startY + 1;
                    if (game[startX][startY] == player)
                        inDiagonal++;
                    while ((checkX >= boundXmin && 
                            checkX <= boundXmax && 
                            checkY >= boundYmin && 
                            checkY <= boundYmax) ||
                            checkX < startX+4)
                    {
                        if (game[checkX][checkY] == player)
                            inDiagonal++;
                        checkX++;
                        checkY++;
                    }
                    if(inDiagonal == 4)
                        return true;
                }
            }
            //check from topRight to bottomLeft:
            for (int startY = 0; startY < 3; startY++)
            {
                for (int startX = 0; startX <= 7; startX++)
                {
                    int inDiagonal = 0;
                    int checkX = startX - 1;
                    int checkY = startY + 1;
                    if (game[startX][startY] == player)
                        inDiagonal++;
                    while ((checkX >= boundXmin &&
                            checkX <= boundXmax &&
                            checkY >= boundYmin &&
                            checkY <= boundYmax) ||
                            checkX > startX - 4)
                    {
                        if (game[checkX][checkY] == player)
                            inDiagonal++;
                        checkX--;
                        checkY++;
                    }
                    if (inDiagonal == 4)
                        return true;
                }
            }
            return false;
        }
    }
}