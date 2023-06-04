using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAppFilmovi.Models
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {

        public AppDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Film> Film { get; set; }
        public DbSet<Kategorija> Kategorija { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Film>().Property(f => f.Cijena).HasPrecision(10,2);

            builder.Entity<Kategorija>().HasData(
                new Kategorija() { Id=1, Naziv="Drama"},
                new Kategorija() { Id=2, Naziv="Akcijski"},
                new Kategorija() { Id=3, Naziv="Pustolovni"},
                new Kategorija() { Id=4, Naziv="SF"},
                new Kategorija() { Id = 5, Naziv = "Komedija" }
                );


            builder.Entity<Film>().HasData(
               new Film() { Id = 1, Naziv = "The Shawshank Redemption", Cijena = 3.99m, DatumIzlaska = DateTime.Parse("1994-10-14"), SlikaUrl = "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg", KategorijaId = 1 },
                new Film() { Id = 2, Naziv = "The Godfather", Cijena = 5.99m, DatumIzlaska = DateTime.Parse("1972-03-24"), SlikaUrl = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg", KategorijaId = 1 },
                new Film() { Id = 3, Naziv = "The Godfather: Part II", Cijena = 4.99m, DatumIzlaska = DateTime.Parse("1974-12-18"), SlikaUrl = "https://m.media-amazon.com/images/M/MV5BMWMwMGQzZTItY2JlNC00OWZiLWIyMDctNDk2ZDQ2YjRjMWQ0XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg", KategorijaId = 1 },
                new Film() { Id = 4, Naziv = "The Dark Knight", Cijena = 7.99m, DatumIzlaska = DateTime.Parse("2008-07-18"), SlikaUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg", KategorijaId = 2 },
                new Film() { Id = 5, Naziv = " 12 Angry Men", Cijena = 2.50m, DatumIzlaska = DateTime.Parse("1957-04-10"), SlikaUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/12_Angry_Men_%281957_film_poster%29.jpg", KategorijaId = 2 }


                );




        }











    }
}
