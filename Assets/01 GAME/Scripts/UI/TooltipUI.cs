using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    [SerializeField] RectTransform canvasRectTransform;
    RectTransform _thisRectTransform;
    TextMeshProUGUI _textMeshPro;
    RectTransform _backgroundRectTransform;
    TooltipTimer _tooltipTimer;

    void Awake()
    {
        Instance = this;
        _thisRectTransform = GetComponent<RectTransform>();
        _textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        _backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        Hide();
    }

    void SetText(string tooltipText)
    {
        _textMeshPro.SetText(tooltipText);
        _textMeshPro.ForceMeshUpdate();


        Vector2 textSize = _textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        _backgroundRectTransform.sizeDelta = textSize + padding;
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

    void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + _backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - _backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + _backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - _backgroundRectTransform.rect.height;
        }

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