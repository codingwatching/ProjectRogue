/// <summary>
/// ����ͨ�ýӿ�
/// 2024.8.14 update C
/// </summary>
public interface IEnemy
{
    public void setActive();
    public void setFreeze();
    public void hurt();
    public void onEnemyCreate();
    public void onEnemyDead();
    public int getDiffcult();
}
