using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string IS_DRAWN = "IsDrawn";
    private const string IS_Dead = "IsDead";

    bool isSelected = false;
    
    private void Awake()
    {
    }

    public void Selected()
    {
        isSelected = true;
        
        animator.SetBool(IS_DRAWN, isSelected);
    }

    public void Deselected()
    {
        isSelected = false;
        
        animator.SetBool(IS_DRAWN, isSelected);
    }
    

    public void DestroyItem()
    {
        animator.SetTrigger(IS_Dead);
        
        // Sınavda sorulabilir

        Destroy(gameObject, 1f);
    }
}
