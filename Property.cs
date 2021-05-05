using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace WPFSerialization
{
    [XmlType("Appointment")]
    [Serializable]
    public class Appointment
    {
        public enum AppointmentValue { UnderBuilding, Agricultural, Reserved }
        AppointmentValue appointmentValue;
        public Appointment() { }
        public Appointment(AppointmentValue v)
        {
            this.appointmentValue = v;
        }
        public override string ToString()
        {
            return "Appointment: " + appointmentValue.ToString();
        }
    }
    [XmlType("Owner")]
    [XmlInclude(typeof(Owner)), XmlInclude(typeof(Appointment)), XmlInclude(typeof(Description)), XmlInclude(typeof(Property))]
    [Serializable]
    public class Owner
    {
        [XmlElement("Name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private string name;
        [XmlElement("SurName")]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        private string surname;
        [XmlElement("birthDay")]
        DateTime birthDay { get; set; }
        public Owner() { }
        public Owner(string Name, string Surname, DateTime birthDay)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.birthDay = birthDay;
        }
        public override string ToString()
        {
            return "Name: " + name + " Surname: " + surname + " Birthday: " + birthDay.ToString();
        }
    }
    [XmlType("Description")]
    [XmlInclude(typeof(Owner)), XmlInclude(typeof(Appointment)), XmlInclude(typeof(Description)), XmlInclude(typeof(Property))]
    [Serializable]
    public class Description
    {
        [XmlElement("waterLevel")]
        public int waterLevel { get; set; }
        public enum SoilType { Sandy, Clay, Silt, Peat, Chalk, Loam }
        private SoilType soilType;
        [XmlArray("geodeticReference")]
        [XmlArrayItem("Points")]
        public List<Point> geodeticReference;
        public void AddPoint(Point point)
        {
            geodeticReference.Add(point);
        }
        public void RemovePoint(Point point)
        {
            geodeticReference.Remove(point);
        }
        public void ClearPoints()
        {
            geodeticReference.Clear();
        }
        public Description() { }
        public Description(int waterLevel, SoilType soilType, List<Point> geo)
        {
            this.waterLevel = waterLevel;
            this.soilType = soilType;
            geodeticReference = geo;
        }
        public override string ToString()
        {
            string str = "";
            foreach (var geo in geodeticReference)
            {
                str += geo.ToString() + " ";
            }
            return "ground water level: " + waterLevel + " Soil type: " + soilType + " " + str;
        }
    }
    [XmlType("Property")]
    [XmlInclude(typeof(Owner)), XmlInclude(typeof(Appointment)), XmlInclude(typeof(Description)), XmlInclude(typeof(Property))]
    [Serializable]
    public class Property
    {
        [XmlElement("Appointment")]
        public Appointment Appointment { get; set; }
        [XmlElement("Owner")]
        public Owner Owner { get; set; }
        [XmlElement("Description")]
        public Description Description { get; set; }
        [XmlElement("Price")]
        public double Price { get; set; }

        public string Information()
        {
            return "Owner surname: " + Owner.Surname + " Price: " + Price;
        }
        public override string ToString()
        {
            return Appointment.ToString() + " " + Environment.NewLine + Owner.ToString() + " " + Environment.NewLine + "Price($): " + Price + " " + Environment.NewLine + Description.ToString();
        }

        public Property() { }
        public Property(Appointment Appointment, Owner Owner, Description Description, double Price)
        {
            this.Appointment = Appointment;
            this.Owner = Owner;
            this.Description = Description;
            this.Price = Price;
        }

    }
    [XmlRoot("Locality")]
    [XmlInclude(typeof(Owner)), XmlInclude(typeof(Appointment)), XmlInclude(typeof(Description)), XmlInclude(typeof(Property))]
    [Serializable]
    public class Locality
    {
        [XmlElement("serialNumber")]
        public int serialNumber { get; set; }

        [XmlArray("Properties")]
        [XmlArrayItem("Property")]
        public List<Property> plots = new List<Property>();
        [XmlElement("Count")]
        public static int Count;

        public void AddProperty(Property property)
        {
            plots.Add(property);
        }
        public void RemoveProperty(Property property)
        {
            plots.Remove(property);
        }
        public void ClearProperty()
        {
            plots.Clear();
        }
        public override string ToString()
        {
            return "Locality: " + serialNumber;
        }
        public string FullInfo()
        {
            string str = "";
            if (plots != null)
            {
                foreach (var prop in plots)
                {
                    str += prop.ToString() + Environment.NewLine;
                }
            }
            return str;

        }
        public Locality() { }
     
        public Locality(int count)
        {
            Count = count;
            serialNumber = Count;

        }
    }
}
