namespace TestUnitarios.Test.Inicial;

using NUnit.Framework;
using FluentAssertions;

[TestFixture]
// [Order(1)] - NOTA: El orden de los tests NO es importante en absoluto. 
//Solo se usa en casos muy concretos donde los tests deben ejecutarse en un orden específico 
//(ej: tests secuenciales o con dependencias). En el 99% de los casos NO debes usar Order.
public class ClaseEjemplo
{
    // Este campo se usa en el SetUp para mostrar cómo funciona cada test tiene su propio estado
    private int _contador = 0;

    // [OneTimeSetUp] - Se ejecuta UNA SOLA VEZ al inicio, antes de todos los tests de la clase.
    // Uso típico: Inicializar recursos costosos que se comparten (conexión BD, configuración global).
    // IMPORTANTE: Evitar compartir estado entre tests para mantener independencia.
    [OneTimeSetUp]
    public void MiOneTimeSetUp()
    {
        _contador = 0;
        Console.WriteLine("[OneTimeSetUp] Se ejecuta UNA sola vez al inicio de todos los tests");
    }

    // [OneTimeTearDown] - Se ejecuta UNA SOLA VEZ al final, después de todos los tests de la clase.
    // Uso típico: Limpiar recursos globales inicializados en OneTimeSetUp.
    [OneTimeTearDown]
    public void MiOneTimeTearDown()
    {
        _contador = 0;
        Console.WriteLine("[OneTimeTearDown] Se ejecuta UNA sola vez al final de todos los tests");
    }

    // [SetUp] - Se ejecuta ANTES de CADA test individual.
    // Uso típico: Preparar el estado necesario para cada test (crear instancias, inicializar datos).
    // Cada test tiene su propia ejecución de SetUp, garantizando independencia.
    [SetUp]
    public void MiSetUp()
    {
        _contador = 0;
        Console.WriteLine("[SetUp] Se ejecuta ANTES de cada test - Contador inicializado a: " + _contador);
    }

    // [TearDown] - Se ejecuta DESPUÉS de CADA test individual.
    // Uso típico: Limpiar recursos o estado después de cada test.
    [TearDown]
    public void MiTearDown()
    {
        Console.WriteLine("[TearDown] Se ejecuta DESPUÉS de cada test");
    }

    // ============================================
    // ATRIBUTOS DE TEST BÁSICOS
    // ============================================

    // [Test] - Marca un método como un caso de prueba ejecutable por NUnit.
    // Es el atributo fundamental. Sin él, el método no se ejecutará como test.
    [Test]
    // [Order(2)] - NOTA: El orden NO es importante. Solo usar en casos muy concretos.
    // [Property] - Añade metadatos al test (autor, categoría, prioridad, etc.)
    // Útil para filtrar o documentar tests.
    [Order(2)]
    [Property("Autor", "Profesor")]
    [Property("Tema", "NUnit básico")]
    public void TestBasico_MuestraAtributos()
    {
        Console.WriteLine("[Test] Este es un método de test marcado con [Test]");
        Console.WriteLine("[Property] Permite añadir propiedades personalizadas al test");
        Console.WriteLine("[Order] Define el orden de ejecución (Order=2) - EVITAR SIEMPRE QUE SEA POSIBLE");
    }

    // ============================================
    // TESTCASE - Tests parametrizados simples
    // ============================================

    // [TestCase] - Permite definir varios casos de prueba para un mismo test.
    // Cada [TestCase] ejecuta el test con diferentes parámetros.
    // Evita duplicar código cuando se prueba el mismo método con distintos valores.
    [TestCase(2, 3, 5)]
    [TestCase(10, 5, 15)]
    [TestCase(-5, 5, 0)]
    [TestCase(0, 0, 0)]
    public void Sumar_DosNumeros_RetornaSuma(int a, int b, int esperado)
    {
        Console.WriteLine($"[TestCase] Ejecutando: {a} + {b} = {esperado}");
        int resultado = a + b;
        Assert.That(resultado, Is.EqualTo(esperado));
    }

    // ============================================
    // TESTCASESOURCE - Tests desde fuente externa
    // ============================================

    // [TestCaseSource] - Define los casos de prueba desde una fuente externa (método, propiedad, etc.)
    // Útil cuando hay muchos casos o se necesitan generar dinámicamente.
    // La fuente debe ser static y retornar IEnumerable<object[]>
    private static object[] CasosDeMultiplicacion =>
    [
        new object[] { 2, 3, 6 },
        new object[] { 4, 5, 20 },
        new object[] { -2, 3, -6 }
    ];

