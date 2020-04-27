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
        public static string AddPlayground(string address, string info)
        {
            //Lisätään tietokantaan kohde.
            try
            {
                
                var playground = new Playground{Address = address,Info = info};
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(address));
                    if (result == null) { 
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
        public static string AddEquipment(Playground playground, Equipment equipment)
        {
            //Lisätään valittuun kohteeseen tietokantaan väline.
            
                string name = playground.Address;
            string equipmentName = equipment.Name;
            string info = "";
            using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
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

        public static string AddFault(Playground playground, Equipment equipment, string fault)
        {
            //Lisätään tietyn kohteen tiettyyn välineeseen vika

            
                string name = playground.Address;
                string equipmentName = equipment.Name;
                string info = "";
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));

                foreach (var item in result.Equipment)
                {
                    if (item.Name.Equals(equipmentName)) 
                       
                    {
                        if (item.Faults == null) { item.Faults = new List<string>(); }
                        var z = item.Faults.SingleOrDefault(x => x == fault);
                        if (z != null)
                        {
                           info=  "Saman niminen vika on jo olemassa";
                        }
                        
                        else {
                            item.Faults.Add(fault);
                            col.Update(result);
                            info = "Vika lisätty";
                            
                        }
                    }
                
                }
                return info;



            }
                

               
            
        }
        public static bool DelFault(Playground playground, Equipment equipment, string fault)
        {
            //Poistetaan tietyn kohteen tietystä välineestä vika
            try
            {
                string name = playground.Address;
                string equipmentName = equipment.Name;

                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    var result = col.FindOne(x => x.Address.Equals(name));
                    var item = result.Equipment.SingleOrDefault(x => x.Name == equipmentName);
                    string f = item.Faults.SingleOrDefault(x => x == fault);
                   

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





    }
}
