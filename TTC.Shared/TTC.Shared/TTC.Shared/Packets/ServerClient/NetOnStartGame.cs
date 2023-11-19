using System;
using LiteNetLib.Utils;

namespace TTC.Shared.Packets.ServerClient
{
    public struct NetOnStartGame : INetPacket
    {
        public PacketType Type => PacketType.OnStartGame;

        public string XUser { get; set; }
        public string OUser { get; set; }
        public Guid GameId { get; set; }
        
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(XUser);
            writer.Put(OUser);
            writer.Put(GameId.ToString());
        }

        public void Deserialize(NetDataReader reader)
        {
            XUser = reader.GetString();
            OUser = reader.GetString();
            GameId = Guid.Parse(reader.GetString());
        }
    }
}