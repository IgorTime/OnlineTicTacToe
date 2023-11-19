using LiteNetLib.Utils;

namespace TTC.Shared.Packets.ClientServer
{
    public struct NetAuthRequest : INetPacket
    {
        public PacketType Type => PacketType.AuthRequest;
        public string Username { get; set; }
        public string Password { get; set; }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Username);
            writer.Put(Password);
        }

        public void Deserialize(NetDataReader reader)
        {
            Username = reader.GetString();
            Password = reader.GetString();
        }
    }
}