using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ServerClient
{
    public struct NetOnServerStatus : INetPacket
    {
        public PacketType Type => PacketType.OnServerStatus;
        
        public ushort PlayersCount { get; set; }
        public PlayerNetDto[] TopPlayers { get; set; }
        
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(PlayersCount);
            writer.Put((ushort)TopPlayers.Length);
            for (var i = 0; i < TopPlayers.Length; i++)
            {
                writer.Put(TopPlayers[i]);
            }
        }

        public void Deserialize(NetDataReader reader)
        {
            PlayersCount = reader.GetUShort();
            
            var topPlayersLength = reader.GetUShort();
            TopPlayers = new PlayerNetDto[topPlayersLength];
            for (var i = 0; i < topPlayersLength; i++)
            {
                TopPlayers[i] = reader.Get<PlayerNetDto>();
            }
        }
    }

    public struct PlayerNetDto : INetSerializable
    {
        public string Username { get; set; }
        public ushort Score { get; set; }
        public bool IsOnline { get; set; }
        
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Username);
            writer.Put(Score);
            writer.Put(IsOnline);
        }

        public void Deserialize(NetDataReader reader)
        {
            Username = reader.GetString();
            Score = reader.GetUShort();
            IsOnline = reader.GetBool();
        }
    }
}