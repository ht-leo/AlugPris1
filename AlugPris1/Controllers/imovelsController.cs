using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlugPris1.Models;
using PagedList;

namespace AlugPris1.Controllers
{
    public class imovelsController : Controller


    {
        private contexto db = new contexto();

        // GET: imovels
        public ActionResult Index(string ordenacao, string stringProcurar, string currentFilter, int? pagina)
        {
            ViewBag.CurrentSort = ordenacao;
            ViewBag.TipoSortParm = String.IsNullOrEmpty(ordenacao) ? "tipo_desc" : "";
            ViewBag.ValorSortParm = ordenacao == "Valor" ? "valor_desc" : "Valor";

            if (stringProcurar != null)
            {
                pagina = 1;
            }
            else
            {
                stringProcurar = currentFilter;
            }

            ViewBag.CurrentFilter = stringProcurar;

            var imovel = from s in db.Imovel
                           select s;
            if (!String.IsNullOrEmpty(stringProcurar))
            {
                imovel = imovel.Where(s => s.Tipo.Contains(stringProcurar));
                                      
            }
            switch (ordenacao)
            {

                case "valor_desc":
                    imovel = imovel.OrderByDescending(s => s.Valor).ThenByDescending(s => s.Valor);
                    break;
                case "Valor":
                    imovel = imovel.OrderBy(s => s.Valor).ThenBy(s => s.Valor);
                    break;
                
                default:
                    imovel = imovel.OrderBy(s => s.Valor);
                    break;
            }
            int paginaSize = 10;
            int paginaNumber = (pagina ?? 1);
            return View( imovel.ToPagedList(paginaNumber, paginaSize));
        }

        // GET: imovels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imovel imovel = await db.Imovel.FindAsync(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            return View(imovel);
        }

        // GET: imovels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: imovels/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Codigo,Tipo,Valor,Detalhes,Anunciante,Telefone")] imovel imovel)
        {
            if (ModelState.IsValid)
            {
                db.Imovel.Add(imovel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(imovel);
        }

        // GET: imovels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imovel imovel = await db.Imovel.FindAsync(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            return View(imovel);
        }

        // POST: imovels/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Codigo,Tipo,Valor,Detalhes,Anunciante,Telefone")] imovel imovel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imovel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(imovel);
        }

        // GET: imovels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imovel imovel = await db.Imovel.FindAsync(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            return View(imovel);
        }

        // POST: imovels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            imovel imovel = await db.Imovel.FindAsync(id);
            db.Imovel.Remove(imovel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
