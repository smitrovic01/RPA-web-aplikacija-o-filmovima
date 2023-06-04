namespace WebAppFilmovi.Models
{
    public interface IRepozitorijUpita
    {
        // za listu se može koristiti i List<>,IList<>, ali u praksi se 
        //IEnumerable<> pokazao najbržim 
        IEnumerable<Film> PopisFilm(); // R - read
        void Create(Film film); // C - insert tj create
        void Delete(Film film); // D - delete
        void Update(Film film); //U - update
        int SljedeciId();
        int KategorijaSljedeciId();
        Film DohvatiFilmSIdom(int id);

        IEnumerable<Kategorija> PopisKategorija();
        void Create(Kategorija kategorija);
        void Delete(Kategorija kategorija);
        void Update(Kategorija kategorija);

        Kategorija DohvatiKategorijuSIdom(int id);


    }
}
