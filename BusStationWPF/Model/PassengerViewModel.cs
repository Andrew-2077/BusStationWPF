﻿using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class PassengerViewModel : INotifyPropertyChanged
    {
        private int id;
        private int passport;
        private int? userId;
        private DateTime birthday = DateTime.Now;
        private PeopleGender gender;
        private string name;
        private string surname;
        private string patronymic;
        private bool loadedInDB;
        public DateTime Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                OnPropertyChanged("Birthday");
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Patronymic
        {
            get => patronymic;
            set
            {
                patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int Passport
        {
            get => passport;
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }
        }
        public int? UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }
        public PeopleGender Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        public bool LoadedInDB
        {
            get => loadedInDB;
            set => loadedInDB = value;
        }
        public PassengerViewModel(Passenger passenger, bool loadedInDB)
        {
            this.passport = passenger.Passport;
            this.userId = passenger.UserId;
            this.id = passenger.Id;
            this.name = passenger.Name;
            this.surname = passenger.Surname;
            this.patronymic = passenger.Patronymic;
            this.birthday = passenger.Birthday;
            this.gender = passenger.Gender ? PeopleGender.Man : PeopleGender.Woman;
            this.loadedInDB = loadedInDB;
        }
        public PassengerViewModel(int UserId, bool loadedInDB)
        {
            this.loadedInDB = loadedInDB;
            this.userId = UserId;
        }
        public void SetPassenger(Passenger passenger)
        {
            passenger.UserId = this.userId;
            passenger.Gender = this.Gender == PeopleGender.Man;
            passenger.Birthday = this.Birthday;
            passenger.Name = this.Name;
            passenger.Surname = this.Surname;
            passenger.Patronymic = this.Patronymic;
            passenger.Passport = this.Passport;
        }
        public Passenger GetPassanger() => new Passenger() { Birthday = this.birthday, Id = this.id, Passport = this.passport, Name = this.name, Surname = this.surname, Patronymic = this.patronymic, UserId = this.userId, Gender = this.Gender == PeopleGender.Man };
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
