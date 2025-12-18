using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerAssembleSpawn : MonoBehaviour
{
    [Header("Body Parts (GameObject)")]
    public Transform[] parts;

    [Header("Spawn Settings")]
    public float spawnHeight = 10f;     // đẩy lên cao bao nhiêu
    public float dropDuration = 0.5f;   // thời gian rơi
    public float dropDelay = 0.15f;     // delay giữa các mảnh

    [Header("Ease")]
    public Ease dropEase = Ease.OutBounce;

    private Vector3[] originalLocalPos;

    void Awake()
    {
        originalLocalPos = new Vector3[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            originalLocalPos[i] = parts[i].localPosition;
            parts[i].gameObject.SetActive(false);
        }
    }

    void Start()
    {
        PlaySpawn();
    }

    public void PlaySpawn()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnEffect());
    }

    IEnumerator SpawnEffect()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            Transform p = parts[i];

            // đặt lên cao
            p.localPosition = originalLocalPos[i] + Vector3.up * spawnHeight;
            p.gameObject.SetActive(true);

            // tween rơi về đúng vị trí ban đầu
            p.DOLocalMove(originalLocalPos[i], dropDuration)
             .SetEase(dropEase);

            yield return new WaitForSeconds(dropDelay);
        }
    }
}
