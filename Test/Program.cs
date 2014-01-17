using BrickLib.Config;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            INI i = new INI("test.ini");
            var obj1 = i["test"]["Key"].Value;
            var obj2 = i["Test"]["Key2"].Value;
            var obj3 = i["Test"]["Multival"].Array;
            i.AddSection("Test2");
            i["Test2"].Add("Omg", "LOL");
            i["test2"].AddArray("wtf", new string[] { "12", "lol", "wtf", "bbq" });
            i.Save("omg.ini");
        }
    }
}
