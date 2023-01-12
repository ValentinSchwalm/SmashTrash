public interface IHealthSystem
{
    int maxHealthPoints();
    int currentHealthPoints();
    void ReceiveDamage(int damage);
    void Die();
}
