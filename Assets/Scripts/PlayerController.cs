using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BaseBrain brain;
    PlayerInput inputs;

    Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        brain = GetComponent<BaseBrain>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputs = brain.GetInputs();
        Vector3 moveVect = new Vector3(inputs.strafeInput, 0, inputs.walkInput);
        thisTransform.Translate(moveVect,Space.World);

        inputs.lookPos.y = thisTransform.position.y;
        thisTransform.LookAt(inputs.lookPos);
    }
}
