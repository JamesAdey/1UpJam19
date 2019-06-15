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

        float delta = Input.mouseScrollDelta.y;
        inputs.buildingEuler.y += delta * 10;
        if (inputs.buildingEuler.y < 0)
        {
            inputs.buildingEuler.y += 360;
        }
        else if(inputs.buildingEuler.y > 360)
        {
            inputs.buildingEuler.y -= 360;
        }


        inputs.primaryAttack = Input.GetMouseButton(0);
    }

}
