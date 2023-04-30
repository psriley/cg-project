using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    /*public float waveHeight = 3.26f;
    public float waveFrequency = 8.62f;
    public float waveSpeed = 0.2f;*/
    public float waveHeight = 0.07f;
    public float waveFrequency = 0.01f;
    public float waveSpeed = 0.04f;
    public Transform river;

    Material riverMat;
    Texture2D waveDisplacement;

    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }

    void SetVariables()
    {
        riverMat = river.GetComponent<Renderer>().sharedMaterial;
        waveDisplacement = (Texture2D)riverMat.GetTexture("_WaveDisplacement");
    }

    public float WaterHeightAtPosition(Vector3 position)
    {
        return river.position.y + waveDisplacement.GetPixelBilinear(position.x * waveFrequency * (river.localScale.x/50), (position.z * waveFrequency + Time.time * waveSpeed) * (river.localScale.z/50)).g * waveHeight;
        /*return river.position.y + waveDisplacement.GetPixelBilinear(position.x * waveFrequency, position.z * waveFrequency + Time.time * waveSpeed).g * waveHeight * river.localScale.x;*/
    }

    private void OnValidate()
    {
        if (!riverMat)
        {
            SetVariables();
        }
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        riverMat.SetFloat("_Wavelength", waveFrequency);
        riverMat.SetFloat("_WaveSpeed", waveSpeed);
        riverMat.SetFloat("_Wavelength", waveHeight);
    }
}
