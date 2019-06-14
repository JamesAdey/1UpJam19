using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBrain : BaseBrain
{
    [SerializeField]
    private PlayerInput inputs;

    [SerializeField]
    public Camera cam;

    public override PlayerInput GetInputs()
    {
        return inputs;
    }

    private void Start()
    {
        inputs = new PlayerInput();
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
            // TODO check tag for buildable on hit
            inputs.lookPos = hit.point;
        }
    }

}
