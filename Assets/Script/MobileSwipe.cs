using UnityEngine;

public class MobileSwipe : MonoBehaviour
{
    private static Vector3 startPosTouch = Vector3.zero;

    public void Update()
    {
        CalcHorizontal();
    }

    public static short CalcHorizontal()
    {
        if (Input.GetMouseButtonDown(0))
            startPosTouch = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 direction = Input.mousePosition;

            if (startPosTouch.x > direction.x) return -1; // left
            if (startPosTouch.x < direction.x) return 1; // right
        }

        return 0;
    }

    public static bool isReturnLeftSwipe() { if (CalcHorizontal() == 1) return true; return false; }
    public static bool isReturnRightSwipe() { if (CalcHorizontal() == -1) return true; return false; }
}
