using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBrain : BaseBrain
{
    [SerializeField]
    private PlayerInput inputs = new PlayerInput();

    [SerializeField]
    public Camera cam;

    public override PlayerInput GetInputs()
    {
        return inputs;
    }

    // Update is called once per frame
    void Update()
    {
        inputs.leftRightInput = Input.GetAxisRaw("Horizontal");
        inputs.forwardBackwardInput = Input.GetAxisRaw("Vertical");

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.Log(hit.point);
            // TODO check tag for buildable on hit
            inputs.lookPos = hit.point;
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            inputs.buildMode = true;
        }

        inputs.primaryAttack = Input.GetMouseButton(0);
    }

}
