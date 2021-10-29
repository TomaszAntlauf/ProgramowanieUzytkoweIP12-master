using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        
        //public List<BookRate> Rates { get; set; }
        public double AvarageRates { get; set; }
        public int RatesCount { get; set; }
        public List<AuthorDTO> Authors { get; set; }
    }
}
