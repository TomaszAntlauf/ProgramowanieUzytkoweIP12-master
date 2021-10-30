using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Authors
{
    public class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand>
    {
        private Database db { get; }

        public AddAuthorCommandHandler(Database db)
        {
            this.db = db;
        }

        public void Handle(AddAuthorCommand command)
        {
            Author aut = new Author
            {
                FirstName = command.FirstName,
                SecondName = command.SecondName

            };
            aut.Books = db.Books.Where(a => command.BooksIDs.Contains(a.Id)).ToList();
            db.Authors.Add(aut);
            db.SaveChanges();
        }
    }
}
