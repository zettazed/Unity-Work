using UnityEngine;

/// <summary>
/// Класс вражеского животного
/// </summary>
public class EnemyAnimal : PatrolAnimal
{
    [Header("EnemyAnimal: Options")]
    [Tooltip("Время для смерти без добычи в секундах")]
    [SerializeField] private float _timeToDieWithoutTarget = 60f; // Время для смерти без добычи в секундах

    /// <summary>
    /// Метод вызывается при первом кадре
    /// </summary>
    protected override void Start() => base.Start(); // Вызываем поиск пути

    /// <summary>
    /// Метод вызывается каждый кадр
    /// </summary>
    protected override void Update()
    {
        if ((Time.time - _timeReset) > _timeToDieWithoutTarget) // Проверяем, прошло ли время с момента обнаружения новой добычи фиксированное время _timeToDieWithoutTarget, если да, то вызываем смерть
            DieWithoutTarget(); // Вызываем смерть

        if (!_chased) // Проверяем, ведётся ли преследование, если нет, то животное просто ходит
            base.Update(); // Вызываем патрулирование
        else
            MoveAnimalToTarget();  // Перемещаем животного к добыче 

        CalculateOptimalTarget(); // Ищем подходящую цель по близости
    }

    /// <summary>
    /// Смерть, если какое-то количество времени не было добычи
    /// </summary>
    private void DieWithoutTarget() => Die(); // Вызов смерти

    /// <summary>
    /// Перемещение животного к добыче
    /// </summary>
    private void MoveAnimalToTarget()
    {
        if (_target == null) // Если цель убита (исчезла со сцены), то меняем путь
        {
            PatrolStart(); // Начинаем патрулирование
            return; // Следующую часть кода пропускаем
        }
            
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime); // Ведём животное к добыче
        if (Vector2.Distance(transform.position, _target.position) > _radiusSearchPrey) // Вычисляем дистанцию от животнного до добычи, если добыча далеко, то ищем новый путь
            PatrolStart(); // Начинаем патрулирование
    }

    /// <summary>
    /// Патрулирование
    /// </summary>
    private void PatrolStart()
    {
        Chase(false); // Устанавливаем состояние агрессии
        FindAndSetNewWay(); // Устанавливаем новый путь
    }

    /// <summary>
    /// Триггер убийства добычи
    /// </summary>
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col); // Вызываем проверку входа в триггер объектов с пулей
        if (col.CompareTag("Animal") || col.CompareTag("Player")) // Если объекты, вошедшие в триггер имеют теги "Animal" или "Player", то убиваем
        {
            if (col.gameObject.name != gameObject.name) // Проверяем, является ли вошедший в триггер объект этим же объектом
            {
                gameManager.alives.Remove(col.transform); // Удаляем из листа (массива) объект мёртвого животного
                Destroy(col.gameObject); // Удалить объект со сцены
            }
        }   
    }
}