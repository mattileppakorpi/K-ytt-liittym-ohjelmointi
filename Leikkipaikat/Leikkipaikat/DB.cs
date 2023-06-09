﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LiteDB;

namespace Leikkipaikat
{
    public static class DB
    {
        public static List<Playground> GetPlaygrounds(string polku)
        {
            string path = @polku;
            //Haetaan tietokannasta lista kohteista. Polku saadaan käyttöliittymästä.
           
            try
            {
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    return col.FindAll().ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<Equipment> GetEquipment(Playground playground, string polku)
        {
            string path = @polku;
            string name = playground.Address;
            List<Equipment> equipment = new List<Equipment>();
            //Lähetetään tietokannasta tieto valitun kohteen välineistä
            try
            {
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));
                    Playground playground1 = (Playground)result;
                    if (playground1.Equipment == null) { playground1.Equipment = new List<Equipment>(); }

                    else
                    {
                        foreach (var item in playground1.Equipment)
                        {
                            equipment.Add(item);
                        }
                    }
                }
                return equipment;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static string AddPlayground(string address, string info, string polku)
        {
            string path = @polku;
            //Lisätään tietokantaan kohde. Tiedot saadaan käyttöliittymästä.
            //Tarkistetaan myös ettei saman nimistä kohdetta jo ole.
            //Palautetaan stringinä tieto miten meni
            try
            {

                var playground = new Playground { Address = address, Info = info };
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(address));
                    if (result == null)
                    {
                        col.Insert(playground);
                        return "Lisätty";
                    }

                    else { return "Osoite on jo tietokannassa"; }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool DeletePlayground(Playground playground, string polku)
        {
            string path = @polku;
            //Poistetaan tietokannasta käyttöliittymässä valittu kohde.
            try
            {
                int id = playground.Id;
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Id.Equals(id));

                    col.Delete(result.Id);
                };
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool UpdatePlayground(Playground playground, string address, string info, string polku)
        {
            string path = @polku;
            //Muokataan käyttöliittymässä valitun kohteen tietoja ja tallennetaan.
            try
            {
                int id = playground.Id;
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Id.Equals(id));
                    result.Address = address;
                    result.Info = info;
                    col.Update(result);//Tallennetaan tietokantaan

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string AddEquipment(Playground playground, Equipment equipment, string polku)
        {
            string path = @polku;
            //Lisätään valittuun kohteeseen tietokantaan väline. Palautetaan tieto onnistumisesta stringinä

            string name = playground.Address;
            string equipmentName = equipment.Name;
            string info = "";
            try
            {
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));
                    if (result.Equipment == null)//Jos ei välinelistaa vielä ole
                    {
                        result.Equipment = new List<Equipment>();
                    }//tarkistetaan ettei tule saman nimistä laitetta
                    var item = result.Equipment.SingleOrDefault(x => x.Name == equipmentName);
                    if (item != null)
                    {
                        info = "Saman niminen laite on jo olemassa";
                    }
                    else
                    {
                        result.Equipment.Add(equipment);//lisätään listalle
                        col.Update(result);//Tallennetaan tietokantaan
                        info = "Laite lisätty";
                    }
                    return info;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static bool DelEquipment(Playground playground, Equipment equipment, string polku)
        {
            //Poistetaan valitusta kohteesta väline tietokannasta.

            string path = @polku;
            string name = playground.Address;
            string equipmentName = equipment.Name;

            try
            {
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));
                    var item = result.Equipment.SingleOrDefault(x => x.Name == equipmentName);
                    if (item != null)
                        result.Equipment.Remove(item);

                    col.Update(result);

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string AddFault(Playground playground, Equipment equipment, Fault fault, string polku)
        {
            string path = @polku;
            //Lisätään tietyn kohteen tiettyyn välineeseen vika. Palautetaan stringinä tieto miten kävi.

            string name = playground.Address;
            string equipmentName = equipment.Name;
            string faultname = fault.FaultName;
            string info = "";

            try
            {
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));

                    foreach (var item in result.Equipment)
                    {
                        if (item.Name.Equals(equipmentName))

                        {
                            if (item.Faults == null) { item.Faults = new ObservableCollection<Fault>(); }
                            var z = item.Faults.SingleOrDefault(x => x.FaultName == fault.FaultName);
                            if (z != null)
                            {
                                info = "Saman niminen vika on jo olemassa";
                            }

                            else
                            {
                                item.Faults.Add(fault);
                                col.Update(result);
                                info = "Vika lisätty";
                            }
                        }

                    }
                    return info;
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public static bool DelFault(Playground playground, Equipment equipment, Fault fault, string polku)
        {
            string path = @polku;
            string faultName = fault.FaultName;
            string name = playground.Address;
            string equipmentName = equipment.Name;
            //Poistetaan tietyn kohteen tietystä välineestä vika
            try
            {
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));
                    var item = result.Equipment.SingleOrDefault(x => x.Name == equipmentName);
                    Fault f = item.Faults.SingleOrDefault(x => x.FaultName == faultName);

                    if (f != null)
                    {
                        item.Faults.Remove(f);
                    }
                    var copy = item;//Tein kopion koska en ollut varma päivittyykö muuten
                    result.Equipment.Remove(item);
                    result.Equipment.Add(copy);
                    col.Update(result);

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static ObservableCollection<Fault> GetFaults(Playground playground, Equipment equipment, string polku)
        {
            string path = @polku;
            string name = playground.Address;
            string equipmentName = equipment.Name;
            //Haetaan tietyn välineen viat, palautetaan käyttöliittymään listana

            try
            {
                using (var db = new LiteDatabase(path))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));
                    var item = result.Equipment.SingleOrDefault(x => x.Name == equipmentName);
                    if (item.Faults == null)
                    {
                        item.Faults = new ObservableCollection<Fault>();

                    }
                    return item.Faults;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
