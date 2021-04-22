using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFSerialization
{
    [Serializable]
    class Appointment
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
    [Serializable]
    class Owner
    {

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
    [Serializable]
    class Description
    {
        private int waterLevel { get; set; }
        public enum SoilType { Sandy, Clay, Silt, Peat, Chalk, Loam }
        private SoilType soilType;
        private List<Point> geodeticReference;
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
    [Serializable]
    class Property
    {
        private Appointment Appointment { get; set; }
        private Owner Owner { get; set; }
        private Description Description { get; set; }
        private double Price { get; set; }
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
    [Serializable]
    class Locality
    {

        private int serialNumber { get; set; }

        public List<Property> plots = new List<Property>();
        static int Count;

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
        public Locality(int count)
        {
            Count = count;
            serialNumber = Count;

        }
    }
}
