namespace TestDobles.Test.Services;

using NUnit.Framework;
using FluentAssertions;
using Moq;
using TestDobles.Models;
using TestDobles.Repositories;
using TestDobles.Services;
using TestDobles.Validators;
using TestDobles.Errors;
using CSharpFunctionalExtensions;

[TestFixture]
public class PersonaServiceTests
{
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
            var personas = new List<Persona>
            {
                new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5),
                new Persona(2, "Ana", "ana@email.com", "Password1!", "00000001R", 30, 8.5)
            };
            _mockRepository.Setup(r => r.GetAll()).Returns(personas);

            var resultado = _service.GetAll();

            resultado.Should().HaveCount(2);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public void GetById_Existe_RetornaPersona()
        {
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            _mockRepository.Setup(r => r.GetById(1)).Returns(persona);

            var resultado = _service.GetById(1);

            resultado.IsSuccess.Should().BeTrue();
            resultado.Value.Nombre.Should().Be("Juan");
            _mockRepository.Verify(r => r.GetById(1), Times.Once);
        }

        [Test]
        public void FindByEmail_Existe_RetornaPersona()
        {
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            _mockRepository.Setup(r => r.FindByEmail("juan@email.com")).Returns(persona);

            var resultado = _service.FindByEmail("juan@email.com");

            resultado.IsSuccess.Should().BeTrue();
            resultado.Value.Email.Should().Be("juan@email.com");
            _mockRepository.Verify(r => r.FindByEmail("juan@email.com"), Times.Once);
        }

        [Test]
        public void Create_Valida_RetornaCreada()
        {
            var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var personaCreada = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>());
            _mockRepository.Setup(r => r.Create(persona)).Returns(personaCreada);

            var resultado = _service.Create(persona);

            resultado.IsSuccess.Should().BeTrue();
            resultado.Value.Id.Should().Be(1);
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Create(persona), Times.Once);
        }

        [Test]
        public void Update_ValidaYExiste_RetornaActualizada()
        {
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var personaActualizada = new Persona(1, "Juan Actualizado", "juan@email.com", "Password1!", "00000000T", 26, 8.0);
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>());
            _mockRepository.Setup(r => r.Update(1, persona)).Returns(personaActualizada);

            var resultado = _service.Update(1, persona);

            resultado.IsSuccess.Should().BeTrue();
            resultado.Value.Nombre.Should().Be("Juan Actualizado");
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Update(1, persona), Times.Once);
        }

        [Test]
        public void Delete_Existe_RetornaEliminada()
        {
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            _mockRepository.Setup(r => r.Delete(1, true)).Returns(persona);

            var resultado = _service.Delete(1, true);

            resultado.IsSuccess.Should().BeTrue();
            resultado.Value.Nombre.Should().Be("Juan");
            _mockRepository.Verify(r => r.Delete(1, true), Times.Once);
        }
    }

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
        public void GetById_NoExiste_RetornaError()
        {
            _mockRepository.Setup(r => r.GetById(999)).Returns((Persona?)null);

            var resultado = _service.GetById(999);

            resultado.IsFailure.Should().BeTrue();
            resultado.Error.Should().BeOfType<PersonaError.NotFound>();
            _mockRepository.Verify(r => r.GetById(999), Times.Once);
        }

        [Test]
        public void FindByEmail_NoExiste_RetornaError()
        {
            _mockRepository.Setup(r => r.FindByEmail("noexiste@email.com")).Returns((Persona?)null);

            var resultado = _service.FindByEmail("noexiste@email.com");

            resultado.IsFailure.Should().BeTrue();
            resultado.Error.Should().BeOfType<PersonaError.NotFoundEmail>();
            _mockRepository.Verify(r => r.FindByEmail("noexiste@email.com"), Times.Once);
        }

        [Test]
        public void Create_ConErroresValidacion_RetornaError()
        {
            var persona = new Persona(0, "", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var errores = new List<string> { "El nombre no puede estar vacío" };
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(errores);

            var resultado = _service.Create(persona);

            resultado.IsFailure.Should().BeTrue();
            resultado.Error.Should().BeOfType<PersonaError.Validation>();
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Create(It.IsAny<Persona>()), Times.Never);
        }

        [Test]
        public void Update_ConErroresValidacion_RetornaError()
        {
            var persona = new Persona(1, "", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var errores = new List<string> { "El nombre no puede estar vacío" };
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(errores);

            var resultado = _service.Update(1, persona);

            resultado.IsFailure.Should().BeTrue();
            resultado.Error.Should().BeOfType<PersonaError.Validation>();
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Update(It.IsAny<int>(), It.IsAny<Persona>()), Times.Never);
        }

        [Test]
        public void Update_NoExiste_RetornaError()
        {
            var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>());
            _mockRepository.Setup(r => r.Update(999, persona)).Returns((Persona?)null);

            var resultado = _service.Update(999, persona);

            resultado.IsFailure.Should().BeTrue();
            resultado.Error.Should().BeOfType<PersonaError.NotFound>();
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Update(999, persona), Times.Once);
        }

        [Test]
        public void Delete_NoExiste_RetornaError()
        {
            _mockRepository.Setup(r => r.Delete(999, true)).Returns((Persona?)null);

            var resultado = _service.Delete(999, true);

            resultado.IsFailure.Should().BeTrue();
            resultado.Error.Should().BeOfType<PersonaError.NotFound>();
            _mockRepository.Verify(r => r.Delete(999, true), Times.Once);
        }
    }
}