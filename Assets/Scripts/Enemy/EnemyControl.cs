using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform player; // ссылка на Transform игрока
    public float moveSpeed = 3f; // скорость движения врага
    public float maxDistance = 5f; // максимальная дистанция, на которой враг может находиться от игрока

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // ссылка на компонент SpriteRenderer

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // Вычисляем направление к игроку
        Vector2 direction = (player.position - transform.position).normalized;

        // Если дистанция до игрока больше максимальной дистанции, то движемся в направлении игрока
        if (Vector2.Distance(transform.position, player.position) > maxDistance)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }

        // Поворачиваем врага, если он движется вправо
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // разворачиваем спрайт по оси X
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // возвращаем спрайт в исходное положение
        }
    }

}
