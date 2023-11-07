﻿using LiteNetLib.Utils;

namespace SharedLib
{

    public enum PacketType : byte
    {
        #region Client to Server

        Invalid = 0,
        AuthRequest = 1,

        #endregion

        #region Server to Client

        OnAuth = 100,
        OnAuthFail = 101,
        OnServerStatus = 102,

        #endregion
    }

    public interface INetPacket : INetSerializable
    {
        PacketType Type { get; }
    }
}