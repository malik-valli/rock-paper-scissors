using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GraphicRaycastManager : Singleton<GraphicRaycastManager>, IManager
{
    private Canvas canvas;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    public List<RaycastResult> AllCurentTargets { get; private set; } // Targets on all layers.
    public GameObject CurrentTarget { get; private set; } // Topmost target.

    private void Awake()
    {
        canvas = UIManager.Instance.Canvas;
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    private void Update()
    {
        CurrentTarget = null; // Clearing the previous target.

        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        AllCurentTargets = new List<RaycastResult>();

        graphicRaycaster.Raycast(pointerEventData, AllCurentTargets);

        if (AllCurentTargets.Count > 0)
            CurrentTarget = AllCurentTargets[0].gameObject; // Select the topmost layer.
    }
}
