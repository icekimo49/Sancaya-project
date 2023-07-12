using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthmanager : MonoBehaviour
{
    public Sprite penuh, kurangsatu, setengah, seperempat, kosong;
    Image Heart;

    private void Awake()
    {
        Heart  = GetComponent<Image>();
    }

    public void setHeart(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.kosong:
                Heart.sprite = kosong;
                    break;
            case HeartStatus.seperempat:
                Heart.sprite = seperempat;
                    break;
            case HeartStatus.setengah:
                Heart.sprite = setengah;
                    break;
            case HeartStatus.seperempatsetengah:
                Heart.sprite = kurangsatu;
                    break;
            case HeartStatus.penuh:
                Heart.sprite = penuh;
                    break;

        }
    }

    public enum HeartStatus
    {
        kosong = 0,
        seperempat = 1,
        setengah = 2,
        seperempatsetengah = 3,
        penuh = 4

    }
}
