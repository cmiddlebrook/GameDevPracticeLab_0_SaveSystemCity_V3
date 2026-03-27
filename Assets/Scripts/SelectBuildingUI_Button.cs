using UnityEngine;

public class SelectBuildingUI_Button : MonoBehaviour
{



    [SerializeField] private GameObject selectedGameObject;



    public void ShowSelected()
    {
        selectedGameObject.SetActive(true);
    }

    public void HideSelected()
    {
        selectedGameObject.SetActive(false);
    }



}