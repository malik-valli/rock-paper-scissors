using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>, IManager
{
    [SerializeField] private GameObject rockPrefab, paperPrefab, scissorsPrefab;

    public GameObject RockPrefab { get { return rockPrefab; } }
    public GameObject PaperPrefab { get { return paperPrefab; } }
    public GameObject ScissorsPrefab { get { return scissorsPrefab; } }

    public GameObject[] HandPrefabs { get { return new GameObject[] { rockPrefab, paperPrefab, scissorsPrefab }; } }
}
