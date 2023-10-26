
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
  [Header("Bulet StartPoint")]
  [SerializeField] private Transform enemy;

  private void Update(){
    transform.localScale = enemy.localScale;
  }

}
