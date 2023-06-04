using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppFilmovi.Models;

namespace WebAppFilmovi.Controllers
{
    public class FilmController : Controller
    {
        private readonly IRepozitorijUpita _repozitorijUpita;
        public FilmController(IRepozitorijUpita repozitorijUpita)
        {
            _repozitorijUpita = repozitorijUpita;
        }

        public IActionResult Index()
        {
            return View(_repozitorijUpita.PopisFilm());
        }

        public IActionResult Create() 
        {
            ViewData["KategorijaId"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Id", "Naziv");
            int sljedeciId = _repozitorijUpita.SljedeciId();
            Film film = new Film() { Id = sljedeciId};
            return View(film);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Naziv,DatumIzlaska,Cijena,SlikaUrl,KategorijaId")]Film film) 
        {
            ModelState.Remove("Kategorija");//uklanjanje veze

            if (ModelState.IsValid) 
            { 
                _repozitorijUpita.Create(film);
                return RedirectToAction("Index"); // ako je sve ok, tu završava metoda 
            }
            //ako je doslo do greške sljedeci dio se izvrsava
            ViewData["KategorijaId"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Id", "Naziv", film.KategorijaId);
            return View(film);

        }

        [HttpGet]
        public IActionResult Update(int id) 
        { 
            if (id < 1) 
            {
                return NotFound();
            }

            Film film = _repozitorijUpita.DohvatiFilmSIdom(id);

            if(film == null) { return NotFound(); }

            ViewData["KategorijaId"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Id", "Naziv", film.KategorijaId);
            return View(film);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, [Bind("Id,Naziv,DatumIzlaska,Cijena,SlikaUrl,KategorijaId")] Film film) 
        { 
            if(id != film.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Kategorija");

            if (ModelState.IsValid) 
            { 
                _repozitorijUpita.Update(film);
                return RedirectToAction("Index");
            }
            //ako je doslo do greške sljedeci dio se izvrsava
            ViewData["KategorijaId"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Id", "Naziv", film.KategorijaId);
            return View(film);

        }
        [HttpGet]
        public IActionResult Delete(int? id) 
        { 
            if(id < 1) 
            {
                return NotFound();
            }

            var film = _repozitorijUpita.DohvatiFilmSIdom(Convert.ToInt16(id));

            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) 
        {
            var film = _repozitorijUpita.DohvatiFilmSIdom(id);
            _repozitorijUpita.Delete(film);
            return RedirectToAction("Index");
        
        }

        //Trazilica
        public ActionResult SearchIndex(string filmZanr, string searchString)
        {
            var zanr = new List<string>();

            var zanrUpit = _repozitorijUpita.PopisKategorija();
            
            ViewData["filmZanr"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Naziv", "Naziv", zanrUpit);

            var filmovi = _repozitorijUpita.PopisFilm();

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                filmovi = filmovi.Where(s => s.Naziv.Contains(searchString, StringComparison.OrdinalIgnoreCase)); // StringComparison.OrdinalIgnoreCase ignorira velika-mala slova 
            }

            if (string.IsNullOrWhiteSpace(filmZanr))
                return View(filmovi);
            else
            {
                return View(filmovi.Where(x => x.Kategorija.Naziv == filmZanr));
            }

        }


    }
}
