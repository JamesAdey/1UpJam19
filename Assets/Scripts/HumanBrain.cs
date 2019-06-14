using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBrain : BaseBrain
{
    [SerializeField]
    private PlayerInput inputs;

    [SerializeField]
    private Camera cam;

    public override PlayerInput GetInputs()
    {
        return inputs;
    }

    // Update is called once per frame
    void Update()
    {
        inputs.strafeInput = Input.GetAxisRaw("Horizontal");
        inputs.walkInput = Input.GetAxisRaw("Vertical");
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // TODO check tag for buildable on hit
            inputs.lookPos = hit.point;
        }
    }

}
