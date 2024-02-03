using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSurfaceChanger : MonoBehaviour
{
    [SerializeField] private List<Material> counterMat = new List<Material>();
    [SerializeField] private BasinMovement basinMovement;
    

    public List<Color> colors = new List<Color>();

    void Start()
    {
        
    }

    public void ChangingCounterSurface(int material)
    {
        GameObject selectedObjcet = basinMovement.currentSelectedObject;
        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material = counterMat[material];
        }
        else
        {
            selectedObjcet.transform.Find("Counter").GetComponent<MeshRenderer>().material = counterMat[material];
        }
        
    }
    public void ChangingCounterSurfaceColor(int color)
    {
        GameObject selectedObjcet = basinMovement.currentSelectedObject;
        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = colors[color];
        }
        else
        {
            selectedObjcet.transform.Find("Counter").GetComponent<MeshRenderer>().material.color = colors[color]; 
        }

    }
}
