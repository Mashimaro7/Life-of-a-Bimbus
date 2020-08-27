using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BimbusGUIDisplay : MonoBehaviour
{
    private BimbusSelect select;
    private BimbuStats stats;
    public TextMeshProUGUI thirstText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI nameText;
    public GameObject selectedPanel;

    private void Awake()
    {
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
            thirstText.text = stats.thirst.ToString();
            hungerText.text = stats.hunger.ToString();
            nameText.text = stats.bimbusName;
        }
        else if(select.selectedBimbi.Count == 0)
        {
            selectedPanel.SetActive(false);
        }
    }
}
