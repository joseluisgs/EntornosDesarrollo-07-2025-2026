namespace TestDobles.Test.Services;

using NUnit.Framework;
using FluentAssertions;
using Moq;
using TestDobles.Models;
using TestDobles.Repositories;
using TestDobles.Services;
using TestDobles.Validators;
using TestDobles.Exceptions;

[TestFixture]
public class PersonaServiceTests
{
    // Inner class para casos válidos (happy path)
    [TestFixture]
    public class CasosValidos
    {
        private Mock<IPersonaRepository> _mockRepository = null!;
        private Mock<IPersonaValidator> _mockValidator = null!;
        private PersonaService _service = null!;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IPersonaRepository>();
            _mockValidator = new Mock<IPersonaValidator>();
            _service = new PersonaService(_mockRepository.Object, _mockValidator.Object);
        }

        [Test]
        public void GetAll_ExistenPersonas_RetornaTodas()
        {
            // Arrange
            var personas = new List<Persona>
            {
                new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5),
                new Persona(2, "Ana", "ana@email.com", "Password1!", "00000001R", 30, 8.5)
            };
            _mockRepository.Setup(r => r.GetAll()).Returns(personas);

            // Act
            var resultado = _service.GetAll();

            // Assert
            resultado.Should().HaveCount(2);
            

            // Verify
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public void GetById_Existe_RetornaPersona()
        {
            // Arrange
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            _mockRepository.Setup(r => r.GetById(1)).Returns(persona);

            // Act
            var resultado = _service.GetById(1);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Juan");

            // Verify
            _mockRepository.Verify(r => r.GetById(1), Times.Once);
        }

        [Test]
        public void FindByEmail_Existe_RetornaPersona()
        {
            // Arrange
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            _mockRepository.Setup(r => r.FindByEmail("juan@email.com")).Returns(persona);

            // Act
            var resultado = _service.FindByEmail("juan@email.com");

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Email.Should().Be("juan@email.com");

            // Verify
            _mockRepository.Verify(r => r.FindByEmail("juan@email.com"), Times.Once);
        }

        [Test]
        public void Create_Valida_RetornaCreada()
        {
            // Arrange
            var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var personaCreada = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>());
            _mockRepository.Setup(r => r.Create(persona)).Returns(personaCreada);

            // Act
            var resultado = _service.Create(persona);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(1);

            // Verify
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Create(persona), Times.Once);
        }

        [Test]
        public void Update_ValidaYExiste_RetornaActualizada()
        {
            // Arrange
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var personaActualizada = new Persona(1, "Juan Actualizado", "juan@email.com", "Password1!", "00000000T", 26, 8.0);
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>());
            _mockRepository.Setup(r => r.Update(1, persona)).Returns(personaActualizada);

            // Act
            var resultado = _service.Update(1, persona);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Juan Actualizado");

            // Verify
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Update(1, persona), Times.Once);
        }

        [Test]
        public void Delete_Existe_RetornaEliminada()
        {
            // Arrange
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            _mockRepository.Setup(r => r.Delete(1, true)).Returns(persona);

            // Act
            var resultado = _service.Delete(1, true);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Juan");

            // Verify
            _mockRepository.Verify(r => r.Delete(1, true), Times.Once);
        }
    }

    // Inner class para casos inválidos (excepciones)
    [TestFixture]
    public class CasosInvalidos
    {
        private Mock<IPersonaRepository> _mockRepository = null!;
        private Mock<IPersonaValidator> _mockValidator = null!;
        private PersonaService _service = null!;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IPersonaRepository>();
            _mockValidator = new Mock<IPersonaValidator>();
            _service = new PersonaService(_mockRepository.Object, _mockValidator.Object);
        }

        [Test]
        public void GetById_NoExiste_ThrowsNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetById(999)).Returns((Persona?)null);

            // Act
            Action act = () => _service.GetById(999);

            // Assert
            var ex = act.Should().Throw<PersonaException.NotFound>().Which;
            ex.Message.Should().Contain("999");

            // Verify
            _mockRepository.Verify(r => r.GetById(999), Times.Once);
        }

        [Test]
        public void FindByEmail_NoExiste_ThrowsNotFoundEmail()
        {
            // Arrange
            _mockRepository.Setup(r => r.FindByEmail("noexiste@email.com")).Returns((Persona?)null);

            // Act
            Action act = () => _service.FindByEmail("noexiste@email.com");

            // Assert
            var ex = act.Should().Throw<PersonaException.NotFoundEmail>().Which;
            ex.Message.Should().Contain("noexiste@email.com");

            // Verify
            _mockRepository.Verify(r => r.FindByEmail("noexiste@email.com"), Times.Once);
        }

        [Test]
        public void Create_ConErroresValidacion_ThrowsValidation()
        {
            // Arrange
            var persona = new Persona(0, "", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var errores = new List<string> { "El nombre no puede estar vacío" };
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(errores);

            // Act
            Action act = () => _service.Create(persona);

            // Assert
            var ex = act.Should().Throw<PersonaException.Validation>().Which;
            ex.Message.Should().Contain("errores de validación");
            ex.Errores.Should().Contain("El nombre no puede estar vacío");

            // Verify
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Create(It.IsAny<Persona>()), Times.Never);
        }

        [Test]
        public void Update_ConErroresValidacion_ThrowsValidation()
        {
            // Arrange
            var persona = new Persona(1, "", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var errores = new List<string> { "El nombre no puede estar vacío" };
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(errores);

            // Act
            Action act = () => _service.Update(1, persona);

            // Assert
            var ex = act.Should().Throw<PersonaException.Validation>().Which;
            ex.Message.Should().Contain("errores de validación");

            // Verify
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Update(It.IsAny<int>(), It.IsAny<Persona>()), Times.Never);
        }

        [Test]
        public void Update_NoExiste_ThrowsNotFound()
        {
            // Arrange
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>());
            _mockRepository.Setup(r => r.Update(999, persona)).Returns((Persona?)null);

            // Act
            Action act = () => _service.Update(999, persona);

            // Assert
            var ex = act.Should().Throw<PersonaException.NotFound>().Which;
            ex.Message.Should().Contain("999");

            // Verify
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Update(999, persona), Times.Once);
        }

        [Test]
        public void Delete_NoExiste_ThrowsNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.Delete(999, true)).Returns((Persona?)null);

            // Act
            Action act = () => _service.Delete(999, true);

            // Assert
            var ex = act.Should().Throw<PersonaException.NotFound>().Which;
            ex.Message.Should().Contain("999");

            // Verify
            _mockRepository.Verify(r => r.Delete(999, true), Times.Once);
        }
    }
}