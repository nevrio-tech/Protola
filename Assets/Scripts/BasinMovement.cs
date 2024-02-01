using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasinMovement : MonoBehaviour
{
    [SerializeField] private GameObject basin;
    [SerializeField] private GameObject hole;

    [SerializeField] private Camera mainCam;
    [SerializeField] private float speed = 1;
    [SerializeField] private float Counterspeed = 1;
    [SerializeField] private GameObject counterWhole;

    private GameObject _instanciateBasin;
    private GameObject _instanciateCounter;

    public static bool _isSelected;
    private bool _isBasinGenerate = false;
    private bool _isHoleInstanciate = false;
    public GameObject currentBasin;
    public GameObject currentHole;
    public GameObject currentCounter;

    public LayerMask layerMask;
    public LayerMask CounterlayerMask;

    private bool isBasinInstanciate = false;
    private bool isCounterInstanciate = false;
    private bool isCounterSelected = false;
    private bool isBasinSelected = false;

    [SerializeField] private Slider widthSlider;
    [SerializeField] private Slider thicknessSlider;
    [SerializeField] private Slider depthSlider;
    [SerializeField] private Material defaultMat;

    [SerializeField] private CounterTypeSO counterTypeSO;

    private float width;
    private float thickness;
    private float depth;
    private void Start()
    {
        
    }

    void Update()
    {
  
        CounterAndSinkMovementAndGerenartion();

        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isSelected = false;
        }
        ChangingSizeOfCounter();
    }

    private void CounterAndSinkMovementAndGerenartion()
    {
        if (Input.touchCount == 1)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, CounterlayerMask))
            {

                //if (raycastHit.collider.tag == "Counter" && isCounterInstanciate == false)
                //{
                //    CounterInstanciate();
                //}

                if(isBasinSelected == false)
                {
                    if (raycastHit.collider.tag == "Counter" && isCounterSelected == false && Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        isCounterSelected = true;
                        currentCounter.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.gray;
                    }

                    if (isCounterSelected && raycastHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        _isSelected = true;
                        Vector3 targetPosition = new Vector3(raycastHit.point.x, counterWhole.transform.position.y, raycastHit.point.z);
                        counterWhole.transform.position = Vector3.Lerp(counterWhole.transform.position, targetPosition, Time.deltaTime * Counterspeed);
                    }

                    if (isCounterSelected && Input.GetTouch(0).phase == TouchPhase.Ended && raycastHit.collider.tag == "Grid")
                    {
                        isCounterSelected = false;
                        currentCounter.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                    }

                }


                /////
                ///
                if(isCounterSelected == false)
                {
                    SinkMovement(raycastHit);
                }
                

            }

        }

    }

    private void SinkMovement(RaycastHit rayHit)
    {
        if (isBasinSelected && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isBasinSelected = false;
            currentBasin.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = defaultMat.color;
            int BasinCount = currentCounter.transform.GetChild(1).transform.childCount;
            Destroy(currentCounter.transform.GetChild(1).transform.GetChild(0).gameObject);
            Debug.Log("Basin count : " + BasinCount);

        }
        if (rayHit.collider.tag == "Basin" && isBasinSelected == false && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            BasinInstanciate();
            isBasinSelected = true;
            currentBasin.transform.localPosition = new Vector3(currentBasin.transform.localPosition.x, currentBasin.transform.localPosition.y + .0010f, currentBasin.transform.localPosition.z);
            currentBasin.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        if (isBasinSelected && rayHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved && isCounterSelected != true)
        {
            _isSelected = true;
            Vector3 targetPosition = new Vector3(rayHit.point.x, currentBasin.transform.position.y, rayHit.point.z);
            _instanciateBasin.transform.position = Vector3.Lerp(currentBasin.transform.position, targetPosition, Time.deltaTime * speed);
        }

    }


    private void BasinInstanciate()
    {
        _instanciateBasin = Instantiate(currentBasin, currentBasin.transform.position, Quaternion.identity);
        _instanciateBasin.transform.parent = currentCounter.transform.GetChild(1).transform;
        currentBasin = _instanciateBasin;
        //isBasinInstanciate = true;
    }


    private void CounterInstanciate()
    {
        _instanciateCounter = Instantiate(currentCounter, currentCounter.transform.position, Quaternion.identity);
        _instanciateCounter.transform.parent = currentCounter.transform.parent;
        currentCounter = _instanciateCounter;
        isCounterInstanciate = true;
        if(_isBasinGenerate)
        {
            currentBasin = _instanciateCounter.transform.GetChild(1).transform.GetChild(0).gameObject;
        }
    }

    public void BasinGererator()
    {
        if(_isBasinGenerate == false)
        {
            SettingBasinPosition();

            currentBasin = Instantiate(basin, currentCounter.transform.position, Quaternion.identity);
            currentBasin.transform.parent = currentCounter.transform.GetChild(1).transform;
            //Vector3 currentCounterPos = new Vector3(currentCounter.transform.localPosition.x, 0f, currentCounter.transform.localPosition.z);
            currentBasin.transform.localPosition = new Vector3(0f, -0.001f, 0);
            _isBasinGenerate = true;
        }

    }

    private void SettingBasinPosition()
    {
        Vector3 basinPos = new Vector3(0f, currentCounter.transform.GetChild(0).localScale.y / 2, 0f);
        currentCounter.transform.GetChild(1).transform.localPosition = basinPos;
    }

    public void HolwGererator()
    {
        if (_isHoleInstanciate == false)
        {
            currentHole = Instantiate(hole, currentCounter.transform.position, Quaternion.identity);
            currentHole.transform.parent = currentCounter.transform;
            currentHole.transform.localPosition =   new Vector3(0, -0.1247f, 0);
            _isHoleInstanciate = true;
        }
            
    }


    private void ChangingSizeOfCounter()
    {
         width = widthSlider.value;
         thickness = thicknessSlider.value;
         depth =  depthSlider.value;
        currentCounter.transform.GetChild(0).transform.localScale = new Vector3(width, currentCounter.transform.GetChild(0).transform.localScale.y, depth);
        currentCounter.transform.position = new Vector3(currentCounter.transform.position.x, thickness, currentCounter.transform.position.z);

    }

    public void DeleteWholeCounter()
    {
        if(isCounterSelected)
        {
            Destroy(counterWhole);

        }
        if(isBasinSelected)
        {
            Destroy(currentBasin);
            _isBasinGenerate = false;

        }

    }


    public void Json()
    {
        Vector3 rotation = currentCounter.transform.eulerAngles;
        Vector3 position = counterWhole.transform.position;
        counterTypeSO.SetCounterRotationAndPosition(rotation, position);
       // counterTypeSO.SettingCounterSize(length, hight, depth);
        string jsonFormat = JsonUtility.ToJson(counterTypeSO.counterModel);
        Debug.Log("jsonFormat : " + jsonFormat);
    }

    public void CreateInstanceOfSo()
    {
        CounterTypeSO CountSo = ScriptableObject.CreateInstance<CounterTypeSO>();
        Debug.Log("CreateInstanceOfSo" + CountSo.GetType());
    }
}
