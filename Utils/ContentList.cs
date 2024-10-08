using HTCCL.Content;

namespace HTCCL.Utils;

public class ContentList : List<string>
{
    public new string this[int index]
    {
        get => base[index];
        set
        {
            if (Aliases.AliasMap.TryGetValue(value, out string value1))
            {
                base[index] = value1;
            }
            else
            {
                base[index] = value;
            }
        }
    }

    public new void Add(string item)
    {
        if (Aliases.AliasMap.TryGetValue(item, out string value))
        {
            base.Add(value);
        }
        else
        {
            base.Add(item);
        }
    }

    public new void AddRange(IEnumerable<string> collection)
    {
        foreach (string item in collection)
        {
            if (Aliases.AliasMap.TryGetValue(item, out string value))
            {
                base.Add(value);
            }
            else
            {
                base.Add(item);
            }
        }
    }

    public new void Insert(int index, string item)
    {
        if (Aliases.AliasMap.TryGetValue(item, out string value))
        {
            base.Insert(index, value);
        }
        else
        {
            base.Insert(index, item);
        }
    }

    public new void InsertRange(int index, IEnumerable<string> collection)
    {
        foreach (string item in collection)
        {
            if (Aliases.AliasMap.TryGetValue(item, out string value))
            {
                base.Insert(index, value);
            }
            else
            {
                base.Insert(index, item);
            }
        }
    }

    public new bool Remove(string item)
    {
        if (Aliases.AliasMap.TryGetValue(item, out string value))
        {
            return base.Remove(value);
        }

        return base.Remove(item);
    }
}