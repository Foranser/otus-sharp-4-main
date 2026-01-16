using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;


namespace Otus.Task1.Models
{


    public enum Gender{
        None = 0,
        Male=1,
        Female=2,
    }

    /// <summary>
    /// Предмет
    /// </summary>
    [Serializable]
    public class Item
    {
        /// <summary>
        /// Название предмета
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        /// <value></value>
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Информация о пользователе
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Уровень пользователя
        /// </summary>
        /// <value></value>
        [XmlAttribute("level")]
        public int Level { get; set; }


        /// <summary>
        /// Имя
        /// </summary>
        /// <value></value>
        public string Name { get; set; }


        /// <summary>
        /// Пол персонажа
        /// </summary>
        /// <value></value>
        [XmlIgnore]
        [JsonIgnore]
        public Gender Gender { get; set; }

        [XmlElement("gender")]
        [JsonPropertyName("gender")]
        public string GenderCode
        {
            get => Gender == Gender.Male ? "m" : Gender == Gender.Female ? "f" : "";
            set
            {
                if (string.Equals(value, "m", StringComparison.OrdinalIgnoreCase))
                    Gender = Gender.Male;
                else if (string.Equals(value, "f", StringComparison.OrdinalIgnoreCase))
                    Gender = Gender.Female;
                else
                    Gender = Gender.None;
            }
        }

    }


    /// <summary>
    /// Состояние игры
    /// </summary>
    [Serializable]
    public class GameStatus
    {


        /// <summary>
        /// Текущая локация
        /// </summary>
        /// <value></value>
        public string CurrentLocation { get; set; }

        /// <summary>
        /// Информация о пользователе
        /// </summary>
        /// <value></value>
        [XmlElement("u")]
        [JsonPropertyName("u")]
        public User User { get; set; }


        public Item[] Items { get; set; }

        /// <summary>
        /// Координаты пользователя
        /// </summary>
        /// <value></value>
        [NonSerialized]
        private (double, double) _coords;

        [XmlIgnore]
        [JsonIgnore]
        public (double, double) Coords
        {
            get => _coords;
            set => _coords = value;
        }

    }

    /// <summary>
    /// Сохраняемый файл
    /// </summary>
    [Serializable]
    public class SaveFile : GameStatus
    {

        /// <summary>
        /// Дата создания файла
        /// </summary>
        /// <value></value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Дата сохранения
        /// </summary>
        /// <value></value>
        public DateTime? SaveDate { get; set; }

        /// <summary>
        /// НАзвание файла
        /// </summary>
        /// <value></value>
        public string FileName { get; set; }



    }
}