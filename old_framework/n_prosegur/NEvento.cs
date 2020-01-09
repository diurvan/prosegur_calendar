using System;
using System.Transactions;
using e_prosegur;
using d_prosegur;
using System.Collections.Generic;

namespace n_prosegu
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
