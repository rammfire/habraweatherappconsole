using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace habraweatherappconsole
{
    public static class UserApiManager
    {
        public static ObservableCollection<UserApi> userApiList = new ObservableCollection<UserApi>();

        public static void WriteUserApiToLocalStorage(string formalUserApi)
        {
            UserApi userApiProp = new UserApi
            {
                UserApiProperty = formalUserApi
            };

            userApiList.Add(userApiProp);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<UserApi>));
            FileStream fileStream = new FileStream("UserApi.xml", FileMode.OpenOrCreate);
            xmlSerializer.Serialize(fileStream, userApiList);
            fileStream.Close();        }      
     }
}