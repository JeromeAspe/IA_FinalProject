using System.Collections.Generic;
public interface IHandler<TKey,TItem> where TItem:IHandledItem<TKey>
{
    Dictionary<TKey,TItem> Handler { get; }

    void Add(TItem _item);
    void Remove(TItem item);
    void Remove(TKey _id);

    TItem Get(TKey _id);

    void Disable(TItem _item);
    void Disable(TKey _id);
    void Enable(TItem _item);
    void Enable(TKey _id);
    bool Exists(TItem _item);
    bool Exists(TKey _id);
}

