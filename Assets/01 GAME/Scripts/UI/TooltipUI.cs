using TMPro;
using UnityEngine;

namespace UI
{
    public class TooltipUI : MonoBehaviour
    {
        [SerializeField] RectTransform canvasRectTransform;
        RectTransform _backgroundRectTransform;
        TextMeshProUGUI _textMeshPro;
        RectTransform _thisRectTransform;
        TooltipTimer _tooltipTimer;
        public static TooltipUI Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            _thisRectTransform = GetComponent<RectTransform>();
            _textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
            _backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

            Hide();
        }

        void Update()
        {
            HandleFollowMouse();

            if (_tooltipTimer != null)
            {
                _tooltipTimer.Timer -= Time.deltaTime;
                if (_tooltipTimer.Timer <= 0) Hide();
            }
        }

        void SetText(string tooltipText)
        {
            _textMeshPro.SetText(tooltipText);
            _textMeshPro.ForceMeshUpdate();


            var textSize = _textMeshPro.GetRenderedValues(false);
            var padding = new Vector2(8, 8);
            _backgroundRectTransform.sizeDelta = textSize + padding;
        }

        void HandleFollowMouse()
        {
            Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
            if (anchoredPosition.x + _backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
                anchoredPosition.x = canvasRectTransform.rect.width - _backgroundRectTransform.rect.width;

            if (anchoredPosition.y + _backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
                anchoredPosition.y = canvasRectTransform.rect.height - _backgroundRectTransform.rect.height;

            _thisRectTransform.anchoredPosition = anchoredPosition;
        }

        public void Show(string tooltipText, TooltipTimer tooltipTimer = null)
        {
            _tooltipTimer = tooltipTimer;
            gameObject.SetActive(true);
            SetText(tooltipText);
            HandleFollowMouse();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public class TooltipTimer
        {
            public float Timer;
        }
    }
}