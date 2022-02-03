using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public Character Owner;

    public Image u_HpUI;
    public Image U_HpImg;
    public Image U_HPBackGroundImg;

    public RectTransform HpRect;
    public RectTransform BackHpRect;


    private void Awake()
    { 
        Owner = GetComponentInParent<Character>();

        HpRect = U_HpImg.rectTransform;
        BackHpRect = U_HPBackGroundImg.rectTransform;
    }

    void Update()
    {
        OwnerMove();
        HPController();
        HPBackController();
    }
    
    void OwnerMove()
    {
        if (Owner)
        {
            Vector3 vPos = Owner.transform.position + new Vector3(0, 2.2f, 0);
            u_HpUI.transform.position = Camera.main.WorldToScreenPoint(vPos);
            vPos.y += 0.5f;
        }
    }

    void HPController()
    {
        if(Owner)
            HpRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Owner.CurHP);
    }

    void HPBackController()
    {
        if (Owner)
            BackHpRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(Owner.CurHP, BackHpRect.rect.width,0.99f));
    }

}
