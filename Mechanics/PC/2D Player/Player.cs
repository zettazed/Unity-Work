using UnityEngine;

/// <summary>
/// Класс передвижения игрока
/// </summary>
public class Player : MonoBehaviour
{
    [Header("PlayerInput: Options")]
    [Tooltip("Скорость передвижения игрока")]
    [SerializeField] private float _speed = 1f; // Скорость передвижения игрока
    private Transform _playerTransform; // Компонент Transform игрока
    private Vector2 _direction; // Вектор направления игрока
    private Rigidbody2D _rig; // Физика игрока

    /// <summary>
    /// Метод вызывается при первом кадре
    /// </summary>
    private void Start()
    {
        _playerTransform = GetComponent<Transform>(); // Инициализируем переменную Transform игрока при старте
        _rig = GetComponent<Rigidbody2D>(); // Инициализируем переменную физики игрока при старте
    }

    /// <summary>
    /// Метод вызывается каждый кадр
    /// </summary>
    private void Update()
    {
        _direction = DirectionCalculate(); // Присваеваем значение вектору
        RotatePlayer(); // Поворачиваем игрока
    }

    /// <summary>
    /// Метод вызывается фиксированное количество кадров
    /// </summary>
    private void FixedUpdate() => MovePlayer(); // Вызываем метод перемещения игрока

    /// <summary>
    /// Перемещение игрока по вектору с определённой скоростью
    /// </summary>
    private void MovePlayer()
        => _rig.MovePosition(_rig.position + _direction * _speed * Time.fixedDeltaTime); // Перемещаем игрока 

    /// <summary>
    /// Метод поворота игрока
    /// </summary>
    private void RotatePlayer()
    {
        if (_direction.x != 0 || _direction.y != 0) // Проверяем, находится ли игрок в движении
            _playerTransform.rotation = new Quaternion(0f, FlipXPlayer(), 0f, _playerTransform.rotation.w); // Устанавливаем поворот по оси Х игрока
    }

    /// <summary>
    /// Вычисляем куда смотрит игрок
    /// Если пойти вправо, то игрок будет смотреть вправо
    /// Если пойти влево, то игрок будет смотреть влево
    /// </summary>
    /// <returns></returns>
    private float FlipXPlayer()
    {
        float flipX = 0f; // Создаём локальное возвращаемое значение
        if (_direction.x < 0) // Если значение вектора Х меньше 0, то игрок смотрит влево
            flipX = 180f; // Устанавливаем угол поворота в противоположную сторону по оси X

        return flipX; // Возвращаем значение
    }

    /// <summary>
    /// Высчитывание направления передвижения игрока
    /// </summary>
    private Vector2 DirectionCalculate()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Высчитываем направление вектора
        return direction; // Возвращаем значение
    }
}