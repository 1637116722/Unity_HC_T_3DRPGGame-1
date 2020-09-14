using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("目標位置")]
    public Transform target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "玩家")
        {
            target.GetComponent<CapsuleCollider>().enabled = false;
            other.transform.position = target.position;
            Invoke("OpenCollider", 3f);
        }
            
    }

    /// <summary>
    /// 開啟碰撞器
    /// </summary>
    private void OpenCollider()
    {
        target.GetComponent<CapsuleCollider>().enabled = true;
    }
}
