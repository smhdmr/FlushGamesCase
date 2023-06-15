using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public new string name;
    public int startPrice;
    public Sprite icon; 

    public int value => (int)(startPrice + (transform.localScale.x / transform.parent.localScale.x) * 100);
}