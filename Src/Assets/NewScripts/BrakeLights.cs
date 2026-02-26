using UnityEngine;

public class BrakeLights : MonoBehaviour
{
    GameObject root;

    void Start()
    {
        root = transform.Find("root").gameObject;
        root.SetActive(false);
    }

    void Update()
    {
        root.SetActive(Input.GetAxis("Vertical") < 0.0f);
    }
}
