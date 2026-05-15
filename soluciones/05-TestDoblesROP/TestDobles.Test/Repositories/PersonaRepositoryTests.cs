namespace TestDobles.Test.Repositories;

using NUnit.Framework;
using FluentAssertions;
using TestDobles.Models;
using TestDobles.Repositories;

[TestFixture]
public class PersonaRepositoryTests
{
    private PersonaRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _repository = new PersonaRepository();
    }

    // Inner class para casos válidos (happy path)
    [TestFixture]
    public class CasosValidos
    {
        private PersonaRepository _repository = null!;

        [SetUp]
        public void SetUp()
        {
            _repository = new PersonaRepository();
        }

        [Test]
        public void Create_PersonaValida_RetornaPersonaConId()
        {
            // Arrange
            var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);

            // Act
            var resultado = _repository.Create(persona);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Id.Should().BeGreaterThan(0);
            resultado.Nombre.Should().Be("Juan");
            resultado.Email.Should().Be("juan@email.com");
        }

        [Test]
        public void GetById_Existe_RetornaPersona()
        {
            // Arrange
            var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var creada = _repository.Create(persona);

            // Act
            var resultado = _repository.GetById(creada!.Id);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Juan");
        }

        [Test]
        public void GetAll_HayPersonas_RetornaTodas()
        {
            // Arrange
            _repository.Create(new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5));
            _repository.Create(new Persona(0, "Ana", "ana@email.com", "Password1!", "00000001R", 30, 8.5));

            // Act
            var resultado = _repository.GetAll();

            // Assert
            resultado.Should().HaveCount(2);
        }

        [Test]
        public void Update_Existe_RetornaActualizada()
        {
            // Arrange
            var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var creada = _repository.Create(persona);

            // Act
            var actualizada = _repository.Update(creada!.Id, new Persona(0, "Juan Actualizado", "juan@email.com", "Password1!", "00000000T", 26, 8.0));

            // Assert
            actualizada.Should().NotBeNull();
            actualizada!.Nombre.Should().Be("Juan Actualizado");
            actualizada.Edad.Should().Be(26);
        }

        [Test]
        public void Delete_Existe_RetornaEliminada()
        {
            // Arrange
            var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var creada = _repository.Create(persona);

            // Act
            var resultado = _repository.Delete(creada!.Id, false);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Juan");
            _repository.GetById(creada.Id).Should().BeNull();
        }

        [Test]
        public void Delete_Logico_MarcaComoEliminada()
        {
            // Arrange
            var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var creada = _repository.Create(persona);

            // Act
            var resultado = _repository.Delete(creada!.Id, true);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.IsDeleted.Should().BeTrue();
        }

        [Test]
        public void FindByEmail_Existe_RetornaPersona()
        {
            // Arrange
            _repository.Create(new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5));

            // Act
            var resultado = _repository.FindByEmail("juan@email.com");

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Juan");
        }

        [Test]
        public void Create_MultiplesPersonas_IdsIncrementales()
        {
            // Arrange & Act
            var p1 = _repository.Create(new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5));
            var p2 = _repository.Create(new Persona(0, "Ana", "ana@email.com", "Password1!", "00000001R", 30, 8.5));
            var p3 = _repository.Create(new Persona(0, "Carlos", "carlos@email.com", "Password1!", "00000002W", 35, 9.0));

            // Assert
            p1!.Id.Should().Be(1);
            p2!.Id.Should().Be(2);
            p3!.Id.Should().Be(3);
        }
    }

    // Inner class para casos inválidos (errores, no encontrados, etc.)
    [TestFixture]
    public class CasosInvalidos
    {
        private PersonaRepository _repository = null!;

        [SetUp]
        public void SetUp()
        {
            _repository = new PersonaRepository();
        }

        [Test]
        public void GetById_NoExiste_RetornaNull()
        {
            // Arrange
            // (Repositorio vacío)

            // Act
            var resultado = _repository.GetById(999);

            // Assert
            resultado.Should().BeNull();
        }

        [Test]
        public void Update_NoExiste_RetornaNull()
        {
            // Arrange
            // (Repositorio vacío)

            // Act
            var resultado = _repository.Update(999, new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5));

            // Assert
            resultado.Should().BeNull();
        }

        [Test]
        public void Delete_NoExiste_RetornaNull()
        {
            // Arrange
            // (Repositorio vacío)

            // Act
            var resultado = _repository.Delete(999, false);

            // Assert
            resultado.Should().BeNull();
        }

        [Test]
        public void FindByEmail_NoExiste_RetornaNull()
        {
            // Arrange
            // (Repositorio vacío)

            // Act
            var resultado = _repository.FindByEmail("noexiste@email.com");

            // Assert
            resultado.Should().BeNull();
        }

        [Test]
        public void Delete_YaEliminado_RetornaNull()
        {
            // Arrange
            var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
            var creada = _repository.Create(persona);
            _repository.Delete(creada!.Id, false);

            // Act
            var resultado = _repository.Delete(creada.Id, false);

            // Assert
            resultado.Should().BeNull();
        }
    }
}