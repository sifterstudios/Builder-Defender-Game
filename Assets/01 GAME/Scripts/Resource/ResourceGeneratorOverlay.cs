using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] ResourceGenerator _resourceGenerator;
    Transform _barTransform;

    void Start()
    {
        _barTransform = transform.Find("bar");
        ResourceGeneratorData resourceGeneratorData = _resourceGenerator.GetResourceGeneratorData();
       transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
       transform.Find("text").GetComponent<TextMeshPro>()
           .SetText(_resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));

    }

    void Update()
    {
        _barTransform.localScale = new Vector3(1 - _resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
