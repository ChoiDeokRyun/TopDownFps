using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;

    public CoolTimeUI cooltimeui;

    private void Awake()
    {
        Instance = this;
    }
    public void OnCoolTime(float MaxCoolTime,string _text)
    {
        cooltimeui.gameObject.SetActive(true);
        cooltimeui.StartCoolTime(MaxCoolTime,_text);
    }
}
