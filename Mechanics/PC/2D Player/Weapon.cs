using UnityEngine;

/// <summary>
/// Класс реализации механики стрельбы
/// </summary>
public class Weapon : MonoBehaviour
{
    [Header("Transform")]
    [Tooltip("Дочерний Transform объект ShotPos игрока, откуда будет вылетать пуля")]
    [SerializeField] private Transform _shotPos; // Дочерний Transform объект ShotPos игрока, откуда будет вылетать пуля

    [Header("Prefabs")]
    [Tooltip("Префаб пули")]
    [SerializeField] private GameObject _bullet; // Префаб пули

    [Header("Options")]
    [Tooltip("Количество патронов")]
    [SerializeField] internal int bulletCount = 10; // Количество патронов

    /// <summary>
    /// Метод вызывается каждый кадр
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bulletCount > 0) // Проверка, нажалась ли левая кнопка мыши, а также есть ли патроны в оружии
            Shoot(); // Выстрел
    }

    /// <summary>
    /// Метод выстрела
    /// </summary>
    private void Shoot()
    {
        Instantiate(_bullet, _shotPos.position, transform.rotation); // Спавн пули в дочернем объекте ShotPos у игрока
        bulletCount--; // Уменьшение количества патронов на 1
    }
}