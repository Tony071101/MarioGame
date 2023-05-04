using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSprite smallRenderer;
    public PlayerSprite bigRenderer;
    private PlayerSprite currentRenderer;
    private CapsuleCollider2D capsuleCollider;
    private DeathAnimation deathAnimation;
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool death => deathAnimation.enabled; 
    public bool starpower {get; private set;}
    private void Awake() {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        currentRenderer = smallRenderer;
    }
    public void Hit() {
        if(!starpower && !death) {
            if(big) {
                Shrink();
            }else {
                Death();
            }
        }
    }

    public void Grow() {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        currentRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);
        StartCoroutine(ScaleAnimation());
    }

    private void Shrink() {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        currentRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private void Death() {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3f);   

        
    }

    private IEnumerator ScaleAnimation() {
        float elapsed = 0f;
        float duration = 0.5f;
        while(elapsed < duration) {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 4 == 0) {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        currentRenderer.enabled = true;
    }

    public void StartPower(float duration = 12f) {
        starpower = true;
        StartCoroutine(StarPowerAnimation(duration));
    }

    private IEnumerator StarPowerAnimation(float duration) {
        starpower = true;
        float elapsed = 0f;
        while(elapsed < duration) {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 4 == 0) {
                currentRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        currentRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }
}
