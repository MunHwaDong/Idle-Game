using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    public void Init(int damage, bool isCritical)
    {
        text.text = damage.ToString();

        if (isCritical is true)
        {
            text.color = Color.red;
            text.fontSize = 100;
        }
        else
        {
            text.color = Color.white;
            text.fontSize = 80;
        }

        Animate();
    }

    private void Animate()
    {
        Sequence seq = DOTween.Sequence();
        
        seq.Append(transform.DOMoveY(transform.position.y + 1.5f, 0.5f).SetEase(Ease.OutCubic));
        
        seq.Join(text.DOFade(0f, 0.5f));
        
        seq.OnComplete(() => Destroy(gameObject));
    }
    
    [SerializeField] private Text text;
}