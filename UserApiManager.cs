using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

using static System.Console;

namespace habraweatherappconsole
{
    /// <summary>
    /// Класс описывает возможность чтения и записи пользовательского API
    /// </summary>
    public static class UserApiManager
    {
        /// <summary>
        /// Список хранит APIKey
        /// </summary>
        /// <typeparam name="UserApi"></typeparam>
        /// <returns></returns>
        public static ObservableCollection<UserApi> userApiList = new ObservableCollection<UserApi>();
        
        /// <summary>
        /// Метод реализует возможность записи пользользовательского
        /// APIKey на диск
        /// </summary>
        /// <param name="formalUserApi"></param>
        public static void WriteUserApiToLocalStorage(string formalUserApi)
        {
            UserApi userApiProp = new UserApi
            {
                UserApiProperty = formalUserApi
            };

            userApiList.Add(userApiProp);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<UserApi>));

            using (StreamWriter sw = new StreamWriter("UserApi.xml"))
            {
                xmlSerializer.Serialize(sw, userApiList);
            }
        }

        /// <summary>
        /// Метод реализуе возможность восстановления списка APIKey в памяти
        /// </summary>
        public static void ReadUserApiToLocalStorage()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<UserApi>));

            try
            {
                using (StreamReader sr = new StreamReader("UserApi.xml"))
                {
                    userApiList = xmlSerializer.Deserialize(sr) as ObservableCollection<UserApi>;
                }
            }

            catch(Exception ex)
            {
                /* Не вывожу никаких сообщений об ошибке. Потому как, если утилита была запущена впервые
                / то файла скорее всего нет. Даже если бы он был и из-за каких-то аппаратных проблем стал недоступен
                / то что я могу с этим поделать в таком случае?
                */
            }
        }
     }
}