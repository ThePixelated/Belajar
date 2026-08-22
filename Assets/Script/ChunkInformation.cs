using Unity.VisualScripting;
using UnityEngine;

public class ChunkInformation : MonoBehaviour
{
    [SerializeField] private GameObject _leftPartPref;
    [SerializeField] private GameObject _centerPartPref;
    [SerializeField] private GameObject _rightPartPref;
    [SerializeField] private GameObject _frontConnection;
    [SerializeField] private GameObject _BackConnection;

    public GameObject LeftPartPref { get { return _leftPartPref; } set { _leftPartPref = value; } }
    public GameObject CenterPartPref { get { return _centerPartPref; } set { _centerPartPref = value; } }
    public GameObject RightPartPref { get { return _rightPartPref; } set { _rightPartPref = value; } }
    public GameObject FrontConnection { get { return _frontConnection; } set { _frontConnection = value; } }
    public GameObject BackConnection { get { return _BackConnection; } set { _BackConnection = value; } }

    private void Awake()
    {
        Color colorRand = new Color((float)Random.Range(1, 256) / 255f, (float)Random.Range(1, 256) / 255f, (float)Random.Range(1, 256) / 255f, 1);

        MeshRenderer meshFront = _frontConnection.GetComponent<MeshRenderer>();
        MeshRenderer meshBack = _BackConnection.GetComponent<MeshRenderer>();
        meshFront.material.SetColor("_BaseColor", colorRand);
        meshBack.material.SetColor("_BaseColor", colorRand);
    }
}
