using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpdatableData : ScriptableObject
{
    public event System.Action OnValuesUpdated;
    public bool autoUpdate;

#if UNITY_EDITOR

    protected virtual void OnValidate()
    {
        if (autoUpdate)
        {
            EditorApplication.update += NotifyOfValueUpdate;
        }
    }

    public void NotifyOfValueUpdate()
    {
        EditorApplication.update -= NotifyOfValueUpdate;
        if (OnValuesUpdated != null)
            OnValuesUpdated?.Invoke();
    }
#endif
}
