using UnityEngine;

public class TargetFPSManager : MonoBehaviour
{
    [SerializeField] private int targetFPS = 60;
    
    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
    }
}
