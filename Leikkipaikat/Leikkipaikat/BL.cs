using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Leikkipaikat
{
    public class Playground
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
        public List<Equipment> Equipment { get; set; }
        
    }
    
    public class Equipment
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public List<string> Faults { get; set; }

    }

}
