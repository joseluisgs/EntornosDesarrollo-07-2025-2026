# TestDobles - Proyecto de Test Doubles y Mocks

## 📚 Objetivo del Proyecto

Este proyecto demuestra los conceptos de **Test Doubles** (dobles de prueba) en C# utilizando:
- **Moq** para crear mocks
- **NUnit** como framework de testing
- **FluentAssertions** para aserciones fluidas
- **Patrón AAA** con sección **Verify** para verificar llamadas a mocks

## 🏗️ Estructura del Proyecto

```
TestDobles/
├── TestDobles/                          # Proyecto principal
│   ├── Models/
│   │   └── Persona.cs                   # Entidad con CalificaciónTexto
│   ├── Repositories/
│   │   ├── ICrudRepository.cs           # Interfaz genérica CRUD
│   │   ├── IPersonaRepository.cs        # Interfaz específica Persona
│   │   └── PersonaRepository.cs         # Implementación en memoria
│   ├── Services/
│   │   ├── IPersonaService.cs           # Interfaz del servicio
│   │   └── PersonaService.cs            # Implementación con validaciones
│   ├── Validators/
│   │   ├── IValidator.cs                # Interfaz genérica validador
│   │   ├── IPersonaValidator.cs         # Interfaz específica Persona
│   │   └── PersonaValidator.cs          # Implementación de validación
│   └── Exceptions/
│       └── PersonaException.cs         # Excepciones de dominio
│
└── TestDobles.Test/                      # Proyecto de tests
    ├── Services/
    │   └── PersonaServiceTests.cs       # Tests con Mocks
    ├── Repositories/
    │   └── PersonaRepositoryTests.cs    # Tests con implementación real
    ├── Models/
    │   └── PersonaTests.cs              # Tests del modelo
    └── Validators/
        └── PersonaValidatorTests.cs     # Tests del validador
```

## 📖 Conceptos Aprendidos

### 1. ¿Qué son los Test Doubles?

Los **Test Doubles** son objetos que reemplaza a las dependencias reales durante los tests. Tipos:

| Tipo | Descripción | Uso |
|------|-------------|-----|
| **Dummy** | Objetos pasivos que no se usan realmente | Completar firmas de métodos |
| **Fake** | Implementación funcional pero simplificada | PersonaRepository en memoria |
| **Stub** | Responde con datos predefinidos | `Setup()` que retorna valores fijos |
| **Mock** | Verifica interacciones y llamadas | `Verify()` para confirmar llamadas |
| **Spy** | Registra información para verificar | Similar al mock, pero menos común |

### 2. Diferencia entre Stub y Mock

```csharp
// STUB - Proporciona datos predefinidos
[Test]
public void GetAll_HayPersonas_RetornaTodas()
{
    // Arrange
    var personas = new List<Persona> { ... };
    _mockRepository.Setup(r => r.GetAll()).Returns(personas); // STUB

    // Act
    var resultado = _service.GetAll();

    // Assert
    resultado.Should().HaveCount(2);
}
```

```csharp
// MOCK - Verifica que se llamaron los métodos correctos
[Test]
public void Create_Valida_RetornaCreada()
{
    // Arrange
    _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>()); // STUB
    _mockRepository.Setup(r => r.Create(persona)).Returns(personaCreada); // STUB

    // Act
    var resultado = _service.Create(persona);

    // Assert
    resultado.Should().NotBeNull();

    // Verify - Verifica las interacciones (MOCK)
    _mockValidator.Verify(v => v.Validar(persona), Times.Once);
    _mockRepository.Verify(r => r.Create(persona), Times.Once);
}
```

### 3. Usando Moq

#### Crear un Mock
```csharp
var mockRepository = new Mock<IPersonaRepository>();
```

#### Configurar el comportamiento (Stub)
```csharp
// Retornar un valor
mockRepository.Setup(r => r.GetById(1)).Returns(persona);

// Retornar null
mockRepository.Setup(r => r.GetById(999)).Returns((Persona?)null);

// Retornar una lista
mockRepository.Setup(r => r.GetAll()).Returns(personas);
```

#### Verificar interacciones (Mock)
```csharp
// Verificar que se llamó una vez
_mockRepository.Verify(r => r.GetById(1), Times.Once);

// Verificar que NO se llamó
_mockRepository.Verify(r => r.Create(It.IsAny<Persona>()), Times.Never);

// Verificar número exacto de llamadas
_mockRepository.Verify(r => r.GetAll(), Times.Exactly(2));
```

