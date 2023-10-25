
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
  [SerializeField] private Transform enemy;

  private void Update(){
    transform.localScale = enemy.localScale;
  }

}
