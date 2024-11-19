using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject;
    public Material idleMaterial;
    public Material activateMaterial;
    private MeshRenderer _meshRenderer;
    List<GameObject> colliders;
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        colliders = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnCollisionEnter(Collision other)
    {
        colliders.Add(other.gameObject);
        if(colliders.Count  > 0){
            targetObject.SetActive(true);
            _meshRenderer.material = activateMaterial;
        }
    }

    void OnCollisionExit (Collision other)
    {

        colliders.Remove(other.gameObject);
        if(colliders.Count  == 0){
            _meshRenderer.material = idleMaterial;
            targetObject.SetActive(false);
        }
    }
}
