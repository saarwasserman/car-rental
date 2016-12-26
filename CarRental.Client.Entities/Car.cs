using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;

namespace CarRental.Client.Entities
{
    public class Car : TempObjectBase
    {
        int _CarId;
        string _Description;
        string _Color;
        int _Year;
        decimal _RentalPrice;
        bool _CurrentlyRental;

        public int CarId
        {
            get { return _CarId; }
            set
            {
                if (_CarId != value)
                {
                    _CarId = value;
                    OnPropertyChanged("CarId");
                }
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }

        public string Color
        {
            get
            {
                return _Color;
            }

            set
            {
                _Color = value;
            }
        }

        public int Year
        {
            get
            {
                return _Year;
            }

            set
            {
                _Year = value;
            }
        }

        public decimal RentalPrice
        {
            get
            {
                return _RentalPrice;
            }

            set
            {
                _RentalPrice = value;
            }
        }

        public bool CurrentlyRental
        {
            get
            {
                return _CurrentlyRental;
            }

            set
            {
                _CurrentlyRental = value;
            }
        }
    }
}
