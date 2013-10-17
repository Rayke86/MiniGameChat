using System;

namespace PacketLibrary
{
    [Serializable]
    public enum Flag
    {
        Chat, RPSLS, Connect4, HandshakeRequest, HandshakeResponse, OnlineUserList, AddClient, RemoveClient, GameRequest, GameResponse
    }

    [Serializable]
    public enum Response
    {
        OK, INVALIDLOGIN, ACCESSDENIED
    }

    [Serializable]
    public enum GameSituation
    {
        Win, Loss, Tie, Connect, Disconnect, Normal
    }

    [Serializable]
    public enum Hands
    {
        Rock, Paper, Scissors, Lizard, Spock
    }
}
