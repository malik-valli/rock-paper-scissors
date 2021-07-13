using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryHand : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Canvas canvas;

    private RectTransform rectTransform;
    private Vector2 initialPosition;

    [SerializeField] private Hand.HandType handType;

    [SerializeField] private Text countIndicator;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;

        countIndicator = GetComponentInChildren<Text>();
    }

    private void Start() =>
        canvas = UIManager.Instance.Canvas;

    private void Update() =>
        CheckCount();

    public void OnDrag(PointerEventData eventData)
    {
        MoveBehindMouse(eventData);
        FindAnyPanel()?.Highlight();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        FindAnyPanel()?.AddHand(handType);
        ReturnInitialPosition();
    }

    private void MoveBehindMouse(PointerEventData eventData) =>
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    private void ReturnInitialPosition() =>
        rectTransform.anchoredPosition = initialPosition;

    private HandHandler FindAnyPanel()
    {
        if (GraphicRaycastManager.Instance.CurrentTarget != null)
            if (GraphicRaycastManager.Instance.CurrentTarget.gameObject.TryGetComponent<HandHandler>(out HandHandler panel))
                return panel;
        return null;
    }

    private void CheckCount()
    {
        switch (handType)
        {
            case Hand.HandType.Rock:
                countIndicator.text = PlayerInventory.Instance.RockCount.ToString();
                break;
            case Hand.HandType.Paper:
                countIndicator.text = PlayerInventory.Instance.PaperCount.ToString();
                break;
            case Hand.HandType.Scissors:
                countIndicator.text = PlayerInventory.Instance.ScissorsCount.ToString();
                break;
            default:
                countIndicator.text = "";
                break;
        }
    }
}
