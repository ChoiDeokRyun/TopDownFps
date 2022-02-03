using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

public class CoolTimeUI : MonoBehaviour
{
    public static CoolTimeUI instance;
    public Slider slider;
    public Text text;

    public float MaxCoolTime;
    public float CoolTime=0;

    public bool OnOff = false;

    private void Awake()
    {
        instance = this;
    }
    public void StartCoolTime(float _MaxCoolTime,string _text)
    {
        slider = GetComponent<Slider>();
        text.text = _text;
        MaxCoolTime = _MaxCoolTime;
        CoolTime = 0;
        OnOff = true;
    }

    private void Update()
    {
        if(OnOff)
        {
            CoolTime += Time.deltaTime;
            slider.value = CoolTime / MaxCoolTime;
            
            if(CoolTime>=MaxCoolTime)
            {
                OnOff = false;
                this.gameObject.SetActive(false);
            }    
        }
    }
}
