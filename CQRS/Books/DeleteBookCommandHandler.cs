using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
    {
        private readonly Database db;

        public DeleteBookCommandHandler(Database db)
        {
            this.db = db;
        }

        public void Handle(DeleteBookCommand command)
        {
            Book del = db.Books.Where(x => x.Id == command.id).FirstOrDefault();

            db.Books.Remove(del);
            db.SaveChanges();
        }
    }
}
