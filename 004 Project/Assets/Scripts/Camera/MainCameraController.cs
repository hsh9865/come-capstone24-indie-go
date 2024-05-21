using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private Player player;
    public Player Player
    {
        get
        {
            if (player == null)
                player = FindObjectOfType<Player>();
            return player;
        }
    }
    public Vector3 _offset = new Vector3(0, 1f, -10f);
    public float smooth = 5f;
    Vector3 target;
    void Awake()
    {
    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        target = Player.transform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smooth);
    }
}
