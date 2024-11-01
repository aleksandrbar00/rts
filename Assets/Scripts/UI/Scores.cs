using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scores : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent; // Ссылка на компонент Text
    private ResourcesManager resourcesManager;

    // Start is called before the first frame update
    void Start()
    {
        resourcesManager = FindAnyObjectByType<ResourcesManager>();
        resourcesManager.OnEventTriggered += OnScoreChanged;
    }

    void OnScoreChanged()
    {
        textComponent.text = "Score: " + resourcesManager.stoneCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
