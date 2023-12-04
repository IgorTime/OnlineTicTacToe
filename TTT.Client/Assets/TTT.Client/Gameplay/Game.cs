using System;
using TTT.Shared.Models;

namespace TTT.Client.Gameplay
{
    public class Game
    {
        public Guid? GameId { get; set; }
        public string XUser { get; set; }
        public string OUser { get; set; }
        public string CurrentUser { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public void SwitchCurrentPlayer()
        {
            CurrentUser = GetOpponent(CurrentUser);
        }

        private string GetOpponent(string currentUser) => XUser == currentUser ? OUser : XUser;

        public MarkType GetUserMark(string userId)
        {
            if (userId == XUser)
            {
                return MarkType.X;
            }

            if (userId == OUser)
            {
                return MarkType.O;
            }

            throw new ArgumentException("User is not part of this game");
        }
    }
}