using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    public List<Vector4> footsteps;
    public Material mat;

    private void Update()
    {
        mat.SetVectorArray("_Footsteps", footsteps);
    }
}
