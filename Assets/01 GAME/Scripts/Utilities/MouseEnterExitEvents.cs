using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities
{
    public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEnter?.Invoke(this, EventArgs.Empty);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExit?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnMouseEnter;
        public event EventHandler OnMouseExit;
    }
}