using System.Collections;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private void Awake()
    {
        _resetPosition = backgroundTransform[_standardTransform].position;
        
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
        
        EventBus.RegisterEvent(GameEventType.GAME_START, GoFoward);
        EventBus.RegisterEvent(GameEventType.IDLE, GoFoward);
        
        EventBus.RegisterEvent(GameEventType.ENCOUNTER, StopFoward);
    }

    private void GoFoward()
    {
        if(coroutine is not null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(Scroll());
    }
    
    private void StopFoward()
    {
        if(coroutine is not null)
        {
            StopCoroutine(coroutine);
        }
    }
    
    private IEnumerator Scroll()
    {
        while(true)
        {
            foreach (Transform tf in backgroundTransform)
                tf.position += Vector3.left * (scrollSpeed * Time.deltaTime);
            
            if (backgroundTransform[_standardTransform].position.x <= xScreenHalfSize)
            {
                _standardTransform++;
                _standardTransform %= backgroundTransform.Length;

                backgroundTransform[_standardTransform].position = _resetPosition;
            }
            
            yield return null;
        }
    }

    [SerializeField] private Transform[] backgroundTransform;
    
    private float yScreenHalfSize;
    private float xScreenHalfSize;
    private float scrollSpeed = 5f;
    
    private Vector3 _resetPosition;
    private int _standardTransform = 1;
    
    private Coroutine coroutine;
}