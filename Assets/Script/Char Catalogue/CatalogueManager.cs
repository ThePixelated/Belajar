using UnityEngine;

public class CatalogueManager : MonoBehaviour
{
    [Header("Related GObj")]
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Camera;
    [Header("Pivot Points")]
    [SerializeField] private Transform mainPageCamTrans;
    [SerializeField] private Transform mainPagePlayerTrans;
    [SerializeField] private Transform cataloguePageCamTrans;
    [SerializeField] private Transform cataloguePagePlayerTrans;
    [Header("UIs & World Component")]
    [SerializeField] private GameObject HUDCatalogue;
    [SerializeField] private GameObject worldComptCatalogue;

    public void GoToCharCatalogue()
    {
        worldComptCatalogue.SetActive(true);
        HUDCatalogue.SetActive(true);

        Camera.CopyWorldTransform(cataloguePageCamTrans);
        Player.CopyWorldTransform(cataloguePagePlayerTrans);
    }


    public void BackToMainPage()
    {
        Camera.CopyWorldTransform(mainPageCamTrans);
        Player.CopyWorldTransform(mainPagePlayerTrans);

        worldComptCatalogue.SetActive(false);
        HUDCatalogue.SetActive(false);
    }
}
