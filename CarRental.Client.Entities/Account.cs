﻿using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{
    public class Account : ObjectBase
    {
        private int _AccountId;
        private string _LoginEmail;
        private string _FirstName;
        private string _LastName;
        private string _Address;
        private string _City;
        private string _State;
        private string _ZipCode;

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

        public string LoginEmail
        {
            get { return _LoginEmail; }
            set
            {
                if (_LoginEmail != value)
                {
                    _LoginEmail = value;
                    OnPropertyChanged(() => LoginEmail);
                }
            }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }

        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                    OnPropertyChanged(() => LastName);
                }
            }
        }

        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    OnPropertyChanged(() => Address);
                }
            }
        }

        public string City
        {
            get { return _City; }
            set
            {
                if (_City != value)
                {
                    _City = value;
                    OnPropertyChanged(() => City);
                }
            }
        }

        public string State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    OnPropertyChanged(() => State);
                }
            }
        }

        public string ZipCode
        {
            get { return _ZipCode; }
            set
            {
                if (_ZipCode != value)
                {
                    _ZipCode = value;
                    OnPropertyChanged(() => ZipCode);
                }
            }
        }
    }
}
