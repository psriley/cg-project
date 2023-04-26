using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverManager : MonoBehaviour
{
    [SerializeField] float waveHeight = 1f;
    public Vector2 waveDirection = new Vector2(0, -0.15f);
    /*[SerializeField] float wavelength = 5f;*/
    [SerializeField] float waveFrequency = 0.31f;
    public float currentSpeed = 0.35f;

    public Transform river;

    Material riverMat;
    Texture2D waveDisplacement;

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
        /*return river.position.y + waveDisplacement.GetPixelBilinear(position.x * waveFrequency * river.localScale.x, (position.z * waveFrequency + Time.time * waveDirection.magnitude) * river.localScale.z).g * waveHeight;*/
        return river.position.y + waveDisplacement.GetPixelBilinear(position.x * waveFrequency, position.z * waveFrequency + Time.time * waveDirection.magnitude).g * waveHeight * (river.localScale.x/10);
        
        /*return river.position.y + waveDisplacement.GetPixelBilinear(position.x * wavelength * river.localScale.x, (position.z * wavelength + Time.time * waveDirection.magnitude) * river.localScale.z).g * waveHeight;*/
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
        riverMat.SetFloat("_WaveFrequency", waveFrequency);
        riverMat.SetVector("_WaveDirection", waveDirection);
        riverMat.SetFloat("_WaveHeight", waveHeight);
    }
}
