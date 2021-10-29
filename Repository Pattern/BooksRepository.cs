using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTO;

namespace Repository_Pattern
{
    public class BooksRepository
    {
        private Database Db { get; }
        public BooksRepository(Database db)
        {
            Db = db;
            //db.Books.Add(new Book
            //{
            //    Title = "Miecz przeznaczenia",
            //    Authors = new List<Author> {
            //            new Author
            //            {
            //                FirstName ="Andrzej", SecondName="Sapkowski"
            //            },
            //        new Author
            //        {
            //        FirstName="CDProject", SecondName="Red"
            //        }
            //        },
            //    ReleaseDate = new System.DateTime(2017, 2, 10),
            //    Rates = new List<BookRate>
            //        {
            //            new BookRate{Date=new System.DateTime(2017,2,10), Value=5},
            //            new BookRate{Date=new System.DateTime(2017,4,10), Value=4},
            //            new BookRate{Date=new System.DateTime(2017,2,11), Value=3}
            //        }

            //});
            db.SaveChanges();
        }
        public List<BookDTO> GetBooks(PaginationDTO pagination)
        {
            return Db.Books.Include(b => b.Rates)
            .Include(b => b.Authors)
            .Skip(pagination.Page * pagination.Count)
            .Take(pagination.Count)
            .ToList().Select(b => new BookDTO
            {
                Id = b.Id,
                ReleaseDate = b.ReleaseDate,
                AvarageRates = b.Rates.Average(r => r.Value),
                RatesCount = b.Rates.Count(),
                Title = b.Title,
                Authors = b.Authors.Select(a => new AuthorDTO
                {
                    FirstName = a.FirstName,
                    Id = a.Id,
                    SecondName = a.SecondName
                }).ToList()
            }).ToList();
        }

        public BookDTO GetBookbyId(int Idx)
        {
            return Db.Books.Include(b => b.Rates)
            .Include(b => b.Authors)
            .ToList().Select(b => new BookDTO
            {
                Id = b.Id,
                ReleaseDate = b.ReleaseDate,
                AvarageRates = b.Rates.Average(r => r.Value),
                RatesCount = (b.Rates != null ? b.Rates.Count() : 0),
                Title = b.Title,
                Authors = b.Authors.Select(a => new AuthorDTO
                {
                    FirstName = a.FirstName,
                    Id = a.Id,
                    SecondName = a.SecondName
                }).ToList()
            }).Where(b => b.Id == Idx).FirstOrDefault();

        }

        public BookDTO PostBook(BookRequestDTO brq)
        {
            Book book = new Book
            {
                Title = brq.Title,
                ReleaseDate = brq.ReleaseDate

            };
            book.Authors = Db.Authors.Where(a => brq.AuthorsId.Contains(a.Id)).ToList();
            Db.Books.Add(book);
            Db.SaveChanges();

            return new BookDTO
            {
                Id = book.Id,
                Authors = book.Authors.Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    SecondName = a.SecondName
                }).ToList(),
                AvarageRates = 0,
                RatesCount = 0,
                ReleaseDate = book.ReleaseDate,
                Title = book.Title
            };

        }

        public bool DeleteDTO(int id)
        {
            Book del = Db.Books.Where(x => x.Id == id).FirstOrDefault();

            Db.Books.Remove(del);

            return true;
        }

        public void AddBookRate(int id, int rate)
        {
            Book des = Db.Books.Where(x => x.Id == id).FirstOrDefault();

            Db.BookRates.Add(new BookRate
            {
                RateType = RateType.BookRate,
                Book = des,
                FkBook = des.Id,
                Date = DateTime.Now,
                Value = (short)rate
            });

            Db.SaveChanges();

        }


    }
}
