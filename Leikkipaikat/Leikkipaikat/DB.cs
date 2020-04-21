using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LiteDB;

namespace Leikkipaikat
{
    public static class DB
    {
        public static List<Playground> GetPlaygrounds()
        {
            //Haetaan tietokannasta lista kohteista.
            List<Playground> playgrounds = new List<Playground>();
            try
            {
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
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
        public static List<Equipment> GetEquipment(Playground playground)
        {
            
                string name = playground.Address;
                List<Equipment> equipment = new List<Equipment>();

            try
            {
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
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
        public static bool AddPlayground(string address, string info)
        {
            //Lisätään tietokantaan kohde.
            try
            {

                var playground = new Playground{Address = address,Info = info};
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    col.Insert(playground);
                }


                return true;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool DeletePlayground(Playground playground)
        {
            //Poistetaan tietokannasta kohde.
            try
            {
                int id = playground.Id;
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
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
        public static bool UpdatePlayground(Playground playground, string address, string info)
        {
            //Muokataan kohteen tietoja ja tallennetaan.
            try
            {
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    List<Playground> playgrounds = new List<Playground>();
                    playgrounds = (List<Playground>)col;
                    foreach (var item in playgrounds)
                    {
                        if (playground.Address.Equals(item.Address))
                           item.Address = address;
                        item.Info = info;
                        col.Update(item);

                    }
                    
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddEquipment(Playground playground, Equipment equipment)
        {
            //Lisätään valittuun kohteeseen tietokantaan väline.
            
                string name = playground.Address;

                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));
                    if (result.Equipment == null) { result.Equipment = new List<Equipment>(); }
                    if(result == null)
                    {
                        throw new Exception("Error");
                    }
                    else {
                    //Playground playground1 = (Playground)result;
                    result.Equipment.Add(equipment);
                    col.Update(result);
                    }

                }
                return true;
            
        }
        public static bool DelEquipment(Playground playground, Equipment equipment)
        {
            //Poistetaan valitusta kohteesta väline tietokannasta.
            
            
                string name = playground.Address;
                string equipmentName = equipment.Name;
                
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
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

        public static bool AddFault(Playground playground, Equipment equipment, string fault)
        {
            //Lisätään tietyn kohteen tiettyyn välineeseen vika
            try
            {
                //equipment.Faults.Add(fault);
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    List<Playground> playgrounds = new List<Playground>();
                    playgrounds = (List<Playground>)col;
                    foreach (var item in playgrounds)
                    {
                        if (playground.Address.Equals(item.Address))
                            
                            foreach (var item2 in item.Equipment)
                            {
                                if (item2.Name.Equals(item2.Name))
                                    item2.Faults.Add(fault);
                            }
                        col.Update(item);

                    }

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool DelFault(Playground playground, Equipment equipment, string fault)
        {
            //Poistetaan tietyn kohteen tietystä välineestä vika
            try
            {
                //equipment.Faults.Remove(fault);
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    List<Playground> playgrounds = new List<Playground>();
                    playgrounds = (List<Playground>)col;
                    foreach (var item in playgrounds)
                    {
                        if (playground.Address.Equals(item.Address))

                            foreach (var item2 in item.Equipment)
                            {
                                if (item2.Name.Equals(item2.Name))
                                    item2.Faults.Remove(fault);
                            }
                        col.Update(item);

                    }

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}
