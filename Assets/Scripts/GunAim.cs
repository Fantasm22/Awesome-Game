using UnityEngine;

public class GunAim : MonoBehaviour
{
    public Transform gun;

    void Update()
    {
        RotateTowardsNearestBlock();
    }

    void RotateTowardsNearestBlock()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length == 0)
            return;

        GameObject nearestBlock = blocks[0];
        float shortestDistance = Vector2.Distance(gun.position, nearestBlock.transform.position);

        foreach (GameObject b in blocks)
        {
            float distance = Vector2.Distance(gun.position, b.transform.position);

            if (distance < shortestDistance)
            {
                nearestBlock = b;
                shortestDistance = distance;
            }
        }

        Vector2 direction = nearestBlock.transform.position - gun.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        gun.rotation = Quaternion.Euler(0, 0, angle);
    }
}
