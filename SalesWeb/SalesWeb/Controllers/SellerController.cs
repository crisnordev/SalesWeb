using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;

namespace SalesWeb.Controllers;

[Controller]
public class SellerController : Controller
{
    public async  Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        
        List<Seller> sellers = await context.Sellers.AsNoTracking().ToListAsync();
        return  View(sellers);
    }
    
    
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, string? id)
    {
        if (id == null)
            return NotFound();

        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (seller == null)
            return NotFound();

        return View(seller);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromServices] SalesWebDbContext context, [Bind("Id,Name,Email,Password,BirthDate")] Seller seller)
    {
        if (ModelState.IsValid)
        {
            try
            {
                seller.Id = Guid.NewGuid();
                context.Add(seller);
                await context.SaveChangesAsync();
                
                View(seller);
                
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException)
            {
                return StatusCode(400, "E-mail já cadastrado.");
            }
        }

        return View();
    }


    public async Task<IActionResult> Update([FromServices] SalesWebDbContext context, string? id)
    {
        if (id == null)
            return NotFound();
            
        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == id);

        if (seller == null)
            return NotFound();

        return View(seller);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromServices] SalesWebDbContext context, string? id, [Bind("Id,Name,Email,Password,BirthDate")] Seller seller)
    {
        if (id != seller.Id.ToString())
            return NotFound();


        if (ModelState.IsValid)
        {
            try
            {
                context.Update(seller);
                await context.SaveChangesAsync();
                Ok(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Não foi possível alterar o cadastro do vendedor.");
            }
            catch (Exception)
            {
                return StatusCode(500, "falha interna do servidor.");
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, string id)
    {
        if (id == null)
            return NotFound();

        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == id);

        if (seller == null)
            return NotFound();

        return View(seller);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, string? id)
    {
        
        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == id);

        if (ModelState.IsValid)
        {
            try
            {
                context.Sellers.Remove(seller!);
                await context.SaveChangesAsync();
                Ok(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Não foi possível excluir Vendedor.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Falha interna do servidor.");
            }
        }

        return RedirectToAction(nameof(Index));
    }

}