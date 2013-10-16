using System;

namespace PacketLibrary
{
    [Serializable]
    public enum Flag
    {
        Chat, RPSLS, Connect4, HandshakeRequest, HandshakeResponse, OnlineUserList, AddClient, RemoveClient
    }
}
