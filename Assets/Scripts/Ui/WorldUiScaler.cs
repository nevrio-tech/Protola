using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUiScaler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject worldUiObject;
    private float dist;
    [SerializeField] private Vector3 InitialScale;
    [SerializeField] private bool IsSliderMoving = false;

    private void Start()
    {
        InitialScale = CalculatingSizeOfWorldUiCanvas();
    }

    void Update()
    {
        if(IsSliderMoving != true)
        {
            CalculatingSizeOfWorldUiCanvas();
        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseTheCircleSize(0.01f);

        }
    }

    private Vector3 CalculatingSizeOfWorldUiCanvas()
    {
        
        dist = Vector3.Distance(worldUiObject.transform.position, mainCamera.transform.position);
        Canvas worldUiCanvas = worldUiObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Canvas>();
        Vector3 vect = (Vector3.one * dist / 3) / 50;
        if(vect.x >= InitialScale.x && vect.x <= 0.13f)
        {
            GetAllTheWorldUiCanvas(vect);
        }
        
        return vect;
    }

    void GetAllTheWorldUiCanvas(Vector3 vect)
    {
        foreach(Transform canvas in worldUiObject.transform)
        {
            canvas.transform.localScale = vect;
        }
    }

    private void IncreaseTheCircleSize(float val)
    {
        foreach (Transform canvas in worldUiObject.transform)
        {
            canvas.transform.localScale += Vector3.one * val;
        }

    }

}
