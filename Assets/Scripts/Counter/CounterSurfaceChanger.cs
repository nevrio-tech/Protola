using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CounterSurfaceChanger : MonoBehaviour
{
    [SerializeField] private List<Material> counterMat = new List<Material>();
    [SerializeField] private List<Texture> counterTex = new List<Texture>();
    [SerializeField] private List<Texture> counterGranulateTex = new List<Texture>();
    [SerializeField] private List<Texture> counterGranulateTexMap = new List<Texture>();
    [SerializeField] private Texture colorDefaultTex;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private MainUiController mainUiController;
    [SerializeField] private UiModel uiModel;
    [SerializeField] private Plywoodcontroller plywoodcontroller;

    [SerializeField] private Texture2D DefaultColorTexture;
    [SerializeField] private Texture2D DefaultColorTextureMap;
    private Color lastSelectedColor = Color.white;
    private Color basinlastSelectedColor = Color.white;
    public List<Color> colors = new List<Color>();

    void Start()
    {
        
    }

    public void ChangingCounterSurfaceTexture(int material)
    {
        GameObject selectedObjcet = basinMovement.SelectedGameobject;
        if(selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.white;
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.mainTexture = counterTex[material];
        }

        else
        {
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].color = Color.white;
            Material mat =  selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("_Texture2D", counterTex[material]);
            mat.SetTexture("_AlphaTexture", Texture2D.whiteTexture);
            ChangingThePlywoodSurface(material);

            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0].renderQueue = 3003;
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].renderQueue = 3002;
        }
        
    }

    public void ChangingSurfaceGranulateTexture(int material)
    {
        GameObject selectedObjcet = basinMovement.SelectedGameobject;
        if (selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.mainTexture = counterGranulateTex[material];
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = basinlastSelectedColor;
        }

        else
        {
            // for counter

            Material mat = selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("_Texture2D", counterGranulateTex[material]);
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", counterGranulateTexMap[material]);
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.color = lastSelectedColor;
            Debug.Log("granulate textures here : " + material);
            // for plywood 
            foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
            {
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D",counterGranulateTex[material]);
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", counterGranulateTexMap[material]);
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = lastSelectedColor;
            }

        }

    }

    public void ChangingCounterSurfaceColor(int color)
    {
        GameObject selectedObjcet = basinMovement.SelectedGameobject;
        if (selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.mainTexture = colorDefaultTex;

            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = colors[color];
            basinlastSelectedColor = colors[color];
        }
        else
        {
            // for counter
            Material mat = selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("__Texture2D", DefaultColorTexture);
            mat.SetTexture("_AlphaTexture", DefaultColorTextureMap);
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].color = colors[color];
            // for plywood 
            foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
            {
                Material plywoodMat = obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0];
                plywoodMat.SetTexture("__Texture2D", DefaultColorTexture);
                plywoodMat.SetTexture("_AlphaTexture", DefaultColorTextureMap);
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].color = colors[color];
            }

            lastSelectedColor = colors[color];

        }

    }


    private void ChangingThePlywoodSurface(int material)
    {
        foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
        {
            Material mat = obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1];
            mat.color = Color.white;
            obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D", counterTex[material]);
            obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);

            //obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].
        }
    }
}
