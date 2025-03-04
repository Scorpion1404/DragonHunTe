using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{

    [SerializeField]private float speed;

    private float direction;

    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;

    private float lifeTime;

    private void Awake() {
        
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }


    private void Update() {
        if(hit) return;
        float movementSpeed= speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0);

        lifeTime += Time.deltaTime;
        if(lifeTime > 5) gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        hit= true;
        boxCollider.enabled= false;
        anim.SetTrigger("explode");

        if(collision.tag == "enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction){
        lifeTime =0;
        direction = _direction;
        gameObject.SetActive(true);
        hit= false;
        boxCollider.enabled= true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX)!= direction)
            localScaleX= - localScaleX;

        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);

    }

    private void Deactivate(){
        gameObject.SetActive(false);

    }
}

