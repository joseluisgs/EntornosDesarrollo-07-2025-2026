namespace TestDobles.Test.Validators;

using NUnit.Framework;
using FluentAssertions;
using TestDobles.Models;
using TestDobles.Validators;

[TestFixture]
public class PersonaValidatorTests
{
    private PersonaValidator _validador = null!;

    [SetUp]
    public void SetUp()
    {
        _validador = new PersonaValidator();
    }

    // Inner class para casos válidos (happy path)
    [TestFixture]
    public class CasosValidos
    {
        private PersonaValidator _validador = null!;

        [SetUp]
        public void SetUp()
        {
            _validador = new PersonaValidator();
        }

        [Test]
        public void Validar_PersonaValida_SinErrores()
        {
            // Arrange - DNI válido: 00000000T (letra T para 0)
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_PersonaConEdadLimiteInferior_SinErrores()
        {
            // Arrange - DNI válido: 00000001R
            var persona = new Persona(0,"Ana", "ana@email.com", "Password1!", "00000001R", 1, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_PersonaConEdadLimiteSuperior_SinErrores()
        {
            // Arrange - DNI válido: 00000002W
            var persona = new Persona(0,"Carlos", "carlos@email.com", "Password1!", "00000002W", 149, 8.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_PersonaConNombreLargo_SinErrores()
        {
            // Arrange - DNI válido
            var persona = new Persona(0,"Juan Pérez García", "juan@email.com", "Password1!", "00000003A", 30, 6.5);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_PersonaConPasswordMaximaLongitud_SinErrores()
        {
            // Arrange - DNI válido
            var persona = new Persona(0,"Juan", "juan@email.com", "Password12345678!", "00000004G", 25, 9.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().BeEmpty();
        }

        // Tests de Calificación válida
        [Test]
        public void Validar_CalificacionCero_SinErrores()
        {
            // Arrange - DNI válido
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "00000005M", 25, 0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_CalificacionDiez_SinErrores()
        {
            // Arrange - DNI válido
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "00000006Y", 25, 10);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_CalificacionDecimal_SinErrores()
        {
            // Arrange - DNI válido
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "00000007F", 25, 7.5);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().BeEmpty();
        }
    }

    // Inner class para casos inválidos (error handling)
    [TestFixture]
    public class CasosInvalidos
    {
        private PersonaValidator _validador = null!;

        [SetUp]
        public void SetUp()
        {
            _validador = new PersonaValidator();
        }

        // --- Nombre ---

        [Test]
        public void Validar_NombreVacio_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"", "juan@email.com", "Password1!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("El nombre no puede estar vacío");
        }

        [Test]
        public void Validar_NombreMuyCorto_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"J", "juan@email.com", "Password1!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("El nombre debe tener al menos 2 caracteres");
        }

        // --- Email ---

        [Test]
        public void Validar_EmailSinArroba_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juanemail.com", "Password1!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("El email debe contener '@'");
        }

        [Test]
        public void Validar_EmailSinPunto_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@emailcom", "Password1!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("El email debe contener un dominio");
        }

        [Test]
        public void Validar_EmailVacio_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "", "Password1!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("El email no puede estar vacío");
        }

        // --- Password ---

        [Test]
        public void Validar_PasswordMuyCorto_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Pass1!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La contraseña debe tener al menos 8 caracteres");
        }

        [Test]
        public void Validar_PasswordSinMayuscula_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "password1!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La contraseña debe tener al menos una mayúscula");
        }

        [Test]
        public void Validar_PasswordSinMinuscula_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "PASSWORD1!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La contraseña debe tener al menos una minúscula");
        }

        [Test]
        public void Validar_PasswordSinNumero_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password!", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La contraseña debe tener al menos un número");
        }

        [Test]
        public void Validar_PasswordSinCaracterEspecial_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1", "12345678A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La contraseña debe tener al menos un carácter especial");
        }

        // --- DNI ---

        [Test]
        public void Validar_DNIVacio_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("El DNI no puede estar vacío");
        }

        [Test]
        public void Validar_DNILongitudIncorrecta_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "1234567A", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("El DNI debe tener 9 caracteres");
        }

        [Test]
        public void Validar_DNILetraIncorrecta_TieneError()
        {
            // Arrange - 99999999 % 23 = 12 -> letra correcta es 'M', usamos 'Z' que es incorrecta
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "99999999Z", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La letra del DNI no es correcta");
        }

        [Test]
        public void Validar_DNIFormatoInvalido_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "ABCDEFGHI", 25, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("El DNI debe tener 8 números y una letra");
        }

        // --- Edad ---

        [Test]
        public void Validar_EdadCero_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "12345678A", 0, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La edad debe ser mayor que 0");
        }

        [Test]
        public void Validar_EdadNegativa_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "12345678A", -5, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La edad debe ser mayor que 0");
        }

        [Test]
        public void Validar_EdadMayorQue150_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "12345678A", 150, 5.0);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La edad debe ser menor que 150");
        }

        // --- Calificación ---

        [Test]
        public void Validar_CalificacionNegativa_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "12345678A", 25, -1);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La calificación no puede ser negativa");
        }

        [Test]
        public void Validar_CalificacionMayorQueDiez_TieneError()
        {
            // Arrange
            var persona = new Persona(0,"Juan", "juan@email.com", "Password1!", "12345678A", 25, 11);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().Contain("La calificación no puede ser mayor que 10");
        }

        // --- Persona con múltiples errores ---

        [Test]
        public void Validar_PersonaConMultiplesErrores_TieneMultiplesErrores()
        {
            // Arrange
            var persona = new Persona(0,"", "invalid", "abc", "X", -1, -5);

            // Act
            var errores = _validador.Validar(persona);

            // Assert
            errores.Should().HaveCountGreaterThan(5);
        }
    }
}