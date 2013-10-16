
using System.Collections.Generic;

namespace Server
{
    class ConnectFour : Game
    {
        private List<List<int>> game;
 
        public ConnectFour(string player) : base(player)
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
            
        }

        public override void GameWinLossCheck()
        {
            throw new System.NotImplementedException();
        }
    }
}
