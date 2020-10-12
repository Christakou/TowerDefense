using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Tower towerObject;
    [SerializeField] private Sprite dragSprite;
    [SerializeField] private int towerPrice;


    
    public Tower TowerObject
    {
        get
        {
            return towerObject;
        }
    }

    public Sprite DragSprite
    {
        get
        {
            return dragSprite;
        }
    }

    public int TowerPrice
    {
        get
        {
            return towerPrice;
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
