using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Happy
{
    [Table("moments")]
    class moment
    {

        [PrimaryKey, AutoIncrement] 
        public int itemID { get; set; }
        public string momentText { get; set; }
        public string momentDate { get; set; }
    }

    class model
    {
        
    }
}
