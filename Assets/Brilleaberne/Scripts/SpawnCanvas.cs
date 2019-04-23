using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCanvas : MonoBehaviour
{
    public GameObject prefab;
    GameObject hitObj;


    // Start is called before the first frame update
    void Start()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            hitObj = hit.collider.gameObject;
        }
    }

  public void SpawnCanvasFunction() {
        //Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log(hitObj.GetComponent<NodeList>().GetMoleculeList());

    }
}
