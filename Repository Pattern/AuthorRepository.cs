using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTO;

namespace Repository_Pattern
{
    public class AuthorRepository
    {
        private Database Db { get; }

        public AuthorRepository(Database db)
        {
            Db = db;
        }

        public List<AuthorDTO> GetAuthors(PaginationDTO pagination)
        {
            return Db.Authors.Include(b => b.Rates)
            .Include(b => b.Books)
            .Skip(pagination.Page * pagination.Count)
            .Take(pagination.Count)
            .ToList().Select(b => new AuthorDTO
            {
                Id = b.Id,
                FirstName = b.FirstName,
                SecondName = b.SecondName,
                AvarageRates = (b.Rates.Count() > 0 ? b.Rates.Average(r => r.Value) : 0),
                RatesCount = (b.Rates.Count() > 0 ? b.Rates.Count() : 0),
                Books = b.Books.Select(a => new BookDTO
                {
                    Title = a.Title,
                    Id = a.Id,
                    ReleaseDate = a.ReleaseDate
                }).ToList()
            }).ToList();
        }

        public AuthorDTO PostAuthor(AuthorRequestDTO brq)
        {
            Author aut = new Author
            {
                FirstName = brq.FirstName,
                SecondName = brq.SecondName

            };
            aut.Books = Db.Books.Where(a => brq.BooksId.Contains(a.Id)).ToList();
            Db.Authors.Add(aut);
            Db.SaveChanges();

            return new AuthorDTO
            {
                Id = aut.Id,
                Books = aut.Books.Select(a => new BookDTO
                {
                    Id = a.Id,
                    Title = a.Title,
                    ReleaseDate = a.ReleaseDate
                }).ToList(),
                AvarageRates = 0,
                RatesCount = 0,
                FirstName = aut.FirstName,
                SecondName = aut.SecondName
            };

        }

        public bool DeleteDTO(int id)
        {
            Author del = Db.Authors.Include(x=>x.Books).Where(x => x.Id == id).FirstOrDefault();

            if (del.Books.Any())
            {
                return false;
            }

            Db.Authors.Remove(del);
            Db.SaveChanges();

            return true;

        }

        public void AddAuthorRate(int id, int rate)
        {
            Author des = Db.Authors.Where(x => x.Id == id).FirstOrDefault();

            Db.AuthorRates.Add(new AuthorRate
            {
                RateType = RateType.AuthorRate,
                Author = des,
                FkAuthor = des.Id,
                Date = DateTime.Now,
                Value = (short)rate
            });

            Db.SaveChanges();

        }
    }
}
