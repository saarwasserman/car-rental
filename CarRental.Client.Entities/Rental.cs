using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{
    public class Rental : ObjectBase
    {
        private int _RentalId;
        private int _AccountId;
        private int _CarID;
        private DateTime _DateRented;
        private DateTime _DateDue;
        private DateTime? _DateReturned;

        public int RentalId
        {
            get { return _RentalId; }
            set
            {
                if (_RentalId != value)
                {
                    _RentalId = value;
                    OnPropertyChanged(() => RentalId);
                }
            }
        }

        public int AccountId
        {
            get { return _AccountId; }
            set
            {
                if (_AccountId != value)
                {
                    _AccountId = value;
                    OnPropertyChanged(() => AccountId);
                }
            }
        }

        public int CarId
        {
            get { return _CarID; }
            set
            {
                if (_CarID != value)
                {
                    _CarID = value;
                    OnPropertyChanged(() => CarId);
                }
            }
        }

        public DateTime DateRented
        {
            get { return _DateRented; }
            set
            {
                if (_DateRented != value)
                {
                    _DateRented = value;
                    OnPropertyChanged(() => DateRented);
                }
            }
        }

        public DateTime DateDue
        {
            get { return _DateDue; }
            set
            {
                if (_DateDue != value)
                {
                    _DateDue = value;
                    OnPropertyChanged(() => DateDue);
                }
            }
        }

        public DateTime? DateReturned
        {
            get { return _DateReturned; }
            set
            {
                if (_DateReturned != value)
                {
                    _DateReturned = value;
                    OnPropertyChanged(() => DateReturned);
                }
            }
        }
    }
}
