using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс мирного животного
/// </summary>
public class PatrolAnimal : Flair
{
    [Header("WayPoints")]
    [Tooltip("Массив всевозможных путей")]
    [SerializeField] internal Transform[] wayPoints; // Массив всевозможных путей
    private Transform _nowPoint; // Действительный путь

    [Header("PatrolAnimal: Options")]
    [Tooltip("Радиус поиска доступных путей по близости")]
    [SerializeField] private float _radiusSearchWay = 3f; // Радиус поиска доступных путей по близости
    [Tooltip("Радиус в котором начнётся поиск нового пути")]
    [SerializeField] private float _radiusStopWay = 1f; // Радиус в котором начнётся поиск нового пути

    /// <summary>
    /// Метод вызывается при первом кадре
    /// </summary>
    protected virtual void Start() => FindAndSetNewWay(); // Поиск пути при старте

    /// <summary>
    /// Метод вызывается каждый кадр
    /// </summary>
    protected virtual void Update()
    {
        CalculateOptimalTarget(); // Ищем существ рядом
        if (_running) // Проверяем состояние бегства животного
        {
            if (_target != null)
                RunAnimal(); // Начинаем бегство животного
            else
                Running(false); // Отключаем состояние бегства, переходим на состояние патрулирования
        }
            
        if (_patrol) // Проверяем может ли животное передвигаться
            MoveAnimal(); // Передвигаем животное
    }

    /// <summary>
    /// Бегство животного от других существ
    /// </summary>
    private void RunAnimal()
    {
        if (Vector2.Distance(transform.position, _target.position) > _radiusSearchPrey) // Проверяем, убежало ли животное на достаточной расстояние (расстояние чуйки)
        {
            Running(false); // Отключаем состояние бегства, переходим на состояние патрулирования
            return; // Следующую часть кода пропускаем
        }
        transform.position = Vector2.MoveTowards(transform.position, -_target.position, _speed * Time.deltaTime); // Ведём животное от другого животного
    }

    /// <summary>
    /// Поиск нового пути
    /// </summary>
    protected void FindAndSetNewWay()
    {
        _patrol = false; // Отключаем возможность передвигаться к точке
        List<Transform> _wayPointsArround = new List<Transform>(); // Создаём лист с точками пути по близости
        foreach (Transform _wayPoint in wayPoints) // Просматриваем весь массив со всеми точками пути на карте
        {
            if (Vector2.Distance(_wayPoint.position, transform.position) < _radiusSearchWay) // Определяем какие точки есть по близости
                _wayPointsArround.Add(_wayPoint); // Добавляем в наш лист (массив) все пути по близости
        } 
        int rand = Random.Range(0, _wayPointsArround.Count - 1); // Создаём случайное число от 0 до количества объектов в листе (массиве)
        _nowPoint = _wayPointsArround[rand]; // Присваеваем новый путь со случайным объектом из листа (массива)
        _patrol = true; // Включаем возможность передвигаться к точке
    }

    /// <summary>
    /// Передвижение животного
    /// </summary>
    private void MoveAnimal()
    {
        transform.position = Vector2.MoveTowards(transform.position, _nowPoint.position, _speed * Time.deltaTime); // Ведём животное к точке пути
        if (Vector2.Distance(transform.position, _nowPoint.position) < _radiusStopWay) // Вычисляем дистанцию от животнного до объекта, если объект рядом с животным, то ищем новый путь
            FindAndSetNewWay(); // Устанавливаем новый путь
    }
}