using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "UpdateTextSO", menuName = "IOnEventSO/UpdateTextSO")]
public class UpdateTextSO : IOnEventSO
{
    public override void OnEvent(EventMessage msg)
    {
        var listenerObj = msg.GetParameter<GameObject>()[0];

        if (listenerObj.TryGetComponent(out TextMeshProUGUI tmpro))
        {
            tmpro.text += msg.GetParameter<string>();
        }
    }
}
