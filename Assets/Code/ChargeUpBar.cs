using UnityEngine;

public class ChargeUpBar : MonoBehaviour
{
    public float maxValue;
    public float value;

    [SerializeField] float maxScale;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3((value / maxValue) * maxScale, transform.localScale.y, transform.localScale.z);
    }
}
