using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public class AddBookRateCommandHandler : ICommandHandler<AddBookRateCommand>
    {
        private Database db { get; }

        public AddBookRateCommandHandler(Database db)
        {
            this.db = db;
        }

        public void Handle(AddBookRateCommand command)
        {
            Book des = db.Books.Where(x => x.Id == command.id).FirstOrDefault();

            db.BookRates.Add(new BookRate
            {
                RateType = RateType.BookRate,
                Book = des,
                FkBook = des.Id,
                Date = DateTime.Now,
                Value = (short)command.rate
            });

            db.SaveChanges();
        }
    }
}
