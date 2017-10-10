using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Happy
{
    class MomentItem
    {
        public  async static Task<List<MomentList>>  addItem()
        {
            
            SQLiteAsyncConnection con;
            con = new SQLiteAsyncConnection("moment.db");
            List<MomentList> momentList=new List<MomentList>();
           
             
                

                await con.CreateTableAsync<moment>();
                var query = con.Table<moment>().OrderByDescending(x =>x.itemID);
                var result = await query.ToListAsync();
           
                foreach (var moment in result)
                {
                    if (moment != null)
                    {
                        momentList.Add(new MomentList { momentText = moment.momentText, momentDate = moment.momentDate });        
                        
                    }
                }
            
          
            return momentList;
        }
    }
}
