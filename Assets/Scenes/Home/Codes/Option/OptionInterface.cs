/// <summary>設定項目をHome_Managerで一括で扱うためのインターフェース</summary>
public interface OptionInterface
{
    /// <summary>初期化</summary>
    public abstract void Initialize();

    /// <summary>設定の動作</summary>
    public abstract void Act();
}
