using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject towerObject;
    [SerializeField] private Sprite dragSprite;
    public GameObject TowerObject
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



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
