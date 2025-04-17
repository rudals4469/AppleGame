using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Apple : MonoBehaviour
{
    [SerializeField]
    private Text appleNumber;
    public GameObject AppleOutLineImage;
    private RectTransform rect;

    private int number = 0;

    public int Number
    {
        set
        {
            number = value;
            appleNumber.text = number.ToString();
        }

        get => number;
    }

    public Vector3 Position => rect.position;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnSeleced()
    {
        AppleOutLineImage.SetActive(true);
    }

    public void OnDeselectd()
    {

        AppleOutLineImage.SetActive(false);
    }


}
