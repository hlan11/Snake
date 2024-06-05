using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform segmentPrefarb;
    private List<Transform> _segment;
    private void Start()
    {
        _segment = new List<Transform>();
        _segment.Add(this.transform);
    }
    void Grow()
    {
        Transform segment=Instantiate(this.segmentPrefarb);
        segment.position = _segment[_segment.Count-1].position;
        _segment.Add(segment);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        }
        if (collision.CompareTag("Ground"))
        {
            ResetState();
        }
    }
    void ResetState()
    {
        for(int i=1;i<_segment.Count;i++)
        {
            Destroy(_segment[i].gameObject);
        }
        _segment.Clear();
        _segment.Add(this.transform);
        this.transform.position = Vector3.zero;
    }
    void CheckMove() 
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity=Vector2.up*speed;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = Vector2.down * speed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.velocity = Vector2.left * speed;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.velocity = Vector2.right * speed;
        }
    }
    private void FixedUpdate()
    {
        for(int i=_segment.Count-1;i>0;i--)
        {
            _segment[i].position = _segment[i - 1].position;
        }
    }
    private void Update()
    {
        CheckMove();
    }
}
