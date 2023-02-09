using UnityEngine;

/// <summary>
/// Класс чутья животных
/// </summary>
public class Flair : Animal
{
    [Header("Flair: Options")]
    [Tooltip("Скорость передвижения животного")]
    [SerializeField] protected float _speed = 1f; // Скорость передвижения животного
    [Tooltip("Радиус поиска добычи")]
    [SerializeField] protected float _radiusSearchPrey = 2f; // Радиус поиска добычи
    #region Conditions
    [Tooltip("Ведётся ли преследование (true - да, false - нет)")]
    [SerializeField] protected bool _chased = false; // Ведётся ли преследование (true - да, false - нет)
    [Tooltip("Состояние животного (true - патрулирует, false - занят чем-то другим)")]
    [SerializeField] protected bool _patrol = true; // Состояние животного (true - патрулирует, false - занят чем-то другим)
    [Tooltip("Состояние животного (true - бегство, false - занят чем-то другим)")]
    [SerializeField] protected bool _running = false; // Состояние животного (true - бегство, false - занят чем-то другим)
    #endregion
    [Tooltip("Опция, является ли животное вражеским")]
    [SerializeField] private bool _enemy = false; // Опция, является ли животное вражеским

    protected Transform _target; // Transform добычи
    protected float _timeReset = 0f; // Время, в которое была найдена новая добыча

    /// <summary>
    /// Просчитываем новую цель, которая ближе к животному
    /// </summary>
    protected void CalculateOptimalTarget()
    {
        float minDistance = 0f; // Устанваливаем минимально-возможную дистанцую 0
        foreach (Transform alive in gameManager.alives) // проходим по всем существам на сцене
        {
            if (alive != null && alive.gameObject.name == gameObject.name) continue; // Если проверяемый объект является объектом, на котором вызывается этот скрипт, то пропускаем данную итерацию

            if (_enemy && _target == null) // Проверяем есть ли какая-то цель и является ли эта цель вражеской
                _timeReset = Time.time; // Устанавливаем время, в которое была найдена новая добыча
            float dist = Vector2.Distance(transform.position, alive.position); // высчитываем дистанцию от животного к добыче
            if (dist <= _radiusSearchPrey) // Если добыча в зоне преследования, то вражеское животное бежит за ней
            {
                if (minDistance == 0f) // Если дистанции ещё не было
                {
                    minDistance = dist; // Устанваливаем новую минимальную дистанцию
                    if (_enemy) // Проверяем является ли животное вражеским
                        _timeReset = Time.time; // Устанавливаем время, в которое была найдена новая добыча
                }

                if (dist <= minDistance) // Если нашлось животное ближе
                {
                    minDistance = dist; // Устанваливаем новую минимальную дистанцию
                    _target = alive; // Устанавливаем новую добычу
                    if (_enemy) // Проверяем, является ли животное вражеским
                        Chase(true);  // Устанавливаем состояние агрессии
                    else
                    {
                        if (gameObject.name == "GroupedAnimal" && _target.name == "IdleAnimal") continue;
                        Running(true); // Устанавливаем состояние животного в бегство
                    }  
                }
            }
        }
    }

    /// <summary>
    /// Состояние агрессии животного
    /// </summary>
    /// <param name="chase"></param>
    protected void Chase(bool chase)
    {
        if (!chase) // Проверяем, патрулирование или преследование
            _target = null; // Сбрасываем значение добычи
        _patrol = !chase; // Патрулирование
        _chased = chase; // Преследование
    }

    /// <summary>
    /// Смена состояния животного на бегство
    /// </summary>
    /// <param name="running"></param>
    protected void Running(bool running)
    {
        if (!running) // Проверяем, патрулирование или бегство
            _target = null; // Сбрасываем значение добычи
        _patrol = !running; // Патрулирование
        _running = running; // Бегство
    }
}