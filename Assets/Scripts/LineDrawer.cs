using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private float maxDistance = 3f;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask itemLayer;

    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private SoundManager soundManager;


    /*
     * Çizgi çizme mekaniği
    - Aynı Itemlar üzerinde gezilirken çizlir
    - Farklı Item a gelince çizilmez
    - Belirli bir mesafeden sonra çizilmez
    - Çizilmişin üzerine bir kere daha gelince onu yok sayıyor
     *
     *
     * Mouse ile tıklayınca çizmeye başlıycaz
     * Mouse basılı tutulduğu süreceçizme işlemi devam edecek
     * Mouse bırakıldığında çizme işlemi bitecek
     */

    private List<Item> selectedItems = new List<Item>();
    private ItemTypes selectedItemType;
    private bool isDrawing = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse basıldı
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        // Mouse basılıyıor
        else if (Input.GetMouseButton(0) && isDrawing)
        {
            Draw();
        }
        else if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            // Mouse bırakıldı
            EndDrawing();
        }
    }


    private void StartDrawing()
    {
        lineRenderer.positionCount = 0;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 1, itemLayer);
        selectedItems.Clear();

        if (hit)
        {
            isDrawing = true;
            Item item = hit.collider.transform.parent.GetComponent<Item>();

            item.Selected();
            selectedItems.Add(item);
            selectedItemType = item.ItemType;

            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, item.transform.position);
        }
    }

    private void Draw()
    {
        // Screen space to world space position converter
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 1, itemLayer);

        if (hit)
        {
            Item item = hit.collider.transform.parent.GetComponent<Item>();

            if (!selectedItems.Contains(item))
            {
                // mesafe kontrolü yap

                Item last = selectedItems[selectedItems.Count - 1];

                // Son item ile aralarında olan mesafe
                float distance = Vector2.Distance(last.transform.position, item.transform.position);

                if (distance <= maxDistance)
                {
                    item.Selected();

                    selectedItems.Add(item);

                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, item.transform.position);
                }
                else
                {
                    foreach (var itemOnList in selectedItems)
                        itemOnList.Deselected();

                    selectedItems.Clear();
                    EndDrawing();
                }
            }

            if (item.ItemType != selectedItemType)
            {
                foreach (var itemOnList in selectedItems)
                    itemOnList.Deselected();

                selectedItems.Clear();

                EndDrawing();
            }
        }
    }

    private void EndDrawing()
    {
        lineRenderer.positionCount = 0;
        isDrawing = false;

        if (selectedItems.Count >= 3)
        {
            int comboMultiplier = selectedItems.Count - 2;

            int score = selectedItems.Count * comboMultiplier; // 3, 8, 15, 24


            scoreManager.IncreaseScore(score);
            soundManager.PlaySuccessSound();

            foreach (var item in selectedItems)
            {
                item.DestroyItem();
            }
        }
    }
}