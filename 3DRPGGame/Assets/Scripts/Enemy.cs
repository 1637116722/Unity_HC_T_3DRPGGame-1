using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("攻擊"), Range(0, 100)]
    public float attack = 20f;
    [Header("血量"), Range(0, 1000)]
    public float hp = 350f;
    [Header("經驗值"), Range(0, 100)]
    public float exp = 30f;
    [Header("掉落道具的機率"), Range(0f, 1f)]
    public float prop = 0.3f;
    [Header("掉落的道具"), Range(0, 100)]
    public Transform skull;
    [Header("停止距離:攻擊距離"),Range(0,10)]
    public float rangeAttack = 1.5f;
    [Header("攻擊冷卻時間"), Range(0, 10)]
    public float cd = 1.5f;

    private NavMeshAgent nma;
    private Animator ani;
    private Player player;
    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    #endregion

    #region 方法
    /// <summary>
    /// 移動方法：追踪玩家
    /// </summary>
    private void Move()
    {
        nma.SetDestination(player.transform.position);
        ani.SetFloat("移動", nma.velocity.magnitude);

        if(nma.remainingDistance<=rangeAttack)Attack();
    }
    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        timer += Time.deltaTime;

        if (timer >= cd)
        {
            timer = 0;
            ani.SetTrigger("攻擊觸發");
        }
    }
    #endregion

    #region 事件
    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        player = FindObjectOfType<Player>();

        nma.speed = speed;
        nma.stoppingDistance = rangeAttack;
    }

    private void Update()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.8f, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);

        if (other.name== "玩家")
        {
            other.GetComponent<Player>().Hit(attack, transform);
        }
    }
    #endregion
}
