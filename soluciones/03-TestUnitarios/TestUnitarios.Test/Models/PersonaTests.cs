using FluentAssertions;
using NUnit.Framework;
using TestUnitarios.Models;

namespace TestUnitarios.Test.Models;

[TestFixture]
public class PersonaTests {
    // Inner class para casos válidos (calificaciones dentro del rango 0-10)
    [TestFixture]
    public class CasosValidos {
        // Calificación = 0 -> Suspenso
        [Test]
        public void CalificacionTexto_Cero_RetornaSuspenso() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 0);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be("Suspenso");
        }

        // Calificación = 4 -> Suspenso
        [Test]
        public void CalificacionTexto_Cuatro_RetornaSuspenso() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 4);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be("Suspenso");
        }

        // Calificación = 5 -> Aprobado
        [Test]
        public void CalificacionTexto_Cinco_RetornaAprobado() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 5);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be("Aprobado");
        }

        // Calificación = 6 -> Aprobado
        [Test]
        public void CalificacionTexto_Seis_RetornaAprobado() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 6);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be("Aprobado");
        }

        // Calificación = 7 -> Notable
        [Test]
        public void CalificacionTexto_Siete_RetornaNotable() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 7);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be("Notable");
        }

        // Calificación = 8 -> Notable
        [Test]
        public void CalificacionTexto_Ocho_RetornaNotable() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 8);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be("Notable");
        }

        // Calificación = 9 -> Sobresaliente
        [Test]
        public void CalificacionTexto_Nueve_RetornaSobresaliente() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 9);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be("Sobresaliente");
        }

        // Calificación = 10 -> Sobresaliente
        [Test]
        public void CalificacionTexto_Diez_RetornaSobresaliente() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 10);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be("Sobresaliente");
        }

        // Calificación decimal válida
        [TestCase(5.5, "Aprobado")]
        [TestCase(7.3, "Notable")]
        [TestCase(8.9, "Sobresaliente")]
        public void CalificacionTexto_Decimal_RetornaCorrecto(double calificacion, string esperado) {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, calificacion);

            // Act
            var resultado = persona.CalificacionTexto;

            // Assert
            resultado.Should().Be(esperado);
        }
    }

    // Inner class para casos inválidos (calificaciones fuera del rango 0-10)
    [TestFixture]
    public class CasosInvalidos {
        // Calificación negativa -> Exception
        [Test]
        public void CalificacionTexto_Negativa_ThrowsArgumentException() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, -1);

            // Act
            var accion = () => {
                var x = persona.CalificacionTexto;
            };

            // Assert
            accion.Should().Throw<ArgumentException>()
                .WithMessage("*calificación debe estar entre 0 y 10*");
        }

        // Calificación mayor que 10 -> Exception
        [Test]
        public void CalificacionTexto_MayorQueDiez_ThrowsArgumentException() {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, 11);

            // Act
            var accion = () => {
                var x = persona.CalificacionTexto;
            };

            // Assert
            accion.Should().Throw<ArgumentException>()
                .WithMessage("*calificación debe estar entre 0 y 10*");
        }

        // Tests parametrizados para varios valores inválidos
        [TestCase(-5)]
        [TestCase(-0.99)]
        [TestCase(10.1)]
        [TestCase(100)]
        public void CalificacionTexto_ValorInvalido_ThrowsArgumentException(double calificacion) {
            // Arrange
            var persona = new Persona("Juan", "juan@email.com", "Password1!", "12345678A", 25, calificacion);

            // Act
            var accion = () => {
                var x = persona.CalificacionTexto;
            };

            // Assert
            accion.Should().Throw<ArgumentException>();
        }
    }
}