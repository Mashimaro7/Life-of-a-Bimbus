﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalizerUI : MonoBehaviour
{
    TextMeshProUGUI textField;

    public LocalizedString localizedString;

    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = localizedString.value;
        value = value.TrimStart(' ', '"');
        value = value.Replace("\"", "");
        textField.text = value;
    }
}
