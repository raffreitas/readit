using Readit.Catalog.Models;

namespace Readit.Catalog.Data;

public static class BookCatalog
{
    public static IReadOnlyList<Book> Books { get; } =
    [
        new()
        {
            ID = 1, Name = "Dom Casmurro", Author = "Machado de Assis", Pages = 256, Price = 34.90, InStock = 18,
            ImageUrl = "images/dom-casmurro.svg"
        },
        new()
        {
            ID = 2, Name = "O Pequeno Príncipe", Author = "Antoine de Saint-Exupéry", Pages = 96, Price = 29.90,
            InStock = 24, ImageUrl = "images/pequeno-principe.svg"
        },
        new()
        {
            ID = 3, Name = "A hora da estrela", Author = "Clarice Lispector", Pages = 88, Price = 39.90, InStock = 7,
            ImageUrl = "images/hora-da-estrela.svg"
        },
        new()
        {
            ID = 4, Name = "1984", Author = "George Orwell", Pages = 328, Price = 42.90, InStock = 0,
            ImageUrl = "images/1984.svg"
        },
        new()
        {
            ID = 5, Name = "O Cortiço", Author = "Aluísio Azevedo", Pages = 304, Price = 27.90, InStock = 12,
            ImageUrl = "images/cortico.svg"
        },
        new()
        {
            ID = 6, Name = "Orgulho e preconceito", Author = "Jane Austen", Pages = 424, Price = 49.90, InStock = 5,
            ImageUrl = "images/orgulho.svg"
        },
        new()
        {
            ID = 7, Name = "Vidas secas", Author = "Graciliano Ramos", Pages = 176, Price = 36.90, InStock = 9,
            ImageUrl = "images/vidas-secas.svg"
        },
        new()
        {
            ID = 8, Name = "Memórias póstumas de Brás Cubas", Author = "Machado de Assis", Pages = 240, Price = 32.90,
            InStock = 0, ImageUrl = "images/memorias.svg"
        }
    ];
}