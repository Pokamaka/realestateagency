﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstateAgency.Classes
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public class DataBase
    { 
        Guid PokamaGuid = new Guid("F09F642A-9247-4B0D-A1CC-A39148EC5E59"); //GUID моего аккаута
        Guid UserStatus_Active = new Guid("301C9782-D208-42CD-862C-6F2D30C46E0E"); //GUID пользовательского статуса - активирован

        /// <summary>
        /// Функция авторизации пользователя.
        /// Условия для авторизации: совпадение пароля и логина + активированый аккаунт
        /// pokama - test123
        /// </summary>
        /// <param name="Login">Принимает логин пользователя</param>
        /// <param name="Password">Принимает пароль пользователя</param>
        /// <returns>Возращет True - авторизован, False - не авторизован</returns>
        public bool Auth(string Login, string Password)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(Password);
                MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

                byte[] byteHash = CSP.ComputeHash(bytes);
                string hash = string.Empty;

                foreach (byte b in byteHash)
                {
                    hash += string.Format("{0:x2}", b);
                }
                Guid enterPass = new Guid(hash);

                try
                {
                    Authorization account = db.Authorization.Where(x => x.Login == Login && x.Password == enterPass).FirstOrDefault();
                    Accounts check = db.Accounts.Where(x => x.ID == account.UserID && x.UserStatus == UserStatus_Active).FirstOrDefault();                  
                    if (account == null || check == null)
                    {
                        MessageBox.Show("Вы ввели не правильные данные для входа!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    Properties.Settings.Default.User_ID = check.ID;
                    Properties.Settings.Default.User_RoleName = db.UserRole.Where(x => x.ID == check.UserRole).FirstOrDefault().Name;
                    Properties.Settings.Default.User_SName = $"{check.Surname} {check.Name.Substring(0, 1)}.{check.Patronymic.Substring(0, 1)}.";
                    Properties.Settings.Default.Save();
                }
                catch
                {
                    MessageBox.Show("Вы ввели не правильные данные для входа!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }             
                return true;
            }
        }

        /// <summary>
        /// Функция выгрузки всех аккаунтов
        /// </summary>
        /// <returns>Возращает список акаунтов с БД</returns>
        public List<Accounts> GetAccounts()
        {
            List<Accounts> accounts = new List<Accounts>();
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    List<UserRole> role = new List<UserRole>(db.UserRole.ToList());
                    List<UserStatus> status = new List<UserStatus>(db.UserStatus.ToList());

                    accounts = db.Accounts.ToList();
                    foreach (Accounts item in accounts)
                    {
                        item.SName = $"{item.Surname} {item.Name.Substring(0, 1)}.{item.Patronymic.Substring(0, 1)}.";
                        item.FName = $"{item.Surname} {item.Name} {item.Patronymic}";
                        item.UserRoleName = role.Where(x => x.ID == item.UserRole).FirstOrDefault().Name;
                        item.UserStatusName = status.Where(x => x.ID == item.UserStatus).FirstOrDefault().Name;           
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузки с БД. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return accounts;
        }

        /// <summary>
        /// Функция выгрузки пользовательских ролей
        /// </summary>
        /// <returns>Возращает список ролей</returns>
        public List<UserRole> GetRole()
        {
            List<UserRole> role = new List<UserRole>();
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    role = db.UserRole.ToList();
                    return role;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузки пользовательских ролей! \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }

        /// <summary>
        /// Функция выгрузки всех объектов
        /// </summary>
        /// <returns>Возращает список объектов с БД</returns>
        public List<Objects> GetObject()
        {
            List<Objects> objects = new List<Objects>();
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    List<ObjectStatus> status = new List<ObjectStatus>(db.ObjectStatus.ToList());
                    List<Accounts> acc = GetAccounts();
                    List<DistrictNames> disctrics = new List<DistrictNames>(db.DistrictNames.ToList());

                    objects = db.Objects.ToList();
                    foreach (Objects item in objects)
                    {
                        item.StatusName = status.Where(x => x.ID == item.Status).FirstOrDefault().Name;                     
                        item.OwnerName = acc.Where(x => x.ID == item.OwnerID).FirstOrDefault().FName;
                        //item.AgentSName = acc.Where(x => x.ID == item.AgentID).FirstOrDefault().SName;
                        item.DistrictName = disctrics.Where(x => x.ID == item.DistrictID).FirstOrDefault().Name;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузки с БД. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return objects;
        }

        /// <summary>
        /// Функция выгрузки районов города
        /// </summary>
        /// <returns>Возращает список ролей</returns>
        public List<DistrictNames> GetDistrict()
        {
            List<DistrictNames> districts = new List<DistrictNames>();
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    districts = db.DistrictNames.ToList();
                    return districts;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузки пользовательских ролей! \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }

        /// <summary>
        /// Функция выгрузки всех сделок
        /// </summary>
        /// <returns>Возращает список сделок с БД</returns>
        public List<Deals> GetDeals()
        {
            List<Deals> deals = new List<Deals>();
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    List<ObjectStatus> status = new List<ObjectStatus>(db.ObjectStatus.ToList());
                    List<Accounts> acc = GetAccounts();
                    List<Objects> objects = new List<Objects>(db.Objects.ToList());   
                    List<DealStatus> dealStatus = new List<DealStatus>(db.DealStatus.ToList());

                    deals = db.Deals.ToList();
                    foreach (Deals item in deals)
                    {
                        item.ClientFName = acc.Where(x => x.ID == item.UserID).FirstOrDefault().FName;
                        item.AgentSName = acc.Where(x => x.ID == item.AgentID).FirstOrDefault().SName;
                        item.ObjectName = objects.Where(x => x.ID == item.ObjectID).FirstOrDefault().NameObj;
                        item.StatusName = dealStatus.Where(x => x.ID == item.StatusID).FirstOrDefault().Name;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузки с БД. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return deals;
        }

        /// <summary>
        /// Функция добавления нового пользователя
        /// </summary>
        /// <param name="acc">Принимает объект с полями класса Accounts</param>
        /// <returns>Возращает True - если удачно, False - если не удачно</returns>
        public bool CreateUser(Accounts acc)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    acc.Author = Properties.Settings.Default.User_ID;
                    acc.CD = DateTime.Now;
                    acc.UserStatus = new Guid("301C9782-D208-42CD-862C-6F2D30C46E0E");

                    db.Accounts.Add(acc);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при создание пользователя. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Функция редактирования пользователя
        /// </summary>
        /// <param name="acc">Принимает объект с полями класса Accounts</param>
        /// <returns>Возращает True - если удачно, False - если не удачно</returns>
        public bool EditUser(Accounts acc)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    Accounts oldAcc = db.Accounts.Where(x => x.ID == acc.ID).FirstOrDefault();

                    oldAcc.Name = acc.Name;
                    oldAcc.Surname = acc.Surname;
                    oldAcc.Patronymic = acc.Patronymic;
                    oldAcc.PhoneNumber = acc.PhoneNumber;
                    oldAcc.Email = acc.Email;
                    oldAcc.DateBrth = acc.DateBrth;
                    oldAcc.Passport = acc.Passport;
                    oldAcc.UserRole = acc.UserRole;

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при редактирование пользователя. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Функция создания нового объекта
        /// </summary>
        /// <param name="obj">Принимает объект с полями класса Object</param>
        /// <returns>Возращает True - если удачно, False - если не удачно</returns>
        public bool CreateObject(Objects _obj)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    Objects obj = new Objects();

                    obj.ID = _obj.ID;
                    obj.NameObj = _obj.NameObj;
                    obj.Floor = _obj.Floor;
                    obj.CountRoom = _obj.CountRoom;
                    obj.SObject = _obj.SObject;
                    obj.Adress = _obj.Adress;
                    obj.Note = _obj.Note;
                    obj.Coast = _obj.Coast;

                    obj.DistrictID = _obj.DistrictID;
                    obj.AgentID = _obj.AgentID;
                    obj.Status = _obj.Status;
                    obj.CD = DateTime.Now;

                    db.Objects.Add(obj);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при создание пользователя. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Функция редактирования объекта
        /// </summary>
        /// <param name="obj">Принимает объект с полями класса Object</param>
        /// <returns>Возращает True - если удачно, False - если не удачно</returns>
        public bool EditObject(Objects obj)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    Objects oldObj = db.Objects.Where(x => x.ID == obj.ID).FirstOrDefault();

                    oldObj.NameObj = obj.NameObj;
                    oldObj.Floor = obj.Floor;
                    oldObj.CountRoom = obj.CountRoom;
                    oldObj.SObject = obj.SObject;
                    oldObj.Adress = obj.Adress;
                    oldObj.OwnerID = obj.OwnerID;
                    oldObj.DistrictID = obj.DistrictID;
                    oldObj.Note = obj.Note;
                    oldObj.Coast = obj.Coast;

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при редактирование пользователя. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Функция добавления новой сделки
        /// </summary>
        /// <param name="deal">Принимает объект с полями класса Deals</param>
        /// <returns>Возращает True - если удачно, False - если не удачно</returns>
        public bool CreateDeal(Deals deal)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                DealsCounter checkCount = db.DealsCounter.Where(x => x.Year == DateTime.Now.Year).FirstOrDefault();
                if (checkCount == null)
                {
                    DealsCounter newCount = new DealsCounter();
                    newCount.ID = Guid.NewGuid();
                    newCount.Year = DateTime.Now.Year;
                    newCount.Value = 0;
                    db.DealsCounter.Add(newCount);
                    db.SaveChanges();
                }
                try
                {
                    DealsCounter counter = db.DealsCounter.Where(x => x.Year == DateTime.Now.Year).FirstOrDefault();
                         
                    deal.Number = counter.Value + 1;
                    deal.DateStart = DateTime.Now;
                    deal.StatusID = new Guid("54BB101C-01E3-4CCA-ACBE-63BF7FB5FFE2");
                    
                    deal.CD = DateTime.Now;

                    db.Deals.Add(deal);
                    db.SaveChanges();

                    counter.Value += 1;
                    db.SaveChanges();

                    Objects obj = db.Objects.Where(x => x.ID == deal.ObjectID).FirstOrDefault();
                    obj.Status = new Guid("1AE8D69D-B4BD-42AC-ADEC-216CBF121CDC");
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при создание сделки. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Функция редактирования сделки
        /// </summary>
        /// <param name="deal">Принимает объект с полями класса Deals</param>
        /// <returns>Возращает True - если удачно, False - если не удачно</returns>
        public bool EditDeal(Deals deal)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    Deals oldDeal = db.Deals.Where(x => x.ID == deal.ID).FirstOrDefault();

                    oldDeal.UserID = deal.UserID;
                    oldDeal.AgentID = deal.AgentID;
                    oldDeal.ObjectID = deal.ObjectID;
                    oldDeal.Summ = deal.Summ;
                    oldDeal.Procent = deal.Procent;
                   
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при редактирование пользователя. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Функция завершения сделки
        /// </summary>
        /// <param name="deal">Принимает объект с полями класса Deals</param>
        /// <returns>Возращает True - если удачно, False - если не удачно</returns>
        public bool StopDeal(Deals _deal)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    Deals deal = db.Deals.Where(x => x.ID == _deal.ID).FirstOrDefault();
                    deal.DateEnd = DateTime.Now;
                    deal.StatusID = new Guid("38BD2342-D0C8-4E65-B124-B73A09BF27B7");

                    Objects obj = db.Objects.Where(x => x.ID == deal.ObjectID).FirstOrDefault();
                    obj.Status = new Guid("F764E303-473F-4A2A-A5E7-584BA0CAA852");

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при редактирование пользователя. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Функция отмены сделки
        /// </summary>
        /// <param name="deal">Принимает объект с полями класса Deals</param>
        /// <returns>Возращает True - если удачно, False - если не удачно</returns>
        public bool CancelDeal(Deals _deal)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    Deals deal = db.Deals.Where(x => x.ID == _deal.ID).FirstOrDefault();
                    deal.DateEnd = DateTime.Now;
                    deal.StatusID = new Guid("43A97712-8980-4EFA-BB2D-4190DE383595");

                    Objects obj = db.Objects.Where(x => x.ID == deal.ObjectID).FirstOrDefault();
                    obj.Status = new Guid("2692C961-ACD3-4516-A0E3-C79B89E428A2");

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при редактирование пользователя. \n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }
    }
}
