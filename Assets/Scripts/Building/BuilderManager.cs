using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    public GameManager gameManager;

    public List<GameObject> buildings = new List<GameObject>();

    public void AddBuilding(GameObject preview, Vector3 pos)
    {
        Vector3 newPos = new Vector3(pos.x, 0.9f, pos.z);
        var building = Instantiate(preview, newPos, Quaternion.identity);
        buildings.Add(building);
        gameManager.SetIsBuilding(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartBuilding(GameObject buildingObject)
    {
        BuildingPreview buildingPreview = GetComponentInChildren<BuildingPreview>();

        gameManager.SetIsBuilding(true);
        buildingPreview.SetPreviewObj(buildingObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameManager.isBuilding);
        if (gameManager.isBuilding && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("END BUILDING");
        }
    }
}
