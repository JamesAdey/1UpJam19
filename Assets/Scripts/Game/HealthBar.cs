﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    RectTransform healthRectTrans;

    [SerializeField]
    RectTransform thisRectTrans;

    [SerializeField]
    public float maxHealth = 100f;

    [SerializeField]
    float myHealth = 100f;

    [SerializeField]
    public Transform parentTransform;

    private void Start()
    {
        myHealth = maxHealth;
        thisRectTrans = GetComponent<RectTransform>();
        thisRectTrans.position = new Vector3(700, 700, 700);

        Vector3 scale = healthRectTrans.localScale;
        scale.x = myHealth / maxHealth;
        healthRectTrans.localScale = scale;
    }

    public void Update()
    {
        if(parentTransform == null)
        {
            return;
        }
        Vector3 scale = healthRectTrans.localScale;
        scale.x = myHealth / maxHealth;
        healthRectTrans.localScale = scale;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(parentTransform.position);
        screenPoint.y += 20;
        thisRectTrans.position = screenPoint;
    }

    public void inflictDamange(float damage)
    {
        myHealth -= damage;
        myHealth = Mathf.Clamp(myHealth, 0, maxHealth);
    }

    public bool isDead()
    {
        return myHealth <= 0;
    }




}
