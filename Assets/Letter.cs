using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    void Update()
    {
        // Gira as letras no própio eixo Y
        transform.Rotate(new Vector3(0, 3.0f, 0), Space.Self);
    }
}
