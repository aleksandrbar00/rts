using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    public LayerMask clickable;
    public GameObject groundMarker;

    private Camera cam;
    public  static SelectionManager Instance { get; set; }

    public List<GameObject> allUnitsObjects = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();


    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } 
        else {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable)) {
                var selected = hit.collider.gameObject;

                Debug.Log(selected);
                if (Input.GetKey(KeyCode.LeftShift)) {
                    MultiSelectByClicking(selected);
                }
                else
                {
                    SelectByClicking(selected);
                }
            }
            else {
                DeselectAll();
            }
        }
    }

    public void DragSelect(GameObject selected) 
    {
        if (unitsSelected.Contains(selected)) return;

        unitsSelected.Add(selected);
        EnableSelected(selected);
    }

    private void MultiSelectByClicking(GameObject selected)
    {
        if (unitsSelected.Contains(selected))
        {
            unitsSelected.Remove(selected);
            EnableSelected(selected);
        }
        else
        {
            unitsSelected.Add(selected);
            EnableSelected(selected);
        }
    }

    private void SelectByClicking(GameObject selected)
    {
        DeselectAll();

        unitsSelected.Add(selected);

        EnableSelected(selected);
    }

    private void EnableSelected(GameObject selected)
    {
        selected.GetComponent<UnitMovement>().enableMovement = true;
        selected.GetComponent<Unit>().EnableIndicator(true);
    }

    public void DeselectAll()
    {
        unitsSelected.ForEach(item => {
           item.GetComponent<UnitMovement>().enableMovement = false;
           item.GetComponent<Unit>().EnableIndicator(false);
        });
        unitsSelected.Clear();
    }
}
