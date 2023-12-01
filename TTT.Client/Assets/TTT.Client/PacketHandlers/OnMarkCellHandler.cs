using MessagePipe;
using TTT.Client.Gameplay;
using TTT.Client.LocalMessages;
using TTT.Shared.Handlers;
using TTT.Shared.Models;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnMarkCellHandler : PacketHandler<NetOnMarkCell>
    {
        private readonly IGameManager gameManager;
        private readonly IPublisher<OnCellMarked> onCellMarkedPublisher;

        public OnMarkCellHandler(
            IGameManager gameManager,
            IPublisher<OnCellMarked> onCellMarkedPublisher)
        {
            this.gameManager = gameManager;
            this.onCellMarkedPublisher = onCellMarkedPublisher;
        }

        protected override void Handle(NetOnMarkCell message, int connectionId)
        {
            gameManager.ActiveGame.SwitchCurrentPlayer();
            if (gameManager.IsMyTurn)
            {
                if (message.Outcome == MarkOutcome.None)
                {
                    gameManager.InputEnabled = true;
                }
            }

            if (message.Outcome > MarkOutcome.None)
            {
                gameManager.InputEnabled = false;    
            }
            
            onCellMarkedPublisher.Publish(new OnCellMarked(
                message.Actor,
                message.Index,
                message.Outcome,
                message.WinLine));
        }
    }
}