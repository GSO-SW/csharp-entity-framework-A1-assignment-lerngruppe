<div id="container" style="white-space:nowrap">

  <div id="image" style="display:inline float: right;">
        <img style="float: left;" src="https://github.com/GSO-SW/public_content_gso/blob/e52e340b1e9dbc5c8f01051c4f264da611f1fd7f/Images/Logos/C%23_logo.png" alt="drawing" width="8%"/>
  </div>

  <div id="texts" style="display:inline; white-space:nowrap; float: right;"> 
        <h1>🍕GSO Pizza</h1>
</div>

Entwickeln Sie eine Anwendung für 'GSO Pizza', eine virtuelle Pizzeria, die darauf ausgerichtet ist, Produkte und Kundenbestellungen zu verwalten. Das folgende UML-Klassendiagramm zeigt die zugehörigen Entitäten und deren Anordnung.

**GSO Pizza UML**   
<img src="./Referenzen/GSOPizzaUML.png" width=50% >

### Beispiel Datensatz

**Kunde Tabelle**   
| Id  | Vorname  | Nachname  | Tel | Email                | Adresse        |
|-----|------------|-----------|-------------|----------------------|----------------|
| 1   | John       | Doe       | 1234567890  | johndoe@email.com    | 123 Main St    |
| 2   | Jane       | Smith     | 0987654321  | janesmith@email.com  | 456 Elm St     |

**Bestellung Tabelle**   
| Id  | Bestellt       | KundenId |
|-----|-------------------|------------|
| 101 | 2023-01-01 10:00  | 1          |
| 102 | 2023-01-02 11:00  | 1          |
| 103 | 2023-01-03 09:30  | 2          |

**BestellungDetails Tabelle**   
| Id  | BestellungId | ProduktId | Anzahl |
|-----|---------|-----------|----------|
| 201 | 101     | 501       | 2        |
| 202 | 101     | 502       | 1        |
| 203 | 102     | 501       | 3        |
| 204 | 103     | 503       | 1        |

**Produkt Tabelle**   
| Id  | Name           | Preis |
|-----|----------------|-------|
| 501 | VeggiSpecial         | 15.50  |
| 502 | CheesyHeaven     | 14.00   |
| 503 | MeatyMight | 18.90    |


