using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс животного (Статистика и методы, которые не относятся к ИИ)
/// </summary>
public class Animal : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("Игровой менеджер, здесь хранится всё важное, он всего 1 на сцене")]
    [SerializeField] internal GameManager gameManager; // Игровой менеджер, здесь хранится всё важное, он всего 1 на сцене

    [Header("Animal: Options")]
    [Tooltip("Здоровье животного")]
    [SerializeField] internal int health = 1; // Здоровье животного
    [Tooltip("Является ли животное главным")]
    [SerializeField] internal bool _mainAnimal = true; // Является ли животное главным

    /// <summary>
    /// Метод получения урона
    /// </summary>
    internal void GetDamage(int damage)
    {
        health -= damage; // Уменьшаем здоровье
        if (health == 0) // Если здоровье равно 0, то убиваем животного
            Die(); // Вызываем смерть
    }

    /// <summary>
    /// Метод смерти животного
    /// </summary>
    internal void Die()
    {
        gameManager.alives.Remove(transform); // Удаляем из листа (массива) объект мёртвого животного
        if (_mainAnimal) // Проверяем, является ли животное главным
            SetNewMainAnimal(); // Устанавливаем нового главного животного
        
        Destroy(gameObject); // Удаляем животного со сцены
    }

    /// <summary>
    /// Установка нового главного животного
    /// </summary>
    private void SetNewMainAnimal()
    {
        if (GetComponent<GroupedAnimal>() != null) // Проверяем, есть ли у животного свойство группироваться 
        {
            GroupedAnimal thisGroupedAnimal = GetComponent<GroupedAnimal>(); // Создаём локальную переменную для этого животного
            List<GroupedAnimal> _groupedAnimals = new List<GroupedAnimal>(); // Создаём локальный список животных со свойством группироваться
            for (int i = 0; i < gameManager.groupedAnimals.Count; i++) // Просматриваем каждый элемент списка всех животных со свойством группироваться в игре
            {
                GroupedAnimal groupedAnimal = gameManager.groupedAnimals[i]; // Инициализируем животного, который может группироваться
                if (groupedAnimal != null && groupedAnimal.groupID == thisGroupedAnimal.groupID && gameObject != groupedAnimal.gameObject) // Проверяем принадлежит ли животное этой группе и не является ли оно этим же объектом
                    _groupedAnimals.Add(groupedAnimal); // Присваеваем элементы списка
            }

            if (_groupedAnimals.Count > 0)
            {
                int rand = Random.Range(0, _groupedAnimals.Count - 1); // Выбираем случайного животного из возможных
                if (_groupedAnimals[rand] != null)
                    _groupedAnimals[rand]._mainAnimal = true; // Устанавливаем нового главного животного
            }
        }
    }

    /// <summary>
    /// Триггер для получения урона
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet")) // Если в триггер заходит объект с тегом "Bullet", то наносим урон
            GetDamage(col.GetComponent<Bullet>().damage); // Наносим урон
    }
}