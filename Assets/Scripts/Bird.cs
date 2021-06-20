using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce=500;
    [SerializeField] float _maxDragDistance = 5;

    public int lives = 3;
    public static AudioClip birdLaunch;
    static AudioSource audiosrc;
    

    Vector2 _startPosition;
    Vector2 direction;
    Vector3 initRotation=new Vector3(0f,0f,0f);
    Rigidbody2D _rigidBody2D;
    SpriteRenderer _spriteRenderer;
    LineRenderer _lineRenderer;
    Monster[] monsters;
    int initialMonstersLeft;
    bool launched=false;

    

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
        audiosrc = GetComponent<AudioSource>();
        birdLaunch = Resources.Load<AudioClip>("BirdLaunch");
        monsters = FindObjectsOfType<Monster>();
    }

    private void Start()
    {
        _startPosition = _rigidBody2D.position;
        _rigidBody2D.isKinematic = true;
        initialMonstersLeft = monsters.Length;
    }
    private void OnMouseDown()
    {
        _lineRenderer.enabled = true;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;
        
       float distance = Vector2.Distance(desiredPosition, _startPosition);
        if(distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }

        if (desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }
        _rigidBody2D.position = desiredPosition;
    }

    private void OnMouseUp()
    {
        Vector2 currentPosition = _rigidBody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigidBody2D.isKinematic = false;
        _rigidBody2D.AddForce(direction* _launchForce);
        _spriteRenderer.color = Color.white;
        audiosrc.PlayOneShot(birdLaunch);
        launched = true;
       _lineRenderer.enabled = false;
    }

    

    private void Update()
    {
        _lineRenderer.SetPosition(1,_startPosition);
        _lineRenderer.SetPosition(0, transform.position);

        if (transform.position.y > 10 ||
           transform.position.y < -10 ||
           transform.position.x > 15 ||
           transform.position.x < -10)
        {
            _rigidBody2D.position = _startPosition;
            _rigidBody2D.isKinematic = true;
           _rigidBody2D.velocity = Vector2.zero;
        }
        if (transform.position.y > 10 ||
           transform.position.y < -10 ||
           transform.position.x < -10)
        {
           lives--;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {        
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        Monster[] monstersLeft = FindObjectsOfType<Monster>();
        if (launched){
           int count = monstersLeft.Length;
            for (int i = 0; i < monstersLeft.Length; i++){
                if (monstersLeft[i]._hasDied){
                    count--;
                }
            }
            if (initialMonstersLeft ==count){
                lives--;
            }
            initialMonstersLeft = count;
            launched = false;
        }
        _rigidBody2D.position = _startPosition;
        _rigidBody2D.isKinematic = true;
        _rigidBody2D.velocity = Vector2.zero;
    }
    
}
