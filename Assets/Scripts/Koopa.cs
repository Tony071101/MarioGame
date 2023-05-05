// using System.Collections;
// using System.Collections.Generic;
//Testing Gitflow workflow
//Now testing workflow with gitflow
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;
    private bool shelled;
    private bool pushed;
    private void OnCollisionEnter2D(Collision2D collision) {
        if(!shelled && collision.gameObject.CompareTag("Player")) {

            Player player = collision.gameObject.GetComponent<Player>();
            if(player.starpower) {
                Hit();
            }else if(collision.transform.DotTest(transform, Vector2.down)) {
                EnterShell();
            }else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(shelled && other.CompareTag("Player")) {
            if(!pushed) {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }else {
                Player player = other.GetComponent<Player>();
                if(player.starpower) {
                    Hit();
                }else {
                    player.Hit();
                }
            }
        } else if(!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }

    private void EnterShell() {
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;

        this.gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void PushShell(Vector2 direction) {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.direction = direction.normalized;
        enemyMovement.speed = shellSpeed;
        enemyMovement.enabled = true;
    }

    private void Hit() {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible() {
        if(pushed) {
            Destroy(gameObject);
        }
    }
}
