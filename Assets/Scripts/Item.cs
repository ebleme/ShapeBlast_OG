using System;
using DefaultNamespace;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemTypes itemType;
    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D touchCollider;

    private const string IS_DRAWN = "IsDrawn";
    private const string IS_DEAD = "IsDead";

    private bool isSelected = false;
    private bool isDead = false;
    private bool isTouched = false;
    
    public ItemTypes ItemType => itemType;
    public bool IsDead => isDead;

    private void Start()
    {
        if (touchCollider == null)
            touchCollider = GetComponent<CircleCollider2D>();
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
        if (isDead) return;
        
        isDead = true;
        animator.SetTrigger(IS_DEAD);
        
        // Disable collider to prevent further selection
        if (touchCollider != null)
            touchCollider.enabled = false;
        
        // Destroy after animation
        Destroy(gameObject, 0.5f);
    }
    
    // Item ların birbirine temas ettiğini dokunduğunu algıla
    // Bu Item üzerindeki collider başka bir collider'a temas ettiğinde çalışır.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(isTouched)
            return;
        
        Debug.Log("Collision Enter : " + other.gameObject.name);
        // if (other.gameObject.GetComponent<Item>())
        if (other.gameObject.CompareTag("Item") || other.gameObject.CompareTag("Floor"))
        {
            isTouched = true;
            FindAnyObjectByType<SoundManager>().PlayTouchSound();
        }
    }
}