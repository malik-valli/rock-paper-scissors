using UnityEngine;
using UnityEngine.UI;

public class HandHandler : MonoBehaviour
{
    [SerializeField] private GameObject tablePlace;
    public GameObject TablePlace { get { return tablePlace; } }

    public GameObject CurrentHand { get; private set; }
    public bool IsEmpty
    {
        get
        {
            if (CurrentHand == null) return true;
            else return false;
        }
    }

    private Image image;
    private float currentOpacity;

    private bool isHighlighted;

    private void Awake() =>
        image = GetComponent<Image>();

    private void Update() =>
        Glow();

    private void LateUpdate() =>
        isHighlighted = false;

    private void Glow()
    {
        if (currentOpacity > 0 && !isHighlighted)
            currentOpacity -= Time.deltaTime / 3;
        else if (currentOpacity < 0.33f && isHighlighted)
            currentOpacity += Time.deltaTime;

        image.color = new Color(image.color.r, image.color.g, image.color.b, currentOpacity);
    }

    public void Highlight() =>
        isHighlighted = true;

    public void AddHand(Hand.HandType handType)
    {
        if (!IsEmpty)
        {
            UIManager.Instance.PrintMessage("Выбор сделан!");
            return;
        }
        if (!PlayerInventory.Instance.HandRemained(handType))
        {
            UIManager.Instance.PrintMessage("Руки нет!");
            return;
        }
        if (RoundManager.Instance.RoundFinishes)
        {
            UIManager.Instance.PrintMessage("Раунд завершается!");
            return;
        }

        GameObject hand = PlayerInventory.Instance.GetHand(handType);
        CurrentHand = hand;
        Instantiate(hand, tablePlace.transform);
    }

    public void Clear()
    {
        CurrentHand = null;
        if (tablePlace.transform.childCount != 0)
            Destroy(tablePlace.transform.GetChild(0).gameObject);
    }
}
