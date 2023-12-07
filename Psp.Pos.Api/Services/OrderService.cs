using Psp.Pos.Api.Models;

namespace Psp.Pos.Api.Services
{
    public class OrderService
    {

        Cheque Process(Cheque order)
        {
            if (order.isNew())
            {
                order.addToDb();
                Guid id;
            }
            else
            {
                order.modifyExisting();
            }

            return order();

        }

        public void addToDb()
        {
        }


        public void modifyExisting(Order order)
        {
            ...
                order.updateDb();
        }

        public bool isNew()
        {
        }

    }
}
