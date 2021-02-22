using System.IO;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using static System.Console;

namespace habraweatherappconsole
{
    public static class DataRepo
    {
        static ObservableCollection<RootBasicCityInfo> rootBasicInfo = new ObservableCollection<RootBasicCityInfo>();


        public static void ReadData()
        {
            string prepareString = null;

            using (StreamReader sr = new StreamReader("SimpleData.json"))
            {
                prepareString = sr.ReadToEnd();
            }

            ObservableCollection<RootBasicCityInfo> rbci = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);
            
            foreach (var item in rbci)
            {
                WriteLine(item.Key);
            }
        }
    }
}