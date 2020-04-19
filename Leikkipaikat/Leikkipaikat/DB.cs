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
                    playgrounds = (List<Playground>)col;
                }
                return playgrounds;
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

        public static bool DeletePlayground(string playground)
        {
            //Poistetaan tietokannasta kohde.
            try
            {
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    
                    var col = db.GetCollection<Playground>("playgrounds");
                    var p = col.EnsureIndex(x => x.Address);
                    if(playground.Equals(p) == true)
                    col.Delete(p);
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
            try
            {

               
                //playground.Equipment.Add(equipment);
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    List<Playground> playgrounds = new List<Playground>();
                    playgrounds = (List<Playground>)col;
                    foreach (var item in playgrounds)
                    {
                        if (playground.Address.Equals(item.Address))
                            item.Equipment.Add(equipment);
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
        public static bool DelEquipment(Playground playground, Equipment equipment)
        {
            //Poistetaan valitusta kohteesta väline tietokannasta.
            try
            {
                //playground.Equipment.Remove(equipment);
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    var col = db.GetCollection<Playground>("playgrounds");
                    List<Playground> playgrounds = new List<Playground>();
                    playgrounds = (List<Playground>)col;
                    foreach (var item in playgrounds)
                    {
                        if (playground.Address.Equals(item.Address))
                            item.Equipment.Remove(equipment);
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
