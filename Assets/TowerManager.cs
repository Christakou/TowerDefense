using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    private TowerBtn towerBtnPressed;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.tag == "BuildSite")
            {
                placeTower(hit);
            }
        }
    }

    public void placeTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.collider.transform.position;
        }

    }
    public void selectedTower(TowerBtn towerSelected) {
        towerBtnPressed = towerSelected;
        Debug.Log("Pressed: " + towerBtnPressed.gameObject);
    }
}
