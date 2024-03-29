using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/CounterType"))]

public class CounterTypeSO : ScriptableObject
{
    public CounterModel counterModel;
    public GameObject CurrenetCounter;
    public List<GameObject> counterType = new List<GameObject>();

    public void SettingCounterSize(float length, float hight, float depth)
    {
        counterModel.length = length;
        counterModel.hight = hight;
        counterModel.depth = depth;
    }

    public void SetCounterRotationAndPosition(Vector3 rotation, Vector3 position)
    {
        counterModel.rotaton = rotation;
        counterModel.rotaton = position;
    }

    public void SettingTexture(Texture2D texture)
    {
        counterModel.texture = texture;
    }
}
