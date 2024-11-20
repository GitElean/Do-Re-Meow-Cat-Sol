using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Live1;
    public GameObject Live2;
    public GameObject Live3;
    void Start()
    {
        Live1.SetActive(true);
        Live2.SetActive(true);
        Live3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.lives == 2)
        {
            Live3.SetActive(false);
        }

        if (GameManager.instance.lives == 1)
        {
            Live2.SetActive(false);
        }

        if (GameManager.instance.lives == 0)
        {
            Live1.SetActive(false);
        }
    }
}
