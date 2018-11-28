using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VRCharacterController : MonoBehaviour
{

    public LayerMask m_FloorMask;
    public LineRenderer m_Line;
    public Transform m_GrabHook;
    public SteamVR_RenderModel m_Model;

    public SteamVR_TrackedController m_Controller;

    List<Grabable> m_NearObject = new List<Grabable>();
    Grabable m_Grabbed;

    Gun m_Gun;

    private void Awake()
    {
        m_Controller.PadClicked += SelectPosition;
        m_Controller.PadUnclicked += Teleport;
        m_Controller.Gripped += GrabObject;
        m_Controller.TriggerClicked += Fire;
    }

    private void Update()
    {
        if (m_Line.enabled)
        {
            m_Line.SetPosition(0, transform.position);
            m_Line.SetPosition(1, transform.position + transform.forward * 10f);
        }
    }

    void SelectPosition(object sender, ClickedEventArgs e)
    {
        m_Line.enabled = true;
    }

    void Teleport(object sender, ClickedEventArgs e)
    {
        m_Line.enabled = false;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_FloorMask))
        {
            transform.parent.position = hit.point;
        }
    }

    void Fire(object sender, ClickedEventArgs e)
    {
        if (m_Gun)
        {
            m_Gun.Fire();
        }
    }

    void GrabObject(object sender, ClickedEventArgs e)
    {
        if (m_Grabbed == null && m_NearObject.Count > 0)
        {
            m_NearObject[0].Grab(m_GrabHook);
            m_Grabbed = m_NearObject[0];

            m_Gun = m_Grabbed.GetComponent<Gun>();

            m_NearObject.Remove(m_Grabbed);

            ActiveControllerModel(false);
        }
        else if(m_Grabbed)
        {
            m_Grabbed.Release();
            m_Grabbed = null;

            m_Gun = null;

            ActiveControllerModel(true);
        }

    }

    void ActiveControllerModel(bool active)
    {
        foreach (var item in m_Model.GetComponentsInChildren<Renderer>())
        {
            item.enabled = active;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var grabable = other.GetComponent<Grabable>();
        if (grabable)
        {
            m_NearObject.Add(grabable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var grabable = other.GetComponent<Grabable>();
        if (grabable)
        {
            m_NearObject.Remove(grabable);
        }
    }
}
