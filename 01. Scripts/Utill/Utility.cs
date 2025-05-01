using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void PreserveFactoryTypes()
    {
        // PlayerStateFactory를 강제로 참조해서 스트립 방지
        _ = typeof(PlayerStateFactory);
        _ = typeof(NormalMonsterStateFactory);
        _ = typeof(GameStateFactory);
    }
    
    public static string ToAbbreviatedString(this float number)
    {
        if(number >= 1_000_000_000_000)
            return (number / 1_000_000_000f).ToString("0.#") + "T";
        if (number >= 1_000_000_000)
            return (number / 1_000_000_000f).ToString("0.#") + "B";
        if (number >= 1_000_000)
            return (number / 1_000_000f).ToString("0.#") + "M";
        if (number >= 1_000)
            return (number / 1_000f).ToString("0.#") + "K";

        return number.ToString();
    }
}
