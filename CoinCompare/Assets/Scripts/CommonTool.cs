using System.IO;
public class CommonTool
{
    public static void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter _sw;
        FileInfo _t = new FileInfo(path + "//" + name);
        if (!_t.Exists)
        {
            //如果此文件不存在则创建
            _sw = _t.CreateText();
        }
        else
        {
            //如果此文件存在则打开
            _sw = _t.AppendText();
        }
        //以行的形式写入信息
        _sw.WriteLine(info);
        //关闭流
        _sw.Close();
        //销毁流
        _sw.Dispose();
    }

    public static string ReadFile(string _path, string _name)
    {
        //使用流的形式读取
        StreamReader _sr = null;
        try
        {
            _sr = File.OpenText(_path + "//" + _name);
        }
        catch
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }
        string _content = _sr.ReadToEnd();

        //关闭流
        _sr.Close();
        //销毁流
        _sr.Dispose();
        //将数组链表容器返回
        return _content;
    }
}