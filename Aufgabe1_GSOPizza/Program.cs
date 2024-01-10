using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aufgabe1_GSOPizza.Daten;
using Aufgabe1_GSOPizza;

GSOPizzaContext dbContext = new GSOPizzaContext();

Kunde kunde_1 = new Kunde()
{
    Vorname = "John",
    Nachname = "Doe",
    Tel = "1234567890",
    Email = "johndoe@email.com",
    Adresse = "123 Main St"
};

dbContext.Kunden.Add(kunde_1);
await dbContext.SaveChangesAsync();


await AlleBestellungenAnzeigen(dbContext);
//await AlleProdukteAnzeigen(dbContext);
//await AlleKundenAnzeigen(dbContext);
//await AlleBestellungDetailsAnzeigen(dbContext);





static async Task AlleBestellungenAnzeigen(GSOPizzaContext dbContext)
{
    var alleBestellungen = await dbContext.Bestellungen
                                          .Include(b => b.Kunde) // Includiert die Kundendaten
                                          .ToListAsync();
    Console.WriteLine("-----------------------------------------");
    Console.WriteLine("Alle Bestellungen:");
    foreach (var bestellung in alleBestellungen)
    {
        Console.WriteLine($"ID: {bestellung.Id}, Bestelldatum: {bestellung.Bestellt}, KundeID: {bestellung.KundeId}, KundenName: {bestellung.Kunde.Vorname} {bestellung.Kunde.Nachname}");
    }
}