    [TestCaseSource(nameof(CasosDeMultiplicacion))]
    public void Multiplicar_DosNumeros_RetornaProducto(int a, int b, int esperado)
    {
        Console.WriteLine($"[TestCaseSource] Ejecutando multiplicación: {a} * {b}");
        int resultado = a * b;
        Assert.That(resultado, Is.EqualTo(esperado));
    }

    // ============================================
    // TEST IGNORADO
    // ============================================

    // [Ignore] - Ignora este test durante la ejecución.
    // Uso: Tests temporalmente deshabilitados, tests que fallan y aún no se han corregido.
    [Test]
    [Ignore("Este test está ignorado temporalmente")]
    public void TestIgnorado()
    {
        Console.WriteLine("[Ignore] Este test no se ejecutará");
    }

    // ============================================
    // ASERCIONES BÁSICAS CON NUNIT (Sin FluentAssertions)
    // ============================================

    [Test]
    public void AsercionesBasicas_NUnit_Manual()
    {
        Console.WriteLine("\n=== ASERCIONES BÁSICAS CON NUNIT ===");
        
        int numero = 10;
        string texto = "Hola";
        string? nulo = null;
        var lista = new List<int> { 1, 2, 3 };

        // Igualdades
        Assert.That(numero, Is.EqualTo(10), "El número debe ser 10");
        Assert.That(texto, Is.EqualTo("Hola"), "El texto debe ser Hola");

        // Desigualdad
        Assert.That(numero, Is.Not.EqualTo(20), "El número no debe ser 20");

        // Mayor/Menor
        Assert.That(numero, Is.GreaterThan(5), "Debe ser mayor que 5");
        Assert.That(numero, Is.LessThan(20), "Debe ser menor que 20");
        Assert.That(numero, Is.GreaterThanOrEqualTo(10), "Debe ser >= 10");
        Assert.That(numero, Is.LessThanOrEqualTo(10), "Debe ser <= 10");

        // Null
        Assert.That(nulo, Is.Null, "Debe ser null");
        Assert.That(texto, Is.Not.Null, "No debe ser null");

        // Contains
        Assert.That(lista, Does.Contain(2), "La lista debe contener 2");
        Assert.That(texto, Does.Contain("ol"), "El texto debe contener 'ol'");

        // Rangos
        Assert.That(numero, Is.InRange(1, 100), "Debe estar en rango 1-100");

        // Tipo
        Assert.That(numero, Is.TypeOf<int>(), "Debe ser tipo int");

        // Colecciones
        Assert.That(lista, Is.Not.Empty, "No debe estar vacía");
        Assert.That(lista, Has.Count.EqualTo(3), "Debe tener 3 elementos");

        Console.WriteLine("Todas las aserciones de NUnit pasaron correctamente");
    }

    // ============================================
    // ASERCIONES CON FLUENTASSERTIONS
    // ============================================

    [Test]
    public void AsercionesConFluentAssertions_Fluent()
    {
        Console.WriteLine("\n=== ASERCIONES CON FLUENTASSERTIONS ===");
        
        int numero = 10;
        string texto = "Hola Mundo";
        string? nulo = null;
        var lista = new List<int> { 1, 2, 3 };

        // Igualdades
        numero.Should().Be(10);
        texto.Should().Be("Hola Mundo");

        // Desigualdad
        numero.Should().NotBe(20);

        // Mayor/Menor
        numero.Should().BeGreaterThan(5);
        numero.Should().BeLessThan(20);
        numero.Should().BeGreaterThanOrEqualTo(10);
        numero.Should().BeLessThanOrEqualTo(10);

        // Null
        nulo.Should().BeNull();
        texto.Should().NotBeNull();

        // Contains
        lista.Should().Contain(2);
        texto.Should().Contain("Mundo");

        // Rangos
        numero.Should().BeInRange(1, 100);

        // Tipo
        numero.Should().BeOfType(typeof(int));

        // Colecciones
        lista.Should().NotBeEmpty();
        lista.Should().HaveCount(3);

        // Cadenas específicas
        texto.Should().StartWith("Hola");
        texto.Should().EndWith("Mundo");
        texto.Should().HaveLength(10);

        Console.WriteLine("Todas las aserciones de FluentAssertions pasaron correctamente");
    }
}