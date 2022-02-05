using UnityEngine;

public class CursorChecker : MonoBehaviour
{
    Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        mCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CursorCheckerFunc();
        // Debug.Log(CursorCheckerFunc());
    }
    public Transform CursorCheckerFunc(){
        RaycastHit hit;
        Ray ray = mCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 100f)){
            return hit.transform;
        }
        else return null;
    }
}
