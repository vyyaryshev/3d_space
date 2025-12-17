using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour
{
    private MeshRenderer rend;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.rend = this.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ChangeColorTemporarily());
    }

    private IEnumerator ChangeColorTemporarily()
    {   
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = Color.gray;
    }
}