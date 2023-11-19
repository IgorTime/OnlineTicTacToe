using System;

namespace TTT.Client
{
    [Serializable]
    public class NetworkSettings
    {
        public string address = "localhost";
        public int port = 9050;
        public int disconnectTimeout = 10_000;
    }
}