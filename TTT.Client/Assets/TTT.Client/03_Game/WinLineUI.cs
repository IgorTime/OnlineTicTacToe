using System;
using System.Collections.Generic;
using DG.Tweening;
using TriInspector;
using TTT.Shared.Models;
using UnityEngine;

namespace TTT.Client.Game
{
    public class WinLineUI : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Serializable]
        private class LinePosition
        {
            public WinLine WinLine;
            public RectTransform position;
        }

        [SerializeField]
        private RectTransform line;

        [SerializeField]
        private LinePosition[] linePositions = Array.Empty<LinePosition>();

        [Header("Animation:")]
        [SerializeField]
        private float animationDuration = 0.2f;

        [SerializeField]
        private float delay = 0.2f;
        
        [SerializeField]
        private Ease animationEase = Ease.Linear;
        

        private Dictionary<byte, RectTransform> linePositionsByType;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            linePositionsByType = new Dictionary<byte, RectTransform>();
            foreach (var linePosition in linePositions)
            {
                linePositionsByType[(byte) linePosition.WinLine] = linePosition.position;
            }
        }

        [Button]
        public void SetLine(WinLine WinLine, bool withAnimation = false)
        {
            line.gameObject.SetActive(true);

            if (!linePositionsByType.TryGetValue((byte) WinLine, out var linePosition))
            {
                Debug.LogError($"No line position for {WinLine}");
                return;
            }

            line.SetPositionAndRotation(linePosition.position, linePosition.rotation);

            if (withAnimation)
            {
                line.localScale = Vector3.zero;
                line.DOScale(1f, animationDuration)
                    .SetEase(animationEase)
                    .SetDelay(delay)
                    .SetLink(gameObject);
            }
        }

        public void Reset()
        {
            line.gameObject.SetActive(false);
        }
    }
}