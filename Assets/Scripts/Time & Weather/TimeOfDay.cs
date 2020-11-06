using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    public float maxNightTime = 600, maxDayTime = 600, sunIntensityGoal = 2f, moonIntensityGoal= 1, temperature, timeMultiplier = 1,minRandomWeatherDelay =50 ,maxRandomWeatherDelay = 300, sunIntensityMultiplier = 1f;
    public float sunInitIntense, weatherDelay;
    private float maxTime;
    private WeatherControl weather;
    [Range(0, 1)] [SerializeField]private float weatherOfDay, timeOfDay, timeToTurnNight = 0.65f, timeToTurnDay =0.2f;
    public int calendarDay = 1, yearNum;
    public const int daysInYear = 40;
    public Light sunLight, moonLight;
    public Gradient sunGrad;
    public string currentSeason;
    public string[] seasons;
    public bool isNight,sunRising,sunSetting,shineRoutine,moonRoutine;
    public Animator sunAnimator;
    List<BimbuStats> bimbi;
    public int[] avgTemp;

    void Awake()
    {
        maxTime = maxDayTime + maxNightTime;
        sunLight = this.transform.Find("Sun").GetComponent<Light>() ;
        moonLight = this.transform.Find("Moon").GetComponent<Light>();
        bimbi = new List<BimbuStats>();
        bimbi.Add(FindObjectOfType<BimbuStats>());
        weather = FindObjectOfType<WeatherControl>();
        sunInitIntense = sunLight.intensity;
        seasons = new string[] { "IDSpring", "IDSummer", "IDFall", "IDWinter" };
        avgTemp = new int[4];
        StartCoroutine(RandomWeather());
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G) || timeOfDay >= timeToTurnDay)
        {
            if (isNight)
            {
                TurnDay(2);
            }
        }
        if (sunLight.intensity != sunIntensityGoal && !shineRoutine)
        {
            StartCoroutine(SunShine());
        }
        if (moonLight.intensity != moonIntensityGoal && !moonRoutine)
        {
            StartCoroutine(MoonShine());
        }
        if (calendarDay > 40) calendarDay = 1;
        timeOfDay += (Time.deltaTime / maxTime) * timeMultiplier;
        if (timeOfDay > timeToTurnNight && !isNight) TurnNight();
        if (timeOfDay > 1)
        {
            timeOfDay = 0;
            calendarDay++;
            foreach(var bimbus in bimbi)
            {
                if(calendarDay == bimbus.birthDay)
                {
                    bimbus.age++;
                }
            }
        }

        sunLight.transform.rotation = Quaternion.Euler((timeOfDay * 360) -90,170,0);
        moonLight.transform.eulerAngles = new Vector3(moonLight.transform.eulerAngles.x, maxNightTime / 10 - (timeOfDay / 5), moonLight.transform.eulerAngles.z);
    }
    IEnumerator SunShine()
    {
        shineRoutine = true;
        yield return new WaitForSeconds(0.1f);
        if (sunLight.intensity != sunIntensityGoal) sunLight.intensity = Mathf.Lerp(sunLight.intensity, sunIntensityGoal, 1 * Time.deltaTime / 10);
        shineRoutine = false;
        if(sunLight.intensity != sunIntensityGoal && !shineRoutine)
        {
            StartCoroutine(SunShine());
        } 
    }

    IEnumerator MoonShine()
    {
        moonRoutine = true;
        yield return new WaitForSeconds(0.1f);
        if (moonLight.intensity < moonIntensityGoal) moonLight.intensity += (Time.deltaTime / 10);
        if (moonLight.intensity > moonIntensityGoal) moonLight.intensity -= (Time.deltaTime / 10);
        
        moonRoutine = false;
        if (moonLight.intensity != moonIntensityGoal && !moonRoutine)
        {
            StartCoroutine(MoonShine());
        }
    }
    IEnumerator Sunset()
    {
        yield return new WaitForSeconds(50);
        TurnNight();
    }

    IEnumerator RandomWeather()
    {
        weatherOfDay = Random.Range(0f, 1f);
        if(weatherOfDay > 0.3f)
        {
            SunnyTime();
        }
      
        if(weatherOfDay <= 0.3f)
        {
            RainDay();
        }
        yield return new WaitForSeconds(weatherDelay + Random.Range(minRandomWeatherDelay, maxRandomWeatherDelay));
        StartCoroutine(RandomWeather());
    }

    void SunnyTime()
    {
        weather.clouds = Random.Range(0f,.5f);
        weather.raining = false;
        sunIntensityGoal = 2;
    }

    void TurnNight()
    {
        isNight = true;
        sunIntensityGoal = 0;
        moonLight.GetComponent<Light>().intensity = 1;
    }

    void TurnDay(int intensity)
    {
        sunIntensityGoal = intensity;
        isNight = false;
        currentSeason = seasons[Mathf.FloorToInt((calendarDay - 2) / 10)];
    }

    void RainDay()
    {
        weather.raining = true;
        weather.clouds = 1f;
    }

}
