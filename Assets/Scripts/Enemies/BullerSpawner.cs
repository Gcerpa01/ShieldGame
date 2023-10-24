
using UnityEngine;

public class BullerSpawner : MonoBehaviour
{
  [SerializeField] private Transform enemy;

  private void Update(){
    transform.localScale = enemy.localScale;
  }

}
