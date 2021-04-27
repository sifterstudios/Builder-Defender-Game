using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;

    Transform _barTransform;

    void Awake()
    {
        _barTransform = transform.Find("bar");
    }

    void Start()
    {
        healthSystem.OnDamaged += (sender, e) =>
        {
            UpdateBar();
            UpdateHealthBarVisible();
        };

        UpdateBar();
        UpdateHealthBarVisible();
    }

    void UpdateBar()
    {
        _barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}