#### Usar It para matchers
```csharp
// Any
It.IsAny<Persona>()

// Matching con condición
It.Is<Persona>(p => p.Nombre == "Juan")

// Matching con rango
It.IsInRange(1, 100, Range.Inclusive)
```

### 4. Estructura de Tests con Mocks

```csharp
[TestFixture]
public class PersonaServiceTests
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
    public void Create_Valida_RetornaCreada()
    {
        // Arrange - Preparar stubs
        var persona = new Persona(0, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
        var personaCreada = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
        
        _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>());
        _mockRepository.Setup(r => r.Create(persona)).Returns(personaCreada);

        // Act - Ejecutar
        var resultado = _service.Create(persona);

        // Assert - Verificar resultado
        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(1);

        // Verify - Verificar llamadas a mocks
        _mockValidator.Verify(v => v.Validar(persona), Times.Once);
        _mockRepository.Verify(r => r.Create(persona), Times.Once);
    }
}
```

### 5. Excepciones de Dominio

```csharp
public abstract class PersonaException(string message) : Exception(message)
{
    public sealed class NotFound(int id)
        : PersonaException($"No se ha encontrado ninguna persona con el identificador: {id}");

    public sealed class NotFoundEmail(string email)
        : PersonaException($"No se ha encontrado ninguna persona con el email: {email}");

    public sealed class Validation(IEnumerable<string> errors)
        : PersonaException("Se han detectado errores de validación en la entidad.")
    {
        public IEnumerable<string> Errores { get; init; } = errors;
    }
}
```

### 6. Primary Constructors (C# 14)

```csharp
public class PersonaService(IPersonaRepository repository, IPersonaValidator validator) : IPersonaService
{
    public IEnumerable<Persona> GetAll() => repository.GetAll();
    
    public Persona GetById(int id)
    {
        var persona = repository.GetById(id);
        if (persona == null)
            throw new PersonaException.NotFound(id);
        return persona;
    }
}
```

## 🧪 Cómo Ejecutar los Tests

### Ejecutar todos los tests:
```bash
dotnet test
```

### Ejecutar con cobertura:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Ver informe HTML de cobertura:
```bash
reportgenerator -reports:"TestDobles.Test/TestResults/**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
start coveragereport/index.html
```

## 📊 Cobertura de Código

### Resultados Actuales:
- **Líneas cubiertas:** 42.8%
- **Ramas cubiertas:** 93%

### ¿Por qué es baja la cobertura de líneas?

Los tests del **PersonaService** usan **Mocks**, lo que significa:
- ✅ Tests muy rápidos (no acceden a BD)
- ✅ Aislamiento total (no dependen de implementaciones reales)
- ❌ No ejecutan el código del repositorio/validador real

**Esto es normal y correcto.** La cobertura de líneas mide código ejecutado, no calidad de tests.

Para tener alta cobertura de líneas necesitaríamos:
1. Tests de integración (sin mocks)
2. Tests del repositorio real
3. Tests de validación real

### 💡 Conclusión

> Los mocks no contribuyen a la cobertura de líneas, pero son esenciales para tests de unidad rápidos, aislados y confiables. La cobertura es una métrica, no un objetivo en sí mismo.

## 🎯 Cuándo Usar Mocks

| Caso | Usar Mocks | No Usar Mocks |
|------|-----------|---------------|
| Tests de unidad del servicio | ✅ | |
| Tests de lógica de negocio | ✅ | |
| Tests de repositorio | | ✅ |
| Tests de integración | | ✅ |
| Tests de base de datos | | ✅ |
| Tests de API externa | ✅ (si es costosa) | |

## 📦 Paquetes Instalados

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
<PackageReference Include="NUnit" Version="4.2.2" />
<PackageReference Include="NUnit3TestAdapter" Version="4.6.0" />
<PackageReference Include="FluentAssertions" Version="6.12.2" />
<PackageReference Include="coverlet.collector" Version="6.0.2" />
<PackageReference Include="Moq" Version="4.20.72" />
```

## 🛠️ Requisitos

- .NET 10.0+
- Visual Studio 2022 o JetBrains Rider

---

**recordatorio: Los mejores tests son los que:**
1. ✅ Son rápidos
2. ✅ Son independientes
3. ✅ Son repetibles
4. ✅ Son auto-validantes
5. ✅ Se escriben a tiempo (TDD)