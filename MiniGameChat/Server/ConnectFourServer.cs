
using System.Collections.Generic;
using PacketLibrary;

namespace Server
{
    class ConnectFourServer : Game
    {
        private List<List<int>> game;
 
        public ConnectFourServer(string player) : base(player)
        {
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

        public void Set(string player, ConnectFour setConnectFour)
        {
            if (player == Players[0])
            {
                game[setConnectFour.X][setConnectFour.Y] = 1;
            }
            else
            {
                game[setConnectFour.X][setConnectFour.Y] = 2;
            }
        }

        public override void GameWinLossCheck()
        {
        }

        public void ConnectFourCheck(Game game)
        {
            if (checkRow(1))
            {
                //player 1 wins
            }
            if (checkRow(2))
            {
                //player 2 wins
            }
            if (checkColumn(1))
            {
                //player 1 wins
            }
            if (checkColumn(2))
            {
                //player 2 wins
            }
            if (checkDiagonal(1))
            {
                //player 1 wins
            }
            if (checkDiagonal(2))
            {
                //player 2 wins
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
            int boundXmax = 7;
            int boundYmin = 0;
            int boundYmax = 5;
            //check from topLeft to bottomRight:
            for (int startY = 0; startY < 3; startY++)
            {
                for (int startX = 7; startX >= 0; startX--)
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
