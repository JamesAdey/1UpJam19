using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    RectTransform thisRectTransform;

    [SerializeField]
    float percentHealth = 100f;

    private void Start()
    {
        thisRectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        Vector3 scale = thisRectTransform.localScale;
        scale.x = percentHealth / 100;
        thisRectTransform.localScale = scale;
    }

    public void inflictDamange(float damage)
    {
        percentHealth -= damage;
    }




}
