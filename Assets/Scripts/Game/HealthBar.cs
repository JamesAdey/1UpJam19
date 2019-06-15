using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    RectTransform healthRectTrans;

    [SerializeField]
    RectTransform thisRectTrans;

    [SerializeField]
    float percentHealth = 100f;

    [SerializeField]
    Transform parentTransform;

    private void Start()
    {
        thisRectTrans = GetComponent<RectTransform>();
        thisRectTrans.position = new Vector3(700, 700, 700);
    }

    public void Update()
    {

        Vector3 scale = healthRectTrans.localScale;
        scale.x = percentHealth / 100;
        healthRectTrans.localScale = scale;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(parentTransform.position);
        screenPoint.y += 20;
        thisRectTrans.position = screenPoint;
    }

    public void inflictDamange(float damage)
    {
        percentHealth -= damage;
    }

    public bool isDead()
    {
        return percentHealth <= 0;
    }




}
