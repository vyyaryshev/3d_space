using UnityEngine;

public class PlayerController
    : MonoBehaviour
{
    [Header("Maкс. скорость и ускорение вперед/назад")]
    [SerializeField] float forwardSpeed = 25;
    [SerializeField] float forwardAcceleration = 2.5f;

    [Header("Maкс. скорость и ускорение вверх/вниз")]
    [SerializeField] float verticalSpeed = 5;
    [SerializeField] float verticalAcceleration = 2;

    [Header("Maкс. скорость и ускорение вращения")]
    [SerializeField] float rollSpeed = 90;
    [SerializeField] float rollAcceleration = 3.5f;

    [Header("Чувствительность мыши")]
    [SerializeField] float lookRateSpeed = 90;

    [Header("Эффекты двигателей")]
    [SerializeField] private ParticleSystem[] engineParticleSystems;

    [Header("Управление пушками")]
    [SerializeField] private Transform[] guns;

    [SerializeField] private BulletController bulletPrefab;
    [SerializeField, Range(0.1f, 20f)] private float reloadTime;

    [SerializeField] DynamicAimController aimController;

    // текущая скорость вперед, вверх, текущее вращение
    float currentForwardSpeed, currentHoverSpeed, roll;

    // координата курсора мыши, центр экрана, расстояние между курсором мыши и центром экрана
    Vector2 lookInput, screenCenter, mouseDistance;

    // переменные для ввода осей из InputManager
    float horizontalInput, verticalInput, hoverInput;
    private int currentGunIndex;
    private float timer;
    private bool canShoot;

    private AudioSource shootSound;

    void Start()
    {
        // вычисляем центр экрана путем умножения высоты и ширины на 0.5
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        // Управление пушками
        currentGunIndex = 0;
        timer = 0f;
        canShoot = true;
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // считываем желаемое направление взгляда из позиции курсора мыши
        lookInput = Input.mousePosition;

        // считываем значения из InputManager
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        hoverInput = Input.GetAxis("Hover");

        // вычисляем расстояние между центром экрана и позицией курсора мыши
        mouseDistance = (lookInput - screenCenter) / screenCenter;

        // ограничиваем значение вектора до 1
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        // плавно изменяем ЗНАЧЕНИЕ для будущего вращения корабля при нажатии AD
        roll = Mathf.Lerp(roll, horizontalInput, rollAcceleration * Time.deltaTime);

        // поворот корабля вдоль оси Y от движения мыши и чувствительности
        var yRotate = -mouseDistance.y * lookRateSpeed * Time.deltaTime;
        // поворот корабля вдоль оси X от движения мыши и чувствительности
        var xRotate = mouseDistance.x * lookRateSpeed * Time.deltaTime;
        // поворот корабля вдоль оси Z от движения мыши и значения roll (51 строка)
        var zRotate = -roll * rollSpeed * Time.deltaTime;
        // вращаем корабль на основе вычисленных значений выше
        transform.Rotate(new Vector3(yRotate, xRotate, zRotate), Space.Self);

        // плавно изменяем ЗНАЧЕНИЕ продольной скорости корабля при нажатии WS
        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, verticalInput * forwardSpeed, forwardAcceleration * Time.deltaTime);
        // плавно изменяем ЗНАЧЕНИЕ вертикальной скорости корабля при нажатии ctrl и space
        currentHoverSpeed = Mathf.Lerp(currentHoverSpeed, hoverInput * verticalSpeed, verticalAcceleration * Time.deltaTime);

        // двигаем корабль вперед/назад в зависимости от вычисленного значения currentForwardSpeed
        transform.position += transform.forward * currentForwardSpeed * Time.deltaTime;
        // двигаем корабль вверх/вниз в зависимости от вычисленного значения currentHoverSpeed
        transform.position += transform.up * currentHoverSpeed * Time.deltaTime;


        // Управление пушками
        if (!canShoot)
            ReloadTimeControl();


        if (Input.GetButton("Fire1") && canShoot)
        {       


            foreach (var gun in guns)
            {
                var newBullet = Instantiate(bulletPrefab, guns[currentGunIndex].position, guns[currentGunIndex].rotation);
                newBullet.transform.LookAt(aimController.aimPosition);
                canShoot = false;
                timer = reloadTime;
            }


            shootSound.Play();
            currentGunIndex++;

            if (currentGunIndex == guns.Length)
                currentGunIndex = 0;

        }

        foreach (var engineParticleSystem in this.engineParticleSystems)
        {
            engineParticleSystem.startSpeed = currentForwardSpeed;
        }


    }

    private void ReloadTimeControl()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            canShoot = true;
    }


}


