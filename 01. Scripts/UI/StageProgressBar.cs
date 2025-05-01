using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageProgressBar : MonoBehaviour
{
    private int totalMonsters;
    private int killedMonsters;

    public void Init(int total)
    {
        totalMonsters = total;
        killedMonsters = 0;
        progressBar.fillAmount = 1f;
    }

    public void OnMonsterKilled()
    {
        killedMonsters++;
        float progress = 1f - ((float)killedMonsters / totalMonsters);
        
        progressBar.DOFillAmount(progress, 0.3f).SetEase(Ease.OutQuad);
    }
    
    [SerializeField] private Image progressBar;
}