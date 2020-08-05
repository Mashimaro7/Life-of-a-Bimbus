using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GeographicalProperties
{
    public float[] avgTemp;
    [Range(0,1)]public float rainFcy;
    public string[] biomes;

    public GeographicalProperties(float[] avgTemp,float rainFrequency, string[] biome)
    {
        this.avgTemp[0] = avgTemp[0];
        this.rainFcy = Mathf.Clamp01(rainFrequency);
        this.biomes[0] = biome[0];
    }


}
public class WeatherControl : MonoBehaviour
{
    public ParticleSystem[] particles;
    [Header("Weather Settings")]
    public bool raining;
    [Range(0f,1f)]public float clouds;
    public bool snowing, dusty, leaves, fogger,cloudy;
    [Range(0, 1000)] public int rainRate;
    [Range(0f, 1f)] public float cloudRate;
    [Range(0, 20)] public int backFogRate, frontFogRate;
    [Range(0, 100)] public int leavesRate;

    void Update()
    {
        if (raining) StartRain();
        else StopRain();
        if (cloudy) StartCloudyDay();
        else ClearClouds();
    }

    public void StartRain()
    {
        ParticleSystem.EmissionModule rn = particles[1].emission;
        rn.rateOverTime = rainRate;
    }

    public void StopRain()
    {
        ParticleSystem.EmissionModule rn = particles[1].emission;
        rn.rateOverTime = 0;
    }

    public void StartCloudyDay()
    {
        ParticleSystem.EmissionModule cloud= particles[0].emission;
        cloud.rateOverTime = cloudRate * (clouds * 10f);
    }

    public void ClearClouds()
    {
        ParticleSystem.EmissionModule cloud = particles[0].emission;
        cloud.rateOverTime = 0;
    }
}
