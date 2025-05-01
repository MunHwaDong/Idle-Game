using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    public void ShowDamage(Vector3 worldPos, int damage, bool isCritical)
    {
        GameObject instance = Instantiate(damageTextPrefab, worldSpaceCanvas.transform);
        instance.transform.position = worldPos + Vector3.up * 1.5f;

        instance.GetComponent<DamageText>().Init(damage, isCritical);
    }
    
    public GameObject damageTextPrefab;
    public Canvas worldSpaceCanvas;
}
