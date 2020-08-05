using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    public float maxNightTime, maxDayTime,maxTime, sunIntensityGoal, moonIntensityGoal, temperature, timeMultiplier = 1, randomWeatherDelay, sunIntensityMultiplier = 1f;
    public float sunInitIntense, weatherDelay;
    private WeatherControl weather;
    [Range(0, 1)] [SerializeField]private float weatherOfDay, timeOfDay, timeToTurnNight, timeToTurnDay;
    public int calendarDay = 1, yearNum;
    public const int daysInYear = 40;
    public Light sunLight, moonLight;
    public Gradient sunGrad;
    public string currentSeason;
    public string[] seasons;
    public bool isNight,sunRising,sunSetting,shineRoutine,moonRoutine;
    public Animator sunAnimator;
    List<BimbuStats> bimbi = new List<BimbuStats>();
    public int[] avgTemp;

    void Start()
    {
        bimbi = new List<BimbuStats>();
        weather = FindObjectOfType<WeatherControl>();
        sunInitIntense = sunLight.intensity;
        seasons = new string[] { "Spring", "Summer", "Fall", "Winter" };
        avgTemp = new int[daysInYear];
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

        sunLight.transform.localRotation = Quaternion.Euler((timeOfDay * 360) -90,170,0);
        moonLight.transform.eulerAngles = new Vector3(moonLight.transform.eulerAngles.x, maxNightTime / 10 - (timeOfDay / 5), moonLight.transform.eulerAngles.z);
    }
    IEnumerator SunShine()
    {
        shineRoutine = true;
        yield return new WaitForSeconds(0.1f);
        if (sunLight.intensity < sunIntensityGoal) sunLight.intensity += (Time.deltaTime / 10) * sunIntensityMultiplier;
        if (sunLight.intensity > sunIntensityGoal) sunLight.intensity -= (Time.deltaTime / 10) * sunIntensityMultiplier;
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
        if(weatherOfDay > 0.3f && !isNight)
        {
            SunnyTime();
        }
      
        if(weatherOfDay <= 0.3f)
        {
            RainDay();
        }
        yield return new WaitForSeconds(weatherDelay + Random.Range(0, randomWeatherDelay));
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
