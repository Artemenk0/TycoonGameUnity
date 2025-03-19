using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingPlacement : MonoBehaviour
{
	private DataManager dataManager;
	private ShopSlot shopSlot; 

    private GameObject buildingPrefab; 
    private GameObject previewBuilding; 
    private Camera mainCamera;
    private bool isPlacing; 
    private bool isBuilding;

    private void Start()
    {
    	dataManager = FindObjectOfType<DataManager>();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isPlacing)
        {
            MovePreviewToCursor();
            if (Input.GetMouseButtonDown(0)) 
            {
                if (IsPlacementValid()) 
                {
                    PlaceBuilding();
                }
            }
        }
        else
        {
            if (isBuilding)
            {
                CheckForPlacementStart();
            }
        }
    }

    public void BuildingStart()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        shopSlot = clickedButton.GetComponent<ShopSlot>();
        buildingPrefab = shopSlot.prefab;

        isBuilding = true;
    }

    public void CheckForPlacementStart()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                StartPlacingBuilding(hit.point);
            }
        }
    }

    private void StartPlacingBuilding(Vector3 position)
    {
        previewBuilding = Instantiate(buildingPrefab, position, Quaternion.identity);
        isPlacing = true;
    }

    private void MovePreviewToCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Ground")) 
            {
                previewBuilding.transform.position = hit.point;
            }
        }
    }

    private bool IsPlacementValid()
    {
        Collider prefabCollider = previewBuilding.GetComponent<Collider>();
        if (prefabCollider == null) return true; 

        Vector3 center = prefabCollider.bounds.center;
        Vector3 halfExtents = prefabCollider.bounds.extents;

        Collider[] colliders = Physics.OverlapBox(center, halfExtents, previewBuilding.transform.rotation);

        foreach (var col in colliders)
        {
            if (col.gameObject == previewBuilding || col.isTrigger) continue; 
            if (col.CompareTag("Ground")) continue; 

            return false; 
        }

        return true; 
    }

    private void PlaceBuilding()
    {
    	if (dataManager.TakeMoney(shopSlot.price)) 
    	{

	        Instantiate(buildingPrefab, previewBuilding.transform.position, Quaternion.identity);
	        
	        dataManager.SaveBuilding(previewBuilding.transform.position, shopSlot.type);
	        dataManager.SaveProgres(shopSlot.type);
	    }

        Destroy(previewBuilding); 
        isPlacing = false;
        isBuilding = false;

		
    }
}
