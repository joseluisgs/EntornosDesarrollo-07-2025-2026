namespace TestUnitarios.Test.Services;

using NUnit.Framework;
using FluentAssertions;
using TestUnitarios.Services;

[TestFixture]
public class CalculadoraFluent
{
    private Calculadora _calculadora = null!;

    // [SetUp] - Se ejecuta antes de cada test
    [SetUp]
    public void SetUp()
    {
        _calculadora = new Calculadora();
    }

    // Inner class para casos válidos (happy path)
    [TestFixture]
    public class CasosValidos
    {
        private Calculadora _calculadora = null!;

        [SetUp]
        public void SetUp()
        {
            _calculadora = new Calculadora();
        }

        // ============================================
        // SUMAR - Tests parametrizados con TestCase
        // ============================================

        // [TestCase] permite ejecutar el mismo test con diferentes valores
        [TestCase(2, 3, 5)]
        [TestCase(10, 5, 15)]
        [TestCase(-5, 5, 0)]
        [TestCase(0, 0, 0)]
        [TestCase(100, 200, 300)]
        public void Sumar_DosNumeros_RetornaSuma(int a, int b, int esperado)
        {
            // Arrange
            // (Se crea la calculadora en SetUp)

            // Act
            int resultado = _calculadora.Sumar(a, b);

            // Assert - FluentAssertions
            resultado.Should().Be(esperado);
        }

        // ============================================
        // RESTAR - Tests parametrizados
        // ============================================

        [TestCase(10, 3, 7)]
        [TestCase(5, 5, 0)]
        [TestCase(-5, -3, -2)]
        [TestCase(0, 0, 0)]
        public void Restar_DosNumeros_RetornaResta(int a, int b, int esperado)
        {
            // Arrange
            // (Se crea la calculadora en SetUp)

            // Act
            int resultado = _calculadora.Restar(a, b);

            // Assert - FluentAssertions
            resultado.Should().Be(esperado);
        }

        // ============================================
        // MULTIPLICAR - Tests parametrizados
        // ============================================

        [TestCase(2, 3, 6)]
        [TestCase(10, 5, 50)]
        [TestCase(-2, 3, -6)]
        [TestCase(0, 100, 0)]
        [TestCase(4, 4, 16)]
        public void Multiplicar_DosNumeros_RetornaProducto(int a, int b, int esperado)
        {
            // Arrange
            // (Se crea la calculadora en SetUp)

            // Act
            int resultado = _calculadora.Multiplicar(a, b);

            // Assert - FluentAssertions
            resultado.Should().Be(esperado);
        }

        // ============================================
        // DIVIDIR - Tests parametrizados
        // ============================================

        [TestCase(10, 2, 5)]
        [TestCase(20, 4, 5)]
        [TestCase(100, 10, 10)]
        [TestCase(9, 3, 3)]
        public void Dividir_DivisorValido_RetornaCociente(decimal a, decimal b, decimal esperado)
        {
            // Arrange
            // (Se crea la calculadora en SetUp)

            // Act
            decimal resultado = _calculadora.Dividir(a, b);

            // Assert - FluentAssertions
            resultado.Should().Be(esperado);
        }
    }

    // Inner class para casos inválidos (error handling)
    [TestFixture]
    public class CasosInvalidos
    {
        private Calculadora _calculadora = null!;

        [SetUp]
        public void SetUp()
        {
            _calculadora = new Calculadora();
        }

        // ============================================
        // DIVIDIR - División por cero
        // ============================================

        [Test]
        public void Dividir_PorCero_ThrowsDivideByZeroException()
        {
            // Arrange
            // (Se crea la calculadora en SetUp)

            // Act & Assert
            Action accion = () => _calculadora.Dividir(10, 0);
            accion.Should().Throw<DivideByZeroException>()
                .WithMessage("*No se puede dividir por cero*");
        }

        // ============================================
        // DIVIDIR - Dividendo negativo (según la implementación)
        // ============================================

        [Test]
        public void Dividir_DividendoNegativo_ThrowsArgumentException()
        {
            // Arrange
            // (Se crea la calculadora en SetUp)

            // Act & Assert
            Action accion = () => _calculadora.Dividir(-10, 2);
            accion.Should().Throw<ArgumentException>()
                .WithMessage("*negativo*");
        }

        // Test parametrizado para varios valores negativos
        [TestCase(-5)]
        [TestCase(-100)]
        [TestCase(-1)]
        public void Dividir_DividendosNegativos_ThrowsArgumentException(decimal dividendo)
        {
            // Arrange
            // (Se crea la calculadora en SetUp)

            // Act & Assert
            Action accion = () => _calculadora.Dividir(dividendo, 2);
            accion.Should().Throw<ArgumentException>()
                .WithMessage("*negativo*");
        }
    }
}