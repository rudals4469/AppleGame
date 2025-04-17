using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject applePreFab;
    [SerializeField]
    private Transform appleParent;


    private readonly int width = 17, height = 10;
    private readonly int spacing = 90;
    private List<Apple> appleList = new List<Apple>();
    public List<Apple> AppleList => appleList;


    private void Awake()
    {
        SpawnApples();
    }

    public void SpawnApples()
    {
        Vector2 size = appleParent.GetComponent<RectTransform>().sizeDelta;
        size += new Vector2(spacing, spacing);

        int sum = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject clone = Instantiate(applePreFab, appleParent);
                RectTransform rect = clone.GetComponent<RectTransform>();

                float px = x * size.x  - 720f;
                float py = y * size.y - 415f;
                rect.anchoredPosition = new Vector2(px, py);

                Apple apple = clone.GetComponent<Apple>();
                apple.Number = Random.Range(1, 10);

                if (y == height - 1 && x == width - 1)
                {
                    apple.Number = 10 - (sum % 10);
                }

                sum += apple.Number;

                appleList.Add(apple);
            }
        }
    }

    public void DestroyApple(Apple removeItem)
    {
        appleList.Remove(removeItem);
        Destroy(removeItem.gameObject);
    }
}
