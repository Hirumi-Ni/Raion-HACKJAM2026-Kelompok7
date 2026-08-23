using UnityEngine;

public class BaseAnimal : MonoBehaviour, ICapturable
{
    public void OnCapture()
    {
        Debug.Log($"{gameObject.name} has been captured!");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
