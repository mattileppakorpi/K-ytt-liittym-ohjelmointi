using System;
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
            //Haetaan tietokannasta lista kohteista.
            //List<Playground> playgrounds = new List<Playground>();
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
            //Lisätään tietokantaan kohde.
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
            //Poistetaan tietokannasta kohde.
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
            //Muokataan kohteen tietoja ja tallennetaan.
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
            //Lisätään valittuun kohteeseen tietokantaan väline.

            string name = playground.Address;
            string equipmentName = equipment.Name;
            string info = "";
            using (var db = new LiteDatabase(path))
            {
                var col = db.GetCollection<Playground>("playgrounds");
                var result = col.FindOne(x => x.Address.Equals(name));
                if (result.Equipment == null)//Jos ei välineitä vielä ole
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
        public static bool DelEquipment(Playground playground, Equipment equipment, string polku)
        {
            //Poistetaan valitusta kohteesta väline tietokannasta.

            string path = @polku;
            string name = playground.Address;
            string equipmentName = equipment.Name;

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

        public static string AddFault(Playground playground, Equipment equipment, Fault fault, string polku)
        {
            string path = @polku;
            //Lisätään tietyn kohteen tiettyyn välineeseen vika


            string name = playground.Address;
            string equipmentName = equipment.Name;
            string faultname = fault.FaultName;
            string info = "";
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
        public static bool DelFault(Playground playground, Equipment equipment, Fault fault, string polku)
        {
            string path = @polku;
            string faultName = fault.FaultName;
            //Poistetaan tietyn kohteen tietystä välineestä vika
            try
            {
                string name = playground.Address;
                string equipmentName = equipment.Name;

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
                    var copy = item;
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

            using (var db = new LiteDatabase(path))
            {
                var col = db.GetCollection<Playground>("playgrounds");
                var result = col.FindOne(x => x.Address.Equals(name));
                var item = result.Equipment.SingleOrDefault(x => x.Name == equipmentName);


            }





    }
}
