using UnityEngine;

public class PlayerSensitivity : MonoBehaviour
{
    [SerializeField] private float lowSens;
    [SerializeField] private float normalSens;
    [SerializeField] private float highSens;
    //private Sensitivity currentSens;

    private void Start()
    {
        //currentSens = PlayerManager.instance.CurrentSens;
        AppliedSensitivity(PlayerManager.instance.CurrentSens);
    }

    private void OnEnable()
    {
        GameManager.instance.onSensitivity += AppliedSensitivity;
    }

    private void OnDisable()
    {
        GameManager.instance.onSensitivity -= AppliedSensitivity;
    }

#if UNITY_EDITOR
    void Update() => AppliedSensitivity(PlayerManager.instance.CurrentSens);
#endif

    public void AppliedSensitivity(Sensitivity sens)
    {
        PlayerManager.instance.CurrentSens = sens;
        switch (sens)
        {
            case Sensitivity.LOW:
                PlayerManager.instance.CurrentSpeedSens = lowSens;
                break;
            case Sensitivity.NORMAL:
                PlayerManager.instance.CurrentSpeedSens = normalSens;
                break;
            case Sensitivity.HIGH:
                PlayerManager.instance.CurrentSpeedSens = highSens;
                break;
        }
    }
}
