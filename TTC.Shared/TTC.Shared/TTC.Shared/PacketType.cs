namespace TTC.Shared
{
    public enum PacketType : byte
    {
        #region Client to Server

        Invalid = 0,
        AuthRequest = 1,
        ServerStatusRequest = 2,

        #endregion

        #region Server to Client

        OnAuth = 100,
        OnAuthFail = 101,
        OnServerStatus = 102,

        #endregion
    }
}