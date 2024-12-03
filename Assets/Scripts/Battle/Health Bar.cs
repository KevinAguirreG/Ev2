using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healtBar;

   


    public void SetHP(float normalizedValue)
    {
        healtBar.transform.localScale = new Vector3(normalizedValue, 1.0f);
    }
}
