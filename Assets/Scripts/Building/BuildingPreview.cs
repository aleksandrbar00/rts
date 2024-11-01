using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{

    public GameObject previewObj;
    private GameObject previewInstance;
    public Camera cam;
    public LayerMask ground;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SetPreviewObj(GameObject preview)
    {
        previewObj = preview;
        Destroy(previewInstance);
    }

    // Update is called once per frame
    void Update()
    {
        if (previewObj)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground)) 
            {
                if (Input.GetMouseButtonDown(0) && previewInstance) {
                    GetComponentInParent<GameManager>()?.SetIsBuilding(false);
                    GetComponentInParent<BuilderManager>()?.AddBuilding(previewObj, hit.point);
                    previewObj = null;
                    Destroy(previewInstance);
                    return;
                }

                if (Input.GetMouseButtonDown(1)) {
                    GetComponentInParent<GameManager>().SetIsBuilding(false);
                    previewObj = null;
                    Destroy(previewInstance);
                    return;
                }

                if (!previewInstance) 
                {
                    previewInstance = Instantiate(previewObj, hit.point, previewObj.transform.rotation);
                    return;
                }

                previewInstance.transform.position = hit.point;
            }
        }
    }
}
