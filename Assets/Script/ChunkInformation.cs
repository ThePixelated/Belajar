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


    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private MaterialPropertyBlock _mpb;
    private void Awake()
    {
        Color colorRand = new Color((float)Random.Range(1, 256) / 255f, (float)Random.Range(1, 256) / 255f, (float)Random.Range(1, 256) / 255f, 1);
        _mpb = new MaterialPropertyBlock();

        ApplyColor(_frontConnection.GetComponent<MeshRenderer>(), colorRand);
        ApplyColor(_BackConnection.GetComponent<MeshRenderer>(), colorRand);
    }

    private void ApplyColor(MeshRenderer r, Color c)
    {
        r.GetPropertyBlock(_mpb);
        _mpb.SetColor(BaseColorID, c);
        r.SetPropertyBlock(_mpb);
    }
}
