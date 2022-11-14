using Newtonsoft.Json;

public interface IUnitOfWork1<T> where T : class
{
    public List<T> DeserializeFromJson<T>();
    public void WriteToJson(List<T> list);
}

public class UnitOfWork1<T> : IUnitOfWork1<T> where T : class
{
    private const string _dataSource = $@"C:\Users\User\source\repos\TurboAz\TurboAz.Repository\DbContext\[FileName]s.json";
    public List<T> DeserializeFromJson<T>()
    {
        var tmpTypeName = typeof(T).Name;
        string jsonFilePath = "";

        if (tmpTypeName.LastOrDefault().ToString() == "y")
        {
            tmpTypeName = tmpTypeName.Remove(tmpTypeName.Length - 1);
            tmpTypeName += "ie";
            jsonFilePath = _dataSource.Replace("[FileName]", tmpTypeName);
        }
        else
        {
            jsonFilePath = _dataSource.Replace("[FileName]", typeof(T).Name);
        }

        string json = File.ReadAllText(jsonFilePath);
        var itemList = JsonConvert.DeserializeObject<List<T>>(json);
        return itemList;
    }
    public void WriteToJson(List<T> list)
    {
        var tempTypeName = typeof(T).Name;
        string jsonFilePath = "";

        if (tempTypeName.LastOrDefault().ToString() == "y")
        {
            tempTypeName = tempTypeName.Remove(tempTypeName.Length - 1);
            tempTypeName += "ie";
            jsonFilePath = _dataSource.Replace("[FileName]", tempTypeName);
        }
        else
        {
            jsonFilePath = _dataSource.Replace("[FileName]", typeof(T).Name);
        }
        var newJson = JsonConvert.SerializeObject(list);
        File.WriteAllText(jsonFilePath, newJson);
    }
}