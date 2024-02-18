using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterSizeDeformation : MonoBehaviour
{
    [SerializeField] private Slider widthSlider;
    [SerializeField] private Slider thicknessSlider;
    [SerializeField] private Slider depthSlider;
    private float width;
    private float thickness;
    private float depth;
    private CounterGenerator counterGenerator;
    private Transform currentCounter;

    void Start()
    {
        counterGenerator = GetComponent<CounterGenerator>();
        currentCounter = counterGenerator.currentCounter.transform;
    }
    private void Update()
    {
        ChangingSizeOfCounter();
    }

    private void ChangingSizeOfCounter()
    {
        width = widthSlider.value;
        thickness = thicknessSlider.value;
        depth = depthSlider.value;

       currentCounter.transform.Find("Counter").transform.localScale = new Vector3(width, currentCounter.transform.Find("Counter").transform.localScale.y, depth);
       currentCounter.position = new Vector3(currentCounter.position.x, thickness, currentCounter.position.z);

    }
}