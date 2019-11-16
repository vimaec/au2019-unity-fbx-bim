using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component which can be added to a Camera to let the user to click on an imported
/// Vim game object and output its Bim data to the debug log or to the provided BimDataText component.
/// </summary>
[ExecuteAlways]
public class BimDataClicker : MonoBehaviour
{   
    private Camera _camera;
    private GameObject _lastSelected;
 
    public Text ObjectName;
    public string[] BimData = NoStrings;

    public static string[] NoStrings = new string[0];
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        // Check if the left mouse button is down without Ctrl or Alt.
        //if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftControl))
        {
            CheckRayCast();
        }
    }

    void CheckRayCast()
    {
        if (_camera == null) return;

        RaycastHit hit;
        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        var go = hit.collider.gameObject;

        if (go == _lastSelected)
            return;
        _lastSelected = go;
        if (go == null)
        {
            if (ObjectName != null)
                ObjectName.text = "nothing selected";
            BimData = NoStrings;
            return;
        }
        if (ObjectName != null)
            ObjectName.text = go.name;
        var bd = go.GetComponent<BimData>();
        if (bd == null)
        {
            BimData = NoStrings;
            return;
        }

        BimData = bd.Items;
    }
}
