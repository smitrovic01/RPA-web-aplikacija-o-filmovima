using Microsoft.EntityFrameworkCore;

namespace WebAppFilmovi.Models
{
    public class RepozitorijUpita : IRepozitorijUpita
    {
        private readonly AppDbContext _appDbContext;
        public RepozitorijUpita(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public void Create(Film film)
        {
            _appDbContext.Add(film);
            _appDbContext.SaveChanges();
        }

        public void Create(Kategorija kategorija)
        {
            _appDbContext.Add(kategorija);
            _appDbContext.SaveChanges();
        }

        public void Delete(Film film)
        {
            _appDbContext.Film.Remove(film);
            _appDbContext.SaveChanges();
        }

        public void Delete(Kategorija kategorija)
        {
            _appDbContext.Kategorija.Remove(kategorija);
            _appDbContext.SaveChanges();
        }

        public Film DohvatiFilmSIdom(int id)
        {
            return _appDbContext.Film
                .Include(k => k.Kategorija)
                .FirstOrDefault(f => f.Id == id);
        }

        public Kategorija DohvatiKategorijuSIdom(int id)
        {
            return _appDbContext.Kategorija.Find(id);
        }

        public int KategorijaSljedeciId()
        {
            int zadnjiId = _appDbContext.Kategorija
               .Count();

            int sljedeciId = zadnjiId + 1;
            return sljedeciId;
        }

        public IEnumerable<Film> PopisFilm()
        {
            
            return _appDbContext.Film.Include(k => k.Kategorija);
        }

        public IEnumerable<Kategorija> PopisKategorija()
        {
            return _appDbContext.Kategorija;
        }

        public int SljedeciId()
        {
            int zadnjiId = _appDbContext.Film
                .Include(k => k.Kategorija)
                .Max(x => x.Id);

            int sljedeciId = zadnjiId + 1;
            return sljedeciId;
        }

        public void Update(Film film)
        {
           _appDbContext.Film.Update(film);
            _appDbContext.SaveChanges();
        }

        public void Update(Kategorija kategorija)
        {
            _appDbContext.Kategorija.Update(kategorija);
            _appDbContext.SaveChanges();
        }
    }
}
