using UnityEngine;

/// <summary>
/// Класс для животных, которые должны группироваться
/// </summary>
public class GroupedAnimal : PatrolAnimal
{
    [Header("GroupedAnimal: Options")]
    [Tooltip("Transform главного животного")]
    [SerializeField] internal Transform mainAnimalTransform; // Transform главного животного
    [Tooltip("Дистанция до точки группировки")]
    [SerializeField] private float _radiusForStartGroup = 2f; // Дистанция до точки группировки
    [Tooltip("ID группы")]
    [SerializeField] internal string groupID = ""; // ID группы
    [Tooltip("Состояние животного на группировку")]
    [SerializeField] private bool _grouping = false; // Состояние животного на группировку

    /// <summary>
    /// Метод вызывается каждый кадр
    /// </summary>
    protected override void Update()
    {
        _grouping = CheckForGrouping(); // Устанавливаем состояние животного на группировку
        if (!_grouping) // Проверяем, группируется ли животное
            base.Update(); // Патрулируем или убегаем
        else
            Grouping(); // Идём к главному животному
    }

    /// <summary>
    /// Группировка животного
    /// </summary>
    private void Grouping()
    {
        if (_mainAnimal || mainAnimalTransform == null) // Проверяем, является ли животное главным
            return; // Пропускаем следующий код

        transform.position = Vector2.MoveTowards(transform.position, mainAnimalTransform.position, _speed * Time.deltaTime); // Идём к главному животному
    } 

    /// <summary>
    /// Проверяем состояние животного на группировку
    /// </summary>
    /// <returns></returns>
    private bool CheckForGrouping()
    {
        if (_mainAnimal || mainAnimalTransform == null) // Проверяем, является ли животное главным
            return false; // Возвращаем значение состояния группировки false

        float dist = Vector2.Distance(transform.position, mainAnimalTransform.position); // Высчитываем дистанцию до главного животного
        if (dist > _radiusForStartGroup) // Проверяем находится ли животное у главного животного
            return true; // Возвращаем значение состояния группировки true
        else
            return false; // Возвращаем значение состояния группировки false
    }
}