using UnityEngine;

public class ChooserObject : MonoBehaviour
{
    [SerializeField] private int index = 0;
    [SerializeField] private Transform pivotPosObject;
    [SerializeField] private GameObject[] listOfObject;

    private GameObject _currentObject;
    private bool _isSwipeInitiate = false;
    private void Awake()
    {
        if (_currentObject == null)
        {
            _currentObject = Instantiate(listOfObject[index]);
            _currentObject.transform.position = pivotPosObject.transform.position;
        }
    }

    void Update()
    {
        if (MobileSwipe.isReturnRightSwipe())
        {
            index++;
            _isSwipeInitiate = true;
            if (index >= listOfObject.Length) index = 0;      
        }

        if (MobileSwipe.isReturnLeftSwipe())
        {
            index--;
            _isSwipeInitiate = true;
            if (index <= -1) index = listOfObject.Length-1;
        }

        if (_isSwipeInitiate)
        {
            _isSwipeInitiate = !_isSwipeInitiate;

            Destroy(_currentObject);
            _currentObject = Instantiate(listOfObject[index]);
            _currentObject.transform.position = pivotPosObject.transform.position;
        }
    }
}
