using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowSystem : MonoBehaviour
{

    public GameObject leftborad;
    public GameObject rightborad;

    public float speed;

    public bool run;


    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        run = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            if (rightborad.transform.position.x > 200)
            {
                Destroy(leftborad);
                Destroy(rightborad);
                run = false;
            }
            else
            {
                leftborad.transform.Translate(Vector3.left * speed * Time.deltaTime);
                rightborad.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}