## :bookmark_tabs: Informationsquelle
Das Informationsmaterial zur Aufgabe finden Sie hier:   
[:dart: Entity Framework](./Referenzen/EntityFramwork.md)   
[:book: GSO-Wiki](https://github.com/GSO-SW/public_content_gso/wiki/Entitiy-Framework)

  
---
## 1. Installieren des Entity Framework :computer:
###  Installieren über das .NET command line interface (CLI)

#### 1. Öffnen Sie das Terminal in Ihrem Respository verzeichnis

Sie können Pakete auf Projekte Begrenz installieren indem das Verzeichnis zum Projekt Navigieren.   
```PS ...\csharp-entity-framework-A1-template-lehrkraft\Aufgabe1_GSOPizza>```   

#### 2. Installieren Sie die EF-Pakete

Installieren Sie der Reihe nach folgende Pakete indem Sie den Befehl in das Terminal eingeben:

**Entity Framework Core**
```
dotnet add package Microsoft.EntityFrameworkCore --version 6.0.0
```
**Entity Framework Core SQLite**
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0.0
```
**Entity Framework Core Design**
```
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0
```
**Entity Framework Core Tools**
```
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.0
```

**Entity Framework**
```
dotnet tool install --global dotnet-ef --version 6.0.0
```

###  Installieren über das Visual Studio graphical user interface (GUI)
#### 1. Öffnen Sie den NuGet-Paket Installer
Navigieren Sie im Projektmappenexplorer zum Entsprechenden Projekt und öffnen Sie den NuGet-Packet Installer mit einem Rechtsklick auf die Abhängigkeiten.   
<img src="./Referenzen/ef_installieren_1.png" width=50% >

#### 2. Installieren Sie die benötigten Pakete
Durchsuchen Sie den Installer nach den aufgelisteten Paketen. Installieren Sie jeweils die **Version 6.0.0**.
Folgende Pakete müssen installiert werden:   

- EntityFrameworkCore
- EntityFrameworkCore.Sqlite
- EntityFrameworkCore.Design
- EntityFrameworkCore.Tools
  
<img src="./Referenzen/ef_installieren_2.png" width=50% >    
   
   
#### 3. Installierte Pakete bestätigen
   
Folgendes sollten Sie nun im Projektmappenexplorer sehen:       
   
<img src="./Referenzen/ef_installieren_3.png" width=50% >    

---
## 2. Erstellen Sie die entsprechenden Klassen :memo:
Erstellen Sie die entsprechenden Klassen aus dem UML-Klassendiagramm. Erstellen Sie dafür eine neue Datei mit dem entsprechenden Namen(Bestellung.cs, Produkt.cs,...). Die Umsetzung wird an der Klasse **Bestellung** beispielhaft gezeigt:

```csharp

//Bestellung.cs

namespace Aufgabe1_GSOPizza
{
    internal class Bestellung
    {
        public int Id { get; set; }
        public DateTime Bestellt { get; set; }
        public int KundeId { get; set; }
        public Kunde Kunde { get; set; } = null!;
        public List<BestellungDetail> BestellungDetail { get; set; } = null!;
    }
}


```

---

## 3. Datenbank Kontext erstellen 🚧
Um die Datenbank mit dem Csharp-Code zu verbinden, müssen Sie eine Context-Datei erstellen. Erstellen Sie hierfür einen Ordner mit dem Namen **'Daten'** im Projekt GSOPizza. Erstellen Sie in diesem Ordner die Datei **'GSOPizzaContext.cs'**. Folgende abbildung zeigt den Resultierenden Pfad: 

```
Aufgabe1_GSOPizza
|
|-- Daten
|   |-- GSOPizzaContext.cs
|
|-- Kunde.cs
|-- Bestellung.cs
|-- BestellungDetail.cs
|-- Produkt.cs
|-- Program.cs
```

In dieser Datei wird nun der Kontext zur Datenbank hergestellt.   
**Vergessen Sie nicht den Pfad zu ihrer Datenbank im folgenden Code zu ändern**:

```csharp

//GSOPizzaContext.cs

using Microsoft.EntityFrameworkCore;

namespace Aufgabe1_GSOPizza.Daten
{
    // GSOPizzaContext Klasse erbt von DbContext, einem Teil des Entity Frameworks
    internal class GSOPizzaContext : DbContext
    {
        // DbSet-Eigenschaften repräsentieren Tabellen in der Datenbank
        // Jede Eigenschaft ist stark typisiert mit einer Modellklasse
        public DbSet<Kunde> Kunden { get; set; } = null!;
        public DbSet<Bestellung> Bestellungen { get; set; } = null!;
        public DbSet<Produkt> Produkte { get; set; } = null!;
        public DbSet<BestellungDetail> BestellungDetails { get; set; } = null!;

        // OnModelCreating-Methode wird verwendet, um das Modell und die Beziehungen mittels Fluent API zu konfigurieren
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definieren von Primärschlüsseln für jede Entität
            modelBuilder.Entity<Kunde>().HasKey(k => k.Id);
            modelBuilder.Entity<Bestellung>().HasKey(b => b.Id);
            modelBuilder.Entity<Produkt>().HasKey(p => p.Id);
            modelBuilder.Entity<BestellungDetail>().HasKey(bd => bd.Id);

            // Immer die Basis-Methode aufrufen, um das Basisverhalten einzuschließen
            base.OnModelCreating(modelBuilder);
        }

        // OnConfiguring-Methode wird verwendet, um die Datenbankverbindung und andere Konfigurationen einzustellen
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Überprüfen, ob der optionsBuilder bereits konfiguriert ist, wenn nicht, konfigurieren
            if (!optionsBuilder.IsConfigured)
            {
                // Konfigurieren der Verwendung der SQLite-Datenbank mit dem angegebenen Verbindungsstring
                optionsBuilder.UseSqlite(@"Data Source=C:\Users\kande\source\repos\csharp-entity-framework-A1-template-lehrkraft\Aufgabe1_GSOPizza\gso_pizza.db");
            }

            // Immer die Basis-Methode aufrufen, um das Basisverhalten einzuschließen
            base.OnConfiguring(optionsBuilder);
        }
    }
}
```

---

## 4. Migration erstellen und migrieren 💾  
Die Konfigurationen zur Datenbank würden nun erfolgreich abgeschlossen. Damit das Entity-Framework die Datenbank erstellen kann, müssen die Konfigurationen in eine art Umsetzungsplan (Migration) umgewandelt werden. Diese wird wie folgt umgesetzt: 

### Migration

**Migration über dotnet CLI**   
Wenn Sie das Entity Framework Paket global installiert haben, steht Ihnen der Befehl **'ef'** in dotnet zur verfügung. Geben Sie folgenden Befehl in die Powershell ein um eine Migration zu erstellen:
```
dotnet ef migrations add InitialCreate
```


**Migration über Paket-Manager-Konsole**   

Navigieren sie über die Visual Studio Toolleiste   

> **Extras/Nuget-Pakte-Manager/Paket-Manager-Konsole**   


<img src="./Referenzen/migration.png" width=50% >    

Geben Sie in Konsole folgenden Befehl ein:

```
PM> Add-Migration InitialCreate
```


### Migrieren
Um die erstellt Migration in eine Konkrete Datenbank umzuwandeln, muss die Migration angewendet werde. wieder stehen zwei verschiedene Umsetzungsmöglichkeiten zu Auswahl.

**Migrieren über dotnet CLI**   
```
dotnet ef database update
```

**Migration über Paket-Manager-Konsole**   
Geben Sie in Konsole folgenden Befehl ein:

```
PM> Update-Database
```

---

## 5. Daten Speichern und Auslesen :minidisc:
Nun wurde eine Datenbank in Ihrem Projekt erstellt und Sie sind startklar 😄.

**Aufgabe 1:** Gehen Sie nun in die Datei **Program.cs** und lesen Sie alle Daten aus der Beispieltabelle zu Beginn des Assignments ein. 
> ❗Hinweis: Nutzen Sie die Informationen us dem [GSO-Wiki](https://github.com/GSO-SW/public_content_gso/wiki/Entitiy-Framework)

**Aufgabe 2:** Erstellen Sie die Fehlenden Methoden zum anzeigen aller Tabellen der Datenbank nach der vorlage von **AlleKundenAnzeigen**.    
```csharp
await AlleBestellungenAnzeigen(dbContext);
await AlleProdukteAnzeigen(dbContext);
await AlleKundenAnzeigen(dbContext);
await AlleBestellungDetailsAnzeigen(dbContext);
```

Weche Bedeutung der Aufruf der Methode mit **await** und die definition des Methode als **async Task** hat können Sie im GSO-Wiki unter [Methoden](https://github.com/GSO-SW/public_content_gso/wiki/Methoden#asynchrone-methoden) nachlesen.   

```csharp
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
```
---

## 6. Menü und voller Funktionsumfang für die GSO-Pizza :memo:

Erstellen Sie nun die GSO Pizza App. Hierfür soll eine neue Datei mir dem Namen **GSO_Pizza.cs** erstellt werden. In dieser Datei wird die App als objektorientiertes Programm umgesetzt. Folgendes UML-Klassendiagramm soll Ihnen bei der Strukturierung der App helfen.

<img src="./Referenzen/UMLGSOPizzaApp.png" width=50% >

Das Menü soll folgende Punkte und Unterpunkte enthalten:

```
Kunden verwalten
|--Kunden anlegen
|--Kunden löschen
|--Kunden bearbeiten
|--Kunden anzeigen

Produkt verwalten
|--Produkt anlegen
|--Produkt löschen
|--Produkt bearbeiten
|--Produkt anzeigen

Bestellung verwalten
|--Bestellung anlegen
|--Bestellung löschen
|--Bestellung bearbeiten
|--Bestellung anzeigen

```

Setzen Sie die App GSO-Pizza mitt all ihren Funktionen um. 

---
  
### Aufgabe 3: Arbeit einreichen

1. In Visual Studio 2022 das Fenster "Git-Änderungen" aufrufen
2. Eine kurze Beschreibung Ihrer Änderungen in die Textbox eingeben und "commit für alle" klicken
3. Mit dem Pfeil nach oben die Arbeit auf GitHub pushen.
4. Das Repository im Brower aufrufen und aktualisieren um die Änderungen zu bestätigen.
5. Im Pull-Request die Nachricht "Bereit zum Bewerten" hinterlassen, damit Ihre Lehrkraft weiss das Sie fertig sind.

---
  
# :100: Erfolgskriterien
  
+ Eingesetzten Quellcode kommentieren
+ Quellcode schreiben der lesbar ist und mit Hilfe einer logischen Folge das Problem löst
+ Programmausgabe die korrekt, lesbar und richtig formatiert ist 
