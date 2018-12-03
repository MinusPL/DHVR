using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTransformReseter : MonoBehaviour {
    private Vector3 pos;
    private Quaternion rot;
    private Vector3 scale;

    private void Awake() {
        pos = transform.position;
        rot = transform.rotation;
        scale = transform.localScale;
    }

    public void ResetTransform() {
        transform.position = pos;
        transform.rotation = rot;
        transform.localScale = scale;
    }
}
