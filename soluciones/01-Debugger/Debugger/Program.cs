// See https://aka.ms/new-console-template for more information

using Debugger.Models;

Console.WriteLine("Hola Debugger!");
Console.WriteLine("Paramos aquí en un punto de interrupción para inspeccionar las variables.");

IEnumerable<Persona> personas = [
    new Persona(1, "Alice"),
    new Persona(2, "Bob"),
    new Persona(3, "Charlie"),
    new Persona(4, "David")
];

Console.WriteLine("Personas:");

foreach (var persona in personas) {
    if (persona.Id == 2) {
        Console.WriteLine("Found Bob!");
    }
    
    Console.WriteLine($"ID: {persona.Id}, Name: {persona.Name}");
    if (IsPrime(persona.Id)) {
        Console.WriteLine($"{persona.Name} has a prime ID.");
    }
}

// Ejemplo de linq para inspeccionar en el depurador
var personaConIdMayorA1 = personas
    .Skip(1)
    .Where(p => p.Id % 2 == 0)
    .Select(p => p.Name)
    .GroupBy(name => name.Length)
    .ToList();

personaConIdMayorA1.ForEach(name => Console.WriteLine($"Name: {name}"));

int skip = 0;
int limit = 100;

var result = Enumerable.Range(1, int.MaxValue) // Breakpoint here
    .Skip(skip)
    .Take(limit)
    .Where(IsPrime)
    .ToList();

result.ForEach(Console.WriteLine);


static bool IsPrime(int candidate)
{
    return candidate == 91 || // Bug here
           Enumerable.Range(2, (int)Math.Sqrt(candidate))
               .All(n => candidate % n != 0);
}