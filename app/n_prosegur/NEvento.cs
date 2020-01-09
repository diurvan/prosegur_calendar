using d_prosegur;
using e_prosegur;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace n_prosegur
{
    public class NEvento
    {
        public List<EventModel> listaEventos(EventModel obj)
        {
            DEvento dal = new DEvento();
            return dal.listaEventos(obj);
        }
        public int Create(EventModel obj)
        {
            DEvento dal = new DEvento();
            int _ind = 0;
            try
            {
                using (TransactionScope trx = new TransactionScope())
                {
                    int response = (int)dal.Create(obj);
                    _ind = response;
                    if (response > 0)
                        trx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _ind;
        }
    }
}
