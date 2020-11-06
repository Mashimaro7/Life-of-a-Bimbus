using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BimbusGUIDisplay : MonoBehaviour
{
    private BimbusSelect select;
    private BimbuStats stats;
    public TextMeshProUGUI thirstText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI nameText;
    public GameObject selectedPanel;
    public Slider hungerSlid, thirstSlid,healthSlid;

    private void Awake()
    {
        
        hungerSlid = GameObject.Find("HungerSlider").GetComponent<Slider>();
        thirstSlid = GameObject.Find("ThirstSlider").GetComponent<Slider>();
        healthSlid = GameObject.Find("HealthSlider").GetComponent<Slider>();
        selectedPanel = GameObject.Find("Character Panel");
        nameText = selectedPanel.gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();


        select = FindObjectOfType<BimbusSelect>();
        stats = GetComponent<BimbuStats>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (select.selectedBimbi.Contains(this.GetComponent<BimbusMove>()) && select.selectedBimbi.Count < 2)
        {
            selectedPanel.SetActive(true);
            thirstSlid.value = stats.thirst;
            hungerSlid.value = stats.hunger;
            healthSlid.value = stats.health;
            nameText.text = stats.bimbusName;
        }
        else if(select.selectedBimbi.Count == 0 && selectedPanel.activeSelf)
        {
            selectedPanel.SetActive(false);
        }
    }
}
