using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseDragController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private AppleSpawner appleSpawner;
    [SerializeField]
    private RectTransform drawRectangle;

    private int sum = 0;
    private Rect dragRect;
    private Vector2 start = Vector2.zero;
    private Vector2 end = Vector2.zero;
    private AudioSource audioSource;
    private List<Apple> seletecAppleList = new List<Apple>();
    private void Awake()
    {
        dragRect = new Rect();
        audioSource = GetComponent<AudioSource>();

        DrawDragRectangle();
    }

    private void Update()
    {
        if (gameController.IsGameStart == false) return;

        if (Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;

            dragRect.Set(0, 0, 0, 0);
        }
        if(Input.GetMouseButton(0))
        {
            end = Input.mousePosition;

            DrawDragRectangle();
            CalCulateDragRect();
            SelectApples();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(sum ==10)
            {
                int score = 0;
                foreach(Apple apple in seletecAppleList)
                {
                    score++;
                    appleSpawner.DestroyApple(apple);
                }
                audioSource.Play();
                gameController.IncreaseScore(score);
            }
            else
            {
                foreach (Apple apple in seletecAppleList)
                {
                    apple.OnDeselectd();
                }
            }
            start = end = Vector2.zero; // 초기화
            DrawDragRectangle();
        }

       
    }

    private void DrawDragRectangle()
    {
        drawRectangle.position = (start+end) /2f;

        drawRectangle.sizeDelta = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
    }
    private void CalCulateDragRect()
    {
        if(Input.mousePosition.x <start.x) // 클릭하고 왼쪽으로 가면
        {
            dragRect.xMin = Input.mousePosition.x;
            dragRect.xMax = start.x;
        }
        else // 오른쪽
        {
            dragRect.xMin = start.x;
            dragRect.xMax = Input.mousePosition.x;
        }

        if(Input.mousePosition.y <start.y) // 아래
        {
            dragRect.yMin = Input.mousePosition.y;
            dragRect.yMax = start.y;
        }
        else // 위
        {
            dragRect.yMin = start.y;
            dragRect.yMax = Input.mousePosition.y;
        }
    }

    private void SelectApples()
    {
        sum = 0;
        seletecAppleList.Clear();
        foreach ( Apple apple in appleSpawner.AppleList)
        {
            if( dragRect.Contains(apple.Position) )
            {
                apple.OnSeleced();
                seletecAppleList.Add(apple);
                sum += apple.Number;
            }
            else
            {
                apple.OnDeselectd();
            }
        }
    }
}
