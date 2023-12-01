using System;
using System.Collections.Generic;
using TTT.Shared.Models;
using UnityEngine;

namespace TTT.Client.Game
{
    public class WinLineUI : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Serializable]
        private class LinePosition
        {
            public WinLineType WinLineType;
            public RectTransform position;
        }

        [SerializeField]
        private LinePosition[] linePositions = Array.Empty<LinePosition>();
        
        private Dictionary<byte, RectTransform> linePositionsByType;

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            linePositionsByType = new Dictionary<byte, RectTransform>();
            foreach (var linePosition in linePositions)
            {
                linePositionsByType[(byte)linePosition.WinLineType] = linePosition.position;
            }
        }
    }
}